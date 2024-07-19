using NUnit.Framework;
using RestSharp;
using RestSharpProject.Helpers;
using RestSharpProject.Requests;
using TechTalk.SpecFlow;

namespace RestSharpProject.Steps
{
    [Binding]
    public class BoardValidationSteps
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly BoardRequests _boardRequests;
        private RestResponse _response;

        public BoardValidationSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            _boardRequests = new BoardRequests();
        }

        // When Steps
        [When(@"I send a GET request to retrieve boards with an invalid ID")]
        public void WhenISendAGETRequestToRetrieveBoardsWithAnInvalidID()
        {
            _response = _boardRequests.GetBoardsInvalidId();
        }

        [When(@"I send a GET request to retrieve boards without authentication")]
        public void WhenISendAGETRequestToRetrieveBoardsWithoutAuthentication()
        {
            string memberId = _scenarioContext["memberId"].ToString();
            _response = _boardRequests.GetBoardsNoAuth(memberId);
        }

        // Then Steps
        [Then(@"the response status code should be (.*)")]
        public void ThenTheResponseStatusCodeShouldBe(int statusCode)
        {
            Assert.That((int)_response.StatusCode, Is.EqualTo(statusCode));
        }
    }
}
