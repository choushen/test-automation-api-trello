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
    public class ListSteps
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly ListRequests _listRequests;
        private RestResponse _response;

        public ListSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            _listRequests = new ListRequests();
        }

        // Given Steps
        [Given(@"I have the board ID ""(.*)""")]
        public void GivenIHaveTheBoardID(string boardId)
        {
            _scenarioContext["boardId"] = boardId;
        }

        // When Steps
        [When(@"I send a GET request to retrieve lists")]
        public void WhenISendAGETRequestToRetrieveLists()
        {
            string boardId = _scenarioContext["boardId"].ToString();
            _response = _listRequests.GetLists(boardId);
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
    }
}
