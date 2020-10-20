Feature: Search
	In order to search get information from database
	As a user
	I want to enter word in search field

Background: 
Given create rest client for searching

@search
Scenario: Successful searching user`s name
	When I send post searching request
	Then Status response for searching is successful
	Then Users name from response equal name of request
	Then Count users  not equal null