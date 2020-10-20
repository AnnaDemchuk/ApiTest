Feature: Create company
	In order to have page of company 
	As a user
	I want to create company

Background: 
Given create rest client for regisrtation

@company
Scenario: Successful creating company
	Given  Data for create a new company is ready
	When I send post request for creating company
	Then Status response for creating company is successful
	Then Company name from response equal company name of request
	Then Company type from response equal company type of request
	Then Users of company  from response equal users of company of request
	
