Feature: Cards
    I want to verify the cards api endpoints work as expected


Scenario: Verify Get Card
    Given I have the card ID "IN005"
    When I send a GET request to retrieve a card
    Then the response status code should be 200
    And the card name should be "IN-005: 503 Service Unavailable Error Displayed When Trying to Checkout Flights to South Korea"
    And the response should match the schema "GetCardSchema.json"

Scenario: Verify Get Cards
    Given I have the list ID "AwaitDeployFuncTest"
    When I send a GET request to retrieve cards
    Then the response status code should be 200
    And the response should match the schema "GetCardsSchema.json"