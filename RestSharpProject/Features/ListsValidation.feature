Feature: Validate Get Lists

Scenario: Verify Get Lists with Invalid ID
    When I send a GET request to retrieve lists with an invalid ID
    Then the response status code should be 400

Scenario: Verify Get Lists without Authentication
    Given I have the board ID "APITesting"
    When I send a GET request to retrieve lists without authentication
    Then the response status code should be 401
