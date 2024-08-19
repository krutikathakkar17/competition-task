using Mars_Education_Certifications.Pages;
using MarsEducationCertificationsSpecflow.Pages;
using MarsEducationCertificationsSpecflow.Utilities;
using OpenQA.Selenium;
using AventStack.ExtentReports.Gherkin.Model;
using MarsEducationCertificationsSpecflow.JSONDataObject;
using MarsEducationCertificationsTests.JSONDataObject;
using System.Diagnostics.Metrics;
using MarsEduCertAutomation.Utilities;

namespace MarsEducationCertificationsSpecflow.StepDefinitions
{
    [Binding]
    public class MarsEducationCertificationsTestsStepDefinitions
    {

        private readonly IWebDriver _commonDriver;
        private readonly LoginPage _loginPageObj;
        private readonly ProfilePage _profilePageObj;
        private readonly EducationFeature _educationFeatureObj;
        private readonly CertificationsFeature _certificationsFeatureObj;
        JsonDataReader jsonFileObj;
        


        public MarsEducationCertificationsTestsStepDefinitions(CommonDriver commonDriver)
        {
            _commonDriver = commonDriver.driver;
            _loginPageObj = new LoginPage(_commonDriver);
            _profilePageObj = new ProfilePage(_commonDriver);
            _educationFeatureObj = new EducationFeature(_commonDriver);
            _certificationsFeatureObj = new CertificationsFeature(_commonDriver);
         
        }

                                  

        [Given(@"User logs in Mars portal and navigates to profile page")]
        public void GivenUserLogsInMarsPortalAndNavigatesToProfilePage()
        {
            _loginPageObj.LoginActions("krits17.kt@gmail.com", "Singapore_24");
            _profilePageObj.VerifyLoggedinUser();
        }

        [When(@"User adds the education details UniversityName, Country, Title, Degree, Year and clicks on Add button")]
        public void WhenUserAddsTheEducationDetailsUniversityNameCountryTitleDegreeYearAndClicksOnAddButton()
        {
            string AddEducationFilePath = ProjectPathHelper.projectPath + "\\JSONInputFiles\\EducationInputFiles\\AddEducation.json";

            jsonFileObj = new JsonDataReader(AddEducationFilePath);
            List<AddEducation> addEducations = new List<AddEducation>();

            addEducations = jsonFileObj.ReadAddEducationFile();

            foreach (var item in addEducations)
            {
                _educationFeatureObj.AddEducation(item.UniversityName, item.Country, item.Title, item.Degree, item.Year);
            }
        }



        [Then(@"User should see the education details  Country, UniversityName, Title, Degree, Year added in the profile and then delete it")]
        public void ThenUserShouldSeeTheEducationDetailsCountryUniversityNameTitleDegreeYearAddedInTheProfileAndThenDeleteIt()
        {
     
            string AddEducationFilePath = ProjectPathHelper.projectPath + "\\JSONInputFiles\\EducationInputFiles\\AddEducation.json";

            jsonFileObj = new JsonDataReader(AddEducationFilePath);
            List<AddEducation> addEducations = new List<AddEducation>();

            addEducations = jsonFileObj.ReadAddEducationFile();
            foreach (var item in addEducations)
            {
                _educationFeatureObj.VerifyEducationAdded(item.Country, item.UniversityName, item.Title, item.Degree, item.Year);
            }
        }


        [When(@"User adds an empty education and clicks Add button")]
         public void WhenUserAddsAnEmptyEducationAndClicksAddButton()
         {
            
            string AddEmptyEducationFilePath = ProjectPathHelper.projectPath + "\\JSONInputFiles\\EducationInputFiles\\AddEmptyEducation.json";

            jsonFileObj = new JsonDataReader(AddEmptyEducationFilePath);
            List<AddEmptyEducation> addEmptyEducations = new List<AddEmptyEducation>();

            addEmptyEducations = jsonFileObj.ReadAddEmptyEducationFile();

            foreach (var item in addEmptyEducations)
            {
                _educationFeatureObj.AddingEmptyEducation(item.Country);
            }
         }


         [Then(@"User should see an error message")]
         public void ThenUserShouldSeeAnErrorMessage()
         {
            string AddEmptyEducationFilePath = ProjectPathHelper.projectPath + "\\JSONInputFiles\\EducationInputFiles\\AddEmptyEducation.json";

            jsonFileObj = new JsonDataReader(AddEmptyEducationFilePath);
            List<AddEmptyEducation> addEmptyEducations = new List<AddEmptyEducation>();

            addEmptyEducations = jsonFileObj.ReadAddEmptyEducationFile();

            foreach (var item in addEmptyEducations)
            {
                _educationFeatureObj.VerifyNoEmptyEducationAdded();
            }

         }


