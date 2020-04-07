using System;

namespace SynetecMvcAssessment.Services.Implementation
{
	public class BonusCalculator : IBonusCalculator
	{
		public decimal GetBonusForEmployee(decimal employeeSalary, decimal totalSalary, decimal totalBonusPool)
		{
			if (employeeSalary < 0 || totalSalary < 0 || totalBonusPool < 0)
				throw new InvalidOperationException("All fields must be greater than zero!");

			if (employeeSalary > totalSalary)
				throw new InvalidOperationException("Employee salary cannot be higher than total salary!");

			var bonusPercentage = employeeSalary / totalSalary;
			var bonusAllocation = bonusPercentage * totalBonusPool;

			return bonusAllocation;
		}
	}
}