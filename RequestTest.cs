using ConquestTests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace ConquestTests
{
    [TestClass]
    public class RequestTest
    {
        [TestMethod]
        public async System.Threading.Tasks.Task EstablishConnectionAsync()
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
            var requestID = 91;
            var ACCESS_TOKEN = "RO7B15hxNvRZGasvXOR9G/10+Jk=";
            var APIPATH = $"/api/requests/{requestID}";
            string responseDetail = "";
            HttpClient client = new HttpClient
            {
                BaseAddress = new Uri("https://developer-demo.australiaeast.cloudapp.azure.com/")
            };
            // Address SSL and TLS security issue.
            #region
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 |
                                                    SecurityProtocolType.Tls |
                                                    SecurityProtocolType.Tls11 |
                                                    SecurityProtocolType.Tls12;
            #endregion

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ACCESS_TOKEN);
            // GET request
            HttpResponseMessage response = await client.GetAsync(APIPATH);
            if (!response.IsSuccessStatusCode)
            {
                throw new System.ArgumentException(response.StatusCode.ToString(), "original");
            }
            responseDetail = response.Content.ReadAsStringAsync().Result;
            var netObject = JsonConvert.DeserializeObject(responseDetail);
            // Response body
            Debug.WriteLine(netObject);
            Debug.WriteLine("");
            // Response code
            Debug.WriteLine(response);
        }

        [TestMethod]
        public async System.Threading.Tasks.Task CreateRequestAsync()
        {
            var ACCESS_TOKEN = "RO7B15hxNvRZGasvXOR9G/10+Jk=";
            var API_HIERACHY = "/api/list_hierarchy_nodes";
            string responseDetail = "";
            HttpClient client = new HttpClient
            {
                BaseAddress = new Uri("https://developer-demo.australiaeast.cloudapp.azure.com/")
            };

            // Address SSL and TLS security issue.
            #region
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 |
                                                    SecurityProtocolType.Tls |
                                                    SecurityProtocolType.Tls11 |
                                                    SecurityProtocolType.Tls12;
            #endregion

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ACCESS_TOKEN);

            HierachyNode node = new HierachyNode() { IncludeAncestors = true, IncludeChildren = true, IncludeDescendents = 0, IncludeSiblings = true, IncludeSubItems = true };
            node.ObjectKey = new ObjectKey() { ObjectType = "ObjectType_OrganisationUnit" };
            //ObjectKey objKey = new ObjectKey() { ObjectType = "ObjectType_OrganisationUnit" };
            var json = JsonConvert.SerializeObject(node);

            HttpResponseMessage response = await client.PostAsJsonAsync(API_HIERACHY, "{\"ObjectKey\":{\"objectType\":\"ObjectType_OrganisationUnit\"}}");
            if (!response.IsSuccessStatusCode)
            {
                throw new System.ArgumentException(response.StatusCode.ToString(), "original");
            }
            //responseDetail = response.Content.ReadAsStringAsync().Result;

            //Debug.WriteLine(responseDetail);
        }
    }
}
