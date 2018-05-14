Feature: GiftAidCalculation
	In order to give the maximum benefit to charities 
	When a person makes a donation
	I want to calculate the correct gift Aid for the donation. 

Background: 
Given We have the Following Tax Data in the database
| Country | TaxRate |
| UK      | 20      |

@mytag
Scenario: Calcualte Gift Aid
	Given The Event is General
	And the Donation Amount is 100 pounds
	And the Donation Country is UK
	When I make the Donation 
	Then the Total Gift Aid Amount Should be 25 pounds


Scenario: Calcualte Gift Aid for Swimming
	Given The Event is Swimming
	And the Donation Amount is 100 pounds
	And the Donation Country is UK
	When I make the Donation 
	Then the Status Code should be Ok
	Then the Total Gift Aid Amount Should be 30 pounds


Scenario: Invalid input returns a BadRequest
	Given The Event is XYZ
	And the Donation Amount is 100 pounds
	And the Donation Country is UK
	When I make the Donation Call for Bad Request
	Then we get a BadRequest Response


