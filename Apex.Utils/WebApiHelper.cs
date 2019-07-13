using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace Apex.Utils
{
	public class WebApiHelper
    {
        private string GetRequestHeader(string returnType)
        {
            string retValue = "application/json";
			string WebApiCallType = WebConfigHelper.WebApiCallType;


			if ((WebApiCallType != null && WebApiCallType.Trim().ToLower().Equals("xml")) || (returnType != null && returnType.Trim().ToLower().Equals("xml")))
                retValue = "application/xml";

            return retValue;
		}

		//TokenEndpointRequest seems doesn't support JSON yet, but you can use query string
		//http://www.gdomc.com/0420/cant-get-the-asp-net-web-api-token-using-http-client/

		public string GetAPIToken(string baseUri, string urlPath, FormUrlEncodedContent formContent, string returnType = "json")
		{
			string token = "Error. No token received. Check the login call. em5a3t6n.";
			
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(baseUri);
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(GetRequestHeader(returnType)));

				// by calling .Result you are performing a synchronous call
				HttpResponseMessage response = client.PostAsync(urlPath, formContent).Result;
				if (response.IsSuccessStatusCode)
				{
					string responseString = response.Content.ReadAsStringAsync().Result;
					token = JObject.Parse(responseString).GetValue("access_token").ToString();
				}
			}

			return token;
		}

		public string GetDataUsingGet(string token, string baseUri, string urlPath, string urlParams = null, string returnType = "json")
        {
            string responseString = string.Empty;

			if (!string.IsNullOrEmpty(urlParams))
				urlPath += "?" + urlParams.Trim();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUri);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(GetRequestHeader(returnType)));

				if (token != null)
					client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

				// by calling .Result you are performing a synchronous call
				HttpResponseMessage response = client.GetAsync(urlPath).Result;
				
				if (response.IsSuccessStatusCode)
				{
					responseString = response.Content.ReadAsStringAsync().Result;
				}
            }
			
			return responseString;
        }

        public string AddDataUsingPost(string token, string baseUri, string urlPath, HttpContent formContent, string urlParams = null, string returnType = "json")
        {
            string responseString = string.Empty;

            if (!string.IsNullOrEmpty(urlParams))
				urlPath += "?" + urlParams.Trim();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUri);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(GetRequestHeader(returnType)));

				if (token != null)
					client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

				// by calling .Result you are performing a synchronous call
				HttpResponseMessage response = client.PostAsync(urlPath, formContent).Result;
                if (response.IsSuccessStatusCode)
                    responseString = response.Content.ReadAsStringAsync().Result;
            }

            return responseString;
        }

        public string UpdateDataUsingPut(string token, string baseUri, string urlPath, HttpContent formContent, string urlParams = null, string returnType = "json")
        {
            string responseString = string.Empty;

            if (!string.IsNullOrEmpty(urlParams))
				urlPath += "?" + urlParams.Trim();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUri);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(GetRequestHeader(returnType)));

				if (token != null)
					client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

				// by calling .Result you are performing a synchronous call
				HttpResponseMessage response = client.PutAsync(urlPath, formContent).Result;
                if (response.IsSuccessStatusCode)
                    responseString = response.Content.ReadAsStringAsync().Result;
            }

            return responseString;
        }

        public string DeleteDataUsingDelete(string token, string baseUri, string urlPath, string urlParams = null, string returnType = "json")
        {
            string responseString = string.Empty;

            if (!string.IsNullOrEmpty(urlParams))
				urlPath += "?" + urlParams.Trim();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUri);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(GetRequestHeader(returnType)));

				if (token != null)
					client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

				// by calling .Result you are performing a synchronous call
				HttpResponseMessage response = client.DeleteAsync(urlPath).Result;
                if (response.IsSuccessStatusCode)
                    responseString = response.Content.ReadAsStringAsync().Result;
            }

            return responseString;
        }
    }
}
