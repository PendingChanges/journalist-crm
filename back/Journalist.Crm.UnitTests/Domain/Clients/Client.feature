Feature: Client

A short summary of the feature

@client
Scenario: The user create a client
	Given No existing client
	When A user with id "<userid>" create a client with name "<clientName>"
	Then A client "<clientName>" owned by "<userid>" is created
	
	Examples: 
	| userid   | clientName  |
	| testuser | Client Test |

@client
Scenario: A user delete its own client
	Given An existing client with name "<clientName>" and an owner "<userid>"
	When A user with id "<userid>" delete the client
	Then The client is deleted
	And No errors

	Examples: 
	| userid   | clientName  |
	| testuser | Client Test |

@client
Scenario: A user tries to delete a client he doesn't own
	Given An existing client with name "<clientName>" and an owner "<testuser>"
	When A user with id "<otherUserid>" delete the client
	Then An error with code "<errorCode>" is raised
	And The client is not deleted

	Examples: 
	| userid   | clientName  | otherUserid | errorCode        |
	| testuser | Client Test | testuser2   | NOT_CLIENT_OWNER |

@client
Scenario: A user rename the client
	Given An existing client with name "<clientName>" and an owner "<userid>"
	When A user with id "<userid>"rename the client to "<clientNameModified>"
	Then The client is renamed to "<clientNameModified>"
	And No errors

	Examples: 
	| userid   | clientName  | clientNameModified   |
	| testuser | Client Test | Client Test modified |