using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ConquestTests
{
    [TestClass]
    public class RequestTest
    {
        [TestMethod]
        public void EstablishConnection()
        {

            var ACCESS_TOKEN = "RO7B15hxNvRZGasvXOR9G/10+Jk=";
            HttpClient client = new HttpClient
            {
                BaseAddress = new Uri("https://developer-demo.australiaeast.cloudapp.azure.com/")
            };
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ACCESS_TOKEN);

        }

        [TestMethod]
        public async System.Threading.Tasks.Task GetRequestAsync()
        {

            var value = 91;
            var ACCESS_TOKEN = "RO7B15hxNvRZGasvXOR9G/10+Jk=";
            var APIPATH = $"/api/requests/{value}";
            string responseDetail = "";
            HttpClient client = new HttpClient
            {
                BaseAddress = new Uri("https://developer-demo.australiaeast.cloudapp.azure.com/")
            };
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 |
                                                    SecurityProtocolType.Tls | 
                                                    SecurityProtocolType.Tls11 | 
                                                    SecurityProtocolType.Tls12;

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ACCESS_TOKEN);
            HttpResponseMessage response = await client.GetAsync(APIPATH);
            if (response.IsSuccessStatusCode)
            {
                responseDetail = response.Content.ReadAsStringAsync().Result;
            }
            Debug.Write(responseDetail);
        }
    }
}
