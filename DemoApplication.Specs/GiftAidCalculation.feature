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
	Given I have  No Gift Aid Excemption
	When I make the Donation of 100 in UK
	Then the Total Gift Amount Should be 25 pounds
