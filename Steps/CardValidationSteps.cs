using NUnit.Framework;
using RestSharp;
using RestSharpProject.Helpers;
using RestSharpProject.Requests;
using TechTalk.SpecFlow;

namespace RestSharpProject.Steps
{
    [Binding]
    public class CardValidationSteps
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly CardRequests _cardRequests;
        private RestResponse _response;

        public CardValidationSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            _cardRequests = new CardRequests();
        }

        // When Steps
        [When(@"I send a GET request to retrieve a card with an invalid ID")]
        public void WhenISendAGETRequestToRetrieveACardWithAnInvalidID()
        {
            _response = _cardRequests.GetCardInvalidId();
        }

        [When(@"I send a GET request to retrieve a card without authentication")]
        public void WhenISendAGETRequestToRetrieveACardWithoutAuthentication()
        {
            string cardId = _scenarioContext["cardId"].ToString();
            _response = _cardRequests.GetCardNoAuth(cardId);
        }

        // Then Steps
        [Then(@"the response status code should be (.*)")]
        public void ThenTheResponseStatusCodeShouldBe(int statusCode)
        {
            Assert.That((int)_response.StatusCode, Is.EqualTo(statusCode));
        }
    }
}
