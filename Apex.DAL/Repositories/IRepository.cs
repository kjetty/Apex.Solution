using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Apex.DAL.Repositories
{
    public interface IRepository<T> where T : class
    {
        List<T> GetPaginatedRecords(string sortByColumn = null, int page = 1, int pageSize = 10, int activeFlag = 1, string searchTerm = null, string sortType = "asc");
		T GetRecordById(int id);
        List<T> GetRecordsByFilter(Expression<Func<T, bool>> filter);
        void InsertRecord(T entity);
        void UpdateRecord(T entity);
        void DeleteRecord(int id);
    }
}
