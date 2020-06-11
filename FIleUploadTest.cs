using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Configuration;
using ConquestTests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace ConquestTests
{
    [TestClass]
    public class FileUploadTest
    {
        [TestMethod]
        public async System.Threading.Tasks.Task UploadTestAsync()
        {
            var ACCESS_TOKEN = WebConfigurationManager.AppSettings["access_token"];
            var API_CREATE_ASSET = WebConfigurationManager.AppSettings["api_create_asset"];
            var API_ADD_DOCUMENT = WebConfigurationManager.AppSettings["api_add_document"];

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

            // Create Asset
            Asset asset = new Asset() { Proposed = true, AssetDescription = "Jason's Asset C#" };
            // POST newly created asset
            HttpResponseMessage createAssetResponse = await client.PostAsJsonAsync(API_CREATE_ASSET, asset);
            // Check response
            if (!createAssetResponse.IsSuccessStatusCode)
            {
                throw new System.ArgumentException(createAssetResponse.StatusCode.ToString(), "original");
            }
            // Get Asset ID
            string assetID = createAssetResponse.Content.ReadAsStringAsync().Result;
            Debug.WriteLine(assetID);

            // Create Document for chosen Asset
            Document document = new Document() { DocumentDescription="C# document", Address=$"file://conquest_documents/Asset/{assetID}/JasonTestDocument.txt", ContentType="text/plain", };
            document.ObjectKey = new ObjectKey() { ObjectType = "ObjectType_Asset", Int32Value=int.Parse(assetID) };
            // POST newly created document to asset
            HttpResponseMessage createDocumentResponse = await client.PostAsJsonAsync(API_ADD_DOCUMENT, document);
            // Check response
            if (!createDocumentResponse.IsSuccessStatusCode)
            {
                throw new System.ArgumentException(createDocumentResponse.StatusCode.ToString(), "original");
            }
            Debug.WriteLine(createDocumentResponse.Content.ReadAsStringAsync().Result);
        }
    }
}
