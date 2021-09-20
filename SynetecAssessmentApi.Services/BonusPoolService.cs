using Microsoft.EntityFrameworkCore;
using SynetecAssessmentApi.Domain;
using SynetecAssessmentApi.Persistence;
using SynetecAssessmentApi.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SynetecAssessmentApi.Services
{
    public class BonusPoolService : IBonusPoolService
    {
        private readonly AppDbContext _dbContext;

        public BonusPoolService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<EmployeeDto>> GetEmployeesAsync()
        {
            IEnumerable<Employee> employees = await _dbContext
                .Employees
                .Include(e => e.Department)
                .ToListAsync();

            List<EmployeeDto> result = new List<EmployeeDto>();

            foreach (var employee in employees)
            {
                result.Add(
                    new EmployeeDto
                    {
                        Fullname = employee.Fullname,
                        JobTitle = employee.JobTitle,
                        Salary = employee.Salary,
                        Department = new DepartmentDto
                        {
                            Title = employee.Department.Title,
                            Description = employee.Department.Description
                        }
                    });
            }

            return result;
        }

        public async Task<BonusPoolCalculatorResultDto> CalculateAsync(int bonusPoolAmount, int selectedEmployeeId)
        {
            //load the details of the selected employee using the Id
            Employee employee = await this.GetEmployee(selectedEmployeeId);

            if (employee == null)
                throw new ArgumentException("Employee doesn't exists");

            //get the total salary budget for the company
            int totalSalary = (int)_dbContext.Employees.Sum(item => item.Salary);

            //calculate the bonus allocation for the employee
            decimal bonusPercentage = (decimal)employee.Salary / (decimal)totalSalary;
            int bonusAllocation = (int)(bonusPercentage * bonusPoolAmount);

            return new BonusPoolCalculatorResultDto
            {
                Employee = new EmployeeDto
                {
                    Fullname = employee.Fullname,
                    JobTitle = employee.JobTitle,
                    Salary = employee.Salary,
                    Department = new DepartmentDto
                    {
                        Title = employee.Department.Title,
                        Description = employee.Department.Description
                    }
                },

                Amount = bonusAllocation
            };
        }

        /// <summary>
        /// Check if employee exists in database
        /// </summary>
        /// <param name="selectedEmployeeId"></param>
        /// <returns>True if exists. Else False</returns>
        public async Task<bool> CheckEmployeeExists(int selectedEmployeeId)
        {
            Employee employee = await this.GetEmployee(selectedEmployeeId);

            if (employee == null)
                return false;
            else
                return true;
        }

        /// <summary>
        /// Get employee details
        /// </summary>
        /// <param name="selectedEmployeeId">Employee Id</param>
        /// <returns>Employee</returns>
        private async Task<Employee> GetEmployee(int selectedEmployeeId)
        {
            //load the details of the selected employee using the Id
            Employee employee = await _dbContext.Employees
                .Include(e => e.Department)
                .FirstOrDefaultAsync(item => item.Id == selectedEmployeeId);

            return employee;
        }
        
    }
}