using NUnit.Framework;
using RestSharp;
using RestSharpProject.Helpers;
using RestSharpProject.Requests;
using TechTalk.SpecFlow;

namespace RestSharpProject.Steps
{
    [Binding]
    public class ListValidationSteps
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly ListRequests _listRequests;
        private RestResponse _response;

        public ListValidationSteps(ScenarioContext scenarioContext)
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
        [When(@"I send a GET request to retrieve lists with an invalid ID")]
        public void WhenISendAGETRequestToRetrieveListsWithAnInvalidID()
        {
            _response = _listRequests.GetListsInvalidId();
        }

        [When(@"I send a GET request to retrieve lists without authentication")]
        public void WhenISendAGETRequestToRetrieveListsWithoutAuthentication()
        {
            string boardId = _scenarioContext["boardId"].ToString();
            _response = _listRequests.GetListsNoAuth(boardId);
        }

        // Then Steps
        [Then(@"the response status code should be (.*)")]
        public void ThenTheResponseStatusCodeShouldBe(int statusCode)
        {
            Assert.That((int)_response.StatusCode, Is.EqualTo(statusCode));
        }
    }
}
