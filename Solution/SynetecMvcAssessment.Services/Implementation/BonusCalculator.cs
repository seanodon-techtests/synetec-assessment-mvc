namespace SynetecMvcAssessment.Services.Implementation
{
	public class BonusCalculator : IBonusCalculator
	{
		public decimal GetBonusForEmployee(decimal employeeSalary, decimal totalSalary, decimal totalBonusPool)
		{
			var bonusPercentage = employeeSalary / totalSalary;
			var bonusAllocation = bonusPercentage * totalBonusPool;

			return bonusAllocation;
		}
	}
}