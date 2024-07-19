Feature: Validate Get Boards

Scenario: Verify Get Boards with Invalid ID
    When I send a GET request to retrieve boards with an invalid ID
    Then the response status code should be 400

Scenario: Verify Get Boards without Authentication
    Given I have the member ID "Member1"
    When I send a GET request to retrieve boards without authentication
    Then the response status code should be 401
