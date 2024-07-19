using RestSharpProject.Helpers;
using RestSharpProject.Config;
using NUnit.Framework;
using RestSharp;
using System;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using System.Diagnostics;

namespace RestSharpProject.Tests
{
    [TestFixture]
    public class RequestValidationTests
    {

        // PROPERTIES
        private AuthHelper _authHelper;
        private RequestConfig _requestConfig;
        private RestClient _client;
        private GetBoardsTests _getBoardsTests;
        private const string BaseUrl = "https://api.trello.com";
        private const int ExpectedStatusCodeBadRequest = 400;
        private const int ExpectedStatusCodeUnauthorized = 401;
        private const string BaseSchemasFolderPath = "RestSharpProject/Resources/Schemas/";


        // HOOKS
        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _authHelper = new AuthHelper();
            _requestConfig = new RequestConfig();
            _client = new RestClient(BaseUrl);
            _getBoardsTests = new GetBoardsTests();
        }


        [SetUp]
        public void Setup()
        {
            TestContext.WriteLine("Setup");
        }


        [TearDown]
        public void TearDown()
        {
            TestContext.WriteLine("TearDown");
        }


        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            _client.Dispose();
        }


        // TESTS
        [Test]
        public void VerifyGetBoardsInvalidId()
        {
            var request = new RestRequest($"1/boards/InvalidId", Method.Get);
            _authHelper.AddKeyAndToken(request);
            var response = _client.Execute(request);
            // Validate the status code
            Assert.That((int)response.StatusCode, Is.EqualTo(ExpectedStatusCodeBadRequest));
        } // VerifyGetBoardsInvalidId end


        [Test]
        public void VerifyGetBoardsNoAuth()
        {
            var membersConfig = _requestConfig.ConfigBuilder("members");
            var request = new RestRequest($"1/boards/{membersConfig["Member1"]}", Method.Get);
            request.AddQueryParameter("key", "invalidKey");
            request.AddQueryParameter("token", "invalidKey");
            var response = _client.Execute(request);
            // Validate the status code
            Assert.That((int)response.StatusCode, Is.EqualTo(ExpectedStatusCodeUnauthorized));
        } // VerifyGetBoardsNoAuth end


        [Test]
        public void VerifyGetCardInvalidId()
        {

            var request = new RestRequest($"1/cards/InvalidId", Method.Get)
            .AddQueryParameter("fields", "id,name,desc");
            _authHelper.AddKeyAndToken(request);

            var response = _client.Execute(request);

            // Validate the JSON response against the schema
            // Validate the status code
            Assert.That((int)response.StatusCode, Is.EqualTo(ExpectedStatusCodeBadRequest));
        }

        [Test]
        public void VerifyGetCardNoAuth()
        {
            var cardConfig = _requestConfig.ConfigBuilder("cards");
            var request = new RestRequest($"1/cards/{cardConfig["IN005"]}", Method.Get)
            .AddQueryParameter("fields", "id,name,desc");

            var response = _client.Execute(request);

            // Validate the JSON response against the schema
            // Validate the status code
            Assert.That((int)response.StatusCode, Is.EqualTo(ExpectedStatusCodeUnauthorized));
        }
    

    }
}