using ConquestTests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
        /**
         * This method test getting a request using requestID
         */
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

        /**
         * This method test creating a request WITH file upload. Document attach to request.
         * This simulates a user interaction:
         * 1. User creates request.
         * 2. User uploads a document along with the request.
         */
        [TestMethod]
        public async System.Threading.Tasks.Task CreateRequestAsync()
        {
            var ACCESS_TOKEN = WebConfigurationManager.AppSettings["access_token"];
            var API_LIST_HIERACHY = WebConfigurationManager.AppSettings["api_list_hierachy"];
            var API_CREATE_REQUEST = WebConfigurationManager.AppSettings["api_create_request"];
            var API_ADD_DOCUMENT = WebConfigurationManager.AppSettings["api_add_document"];
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
            node.ObjectKey = new ObjectKey() { ObjectType = "ObjectType_OrganisationUnit", Int32Value = 0 };
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

            // Create Document for chosen Request. Change path to "Request" and use requestID.
            Document document = new Document() { DocumentDescription = "This is a request", Address = $"file://conquest_documents/Request/{requestID}/JasonTestDocument.txt", ContentType = "text/plain", };
            document.ObjectKey = new ObjectKey() { ObjectType = "ObjectType_Request", Int32Value = int.Parse(requestID) };
            HttpResponseMessage createDocumentResponse = await client.PostAsJsonAsync(API_ADD_DOCUMENT, document);
            // Check response
            if (!createDocumentResponse.IsSuccessStatusCode)
            {
                throw new System.ArgumentException(createDocumentResponse.StatusCode.ToString(), "original");
            }
            Debug.WriteLine(createDocumentResponse.Content.ReadAsStringAsync().Result);

            // Upload Document for request
            string uploadUrl = "";
            Dictionary<string,dynamic> netObject = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(createDocumentResponse.Content.ReadAsStringAsync().Result);
            if (netObject.TryGetValue("UploadUri", out dynamic value))
            {
                uploadUrl = Convert.ToString(value);
            }
            else
            {
                throw new System.ArgumentException("No upload url found", "original");
            }
            HttpResponseMessage uploadDocumentResponse = await client.PostAsJsonAsync(uploadUrl, document);
            // Check response
            if (!uploadDocumentResponse.IsSuccessStatusCode)
            {
                throw new System.ArgumentException(createDocumentResponse.StatusCode.ToString(), "original");
            }

        }
    }
}
