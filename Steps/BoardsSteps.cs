using NUnit.Framework;
using RestSharp;
using RestSharpProject.Helpers;
using RestSharpProject.Requests;
using TechTalk.SpecFlow;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using System.IO;

namespace RestSharpProject.Steps
{
    [Binding]
    public class BoardSteps
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly BoardRequests _boardRequests;
        private RestResponse _response;

        public BoardSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            _boardRequests = new BoardRequests();
        }

        // Given Steps
        [Given(@"I have the member ID ""(.*)""")]
        public void GivenIHaveTheMemberID(string memberId)
        {
            _scenarioContext["memberId"] = memberId;
        }

        [Given(@"I have the board ID ""(.*)""")]
        public void GivenIHaveTheBoardID(string boardId)
        {
            _scenarioContext["boardId"] = boardId;
        }

        // When Steps
        [When(@"I send a GET request to retrieve boards")]
        public void WhenISendAGETRequestToRetrieveBoards()
        {
            string memberId = _scenarioContext["memberId"].ToString();
            _response = _boardRequests.GetBoards(memberId);
        }

        [When(@"I send a GET request to retrieve a board")]
        public void WhenISendAGETRequestToRetrieveABoard()
        {
            string boardId = _scenarioContext["boardId"].ToString();
            _response = _boardRequests.GetBoard(boardId);
        }

        // Then Steps
        [Then(@"the response status code should be (.*)")]
        public void ThenTheResponseStatusCodeShouldBe(int statusCode)
        {
            Assert.That((int)_response.StatusCode, Is.EqualTo(statusCode));
        }

        [Then(@"the response should match the schema ""(.*)""")]
        public void ThenTheResponseShouldMatchTheSchema(string schemaFileName)
        {
            string schemaPath = PathHelper.GetFilePath($"RestSharpProject/Resources/Schemas/{schemaFileName}");
            var jsonSchema = JSchema.Parse(File.ReadAllText(schemaPath));
            var responseContent = JToken.Parse(_response.Content);
            Assert.True(responseContent.IsValid(jsonSchema), "The JSON response does not match the schema.");
        }

        [Then(@"the board name should be ""(.*)""")]
        public void ThenTheBoardNameShouldBe(string expectedBoardName)
        {
            string boardName = JToken.Parse(_response.Content).SelectToken("name").ToString();
            Assert.That(expectedBoardName, Is.EqualTo(boardName));
        }
    }
}
