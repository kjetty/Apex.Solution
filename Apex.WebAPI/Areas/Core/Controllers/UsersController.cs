using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using Apex.Domain.DBModels;
using Apex.Services;
using Apex.Utils;

namespace Apex.WebAPI.Areas.Core.Controllers
{
	//[Authorize]
	public class UsersController : ApiController
	{
		public UsersController()
		{
			UsersService = new UsersService();
		}

		public UsersService UsersService { get; }

		// GET: api/Users
		public IHttpActionResult GetPaginatedUsers()
		{

			PaginationMeta paginationMeta = new PaginationMeta();
			paginationMeta.Page = 111;
			paginationMeta.PageSize = 222;
			paginationMeta.SearchTerm = "";
			paginationMeta.TotalPagesCount = 301;
			paginationMeta.TotalRecordsCount = 402;

			PaginationList<User> pagedList = new PaginationList<User>();
			pagedList.PaginationResponseData = UsersService.GetPaginatedUsers();
			pagedList.PaginationMetaData = paginationMeta;

			return Ok(pagedList);
		}

		// GET: api/Users/5
		[ResponseType(typeof(User))]
		public IHttpActionResult GetUser(int id)
		{
			var user = UsersService.GetUser(id);

			if (user == null)
				return NotFound();

			return Ok(user);
		}

		// PUT: api/Users/5
		[ResponseType(typeof(void))]
		public IHttpActionResult PutUser(int id, User user)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			if (id != user.UserId)
				return BadRequest();

			UsersService.EditUser(user);

			return StatusCode(HttpStatusCode.NoContent);
		}

		// POST: api/Users
		[ResponseType(typeof(User))]
		public IHttpActionResult PostUser(User user)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			UsersService.AddUser(user);

			return CreatedAtRoute("DefaultApi", new { id = user.UserId }, user);
		}

		// DELETE: api/Users/5
		[ResponseType(typeof(User))]
		public IHttpActionResult DeleteUser(int id)
		{
			var user = UsersService.GetUser(id);

			if (user == null)
				return NotFound();

			UsersService.DeleteUser(id);

			return Ok(user);
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