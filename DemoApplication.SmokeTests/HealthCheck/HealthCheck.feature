Feature: HealthCheck
	In order to make sure that the service is up and running
	As a developer
	I want to be able to call the HealthCheck End Point and verify that the service is up

@mytag
Scenario: Health Check Call Happy Path
	When I call the HealthCheck EndPoint
	Then the result should be that the system is up


