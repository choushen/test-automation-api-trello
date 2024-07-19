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
    public class CardSteps
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly CardRequests _cardRequests;
        private RestResponse _response;

        public CardSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            _cardRequests = new CardRequests();
        }

        // Given Steps
        [Given(@"I have the list ID ""(.*)""")]
        public void GivenIHaveTheListID(string listId)
        {
            _scenarioContext["listId"] = listId;
        }

        [Given(@"I have the card ID ""(.*)""")]
        public void GivenIHaveTheCardID(string cardId)
        {
            _scenarioContext["cardId"] = cardId;
        }

        // When Steps
        [When(@"I send a GET request to retrieve cards")]
        public void WhenISendAGETRequestToRetrieveCards()
        {
            string listId = _scenarioContext["listId"].ToString();
            _response = _cardRequests.GetCards(listId);
        }

        [When(@"I send a GET request to retrieve a card")]
        public void WhenISendAGETRequestToRetrieveACard()
        {
            string cardId = _scenarioContext["cardId"].ToString();
            _response = _cardRequests.GetCard(cardId);
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

        [Then(@"the card name should be ""(.*)""")]
        public void ThenTheCardNameShouldBe(string expectedCardName)
        {
            string cardName = JToken.Parse(_response.Content).SelectToken("name").ToString();
            Assert.That(expectedCardName, Is.EqualTo(cardName));
        }
    }
}
