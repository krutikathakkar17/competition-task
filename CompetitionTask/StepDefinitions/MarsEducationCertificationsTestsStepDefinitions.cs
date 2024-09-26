using Mars_Education_Certifications.Pages;
using MarsEducationCertificationsSpecflow.Pages;
using MarsEducationCertificationsSpecflow.Utilities;
using OpenQA.Selenium;
using AventStack.ExtentReports.Gherkin.Model;
using MarsEducationCertificationsSpecflow.JSONDataObject;
using MarsEducationCertificationsTests.JSONDataObject;
using System.Diagnostics.Metrics;
using MarsEduCertAutomation.Utilities;
using NUnit.Framework;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using System.Collections.Generic;
using MarsEduCertAutomation.Utility;
using NUnit.Framework.Interfaces;


namespace MarsEducationCertificationsSpecflow.StepDefinitions
{
  
    [TestFixture]
    public class MarsEducationCertificationsTestsStepDefinitions
    {

        // private IWebDriver _commonDriver;
        private IWebDriver _driver;
        private CommonDriver _commonDriver;
        private LoginPage _loginPageObj;
        private ProfilePage _profilePageObj;
        private EducationFeature _educationFeatureObj;
        private CertificationsFeature _certificationsFeatureObj;
        JsonDataReader jsonFileObj;

        private ExtentReports _extentReports;
        private ExtentTest _test;

        [OneTimeSetUp]
        public void SetupExtentReports()
        {
            // Initialize the Extent Report
            ExtentReport.ExtentReportInit();
            _extentReports = ExtentReport._extentReports;
        }



        [SetUp]
          public void Setup()

          {

            _test = _extentReports.CreateTest(TestContext.CurrentContext.Test.Name);

            _commonDriver = new CommonDriver();
            _commonDriver.Initialize();
            _driver = _commonDriver.driver;

            //_commonDriver = new CommonDriver().driver;
            _loginPageObj = new LoginPage(_driver);
            _profilePageObj = new ProfilePage(_driver);
            _educationFeatureObj = new EducationFeature(_driver);
            _certificationsFeatureObj = new CertificationsFeature(_driver);

            _loginPageObj.LoginActions("krits17.kt@gmail.com", "Singapore_24");
              _profilePageObj.VerifyLoggedinUser();

          } 


        [Test]
        public void AddEducationDetailsTest()
        {
            string AddEducationFilePath = ProjectPathHelper.projectPath + "\\JSONInputFiles\\EducationInputFiles\\AddEducation.json";

            jsonFileObj = new JsonDataReader(AddEducationFilePath);
            List<AddEducation> addEducations = new List<AddEducation>();

            addEducations = jsonFileObj.ReadAddEducationFile();

            foreach (var item in addEducations)
            {
                _educationFeatureObj.AddEducation(item.Country, item.UniversityName, item.Title, item.Degree, item.Year);
                _educationFeatureObj.VerifyEducationAdded(item.Country, item.UniversityName, item.Title, item.Degree, item.Year);
            }
        }



        [Test]
        public void AddEmptyEducation()
         {
            
            string AddEmptyEducationFilePath = ProjectPathHelper.projectPath + "\\JSONInputFiles\\EducationInputFiles\\AddEmptyEducation.json";

            jsonFileObj = new JsonDataReader(AddEmptyEducationFilePath);
            List<AddEmptyEducation> addEmptyEducations = new List<AddEmptyEducation>();

            addEmptyEducations = jsonFileObj.ReadAddEmptyEducationFile();

            foreach (var item in addEmptyEducations)
            {
                _educationFeatureObj.AddingEmptyEducation(item.Country);
                _educationFeatureObj.VerifyNoEmptyEducationAdded();
            }
         }


            [Test]
            public void EditEducation ()
        {
            string EditEducationFilePath = ProjectPathHelper.projectPath + "\\JSONInputFiles\\EducationInputFiles\\EditEducation.json";

            jsonFileObj = new JsonDataReader(EditEducationFilePath);
            List<EditEducation> editEducations = new List<EditEducation>();

            editEducations = jsonFileObj.ReadEditEducationFile();

            foreach (var item in editEducations)
            {
              _educationFeatureObj.EditEducation(item.Country, item.UniversityName, item.Title, item.Degree, item.Year, item.YearNew);
              _educationFeatureObj.VerifyEducationLeveleditted(item.Year, item.YearNew);

            }
        }


