using RestSharpProject.Helpers;
using RestSharpProject.Config;
using NUnit.Framework;
using RestSharp;
using System;
using Newtonsoft.Json.Linq;

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


        // HOOKS
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


        // TESTS
        [Test]
        public void VerifyGetBoards()
        {
            var membersConfig = _requestConfig.ConfigBuilder("members");

            var response = GetBoards(membersConfig["Member1"]);

            JToken responseJson = JToken.Parse(response.Content);


            TestContext.WriteLine($"Status: {(int)response.StatusCode} {response.StatusDescription}");

            Assert.That((int)response.StatusCode, Is.EqualTo(ExpectedStatusCode));
        } // VerifyGetMembersBoards end


        [Test]
        public void VerifyGetBoard()
        {
            var boardsConfig = _requestConfig.ConfigBuilder("boards");

            var response = GetBoard(boardsConfig["APITesting"]);

            string boardName = JToken.Parse(response.Content).SelectToken("name").ToString();

            Assert.That("API Testing", Is.EqualTo(boardName));
        } // VerifyGetBoard end


        [Test]
        public void VerifyGetCards()
        {
            var listConfig = _requestConfig.ConfigBuilder("lists");

            var response = GetCards(listConfig["AwaitDeployFuncTest"]);

            Assert.That((int)response.StatusCode, Is.EqualTo(ExpectedStatusCode));
        }


        [Test]
        public void VerifyGetCard()
        {
            var cardConfig = _requestConfig.ConfigBuilder("cards");
            var response = GetCard(cardConfig["IN001"]);
            string cardName = JToken.Parse(response.Content).SelectToken("name").ToString();
            Assert.That("IN-001: Flight Search Results Not Displaying Correctly on Mobile", Is.EqualTo(cardName));
        } // VerifyGetCards end


        [Test]
        public void VerifyGetLists()
        {
            var listsConfig = _requestConfig.ConfigBuilder("boards");
            var response = Getlists(listsConfig["APITesting"]);
            Assert.That((int)response.StatusCode, Is.EqualTo(ExpectedStatusCode));
        } // VerifyGetLists end

        
        // SERVICES
        private RestResponse GetBoards(string membersId)
        {    
            var request = new RestRequest($"1/members/{membersId}/boards"); 
            request = _authHelper.AddKeyAndToken(request);
            return _client.Get(request);
        } // GetBoards end


        private RestResponse GetBoard(string boardId)
        {
            var request = new RestRequest($"1/boards/{boardId}");
            request = _authHelper.AddKeyAndToken(request);
            return _client.Get(request);
        } // GetBoard end

        
        private RestResponse GetCards(string listId)
        {
            var request = new RestRequest($"1/lists/{listId}/cards");
            request = _authHelper.AddKeyAndToken(request);
            return _client.Get(request);
        } // GetCards end

        
        private RestResponse GetCard(string cardId)
        {
            var request = new RestRequest($"1/cards/{cardId}");
            request = _authHelper.AddKeyAndToken(request);
            return _client.Get(request);
        } // GetCard end

        private RestResponse Getlists(string boardId)
        {
            var request = new RestRequest($"/1/boards/{boardId}/lists");
            request = _authHelper.AddKeyAndToken(request);
            return _client.Get(request);
        } // GetLists end

    }
}