Feature: mens

A short summary of the feature

@tag1
Scenario: Mens page
	Given Selecting mens option
	When dropdown is clicked
	#When selecting low to high
	Then verify if sorted

Scenario: Selecting the product from the search bar
	Given selecting the mens option
	When selecting the searchbar and entering "Shoes"
	Then verify if shoes are displayed

