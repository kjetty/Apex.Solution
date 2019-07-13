using Apex.Domain.DBModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Dapper;
using System.Data.SqlClient;

namespace Apex.DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private ApexDBContext _context;
		private IDbConnection _connection;

        public UserRepository(ApexDBContext context, IDbConnection connection)
        {
			_context = context;
			_connection = connection;
        }

        public List<User> GetPaginatedRecords(string sortByColumn = null, int page = 1, int pageSize = 10, int activeFlag = 1, string searchTerm = null, string sortType = "asc")
		{
			if (_connection != null)
			{
				//dapper implementation
				sortByColumn = sortByColumn ?? "UserId";

				if (searchTerm != null && searchTerm.Trim().Length > 0)
				{
					string sqlString = "WITH tempCTE AS (SELECT ROW_NUMBER() OVER (ORDER BY tbl." + @sortByColumn + " " + @sortType + ") AS RowNumber, tbl.* FROM Users tbl WHERE tbl.activeFlag = @activeFlag AND ";
					sqlString += "(1 = 0 ";
					sqlString += " OR LOWER(tbl.FirstName) LIKE @firstName ";
					sqlString += " OR LOWER(tbl.LastName) LIKE @lastName ";
					sqlString += " OR LOWER(tbl.Email) LIKE @email ";
					sqlString += ")) SELECT * FROM tempCTE WHERE RowNumber BETWEEN (@page - 1) * @pageSize + 1 AND @page * @pageSize ORDER BY " + @sortByColumn + " " + @sortType;

					_connection.Open();

					return _connection.Query<User>(
						sqlString,
						param: new { activeFlag = activeFlag, firstName = "%" + searchTerm.ToLower() + "%", lastName = "%" + searchTerm.ToLower() + "%", email = "%" + searchTerm.ToLower() + "%", page = page, pageSize = pageSize }
					).ToList();
				}
				else
				{
					string sqlString = "WITH tempCTE AS (SELECT ROW_NUMBER() OVER (ORDER BY tbl." + @sortByColumn + " " + @sortType + ") AS RowNumber, tbl.* FROM Users tbl WHERE tbl.activeFlag = @activeFlag";
					sqlString += ") SELECT * FROM tempCTE WHERE RowNumber BETWEEN (@page - 1) * @pageSize + 1 AND @page * @pageSize ORDER BY " + @sortByColumn + " " + @sortType;

					_connection.Open();

					return _connection.Query<User>(
						sqlString,
						param: new { activeFlag = activeFlag, sortType = sortType, page = page, pageSize = pageSize }
					).ToList();
				}
			}
			else if (_context != null)
			{
				//EF implementation
				int skipRows = (page - 1) * pageSize;
				var tempQry = from tbl in _context.Users orderby tbl.UserId ascending select tbl;
				return tempQry.Skip(skipRows).Take(pageSize).ToList();
			}

			throw new NotImplementedException();
		}
		
		public User GetRecordById(int id)
        {
			
			_connection.Open();

			List<string> results = new List<string>();
			results.Add("dap.sql,EF,ef.obj.sql,ef.db.sql,ef.sp,dap.sp");
			for (int j = 0; j < 5; j++)
			{
				var watch = System.Diagnostics.Stopwatch.StartNew();

				for (int i = 0; i < 2500; i++)
				{
					var temp = _connection.Query<User>(
						"SELECT * FROM Users WHERE UserId = @Userid",
						param: new { UserId = id }
					).FirstOrDefault();
				}

				watch.Stop();
				var elapsedMs1 = watch.ElapsedMilliseconds;

				watch = System.Diagnostics.Stopwatch.StartNew();

				for (int i = 0; i < 2500; i++)
				{
					var temp = _context.Users.Where(tbl => tbl.ActiveFlag.Equals(true) && tbl.UserId.Equals(id)).SingleOrDefault();
				}

				watch.Stop();
				var elapsedMs2 = watch.ElapsedMilliseconds;

				watch = System.Diagnostics.Stopwatch.StartNew();

				for (int i = 0; i < 2500; i++)
				{
					var temp = _context.Users.SqlQuery("SELECT * FROM Users WHERE UserId = @Userid", new SqlParameter("@Userid", id)).FirstOrDefault<User>();
				}

				watch.Stop();
				var elapsedMs3 = watch.ElapsedMilliseconds;

				watch = System.Diagnostics.Stopwatch.StartNew();

				for (int i = 0; i < 2500; i++)
				{
					var temp = _context.Database.SqlQuery<User>("SELECT * FROM Users WHERE UserId = @Userid", new SqlParameter("@Userid", id)).FirstOrDefault();
				}

				watch.Stop();
				var elapsedMs4 = watch.ElapsedMilliseconds;

				watch = System.Diagnostics.Stopwatch.StartNew();

				for (int i = 0; i < 2500; i++)
				{
					var temp = _context.Users.SqlQuery("GetUser {0}", id).Single();
				}

				watch.Stop();
				var elapsedMs5 = watch.ElapsedMilliseconds;

				watch = System.Diagnostics.Stopwatch.StartNew();

				for (int i = 0; i < 2500; i++)
				{
					var temp = _connection.Query<User>("GetUser", new { UserId = id }, commandType: CommandType.StoredProcedure).First();
				}

				watch.Stop();
				var elapsedMs6 = watch.ElapsedMilliseconds;
				
				watch = System.Diagnostics.Stopwatch.StartNew();

				for (int i = 0; i < 2500; i++)
				{
					var temp = _connection.Query<User>("GetUser", new { UserId = id }, commandType: CommandType.StoredProcedure).First();
				}

				watch.Stop();
				var elapsedMs7 = watch.ElapsedMilliseconds;

				//ObjectCache cache = MemoryCache.Default;
				//var listOut = cache[list.ToString()] as IEnumerable;
				//if (listOut != null) return listOut;
				
				






				string result = elapsedMs1 + "," + elapsedMs2 + "," + elapsedMs3 + "," + elapsedMs4 + "," + elapsedMs5 + "," + elapsedMs6;

				results.Add(result);
			}
			












			if (_connection != null)
			{
				//dapper implementation
				_connection.Open();

				return _connection.Query<User>(
					"SELECT * FROM Users WHERE UserId = @Userid",
					param: new { UserId = id }
				).FirstOrDefault();
			}
			else if (_context != null)
			{
				//EF implementation
				//return _context.Users.Where(tbl => tbl.ActiveFlag.Equals(true) && tbl.UserId.Equals(id)).SingleOrDefault();

				//return _context.Users.SqlQuery("SELECT * FROM Users WHERE UserId = @Userid").FirstOrDefault<User>();

				//_context.Database.SqlQuery<User>("SELECT * FROM Users WHERE UserId = @Userid", new SqlParameter("@Userid",id));
			}

			throw new NotImplementedException();
		}

        public List<User> GetRecordsByFilter(Expression<Func<User, bool>> filter)
        {
			return _context.Users.Where(filter).ToList();
			
			//IEnumerable<T> items = null;

			//// extract the dynamic sql query and parameters from predicate
			//QueryResult result = DynamicQuery.GetDynamicQuery(_tableName, predicate);

			//using (IDbConnection cn = Connection)
			//{
			//	cn.Open();
			//	items = cn.Query<T>(result.Sql, (object)result.Param);
			//}

			//return items;
		}

        public void InsertRecord(User entity)
        {
            _context.Users.Add(entity);
        }

        public void UpdateRecord(User entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void DeleteRecord(int id)
        {
            User entity = GetRecordById(id);
            entity.ActiveFlag = false;
            _context.Entry(entity).State = EntityState.Modified;
        }
    }

	public interface IUserRepository : IRepository<User>
	{
	}
}
