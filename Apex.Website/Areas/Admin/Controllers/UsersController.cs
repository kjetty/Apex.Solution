using Apex.Domain.DBModels;
using Apex.Utils;
using Newtonsoft.Json;
using PagedList;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Apex.Website.Areas.Admin.Controllers
{
	public class UsersController : Controller
    {
        public UsersController()
        {
            ApiHelper = new WebApiHelper();
			
			if (System.Web.HttpContext.Current.Session != null && System.Web.HttpContext.Current.Session["token"] != null)
				_token = System.Web.HttpContext.Current.Session["token"].ToString();
		}

        public WebApiHelper ApiHelper { get; private set; }
		private readonly string _webBaseApiBaseUrl = WebConfigHelper.WebApiBaseUrl != null ? WebConfigHelper.WebApiBaseUrl : "~";
		private readonly string _token;

		// GET: Admin/Users
		public ActionResult Index(string sortOrder, string searchString, int? page)
        {
			string apiResponseString = ApiHelper.GetDataUsingGet(_token, _webBaseApiBaseUrl, "/api/users");
			//var paginationMeta = new JavaScriptSerializer().Deserialize<PaginationMeta<User>>(apiResponseString);
			var pagedList = JsonConvert.DeserializeObject<PaginationList<User>>(apiResponseString);
			var users = pagedList.PaginationResponseData;

			if (users != null)
			{
				ViewBag.sortOrder = sortOrder = sortOrder ?? "FirstName";
				ViewBag.searchString = searchString = searchString ?? string.Empty;
				ViewBag.page = page = page ?? 1;

				users = users.Where(tbl => tbl.ActiveFlag.Equals(true)).ToList();

				//search
				if (searchString.Trim().Length > 0)
				{
					users = users.Where(tbl => (tbl.FirstName != null && tbl.FirstName.Contains(searchString))
											|| (tbl.LastName != null && tbl.LastName.Contains(searchString))
											|| (tbl.Address != null && tbl.Address.Contains(searchString))
											|| (tbl.Province != null && tbl.Province.Contains(searchString))
											|| (tbl.Zip != null && tbl.Zip.Contains(searchString))
											|| (tbl.HomePhone != null && tbl.HomePhone.Contains(searchString))
											|| (tbl.OfficePhone != null && tbl.OfficePhone.Contains(searchString))
											|| (tbl.Mobile != null && tbl.Mobile.Contains(searchString))
											|| (tbl.Email != null && tbl.Email.Contains(searchString))
											|| (tbl.LoginId != null && tbl.LoginId.Contains(searchString))).ToList();
				}

				switch (sortOrder)
				{
					case "FirstName":
						users = users.OrderBy(tbl => tbl.FirstName).ToList();
						break;
					case "LastName":
						users = users.OrderBy(tbl => tbl.LastName).ToList();
						break;
					case "Email":
						users = users.OrderBy(tbl => tbl.Email).ToList();
						break;
					default:
						users = users.OrderBy(tbl => tbl.FirstName).ToList();
						break;
				}

				int pageSize = 10;
				int pageNumber = (page ?? 1);

				return View(users.ToPagedList(pageNumber, pageSize));
			}

			return View("NotAllowed");
        }

        // GET: Admin/Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

			string XPaginationData = string.Empty;
			string apiResponseString = ApiHelper.GetDataUsingGet(_token, _webBaseApiBaseUrl, "/api/users/" + id);
            //var user = new JavaScriptSerializer().Deserialize<User>(apiResponseString);
			var user = JsonConvert.DeserializeObject<User>(apiResponseString);

            if (user == null)
            {
                return HttpNotFound();
            }

			return View(user);
		}

        // GET: Admin/Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserId,FirstName,MiddleName,LastName,Address,City,Province,Zip,HomePhone,OfficePhone,Mobile,Email,LoginId,LoginPassword,ActiveFlag,ModifiedBy,ModifiedDate")] User user)
        {
            if (ModelState.IsValid)
            {
				//var formContent = new StringContent(new JavaScriptSerializer().Serialize(user), Encoding.UTF8, "application/json");
				var formContent = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
				ApiHelper.AddDataUsingPost(_token, _webBaseApiBaseUrl, "/api/users/", formContent);

                return RedirectToAction("Index");
            }

            return View(user);
        }

        // GET: Admin/Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

			string XPaginationData = string.Empty;
            string apiResponseString = ApiHelper.GetDataUsingGet(_token, _webBaseApiBaseUrl, "/api/users/" + id);
            //var user = new JavaScriptSerializer().Deserialize<User>(apiResponseString);
			var user = JsonConvert.DeserializeObject<User>(apiResponseString);

            if (user == null)
            {
                return HttpNotFound();
            }

            return View(user);
        }

        // POST: Admin/Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserId,FirstName,MiddleName,LastName,Address,City,Province,Zip,HomePhone,OfficePhone,Mobile,Email,LoginId,LoginPassword,ActiveFlag,ModifiedBy,ModifiedDate")] User user)
        {
            if (ModelState.IsValid)
            {
				//var formContent = new StringContent(new JavaScriptSerializer().Serialize(user), Encoding.UTF8, "application/json");
				var formContent = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
				ApiHelper.UpdateDataUsingPut(_token, _webBaseApiBaseUrl, "/api/users/" + user.UserId, formContent);
                
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Admin/Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

			string XPaginationData = string.Empty;
            string apiResponseString = ApiHelper.GetDataUsingGet(_token, _webBaseApiBaseUrl, "/api/users/" + id);
			//var user = new JavaScriptSerializer().Deserialize<User>(apiResponseString);
			var user = JsonConvert.DeserializeObject<User>(apiResponseString);

            if (user == null)
            {
                return HttpNotFound();
            }

            return View(user);
        }

        // POST: Admin/Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            string apiResponseString = ApiHelper.DeleteDataUsingDelete(_token, _webBaseApiBaseUrl, "/api/users/" + id);
            
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
            }

            base.Dispose(disposing);
        }
    }
}