            [Test]
            public void DeleteEducation()
         {
            
            string DeleteEducationFilePath = ProjectPathHelper.projectPath + "\\JSONInputFiles\\EducationInputFiles\\DeleteEducation.json";

            jsonFileObj = new JsonDataReader(DeleteEducationFilePath);
            List<DeleteEducation> deleteEducations = new List<DeleteEducation>();

            deleteEducations = jsonFileObj.ReadDeleteEducationFile();

            foreach (var item in deleteEducations)
            {
               _educationFeatureObj.DeleteEducation(item.Country, item.UniversityName, item.Title, item.Degree, item.Year);
               _educationFeatureObj.VerifyEducationDeleted(item.DeletedTitle);

            }
         }

        [Test]
            public void AddCertification()
        {
            
            string AddCertificationFilePath = ProjectPathHelper.projectPath + "\\JSONInputFiles\\CertificationInputFiles\\AddCertifications.json";

 

             jsonFileObj = new JsonDataReader(AddCertificationFilePath);
            List<AddCertification> addCertifications = new List<AddCertification>();

            addCertifications = jsonFileObj.ReadCertificationFile();

            foreach (var item in addCertifications)
            { 
                _certificationsFeatureObj.AddCertifications(item.Certification, item.Certifiedfrom, item.year);
                _certificationsFeatureObj.VerifyCertificationsAdded(item.Certification, item.Certifiedfrom, item.year);

            }
        }



        [Test]
        public void DuplicateCertificate()
          {
            
            string AddDuplicateCertificationFilePath = ProjectPathHelper.projectPath + "\\JSONInputFiles\\CertificationInputFiles\\AddDuplicateCertification.json";

            jsonFileObj = new JsonDataReader(AddDuplicateCertificationFilePath);
            List<AddDuplicateCertification> addDuplCertifications = new List<AddDuplicateCertification>();

            addDuplCertifications = jsonFileObj.ReadDuplicateCertification();

            foreach (var item in addDuplCertifications)
            { 

                _certificationsFeatureObj.AddDuplicateCertification(item.CertificationName, item.CertifiedFrom, item.Year, item.CertificationNameNew, item.CertifiedFromNew, item.YearNew);
                _certificationsFeatureObj.VerifyNoDuplicateCertificationAdded();
            }
        }

       

            [Test]
            public void EditCertification ()
        {
           
            string EditCerificationFilePath = ProjectPathHelper.projectPath + "\\JSONInputFiles\\CertificationInputFiles\\EditCertification.json";

            jsonFileObj = new JsonDataReader(EditCerificationFilePath);
            List<EditCertification> addDuplCertifications = new List<EditCertification>();

            addDuplCertifications = jsonFileObj.ReadUpdateCeritificationFile();

            foreach (var item in addDuplCertifications)
            {

                _certificationsFeatureObj.EditCertifications(item.Certification, item.CertifiedFrom, item.Year, item.CertificationNew);
                _certificationsFeatureObj.VerifyCertificateEdited(item.Certification, item.CertificationNew);
            }
        }





        [Test]
            public void DeleteCertification()
         {
          
           string DeleteCerificationFilePath = ProjectPathHelper.projectPath + "\\JSONInputFiles\\CertificationInputFiles\\DeleteCertification.json";

            jsonFileObj = new JsonDataReader(DeleteCerificationFilePath);
            List<DeleteCertification> addDuplCertifications = new List<DeleteCertification>();

            addDuplCertifications = jsonFileObj.ReadDeleteCertificationFile();

            foreach (var item in addDuplCertifications)
            {

                _certificationsFeatureObj.DeleteCertificate(item.Certification, item.CertifiedFrom, item.Year);
                _certificationsFeatureObj.VerifyCertificateDeleted(item.DeletedCertiifcation);
            }
         }


        
          [TearDown]
        public void CleanUp()
        {
            // Take screenshot for every test
            string screenshotPath = ExtentReport.addScreenshot(_driver, TestContext.CurrentContext);
            _test.AddScreenCaptureFromPath(screenshotPath);

            var status = TestContext.CurrentContext.Result.Outcome.Status;

            switch (status)
            {
                case TestStatus.Failed:
                    _test.Log(Status.Fail, "Test failed: " + TestContext.CurrentContext.Result.Message);
                    break;
                case TestStatus.Passed:
                    _test.Log(Status.Pass, "Test passed");
                    break;
                case TestStatus.Skipped:
                    _test.Log(Status.Skip, "Test skipped");
                    break;
            }

            _driver.Quit();
        }

        [OneTimeTearDown]
        public void TearDownExtentReports()
        {
            // Write the report to the file
            ExtentReport.ExtentReportTearDown();
        }

         

    }
}
