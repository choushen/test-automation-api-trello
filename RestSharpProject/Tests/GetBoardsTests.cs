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
    public class TrelloApiTests
    {

        // PROPERTIES
        private AuthHelper _authHelper;
        private RequestConfig _requestConfig;
        private RestClient _client;
        private const string BaseUrl = "https://api.trello.com";
        private const int ExpectedStatusCode = 200;
        private const string BaseSchemasFolderPath = "RestSharpProject/Resources/Schemas/";


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
            string schemaPath = PathHelper.GetFilePath("RestSharpProject/Resources/Schemas/GetBoardsSchema.json");
            var jsonBoardsSchema = JSchema.Parse(File.ReadAllText(schemaPath));
            var responseContent = JToken.Parse(response.Content);
            // Validate the JSON response against the schema
            Assert.True(responseContent.IsValid(jsonBoardsSchema), "The JSON response does not match the schema.");
            // Validate the status code
            Assert.That((int)response.StatusCode, Is.EqualTo(ExpectedStatusCode));
        } // VerifyGetMembersBoards end


        [Test]
        public void VerifyGetBoard()
        {
            var boardsConfig = _requestConfig.ConfigBuilder("boards");
            var response = GetBoard(boardsConfig["APITesting"]);
            string boardName = JToken.Parse(response.Content).SelectToken("name").ToString();
            string schemaPath = PathHelper.GetFilePath("RestSharpProject/Resources/Schemas/GetBoardSchema.json");
            var jsonBoardSchema = JSchema.Parse(File.ReadAllText(schemaPath));
            Debug.WriteLine(response.Content);
            Debug.WriteLine(jsonBoardSchema);
            var responseContent = JToken.Parse(response.Content);
            // Validate the JSON response against the schema
            Assert.True(responseContent.IsValid(jsonBoardSchema), "The JSON response does not match the schema.");
            // Validate the status code
            Assert.That("API Testing", Is.EqualTo(boardName));
        } // VerifyGetBoard end


        [Test]
        public void VerifyGetCards()
        {
            var listConfig = _requestConfig.ConfigBuilder("lists");
            var response = GetCards(listConfig["AwaitDeployFuncTest"]);
            string schemaPath = PathHelper.GetFilePath("RestSharpProject/Resources/Schemas/GetCardsSchema.json");
            var jsonCardSchema = JSchema.Parse(File.ReadAllText(schemaPath));
            var responseContent = JToken.Parse(response.Content);
            // Validate the JSON response against the schema
            Assert.True(responseContent.IsValid(jsonCardSchema), "The JSON response does not match the schema.");
            // Validate the status code
            Assert.That((int)response.StatusCode, Is.EqualTo(ExpectedStatusCode));
        }


        [Test]
        public void VerifyGetCard()
        {
            var cardConfig = _requestConfig.ConfigBuilder("cards");
            var response = GetCard(cardConfig["IN005"]);
            string cardName = JToken.Parse(response.Content).SelectToken("name").ToString();
            string schemaPath = PathHelper.GetFilePath("RestSharpProject/Resources/Schemas/GetCardSchema.json");
            var jsonCardSchema = JSchema.Parse(File.ReadAllText(schemaPath));
            var responseContent = JToken.Parse(response.Content);
            // Validate the JSON response against the schema
            Assert.True(responseContent.IsValid(jsonCardSchema), "The JSON response does not match the schema.");
            // Validate the status code
            Assert.That("IN-005: 503 Service Unavailable Error Displayed When Trying to Checkout Flights to South Korea", Is.EqualTo(cardName));
        } // VerifyGetCards end


        [Test]
        public void VerifyGetLists()
        {
            var listsConfig = _requestConfig.ConfigBuilder("boards");
            var response = Getlists(listsConfig["APITesting"]);
            var schemaPath = PathHelper.GetFilePath("RestSharpProject/Resources/Schemas/GetListsSchema.json");
            var jsonListsSchema = JSchema.Parse(File.ReadAllText(schemaPath));
            var responseContent = JToken.Parse(response.Content);
            // Validate the JSON response against the schema
            Assert.True(responseContent.IsValid(jsonListsSchema), "The JSON response does not match the schema.");
            // Validate the status code
            Assert.That((int)response.StatusCode, Is.EqualTo(ExpectedStatusCode));
        } // VerifyGetLists end

        
        // SERVICES
        private RestResponse GetBoards(string membersId)
        {    
            var request = new RestRequest($"1/members/{membersId}/boards").AddQueryParameter("fields", "id,name"); 
            request = _authHelper.AddKeyAndToken(request);
            return _client.Get(request);
        } // GetBoards end


        private RestResponse GetBoard(string boardId)
        {
            var request = new RestRequest($"1/boards/{boardId}")
            .AddQueryParameter("fields", "id,name");
            request = _authHelper.AddKeyAndToken(request);
            return _client.Get(request);
        } // GetBoard end

        
        private RestResponse GetCards(string listId)
        {
            var request = new RestRequest($"1/lists/{listId}/cards")
            .AddQueryParameter("fields", "id,name,desc");
            request = _authHelper.AddKeyAndToken(request);
            return _client.Get(request);
        } // GetCards end

        
        private RestResponse GetCard(string cardId)
        {
            var request = new RestRequest($"1/cards/{cardId}")
            .AddQueryParameter("fields", "id,name,desc");
            request = _authHelper.AddKeyAndToken(request);
            return _client.Get(request);
        } // GetCard end

        private RestResponse Getlists(string boardId)
        {
            var request = new RestRequest($"/1/boards/{boardId}/lists")
            .AddQueryParameter("fields", "id,name");
            request = _authHelper.AddKeyAndToken(request);
            return _client.Get(request);
        } // GetLists end

    }
}