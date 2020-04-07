using System;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using Moq;
using Shouldly;
using SynetecMvcAssessment.Data.Models;
using SynetecMvcAssessment.Data.Repositories;
using SynetecMvcAssessment.Services.Implementation;
using Xunit;

namespace SynetecMvcAssessment.Services.Tests
{
	public class BonusCalculatorTests
	{
		private readonly BonusCalculator SUT;

		public BonusCalculatorTests()
		{
			SUT = new BonusCalculator();
		}

		[Theory]
		[InlineData(10000, 100000, 100, 10)]
		[InlineData(15000, 100000, 123456, 18518.40)]
		[InlineData(1, 100000, 100000, 1)]
		[InlineData(100000, 100000, 5000, 5000)]
		public void GetBonusForEmployeeWithValidData(decimal employeeSalary, decimal totalSalary, decimal totalBonusPool, decimal expected)
		{
			var result = SUT.GetBonusForEmployee(employeeSalary, totalSalary, totalBonusPool);
			result.ShouldBe(expected);
		}

		[Theory]
		[InlineData(-1, 100000, 100)]
		[InlineData(100, -1, 100)]
		[InlineData(100, 10000, -1)]
		[InlineData(10000, 1000, 100)]
		public void GetBonusForEmployeeWithInvalidDataThrowsException(decimal employeeSalary, decimal totalSalary, decimal totalBonusPool)
		{
			Assert.Throws<InvalidOperationException>(() => SUT.GetBonusForEmployee(employeeSalary, totalSalary, totalBonusPool));
		}
	}
}