        [When(@"User  edits one of the Education feature Year")]
        public void WhenUserEditsOneOfTheEducationFeatureYear()
        {
            string EditEducationFilePath = ProjectPathHelper.projectPath + "\\JSONInputFiles\\EducationInputFiles\\EditEducation.json";

            jsonFileObj = new JsonDataReader(EditEducationFilePath);
            List<EditEducation> editEducations = new List<EditEducation>();

            editEducations = jsonFileObj.ReadEditEducationFile();

            foreach (var item in editEducations)
            {
              _educationFeatureObj.EditEducation(item.UniversityName, item.Country, item.Title, item.Degree, item.Year, item.YearNew);

            }
        }


        [Then(@"Verify edited education record is updated and delete it")]
          public void ThenVerifyEditedEducationRecordIsUpdatedAndDeleteIt()
          {
        
            string EditEducationFilePath = ProjectPathHelper.projectPath + "\\JSONInputFiles\\EducationInputFiles\\EditEducation.json";
           
            jsonFileObj = new JsonDataReader(EditEducationFilePath);
            List<EditEducation> editEducations = new List<EditEducation>();

            editEducations = jsonFileObj.ReadEditEducationFile();

            foreach (var item in editEducations)
            {

                _educationFeatureObj.VerifyEducationLeveleditted(item.Year, item.YearNew);

            }
          }


        [When(@"User deletes one of the existing education")]
         public void WhenUserDeletesOneOfTheExistingEducation()
         {
            
            string DeleteEducationFilePath = ProjectPathHelper.projectPath + "\\JSONInputFiles\\EducationInputFiles\\DeleteEducation.json";

            jsonFileObj = new JsonDataReader(DeleteEducationFilePath);
            List<DeleteEducation> deleteEducations = new List<DeleteEducation>();

            deleteEducations = jsonFileObj.ReadDeleteEducationFile();

            foreach (var item in deleteEducations)
            {
               _educationFeatureObj.DeleteEducation(item.UniversityName, item.Country, item.Title, item.Degree, item.Year);

            }
         }

         [Then(@"Verify education record is deleted")]
         public void Verifylanguagerecordisdeleted()
         {
         
            string DeleteEducationFilePath = ProjectPathHelper.projectPath + "\\JSONInputFiles\\EducationInputFiles\\DeleteEducation.json";

            jsonFileObj = new JsonDataReader(DeleteEducationFilePath);
            List<DeleteEducation> deleteEducations = new List<DeleteEducation>();

            deleteEducations = jsonFileObj.ReadDeleteEducationFile();

            foreach (var item in deleteEducations)
            {

                _educationFeatureObj.VerifyEducationDeleted(item.DeletedTitle);

            }
         }


        [When(@"User adds the certification details Certificate, CertifiedFrom, Year and clicks on Add button")]
        public void WhenUserAddsTheCertificationDetailsCertificateCertifiedFromYearAndClicksOnAddButton()
        {
            
            string AddCertificationFilePath = ProjectPathHelper.projectPath + "\\JSONInputFiles\\CertificationInputFiles\\AddCertifications.json";

 

        jsonFileObj = new JsonDataReader(AddCertificationFilePath);
            List<AddCertification> addCertifications = new List<AddCertification>();

            addCertifications = jsonFileObj.ReadCertificationFile();

            foreach (var item in addCertifications)
            { 
                _certificationsFeatureObj.AddCertifications(item.Certification, item.Certifiedfrom, item.year);

            }
        }


        [Then(@"User should see the certification details  Certificate, CertifiedFrom, Year added in the profile and delete it")]
        public void ThenUserShouldSeeTheCertificationDetailsCertificateCertifiedFromYearAddedInTheProfileAndDeleteIt()
        {
            
            string AddCertificationFilePath = ProjectPathHelper.projectPath + "\\JSONInputFiles\\CertificationInputFiles\\AddCertifications.json";

            jsonFileObj = new JsonDataReader(AddCertificationFilePath);
            List<AddCertification> addCertifications = new List<AddCertification>();

            addCertifications = jsonFileObj.ReadCertificationFile();

            foreach (var item in addCertifications)
            { 
                _certificationsFeatureObj.VerifyCertificationsAdded(item.Certification, item.Certifiedfrom, item.year);
            }
        }



