using System.Collections.Generic;
using System.Threading.Tasks;
using SynetecMvcAssessment.Data.Models;

namespace SynetecMvcAssessment.Data.Repositories
{
	public interface IEmployeeRepository
	{
		Task<HrEmployee> GetEmployeeById(int id);
		Task<List<HrEmployee>> GetAllEmployeesAsync();
	}
}