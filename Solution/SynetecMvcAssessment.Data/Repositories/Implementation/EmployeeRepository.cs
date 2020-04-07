using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using SynetecMvcAssessment.Data.Models;

namespace SynetecMvcAssessment.Data.Repositories.Implementation
{
	public class EmployeeRepository : IEmployeeRepository
	{
		private readonly MvcInterviewV3Entities1 _dbContext;

		public EmployeeRepository(MvcInterviewV3Entities1 dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<HrEmployee> GetEmployeeById(int id)
		{
			return await _dbContext.HrEmployees.FirstOrDefaultAsync(e => e.ID == id);
		}

		public async Task<List<HrEmployee>> GetAllEmployeesAsync()
		{
			return await _dbContext.HrEmployees.ToListAsync();
		}
	}
}