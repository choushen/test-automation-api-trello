using RestSharpProject.Helpers;
using RestSharpProject.Config;
using NUnit.Framework;
using RestSharp;
using System;

namespace RestSharpProject.Tests
{
    [TestFixture]
    public class TrelloApiTests
    {

        // PROPERTIES
        private AuthHelper _authHelper;
        private RequestConfig _requestConfig;
        private RestClient _client;
        private const string BaseUrl = "https://api.trello.com";
        private const int ExpectedStatusCode = 200;


        // METHODS
        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _authHelper = new AuthHelper();
            _requestConfig = new RequestConfig();
            _client = new RestClient(BaseUrl);
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


        [Test]
        public void VerifyGetMembersBoards()
        {
            var membersConfig = _requestConfig.ConfigBuilder("members");

            var response = GetMembersBoards(membersConfig["member1"]);

            TestContext.WriteLine($"Status: {(int)response.StatusCode} {response.StatusDescription}");

            Assert.That((int)response.StatusCode, Is.EqualTo(ExpectedStatusCode));
        } // VerifyGetMembersBoards end

        
        private RestResponse GetMembersBoards(string membersId)
        {    
            var request = new RestRequest($"1/members/{membersId}/boards"); // Use string interpolation here
            request = _authHelper.AddKeyAndToken(request);
            return _client.Get(request);
        } // GetMembersBoards end

    }
}