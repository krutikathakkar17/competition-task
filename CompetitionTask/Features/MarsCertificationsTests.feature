Feature: MarsCertificationsTests

A short summary of the feature

@tag1
    Scenario Outline: Add the Certifications to the profile
	Given User logs in Mars portal and navigates to profile page
	When  User adds the certification details Certificate, CertifiedFrom, Year and clicks on Add button
	Then User should see the certification details  Certificate, CertifiedFrom, Year added in the profile and delete it
      

    Scenario: Adding a duplicate certification
     Given User logs in Mars portal and navigates to profile page
     When  User adds a duplicate certification and clicks Add button
     Then  User should see an error message for duplicate certification

	Scenario: Verify user is able to edit Certification record
	Given User logs in Mars portal and navigates to profile page
	When User edits one of the existing Certification feature Certificate
	Then Verify edited certification record is updated and delete it
      

	Scenario: Verify user is able to delete certification record
	Given User logs in Mars portal and navigates to profile page
	When User deletes one of the existing Certificates
	Then Verify certificate is deleted

	
	