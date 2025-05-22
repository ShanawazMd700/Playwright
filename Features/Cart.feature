Feature: Cart

A short summary of the feature

@tag1
Scenario: Selecting the items to the cart
	Given Selecting the random elements from the page
	When the cart button is clicked
	Then verify the items in the cart

	@1
	Scenario: Selecting mens items
	Given Go to main webpage
	When selected "mens" category on header
	When random mens item is selected from the page
	Then verify the mens items in the cart

	@2
	Scenario: Selecting womens item
	Given going to main webpage
	When selecting womens category on header
	When random womens item is selected from the page
	Then verify the selected items in the cart