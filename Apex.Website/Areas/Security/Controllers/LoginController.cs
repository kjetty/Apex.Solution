using Apex.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Apex.Domain.Models;
using System.Net.Http;
using Apex.Website.Extensions;

namespace Apex.Website.Areas.Security.Controllers
{
    public class LoginController : Controller
    {
        // GET: Security/Login
        public ActionResult Index()
        {
            return View();
        }

		// POST: Security/Login
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Index([Bind(Include = "username,password")] LoginForm loginForm)
		{
			//remove existing login session
			System.Web.HttpContext.Current.Session.Remove("token");

			if (ModelState.IsValid)
			{
				WebApiHelper apiHelper = new WebApiHelper();

				//setup login data
				var formContent = new FormUrlEncodedContent(new[]
				{
					new KeyValuePair<string, string>("grant_type", "password"),
					new KeyValuePair<string, string>("username", loginForm.username),
					new KeyValuePair<string, string>("password", loginForm.password),
				});

				string token = apiHelper.GetAPIToken(WebConfigHelper.WebApiBaseUrl, "/Token", formContent);

				if (!token.Contains("Error."))
				{
					//set the login token session
					System.Web.HttpContext.Current.Session.Add("token", token);

					this.AddNotification("Login is successful. Welcome!.", NotificationType.SUCCESS);

					//redirect to the root website
					return RedirectToAction("index", "home", new {area = ""});
				}
			}

			this.AddNotification("Login Failed. Please try again or contact website administrator.", NotificationType.WARNING);

			return View(loginForm);
		}
	}
}