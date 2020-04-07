namespace SynetecMvcAssessment.Services
{
	public interface IBonusCalculator
	{
		decimal GetBonusForEmployee(decimal employeeSalary, decimal totalSalary, decimal totalBonusPool);
	}
}