Feature: Validate Get Card

Scenario: Verify Get Card with Invalid ID
    When I send a GET request to retrieve a card with an invalid ID
    Then the response status code should be 400

Scenario: Verify Get Card without Authentication
    Given I have the card ID "IN005"
    When I send a GET request to retrieve a card without authentication
    Then the response status code should be 401
