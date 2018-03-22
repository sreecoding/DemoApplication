Feature: Validate Contract For Getting Gift Aid Values 
	In order to validate Gift Aid contract
	As a developer of the Delivery service
	I want to have a valid Gift Aid response

Scenario: Get Gift Aid Calculated
	Given I have a Donation Amount of 1000 
	And the country where I make the donation is UK
	And the event type is General
	When a call is made to get Gift Aid
	Then the response code from GiftAid endpoint should be OK
	And the response contains the correct gift aid amount
