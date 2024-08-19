Feature: MarsEducationTests

This feature tests creates, edits & deletes the records of languages and skills in Mars project
 
	 Scenario Outline: Add the Education to the profile
    Given User logs in Mars portal and navigates to profile page
    When User adds the education details UniversityName, Country, Title, Degree, Year and clicks on Add button
    Then User should see the education details  Country, UniversityName, Title, Degree, Year added in the profile and then delete it
     

  Scenario: Adding an empty education
  Given User logs in Mars portal and navigates to profile page
  When User adds an empty education and clicks Add button
  Then User should see an error message 


	Scenario: Verify user is able to edit education record
	Given User logs in Mars portal and navigates to profile page
	When  User  edits one of the Education feature Year
	Then Verify edited education record is updated and delete it


	Scenario: Verify user is able to delete education record
	Given User logs in Mars portal and navigates to profile page
	When User deletes one of the existing education
	Then Verify education record is deleted

	
