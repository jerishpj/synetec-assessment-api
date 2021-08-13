using Microsoft.EntityFrameworkCore;
using SynetecAssessmentApi.Domain;
using SynetecAssessmentApi.Persistence;
using System;
using System.Collections.Generic;

namespace SynetecAssessmentApi.Services.Tests
{
    public class InMemoryDbContextFixture : IDisposable
    {
        public AppDbContext Context { get; private set; }

        /// <summary>
        /// Get in memory database with seed data. 
        /// We can use Mock if the business logic is having other more complex dependencies. 
        /// </summary>
        public InMemoryDbContextFixture()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "TestHrDb")
            .Options;

            var appDbContext = new AppDbContext(options);

            // Initialize data in the test in memory database

            var departments = new List<Department>
            {
                new Department(1, "Finance", "The finance department for the company"),
                new Department(2, "Human Resources", "The Human Resources department for the company"),
                new Department(3, "IT", "The IT support department for the company"),
                new Department(4, "Marketing", "The Marketing department for the company")
            };

            var employees = new List<Employee>
            {
                new Employee(1, "John Smith", "Accountant (Senior)", 60000, 1),
                new Employee(2, "Janet Jones", "HR Director", 90000, 2),
                new Employee(3, "Robert Rinser", "IT Director", 95000, 3),
                new Employee(4, "Jilly Thornton", "Marketing Manager (Senior)", 55000, 4),
            };

            appDbContext.Departments.AddRange(departments);
            appDbContext.Employees.AddRange(employees);
            appDbContext.SaveChanges();

            Context = appDbContext;
        }

        public void Dispose()
        {
            // Clean up 
            Context.Dispose();
        }
    }
}
