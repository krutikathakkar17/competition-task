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

namespace MarsCompetition.Tests
{
  
    [TestFixture]
    public class MarsCertificationTests
    {

        
        private IWebDriver _driver;
        private CommonDriver _commonDriver;
        private LoginPage _loginPageObj;
        private ProfilePage _profilePageObj;
      
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

            
            _loginPageObj = new LoginPage(_driver);
            _profilePageObj = new ProfilePage(_driver);
            _certificationsFeatureObj = new CertificationsFeature(_driver);

            _loginPageObj.LoginActions("krits17.kt@gmail.com", "Singapore_24");
            _profilePageObj.VerifyLoggedinUser();

          } 


        [Test]
            public void AddCertification()
            {
            _certificationsFeatureObj.DeleteAllCertifications();
            string AddCertificationFilePath = ProjectPathHelper.projectPath + "\\JSONInputFiles\\CertificationInputFiles\\AddCertifications.json";

 

             jsonFileObj = new JsonDataReader(AddCertificationFilePath);
             List<AddCertification> addCertifications = new List<AddCertification>();

             addCertifications = jsonFileObj.ReadCertificationFile();

             foreach (var item in addCertifications)
             { 
                _certificationsFeatureObj.AddCertifications(item.Certification, item.Certifiedfrom, item.year);
                Boolean found =  _certificationsFeatureObj.VerifyCertificationsAdded(item.Certification, item.Certifiedfrom, item.year);
                Assert.IsTrue(found, $"Certificate from '{item.Certification}', '{item.Certifiedfrom}', '{item.year}' not found.");

             }
            }



        [Test]
        public void DuplicateCertificate()
        {
          _certificationsFeatureObj.DeleteAllCertifications();

           string AddDuplicateCertificationFilePath = ProjectPathHelper.projectPath + "\\JSONInputFiles\\CertificationInputFiles\\AddDuplicateCertification.json";

           jsonFileObj = new JsonDataReader(AddDuplicateCertificationFilePath);
           List<AddDuplicateCertification> addDuplCertifications = new List<AddDuplicateCertification>();

           addDuplCertifications = jsonFileObj.ReadDuplicateCertification();
           string expectedMessage = "This information is already exist.";

         foreach (var item in addDuplCertifications)
         { 

           _certificationsFeatureObj.AddDuplicateCertification(item.CertificationName, item.CertifiedFrom, item.Year, item.CertificationNameNew, item.CertifiedFromNew, item.YearNew);
           string popupText =  _certificationsFeatureObj.VerifyNoDuplicateCertificationAdded();
           Assert.AreEqual(expectedMessage, popupText, $"Pop-up message mismatch. Expected: '{expectedMessage}', but got: '{popupText}'");
           Thread.Sleep(5000);
         }

        }

       
            [Test]
            public void EditCertification ()
            {
             _certificationsFeatureObj.DeleteAllCertifications();

             string EditCerificationFilePath = ProjectPathHelper.projectPath + "\\JSONInputFiles\\CertificationInputFiles\\EditCertification.json";

             jsonFileObj = new JsonDataReader(EditCerificationFilePath);
             List<EditCertification> addDuplCertifications = new List<EditCertification>();

             addDuplCertifications = jsonFileObj.ReadUpdateCeritificationFile();

            foreach (var item in addDuplCertifications)
             {

                _certificationsFeatureObj.EditCertifications(item.Certification, item.CertifiedFrom, item.Year, item.CertificationNew);
               string actualCertifiedfromText = _certificationsFeatureObj.VerifyCertificateEdited(item.Certification, item.CertificationNew);
                Assert.AreEqual(item.CertificationNew, actualCertifiedfromText, $"Certificate value mismatch after editing. Expected: '{item.CertificationNew}', but got: '{actualCertifiedfromText}'");
            }

            }



        [Test]
            public void DeleteCertification()
            {
              _certificationsFeatureObj.DeleteAllCertifications();

              string DeleteCerificationFilePath = ProjectPathHelper.projectPath + "\\JSONInputFiles\\CertificationInputFiles\\DeleteCertification.json";

              jsonFileObj = new JsonDataReader(DeleteCerificationFilePath);
              List<DeleteCertification> addDuplCertifications = new List<DeleteCertification>();

              addDuplCertifications = jsonFileObj.ReadDeleteCertificationFile();

            foreach (var item in addDuplCertifications)
            {

                _certificationsFeatureObj.DeleteCertificate(item.Certification, item.CertifiedFrom, item.Year);
                int count = _certificationsFeatureObj.VerifyCertificateDeleted(item.DeletedCertiifcation);
                Assert.IsTrue(count == 0, $"Skill '{item.Certification}' was not deleted successfully.");
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

     

