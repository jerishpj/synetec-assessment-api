using SynetecAssessmentApi.Services.Dtos;
using System;
using System.Collections.Generic;
using Xunit;

namespace SynetecAssessmentApi.Services.Tests
{
    public class BonusPoolServiceTests : IClassFixture<InMemoryDbContextFixture>
    {
        private readonly IBonusPoolService _bonusPoolService;

        public BonusPoolServiceTests(InMemoryDbContextFixture fixture)
        {
            _bonusPoolService = new BonusPoolService(fixture.Context);
        }

        [Fact]
        public void TestGetEmployeesAsyncReturnsNotNull()
        {
            var employees = _bonusPoolService.GetEmployeesAsync();

            Assert.NotNull(employees);
            Assert.NotNull(employees.Result);
        }

        [Fact]
        public void TestGetEmployeesAsyncReturnsAllEmployees()
        {
            List<EmployeeDto> employees = (List<EmployeeDto>)_bonusPoolService.GetEmployeesAsync().Result;
            Assert.Equal(4, employees.Count);
        }

        [Fact]
        public void TestCalculateAsyncThrowsErrorForInvalidInput()
        {
            var ex = Assert.Throws<AggregateException>(() => _bonusPoolService.CalculateAsync(1000, 0).Result);

            Assert.IsType<ArgumentException>(ex.InnerExceptions[0]);
            Assert.Equal("Employee doesn't exists", ex.InnerExceptions[0].Message);
        }

        [Theory]
        [InlineData(5000, 1, 1000)]
        [InlineData(15000, 1, 3000)]
        [InlineData(10000, 2, 3000)]
        [InlineData(0, 3, 0)]
        public void TestCalculateAsyncReturnsCorrectResultForValidInput(int bonusPoolAmount, int selectedEmployeeId, int expectedResult)
        {
            BonusPoolCalculatorResultDto bonusPoolCalculatorResultDto = _bonusPoolService.CalculateAsync(bonusPoolAmount, selectedEmployeeId).Result;

            Assert.Equal(expectedResult, bonusPoolCalculatorResultDto.Amount);
        }

        [Theory]
        [InlineData( 2, true)]
        [InlineData( 1, true)]
        [InlineData( 5, false)]
        [InlineData( 4, true)]
        [InlineData(-4, false)]
        public void TestCheckEmployeeExistsReturnsCorrectResult(int selectedEmployeeId, bool expectedResult)
        {
            bool employeeExists = _bonusPoolService.CheckEmployeeExists(selectedEmployeeId).Result;

            Assert.Equal(expectedResult, employeeExists);
        }

    }
}
