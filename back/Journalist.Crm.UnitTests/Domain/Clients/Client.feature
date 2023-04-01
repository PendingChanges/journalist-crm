Feature: Client

A short summary of the feature

@client
Scenario: The user create a client
	Given No existing client
	When A user with id "testuser" create a client with name "Client Test"
	Then A client "Client Test" owned by "testuser" is created

@client
Scenario: A user delete its own client
	Given An existing client with name "Client Test" and an owner "testuser"
	When A user with id "testuser" delete the client
	Then The client is deleted
	And No errors

@client
Scenario: A user tries to delete a client he doesn't own
	Given An existing client with name "Client Test" and an owner "testuser"
	When A user with id "testuser2" delete the client
	Then An error with code "NOT_CLIENT_OWNER" is raised
	And The client is not deleted