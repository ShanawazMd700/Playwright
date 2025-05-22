Feature: Webpage
![Calculator](https://specflow.org/wp-content/uploads/2020/09/calculator.png)
Simple calculator for adding **two** numbers

Link to a feature: [Calculator](PlayWrightPractice/Features/Calculator.feature)
***Further read***: **[Learn more about how to generate Living Documentation](https://docs.specflow.org/projects/specflow-livingdoc/en/latest/LivingDocGenerator/Generating-Documentation.html)**

@mytag
Scenario: Login with valid credentials
	Given the user is on login page "https://askomdch.com/account/"
	When the user enters username "Md Shanawaz" and password "password"
	When the user clicks login button
	Then the dashboard should be seen

Scenario: Sliding the slider
   Given going to the shoppoing website
   When slider is selected and slided to '20' and '90'
   Then verify if slided

Scenario: Selecting the browse bys category
	Given user goes to the website "https://askomdch.com/store/"
	#When selecting the dropdown to select by category
	When selecting the option "accessories"
	Then verify "accessories" is clicked