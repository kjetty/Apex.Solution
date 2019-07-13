using System.Configuration;

namespace Apex.Utils
{
    public static class WebConfigHelper
    {
		public static string WebsiteBaseUrl { get; }
		public static string WebApiBaseUrl { get; }
        public static string WebApiCallType { get; }
        public static string ApexDbConnectionString { get; }

        static WebConfigHelper()
        {
			if (ConfigurationManager.AppSettings["WebsiteBaseURL"] != null)
				WebsiteBaseUrl = ConfigurationManager.AppSettings["WebsiteBaseURL"];
			
			if (ConfigurationManager.AppSettings["WebAPIBaseURL"] != null) 
				WebApiBaseUrl = ConfigurationManager.AppSettings["WebAPIBaseURL"];

			if (ConfigurationManager.AppSettings["WebAPICallType"] != null)
				WebApiCallType = ConfigurationManager.AppSettings["WebAPICallType"];

			if (ConfigurationManager.ConnectionStrings["ApexDbConnectionString"] != null)
				ApexDbConnectionString = ConfigurationManager.ConnectionStrings["ApexDbConnectionString"].ToString();
        }
    }
}
