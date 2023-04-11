Feature: Pitch

@pitch
Scenario: The user create a pitch
	Given No existing pitch
	When A user with id "<userid>" create a pitch with title "<pitchTitle>", content "<pitchContent>", dead line date "<pitchDeadLineDate>", issue date "<pitchIssueDate>", client id "<pitchClientId>" and idea id "<pitchIdeaId>"
	Then A pitch "<pitchTitle>", content "<pitchContent>", dead line date "<pitchDeadLineDate>", issue date "<pitchIssueDate>", client id "<pitchClientId>" and idea id "<pitchIdeaId>" owned by "<userid>" is created
	
Examples:
	| userid   | pitchTitle | pitchContent  | pitchDeadLineDate | pitchIssueDate | pitchClientId | pitchIdeaId |
	| testuser | Pitch Test | Pitch Content | 8 april 2023      | 9 april 2023   | client Id     | Idea Id     |

@pitch
Scenario: A user delete its own pitch
	Given An existing pitch with title "<pitchTitle>", content "<pitchContent>", dead line date "<pitchDeadLineDate>", issue date "<pitchIssueDate>", client id "<pitchClientId>", idea id "<pitchIdeaId>" and an owner "<userid>"
	When A user with id "<userid>" delete the pitch
	Then The pitch is deleted
	And No errors

Examples:
	| userid   | pitchTitle | pitchContent  | pitchDeadLineDate | pitchIssueDate | pitchClientId | pitchIdeaId |
	| testuser | Pitch Test | Pitch Content | 8 april 2023      | 9 april 2023   | client Id     | Idea Id     |

@pitch
Scenario: A user tries to delete a pitch he doesn't own
	Given An existing pitch with title "<pitchTitle>", content "<pitchContent>", dead line date "<pitchDeadLineDate>", issue date "<pitchIssueDate>", client id "<pitchClientId>", idea id "<pitchIdeaId>" and an owner "<userid>"
	When A user with id "<otherUserid>" delete the pitch
	Then An error with code "<errorCode>" is raised
	And The pitch is not deleted

Examples:
	| userid   | pitchTitle | pitchContent  | pitchDeadLineDate | pitchIssueDate | pitchClientId | pitchIdeaId | otherUserid | errorCode        |
	| testuser | Pitch Test | Pitch Content | 8 april 2023      | 9 april 2023   | client Id     | Idea Id     | testuser2   | NOT_PITCH_OWNER |