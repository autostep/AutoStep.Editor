$Option1
# comments above feature
Feature: My Feature
  Feature description
	
  @scenariotag
  $scenarioinstruction
  Scenario: Setup
    Scenario description

    Given I have logged in to my app as 'USER', password 'PWD' # comment
        And I have turned on the global system config flag

        # Time travel to June 2019 for Order Rule start date
        And the date/time is ':Tomorrow at 13:00'
  
    Given I have selected 'Client Management' -> 'Client Location' in the menu
    Then the 'Client Management - Client Location' page should be displayed

    When I press 'Add'
    Then the 'Client Management - Client Location - Add' page should be displayed

    # I've added bindings in code for these steps:
    Given I have entered 'My Name' into 'Name'
      And I have entered 'My Code' into 'Code'
      And I have selected 'A Type' in the 'Client Type' dropdown 

Step: When I press {button}

    # Click that button!