        [When(@"User adds a duplicate certification and clicks Add button")]
          public void WhenUserAddsADuplicateCertificationAndClicksAddButton()
          {
            
            string AddDuplicateCertificationFilePath = ProjectPathHelper.projectPath + "\\JSONInputFiles\\CertificationInputFiles\\AddDuplicateCertification.json";

            jsonFileObj = new JsonDataReader(AddDuplicateCertificationFilePath);
            List<AddDuplicateCertification> addDuplCertifications = new List<AddDuplicateCertification>();

            addDuplCertifications = jsonFileObj.ReadDuplicateCertification();

            foreach (var item in addDuplCertifications)
            { 

                _certificationsFeatureObj.AddDuplicateCertification(item.CertificationName, item.CertifiedFrom, item.Year, item.CertificationNameNew, item.CertifiedFromNew, item.YearNew);

            }
        }

          [Then(@"User should see an error message for duplicate certification")]
          public void ThenUserShouldSeeAnErrorMessageForDuplicateCertification()
          {
            
            string AddDuplicateCertificationFilePath = ProjectPathHelper.projectPath + "\\JSONInputFiles\\CertificationInputFiles\\AddDuplicateCertification.json";

            jsonFileObj = new JsonDataReader(AddDuplicateCertificationFilePath);
            List<AddDuplicateCertification> addDuplCertifications = new List<AddDuplicateCertification>();

            addDuplCertifications = jsonFileObj.ReadDuplicateCertification();

            foreach (var item in addDuplCertifications)
            {

                _certificationsFeatureObj.VerifyNoDuplicateCertificationAdded();

            }

          }


        [When(@"User edits one of the existing Certification feature Certificate")]
        public void WhenUserEditsOneOfTheExistingCertificationFeatureCertificate()
        {
           
            string EditCerificationFilePath = ProjectPathHelper.projectPath + "\\JSONInputFiles\\CertificationInputFiles\\EditCertification.json";

            jsonFileObj = new JsonDataReader(EditCerificationFilePath);
            List<EditCertification> addDuplCertifications = new List<EditCertification>();

            addDuplCertifications = jsonFileObj.ReadUpdateCeritificationFile();

            foreach (var item in addDuplCertifications)
            {

                _certificationsFeatureObj.EditCertifications(item.Certification, item.CertifiedFrom, item.Year, item.CertificationNew);

            }
        }



        [Then(@"Verify edited certification record is updated and delete it")]
        public void ThenVerifyEditedCertificationRecordIsUpdatedAndDelteIt()
        {
           
            string EditCerificationFilePath = ProjectPathHelper.projectPath + "\\JSONInputFiles\\CertificationInputFiles\\EditCertification.json";

            jsonFileObj = new JsonDataReader(EditCerificationFilePath);
            List<EditCertification> addDuplCertifications = new List<EditCertification>();

            addDuplCertifications = jsonFileObj.ReadUpdateCeritificationFile();

            foreach (var item in addDuplCertifications)
            {

                _certificationsFeatureObj.VerifyCertificateEdited(item.Certification, item.CertificationNew);

            }
        }



        [When(@"User deletes one of the existing Certificates")]
         public void WhenUserDeletesOneOfTheExistingCertificates()
         {
          
           string DeleteCerificationFilePath = ProjectPathHelper.projectPath + "\\JSONInputFiles\\CertificationInputFiles\\DeleteCertification.json";

            jsonFileObj = new JsonDataReader(DeleteCerificationFilePath);
            List<DeleteCertification> addDuplCertifications = new List<DeleteCertification>();

            addDuplCertifications = jsonFileObj.ReadDeleteCertificationFile();

            foreach (var item in addDuplCertifications)
            {

                _certificationsFeatureObj.DeleteCertificate(item.Certification, item.CertifiedFrom, item.Year);

            }
         }

         [Then(@"Verify certificate is deleted")]
         public void thenVerifyCertificateisDeleted()
         {
            
            string DeleteCerificationFilePath = ProjectPathHelper.projectPath + "\\JSONInputFiles\\CertificationInputFiles\\DeleteCertification.json";

            jsonFileObj = new JsonDataReader(DeleteCerificationFilePath);
            List<DeleteCertification> addDuplCertifications = new List<DeleteCertification>();

            addDuplCertifications = jsonFileObj.ReadDeleteCertificationFile();

            foreach (var item in addDuplCertifications)
            {

                _certificationsFeatureObj.VerifyCertificateDeleted(item.DeletedCertiifcation);

            }
         }

         
    }
}
