using System.Threading.Tasks;
using System.Web.Mvc;
using InterviewTestTemplatev2.Models;
using SynetecMvcAssessment.Services;

namespace InterviewTestTemplatev2.Controllers
{
	public class BonusPoolController : Controller
	{
		private readonly IBonusCalculator _bonusCalculator;
		private readonly IEmployeeService _employeeService;

		public BonusPoolController(IEmployeeService employeeService, IBonusCalculator bonusCalculator)
		{
			_employeeService = employeeService;
			_bonusCalculator = bonusCalculator;
		}

		// GET: BonusPool
		public async Task<ActionResult> Index()
		{
			var allEmployees = await _employeeService.GetAllEmployeesAsync();

			var model = new BonusPoolCalculatorModel {AllEmployees = allEmployees};

			return View(model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Calculate(BonusPoolCalculatorModel model)
		{
			var selectedEmployeeId = model.SelectedEmployeeId;
			var totalBonusPool = model.BonusPoolAmount;

			var hrEmployee = await _employeeService.GetEmployeeByIdAsync(selectedEmployeeId);

			var result = new BonusPoolCalculatorResultModel
			{
				HrEmployee = hrEmployee,
				BonusPoolAllocation = await CalculateBonusForEmployee(hrEmployee.Salary, totalBonusPool)
			};

			return View(result);
		}

		private async Task<decimal> CalculateBonusForEmployee(int employeeSalary, int bonusPoolAmount)
		{
			var totalSalary = await _employeeService.GetTotalSalaryOfAllEmployees();

			return _bonusCalculator.GetBonusForEmployee(employeeSalary, totalSalary, bonusPoolAmount);
		}
	}
}