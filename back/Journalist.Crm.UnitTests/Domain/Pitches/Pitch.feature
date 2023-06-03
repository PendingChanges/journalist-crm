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
Scenario: A user cancel its own pitch
	Given An existing pitch with title "<pitchTitle>", content "<pitchContent>", dead line date "<pitchDeadLineDate>", issue date "<pitchIssueDate>", client id "<pitchClientId>", idea id "<pitchIdeaId>" and an owner "<userid>"
	When A user with id "<userid>" cancel the pitch
	Then The pitch is deleted
	And No errors

Examples:
	| userid   | pitchTitle | pitchContent  | pitchDeadLineDate | pitchIssueDate | pitchClientId | pitchIdeaId |
	| testuser | Pitch Test | Pitch Content | 8 april 2023      | 9 april 2023   | client Id     | Idea Id     |

@pitch
Scenario: A user validate its own pitch
	Given An existing pitch with title "<pitchTitle>", content "<pitchContent>", dead line date "<pitchDeadLineDate>", issue date "<pitchIssueDate>", client id "<pitchClientId>", idea id "<pitchIdeaId>" and an owner "<userid>"
	When A user with id "<userid>" validate the pitch
	Then The pitch is validated
	And No errors

Examples:
	| userid   | pitchTitle | pitchContent  | pitchDeadLineDate | pitchIssueDate | pitchClientId | pitchIdeaId |
	| testuser | Pitch Test | Pitch Content | 8 april 2023      | 9 april 2023   | client Id     | Idea Id     |

@pitch
Scenario: A user tries to cancel a pitch he doesn't own
	Given An existing pitch with title "<pitchTitle>", content "<pitchContent>", dead line date "<pitchDeadLineDate>", issue date "<pitchIssueDate>", client id "<pitchClientId>", idea id "<pitchIdeaId>" and an owner "<userid>"
	When A user with id "<otherUserid>" cancel the pitch
	Then An error with code "<errorCode>" is raised
	And The pitch is not deleted

Examples:
	| userid   | pitchTitle | pitchContent  | pitchDeadLineDate | pitchIssueDate | pitchClientId | pitchIdeaId | otherUserid | errorCode        |
	| testuser | Pitch Test | Pitch Content | 8 april 2023      | 9 april 2023   | client Id     | Idea Id     | testuser2   | NOT_PITCH_OWNER |

@pitch
Scenario: A user modify a pitch
	Given An existing pitch with title "<pitchTitle>", content "<pitchContent>", dead line date "<pitchDeadLineDate>", issue date "<pitchIssueDate>", client id "<pitchClientId>", idea id "<pitchIdeaId>" and an owner "<userid>"
	When A user with id "<userid>" modify the pitch title "<newPitchTitle>", summary "<newPitchSummary>", dead line date "<newPitchDeadLineDate>", issue date "<newPitchIssueDate>", client id "<newPitchClientId>", idea id "<newPitchIdeaId>"
	Then The pitch content change to title "<newPitchTitle>" and summary "<newPitchSummary>"
	And The pitch deadline date is rescheduled to "<newPitchDeadLineDate>"
	And The pitch issue date is rescheduled to "<newPitchIssueDate>"
	And The pitch client change to "<newPitchClientId>"
	And The pitch idea change to "<newPitchIdeaId>"
	And No errors

Examples:
	| userid   | pitchTitle | pitchContent  | pitchDeadLineDate | pitchIssueDate | pitchClientId | pitchIdeaId |newPitchTitle | newPitchSummary  | newPitchDeadLineDate | newPitchIssueDate | newPitchClientId | newPitchIdeaId |
	| testuser | Pitch Test | Pitch Content | 8 april 2023      | 9 april 2023   | client Id     | Idea Id     |Pitch Test Modified | Pitch Content Modified | 10 april 2023      | 11 april 2023   | client Id modified    | Idea Id modified     |