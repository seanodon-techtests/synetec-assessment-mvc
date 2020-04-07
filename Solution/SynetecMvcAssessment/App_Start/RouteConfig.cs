﻿using System.Web.Mvc;
using System.Web.Routing;

namespace InterviewTestTemplatev2
{
	public class RouteConfig
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoute(
				"Default",
				"{controller}/{action}/{id}",
				new {controller = "BonusPool", action = "Index", id = UrlParameter.Optional}
			);
		}
	}
}