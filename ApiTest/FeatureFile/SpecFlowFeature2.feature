Feature: Create company
	In order to have page of company 
	As a user
	I want to create company

@mytag
Scenario: Successful creating company
	Given  Datas is ready for create a new company
	When I send post request for creating company
	Then Company has been created
	Then Company type equal company type whith we sended by request creating company
	Then Company name equal company name whith we sended by request creating company