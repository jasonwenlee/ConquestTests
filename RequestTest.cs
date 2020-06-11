using ConquestTests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Web.Configuration;

namespace ConquestTests
{
    [TestClass]
    public class RequestTest
    {
        [TestMethod]
        public async System.Threading.Tasks.Task GetRequestAsync()
        {
            var requestID = 180;
            var ACCESS_TOKEN = WebConfigurationManager.AppSettings["access_token"];
            var API_GET_REQUEST = WebConfigurationManager.AppSettings["api_get_request"];

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
            HttpResponseMessage response = await client.GetAsync(String.Format("{0}/{1}", API_GET_REQUEST, requestID));
            // Check response
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
            var ACCESS_TOKEN = WebConfigurationManager.AppSettings["access_token"];
            var API_LIST_HIERACHY = WebConfigurationManager.AppSettings["api_list_hierachy"];
            var API_CREATE_REQUEST = WebConfigurationManager.AppSettings["api_create_request"];
            string responseDetail = "";

            HttpClient client = new HttpClient
            {
                BaseAddress = new Uri(WebConfigurationManager.AppSettings["apiBaseAddress"])
            };

            // Address SSL and TLS security issue.
            #region
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 |
                                                    SecurityProtocolType.Tls |
                                                    SecurityProtocolType.Tls11 |
                                                    SecurityProtocolType.Tls12;
            #endregion
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ACCESS_TOKEN);

            // Post object type as get organisation unit to a list of units.
            HierachyNode node = new HierachyNode() { IncludeAncestors = true, IncludeChildren = true, IncludeDescendents = 0, IncludeSiblings = true, IncludeSubItems = true };
            node.ObjectKey = new ObjectKey() { ObjectType = "ObjectType_OrganisationUnit" };
            // POST request
            HttpResponseMessage response = await client.PostAsJsonAsync(API_LIST_HIERACHY, node);
            // Check response
            if (!response.IsSuccessStatusCode)
            {
                throw new System.ArgumentException(response.StatusCode.ToString(), "original");
            }
            // Get headers which contain organisation units
            responseDetail = response.Content.ReadAsStringAsync().Result;
            AllHeaders netObjects = JsonConvert.DeserializeObject<AllHeaders>(responseDetail);


            // Get organisation ID from first header for each department
            int orgUnitId = netObjects.Headers[0].ObjectKey.Int32Value;
            // Create user object and changing the necessary fields.
            string[] changes = { "RequestDetail", "RequestorName", "OrganisationUnitID" };
            User user = new User();
            user.ChangeSet = new ChangeSet() { Changes = changes };
            user.ChangeSet.Updated = new Updated { RequestDetail = "Hello From C#, " + netObjects.Headers[0].ObjectName, RequestorName = "Jason Lee", OrganisationUnitID = orgUnitId };
            // POST request
            HttpResponseMessage createUserResponse = await client.PostAsJsonAsync(API_CREATE_REQUEST, user);
            // Check response
            if (!response.IsSuccessStatusCode)
            {
                throw new System.ArgumentException(createUserResponse.StatusCode.ToString(), "original");
            }
            // Get request ID
            string requestID = createUserResponse.Content.ReadAsStringAsync().Result;
            Debug.WriteLine(String.Format("Request ID: {0}    |     Organisation Name: {1}", requestID, netObjects.Headers[0].ObjectName));
        }
    }
}
