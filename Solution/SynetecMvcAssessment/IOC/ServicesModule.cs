using Autofac;
using SynetecMvcAssessment.Services;
using SynetecMvcAssessment.Services.Implementation;

namespace InterviewTestTemplatev2.IOC
{
	public class ServicesModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterType<EmployeeService>().As<IEmployeeService>();
			builder.RegisterType<BonusCalculator>().As<IBonusCalculator>();
		}
	}
}