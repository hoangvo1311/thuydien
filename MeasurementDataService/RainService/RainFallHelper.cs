using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RainService
{
    public static class RainFallHelper
    {
        public static double GetRainFall(Plant plant)
        {
            RestClient client = new RestClient("https://openapi.vrain.vn/v1/stations/stats?last_x_records=1");
            var request = new RestRequest(Method.GET);
            string apiKey = string.Empty;
            switch (plant)
            {
                case Plant.DakSrong2:
                    apiKey = "MSgXPpROeM41WpJermtgE7m0oSFbfwrW1Alha3VV";
                    break;
                case Plant.DakSrong3A:
                    apiKey = "en3uz1b3m15NRLOeczwjV99p7AnjaoM61jcgNrig";
                    break;
            }

            request.AddHeader("x-api-key", apiKey);
            request.AddHeader("Content-Type", "application/json; charset=utf-8");
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            var response = client.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK && response.ErrorException == null)
            {
                switch (plant)
                {
                    case Plant.DakSrong2:
                        var data2 = JsonConvert.DeserializeObject<RainFallResponse_DakSrong2>(response.Content);
                        if (data2.Data.Any())
                        {
                            return Convert.ToDouble(data2.Data[0].depth);
                        }
                        return 0;
                    case Plant.DakSrong3A:
                        var data3A = JsonConvert.DeserializeObject<RainFallResponse_DakSrong3A>(response.Content);
                        if (data3A.Data.Any())
                        {
                            return Convert.ToDouble(data3A.Data[0].depth);
                        }
                        return 0;
                }
            }
            
            return 0;
        }

    }

    public class RainFallResponse_DakSrong2
    {
        [JsonProperty("84399470714")]
        public List<RainFallData> Data { get; set; }
    }

    public class RainFallResponse_DakSrong3A
    {
        [JsonProperty("84399471726")]
        public List<RainFallData> Data { get; set; }
    }

    public class RainFallData
    {
        public string time_point { get; set; }
        public string depth { get; set; }
    }

    public enum Plant
    {
        DakSrong2,
        DakSrong3A
    }
}
