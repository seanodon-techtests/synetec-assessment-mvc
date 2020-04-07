using System.Collections.Generic;
using SynetecMvcAssessment.Data.Models;

namespace InterviewTestTemplatev2.Models
{
	public class BonusPoolCalculatorModel
	{
		public int BonusPoolAmount { get; set; }
		public List<HrEmployee> AllEmployees { get; set; }
		public int SelectedEmployeeId { get; set; }
	}
}