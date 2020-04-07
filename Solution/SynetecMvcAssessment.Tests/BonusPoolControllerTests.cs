using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoFixture;
using InterviewTestTemplatev2.Controllers;
using InterviewTestTemplatev2.Models;
using Moq;
using Shouldly;
using SynetecMvcAssessment.Data.Models;
using SynetecMvcAssessment.Services;
using Xunit;

namespace SynetecMvcAssessment.Tests
{
	public class BonusPoolControllerTests
	{
		private readonly Fixture _fixture;
		private readonly Mock<IEmployeeService> _mockedEmployeeService;
		private readonly Mock<IBonusCalculator> _mockedBonusCalculator;
		private readonly BonusPoolController SUT;

		public BonusPoolControllerTests()
		{
			_fixture = new Fixture();
			_mockedEmployeeService = new Mock<IEmployeeService>();
			_mockedBonusCalculator = new Mock<IBonusCalculator>();
			SUT = new BonusPoolController(_mockedEmployeeService.Object, _mockedBonusCalculator.Object);
		}

		[Fact]
		public async Task Index()
		{
			var employees = _fixture.CreateMany<HrEmployee>(5).ToList();
			_mockedEmployeeService.Setup(repo => repo.GetAllEmployeesAsync())
				.ReturnsAsync(employees);

			var result = await SUT.Index();

			_mockedEmployeeService.Verify(repo => repo.GetAllEmployeesAsync(), Times.Once);
			var viewResult = result.ShouldBeAssignableTo<ViewResult>();
			viewResult.Model.ShouldBeAssignableTo<BonusPoolCalculatorModel>().AllEmployees.ShouldBe(employees);
		}

		[Fact]
		public async Task Calculate()
		{
			var employee = _fixture.Create<HrEmployee>();
			_mockedEmployeeService.Setup(svc => svc.GetEmployeeByIdAsync(It.IsAny<int>()))
				.ReturnsAsync(employee);

			var totalSalary = 100000;
			_mockedEmployeeService.Setup(svc => svc.GetTotalSalaryOfAllEmployees())
				.ReturnsAsync(totalSalary);

			var bonusShare = 30;
			_mockedBonusCalculator.Setup(svc => svc.GetBonusForEmployee(It.IsAny<decimal>(), It.IsAny<decimal>(), It.IsAny<decimal>()))
				.Returns(bonusShare);

			var request = new BonusPoolCalculatorModel
			{
				SelectedEmployeeId = 2,
				BonusPoolAmount = 10000
			};

			var result = await SUT.Calculate(request);

			_mockedEmployeeService.Verify(svc => svc.GetEmployeeByIdAsync(request.SelectedEmployeeId), Times.Once);
			_mockedEmployeeService.Verify(svc => svc.GetTotalSalaryOfAllEmployees(), Times.Once);
			_mockedBonusCalculator.Verify(svc => svc.GetBonusForEmployee(employee.Salary, totalSalary, request.BonusPoolAmount));
			
			var viewResult = result.ShouldBeAssignableTo<ViewResult>();
			var resultModel = viewResult.Model.ShouldBeAssignableTo<BonusPoolCalculatorResultModel>();
			resultModel.HrEmployee.ShouldBe(employee);
			resultModel.BonusPoolAllocation.ShouldBe(bonusShare);
		}
	}
}
