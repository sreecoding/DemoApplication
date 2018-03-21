Feature: SaveTaxRate
	In order to make sure the gift aid tax rate is upto date
	As a user
	I want to be able to update / create tax rates for countries

Background: 
Given We have the Following Tax Data in the database
| Country | TaxRate |
| UK      | 20      |

@mytag
Scenario: Create Gift Aid Tax Rate for a Country
	Given I have specified a Tax Rate of '15'
	And I have specified country as 'US'
	When I Call Tax Controller
	Then the new tax rate is added to the table
