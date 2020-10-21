Feature: Registeration
	In order to have my account with personal information
	As a user
	I want to register in the site

Background: 
Given create rest client

@auth
Scenario: Successful registeration
	Given Data for registration is ready 
	When I send post registration request
	Then Status response is successful
	Then Name from response equal name of request
	Then Email from response equal email of request
	

