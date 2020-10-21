Feature: Test API requests
	In order to have page of company 
	As a user
	I want to create company

	In order to get the job done
	As a boss
	I want to create task for my worker

	In order to have my account with personal information
	As a user
	I want to register in the site

	In order to search get information from database
	As a user
	I want to enter word in search field

	In order to have my account with my task
	As a user
	I want to register in the site

	To log into your account
	As a user
	I want to check my password

Background: 
Given Create rest client

@company
Scenario: Successful creating company
	Given  Data for create a new company is ready
	When I send post request for creating company
	Then Status response is successful
	Then Company name from response equal company name of request
	Then Company type from response equal company type of request
	Then Users of company  from response equal users of company of request

@task
Scenario: Successful creating task
	Given  Data for create a new task is ready
	When I send post request for creating task
	Then Status response is successful
	Then Id task not equal null
	Then Message received - task created successfully


@auth
Scenario: Successful registeration
	Given Data for registration is ready 
	When I send post registration request
	Then Status response is successful
	Then Name from response equal name of request
	Then Email from response equal email of request

@search
Scenario: Successful searching information about user on email
	Given Data for searching is ready 
	When I send post searching request
	Then Status response is 231
	Then Users email from response equal email of request

@auth
Scenario: Successfuly creating user with tasks (existing)
	Given Data for user is ready
	When I send post request with user data
	Then Users information from response equal information of request


@login
Scenario: Login into an account with valid data
	Given Data for login is ready
	When I send post request with login data
	Then Status response is successful
	Then Server response is true
