Feature: Boards
    I want to verify the boards api endpoints work as expected


Scenario: Verify Get Board
    Given I have the board ID "APITesting"
    When I send a GET request to retrieve a board
    Then the response status code should be 200
    And the board name should be "API Testing"
    And the response should match the schema "GetBoardSchema.json"

Scenario: Verify Get Boards
    Given I have the member ID "Member1"
    When I send a GET request to retrieve boards
    Then the response status code should be 200
    And the response should match the schema "GetBoardsSchema.json"