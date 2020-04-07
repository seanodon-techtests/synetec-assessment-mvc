using Autofac;
using SynetecMvcAssessment.Data.Models;
using SynetecMvcAssessment.Data.Repositories;
using SynetecMvcAssessment.Data.Repositories.Implementation;

namespace InterviewTestTemplatev2.IOC
{
	public class RepositoriesModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterType<MvcInterviewV3Entities1>();
			builder.RegisterType<EmployeeRepository>().As<IEmployeeRepository>();
		}
	}
}