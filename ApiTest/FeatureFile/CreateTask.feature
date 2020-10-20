Feature: Create task
	In order to get the job done
	As a boss
	I want to create task for my worker

Background: 
Given create rest client for task

@task
Scenario: Successful creating task
	Given  Data for create a new task is ready
	When I send post request for creating task
	Then Status response for creating task is successful
	Then Id task not equal null
	Then Message received - task created successfully