Feature: HealthCheck
	In order to make sure that the service is up and running
	As a developer
	I want to be able to check the status of the service

@mytag
Scenario: Health Check Call Happy Path
	Given The Database Connection is up and running
	When I call the HealthCheckController
	Then the result should be that the system is up


Scenario: Health Check Call Failure Path
	Given The Database Connection is down
	When I call the HealthCheckController
	Then the result should be that the system is down
