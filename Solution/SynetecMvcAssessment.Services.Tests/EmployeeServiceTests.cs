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
	public class EmployeeServiceTests
	{
		private readonly Fixture _fixture;
		private readonly Mock<IEmployeeRepository> _mockedRepo;
		private readonly EmployeeService SUT;

		public EmployeeServiceTests()
		{
			_fixture = new Fixture();
			_mockedRepo = new Mock<IEmployeeRepository>();
			SUT = new EmployeeService(_mockedRepo.Object);
		}

		[Fact]
		public async Task GetAllEmployees()
		{
			var employees = _fixture.CreateMany<HrEmployee>(5).ToList();
			_mockedRepo.Setup(repo => repo.GetAllEmployeesAsync())
				.ReturnsAsync(employees);

			var result = await SUT.GetAllEmployeesAsync();

			_mockedRepo.Verify(repo => repo.GetAllEmployeesAsync(), Times.Once);
			result.ShouldBe(employees);
		}

		[Fact]
		public async Task GetTotalSalaryOfAllEmployees()
		{
			var i = 1;
			var employees = _fixture.Build<HrEmployee>()
				.With(e => e.Salary, () => 10000 * i++)
				.CreateMany(5).ToList();
			
			_mockedRepo.Setup(repo => repo.GetAllEmployeesAsync())
				.ReturnsAsync(employees);

			var result = await SUT.GetTotalSalaryOfAllEmployees();

			_mockedRepo.Verify(repo => repo.GetAllEmployeesAsync(), Times.Once);
			result.ShouldBe(employees.Sum(e => e.Salary));
		}

		[Fact]
		public async Task GetEmployeeById()
		{
			var id = 2;
			var employee = _fixture.Create<HrEmployee>();
			_mockedRepo.Setup(repo => repo.GetEmployeeById(It.IsAny<int>()))
				.ReturnsAsync(employee);

			var result = await SUT.GetEmployeeByIdAsync(id);

			_mockedRepo.Verify(repo => repo.GetEmployeeById(id), Times.Once);
			result.ShouldBe(employee);
		}
	}
}
