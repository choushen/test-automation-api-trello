Feature: Lists
    I want to verify the lists api endpoints work as expected

Scenario: Verify Get Lists
    Given I have the board ID "APITesting"
    When I send a GET request to retrieve lists
    Then the response status code should be 200
    And the response should match the schema "GetListsSchema.json"