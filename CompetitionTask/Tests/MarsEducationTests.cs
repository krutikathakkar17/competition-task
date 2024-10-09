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


namespace MarsEducation.Tests

{
  
    [TestFixture]
    public class MarsEducationTests
    {

        private IWebDriver _driver;
        private CommonDriver _commonDriver;
        private LoginPage _loginPageObj;
        private ProfilePage _profilePageObj;
        private EducationFeature _educationFeatureObj;
 
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

            _loginPageObj = new LoginPage(_driver);
            _profilePageObj = new ProfilePage(_driver);
            _educationFeatureObj = new EducationFeature(_driver);
           

            _loginPageObj.LoginActions("krits17.kt@gmail.com", "Singapore_24");
            _profilePageObj.VerifyLoggedinUser();

          } 


        [Test]
        public void AddEducationDetailsTest()
        {
             _educationFeatureObj.DeleteAllEducations();
            string AddEducationFilePath = ProjectPathHelper.projectPath + "\\JSONInputFiles\\EducationInputFiles\\AddEducation.json";

            jsonFileObj = new JsonDataReader(AddEducationFilePath);
            List<AddEducation> addEducations = new List<AddEducation>();

            addEducations = jsonFileObj.ReadAddEducationFile();

            foreach (var item in addEducations)
            {
                _educationFeatureObj.AddEducation(item.Country, item.UniversityName, item.Title, item.Degree, item.Year);
                Boolean found = _educationFeatureObj.VerifyEducationAdded(item.Country, item.UniversityName, item.Title, item.Degree, item.Year);


                Assert.IsTrue(found, $"Education from  '{item.Country}', '{item.UniversityName}' , '{item.Title}', '{item.Degree}' , '{item.Year}' not found.");

            }
        }



        [Test]
        public void AddEmptyEducation()
         {
            _educationFeatureObj.DeleteAllEducations();
            string AddEmptyEducationFilePath = ProjectPathHelper.projectPath + "\\JSONInputFiles\\EducationInputFiles\\AddEmptyEducation.json";

            jsonFileObj = new JsonDataReader(AddEmptyEducationFilePath);
            List<AddEmptyEducation> addEmptyEducations = new List<AddEmptyEducation>();

            addEmptyEducations = jsonFileObj.ReadAddEmptyEducationFile();
            string expectedMessage = "Please enter all the fields";

            foreach (var item in addEmptyEducations)
            {
                _educationFeatureObj.AddingEmptyEducation(item.Country);
                string popupText = _educationFeatureObj.VerifyNoEmptyEducationAdded();

                Assert.AreEqual(expectedMessage, popupText, $"Pop-up message mismatch. Expected: '{expectedMessage}', but got: '{popupText}'");
                Thread.Sleep(5000);
            }
         }


            [Test]
            public void EditEducation ()
            {
            _educationFeatureObj.DeleteAllEducations();
            string EditEducationFilePath = ProjectPathHelper.projectPath + "\\JSONInputFiles\\EducationInputFiles\\EditEducation.json";

            jsonFileObj = new JsonDataReader(EditEducationFilePath);
            List<EditEducation> editEducations = new List<EditEducation>();

            editEducations = jsonFileObj.ReadEditEducationFile();

            foreach (var item in editEducations)
             {
              _educationFeatureObj.EditEducation(item.Country, item.UniversityName, item.Title, item.Degree, item.Year, item.YearNew);
              string actualYear = _educationFeatureObj.VerifyEducationLeveleditted(item.Year, item.YearNew);

               Assert.AreEqual(item.YearNew, actualYear, $" Year of Graduation for was not updated correctly.");
               Thread.Sleep(5000);
             }
            }


            [Test]
            public void DeleteEducation()
            {
            _educationFeatureObj.DeleteAllEducations();

            string DeleteEducationFilePath = ProjectPathHelper.projectPath + "\\JSONInputFiles\\EducationInputFiles\\DeleteEducation.json";

            jsonFileObj = new JsonDataReader(DeleteEducationFilePath);
            List<DeleteEducation> deleteEducations = new List<DeleteEducation>();

            deleteEducations = jsonFileObj.ReadDeleteEducationFile();

            foreach (var item in deleteEducations)
             {
               _educationFeatureObj.DeleteEducation(item.Country, item.UniversityName, item.Title, item.Degree, item.Year);
                int count = _educationFeatureObj.VerifyEducationDeleted(item.DeletedTitle);
                Assert.IsTrue(count == 0, $"B.Sc '{item.DeletedTitle}' was not deleted successfully.");
                Thread.Sleep(8000);

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
