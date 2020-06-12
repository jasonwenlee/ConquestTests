using ConquestTests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Web.Configuration;
using DocContainer = ConquestTests.Models.DocContainer;

namespace ConquestTests
{
    [TestClass]
    public class RequestTest
    {
        /**
         * This method test creating a request WITH file upload. Document attach to request.
         * This simulates a user interaction:
         * 1. User creates request.
         * 2. User uploads a text document along with the request.
         */
        [TestMethod]
        public async System.Threading.Tasks.Task CreateRequestImageUploadAsync()
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

            // Post to get organisation unit to a list of units.
            HierachyNode node = new HierachyNode() { IncludeAncestors = true, IncludeChildren = true, IncludeDescendents = 0, IncludeSiblings = true, IncludeSubItems = true };
            node.ObjectKey = new ObjectKey() { ObjectType = "ObjectType_OrganisationUnit", Int32Value = 0 };
            // POST request
            HttpResponseMessage response = await client.PostAsJsonAsync(API_LIST_HIERACHY, node);
            // Check response
            if (!response.IsSuccessStatusCode)
            {
                throw new System.ArgumentException(response.StatusCode.ToString(), "Cannot get organisation units");
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
            user.ChangeSet.Updated = new Updated { RequestDetail = "This a request" + netObjects.Headers[0].ObjectName, RequestorName = "Jason Lee", OrganisationUnitID = orgUnitId };
            // POST request
            HttpResponseMessage createUserResponse = await client.PostAsJsonAsync(API_CREATE_REQUEST, user);
            // Check response
            if (!createUserResponse.IsSuccessStatusCode)
            {
                throw new System.ArgumentException(createUserResponse.StatusCode.ToString(), "Cannot create user");
            }
            // Get request ID
            string requestID = createUserResponse.Content.ReadAsStringAsync().Result;
            Debug.WriteLine(String.Format("Request ID: {0}    |     Organisation Name: {1}", requestID, netObjects.Headers[0].ObjectName));

            // Create Container for chosen Request. Change path to "Request" and use requestID.
            DocContainer con = new DocContainer() { DocumentDescription = "Request Image", Address = $"file://conquest_documents/Request/{requestID}/JasonTestImage.png", ContentType = "image/png", };
            con.ObjectKey = new ObjectKey() { ObjectType = "ObjectType_Request", Int32Value = int.Parse(requestID) };
            HttpResponseMessage createFileResponse = await client.PostAsJsonAsync(API_ADD_DOCUMENT, con);
            // Check response
            if (!createFileResponse.IsSuccessStatusCode)
            {
                throw new System.ArgumentException(createFileResponse.StatusCode.ToString(), "Cannot create file response");
            }
            Debug.WriteLine(createFileResponse.Content.ReadAsStringAsync().Result);

            // Upload image to container
            DocDataObject docDataObject = JsonConvert.DeserializeObject<DocDataObject>(createFileResponse.Content.ReadAsStringAsync().Result);

            var f = System.IO.File.OpenRead("..\\..\\B.png");
            var content = new StreamContent(f);
            var mpcontent = new MultipartFormDataContent();
            content.Headers.ContentType = new MediaTypeHeaderValue("image/png");
            mpcontent.Add(content);

            HttpResponseMessage uploadDocumentResponse = await client.PutAsync(docDataObject.UploadUri, mpcontent);

            //Check response
            if (!uploadDocumentResponse.IsSuccessStatusCode)
            {
                throw new System.ArgumentException(uploadDocumentResponse.StatusCode.ToString(), "Cannot upload image");
            }
        }

        /**
         * This method test creating a request WITH file upload. Document attach to request.
         * This simulates a user interaction:
         * 1. User creates request.
         * 2. User uploads text along with the request.
         */
        [TestMethod]
        public async System.Threading.Tasks.Task CreateRequestTextUploadAsync()
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

            // Post to get organisation unit to a list of units.
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
            user.ChangeSet.Updated = new Updated { RequestDetail = "This a request" + netObjects.Headers[0].ObjectName, RequestorName = "Jason Lee", OrganisationUnitID = orgUnitId };
            // POST request
            HttpResponseMessage createUserResponse = await client.PostAsJsonAsync(API_CREATE_REQUEST, user);
            // Check response
            if (!createUserResponse.IsSuccessStatusCode)
            {
                throw new System.ArgumentException(createUserResponse.StatusCode.ToString(), "original");
            }
            // Get request ID
            string requestID = createUserResponse.Content.ReadAsStringAsync().Result;
            Debug.WriteLine(String.Format("Request ID: {0}    |     Organisation Name: {1}", requestID, netObjects.Headers[0].ObjectName));

            // Create Container for chosen Request. Change path to "Request" and use requestID.
            DocContainer con = new DocContainer() { DocumentDescription = "Request document", Address = $"file://conquest_documents/Request/{requestID}/JasonTestDocument.txt", ContentType = "text/plain", };
            con.ObjectKey = new ObjectKey() { ObjectType = "ObjectType_Request", Int32Value = int.Parse(requestID) };
            HttpResponseMessage createFileResponse = await client.PostAsJsonAsync(API_ADD_DOCUMENT, con);
            // Check response
            if (!createFileResponse.IsSuccessStatusCode)
            {
                throw new System.ArgumentException(createFileResponse.StatusCode.ToString(), "original");
            }
            Debug.WriteLine(createFileResponse.Content.ReadAsStringAsync().Result);

            // Upload document to Container for chosen Request.
            DocDataObject docDataObject = JsonConvert.DeserializeObject<DocDataObject>(createFileResponse.Content.ReadAsStringAsync().Result);
            HttpResponseMessage uploadDocumentResponse = await client.PutAsJsonAsync(docDataObject.UploadUri, "Hello");

            //Check response
            if (!uploadDocumentResponse.IsSuccessStatusCode)
            {
                throw new System.ArgumentException(uploadDocumentResponse.StatusCode.ToString(), "Cannot upload text");
            }
        }
    }
}
