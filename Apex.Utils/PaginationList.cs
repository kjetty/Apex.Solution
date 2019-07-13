using System.Collections.Generic;

namespace Apex.Utils
{
	public class PaginationList<T> where T : class
	{
		public List<T> PaginationResponseData { get; set; }
		public PaginationMeta PaginationMetaData { get; set; }
	}

	public class PaginationMeta
	{
		public int TotalRecordsCount { get; set; }
		public int TotalPagesCount { get; set; }
		public int Page { get; set; }
		public int PageSize { get; set; }
		public string SearchTerm { get; set; }
	}
}
