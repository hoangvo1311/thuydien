using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndusG.Service.Helpers
{
    public class RestAPIHelper
    {
        public static IRestResponse Post(string url, string jsonData = null)
        {
            RestClient client = new RestClient(url);
            var request = new RestRequest(Method.POST);
            request.RequestFormat = DataFormat.Json;

            if (!string.IsNullOrEmpty(jsonData))
            {
                request.AddJsonBody(jsonData);
            }
            request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };

            return client.Execute(request);
        }
    }
}
