using System.Collections.Generic;
using System.Threading.Tasks;
using SynetecMvcAssessment.Data.Models;

namespace SynetecMvcAssessment.Services
{
	public interface IEmployeeService
	{
		Task<HrEmployee> GetEmployeeByIdAsync(int id);
		Task<int> GetTotalSalaryOfAllEmployees();
		Task<List<HrEmployee>> GetAllEmployeesAsync();
	}
}