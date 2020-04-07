using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SynetecMvcAssessment.Data.Models;
using SynetecMvcAssessment.Data.Repositories;

namespace SynetecMvcAssessment.Services.Implementation
{
	public class EmployeeService : IEmployeeService
	{
		private readonly IEmployeeRepository _employeeRepository;

		public EmployeeService(IEmployeeRepository employeeRepository)
		{
			_employeeRepository = employeeRepository;
		}

		public async Task<HrEmployee> GetEmployeeByIdAsync(int id)
		{
			return await _employeeRepository.GetEmployeeById(id);
		}

		public async Task<int> GetTotalSalaryOfAllEmployees()
		{
			var allEmployees = await GetAllEmployeesAsync();
			return allEmployees.Sum(e => e.Salary);
		}

		public async Task<List<HrEmployee>> GetAllEmployeesAsync()
		{
			return await _employeeRepository.GetAllEmployeesAsync();
		}
	}
}