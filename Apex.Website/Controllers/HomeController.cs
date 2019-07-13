using Apex.Utils;
using Apex.Website.Extensions;
using System.Web.Mvc;
using Apex.Domain.Models;
using System.Collections.Generic;
using System.Net.Http;

namespace Apex.Website.Controllers
{
    public class HomeController : Controller
    {
		// GET: Home
		public ActionResult Index()
		{
			return View();
        }
    }
}