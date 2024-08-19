using MarsEducationCertificationsSpecflow.Utilities;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using TechTalk.SpecFlow;
using AventStack.ExtentReports.Gherkin.Model;
using BoDi;

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using MarsEduCertAutomation.Utility;

namespace MarsEducationCertificationsSpecflow.Hooks
{

  


    [Binding]
    public sealed class Hooks :ExtentReport
        {
     
        private readonly IObjectContainer _container;
        private readonly CommonDriver _commonDriver;
        public Hooks(IObjectContainer container, CommonDriver commonDriver)
        
          {
              _container = container;
            _commonDriver = commonDriver;
        }

        [BeforeTestRun]
            public static void BeforeTestRun()
            {
                Console.WriteLine("Running before test run...");
                ExtentReportInit();
            }

            [AfterTestRun]
            public static void AfterTestRun()
            {
                Console.WriteLine("Running after test run...");
                ExtentReportTearDown();
            }


            [BeforeFeature]
            public static void BeforeFeature(FeatureContext featureContext)
            {
                Console.WriteLine("Running before feature...");
                _feature = _extentReports.CreateTest<Feature>(featureContext.FeatureInfo.Title);
            }
       


        [AfterFeature]
            public static void AfterFeature()
            {
                Console.WriteLine("Running after feature...");
            }

            [BeforeScenario("@Testers")]
            public void BeforeScenarioWithTag()
            {
                Console.WriteLine("Running inside tagged hooks in specflow");
            }


            [BeforeScenario(Order = 1)]
            public void FirstBeforeScenario(ScenarioContext scenarioContext)
            {
                Console.WriteLine("Running before scenario...");
                _commonDriver.Initialize();
            _container.RegisterInstanceAs<IWebDriver>(_commonDriver.driver);

            _scenario = _feature.CreateNode<Scenario>(scenarioContext.ScenarioInfo.Title);
            }

        [AfterScenario]
        public void AfterScenario()
        {
            Console.WriteLine("Running after scenario...");
            var driver = _container.Resolve<IWebDriver>();

            if (driver != null)
            {
                driver.Quit();
            }
        }

        [AfterStep]
        public void AfterStep(ScenarioContext scenarioContext)
        {
            Console.WriteLine("Running after step....");
            string stepType = scenarioContext.StepContext.StepInfo.StepDefinitionType.ToString();
            string stepName = scenarioContext.StepContext.StepInfo.Text;


            var driver = _container.Resolve<IWebDriver>();
            string screenshotPath = addScreenshot(driver, scenarioContext);

            //When scenario passed
            if (scenarioContext.TestError == null)
            {
                if (stepType == "Given")
                {
                     _scenario.CreateNode<Given>(stepName).Pass("Step passed",
                      MediaEntityBuilder.CreateScreenCaptureFromPath(screenshotPath).Build());
                   

                }
                else if (stepType == "When")
                {
                       _scenario.CreateNode<When>(stepName).Pass("Step passed",
                       MediaEntityBuilder.CreateScreenCaptureFromPath(screenshotPath).Build());
                   
                }
                else if (stepType == "Then")
                {
                     _scenario.CreateNode<Then>(stepName).Pass("Step passed",
                      MediaEntityBuilder.CreateScreenCaptureFromPath(screenshotPath).Build());
                  
                }
                else if (stepType == "And")
                {
                     _scenario.CreateNode<And>(stepName).Pass("Step passed",
                      MediaEntityBuilder.CreateScreenCaptureFromPath(screenshotPath).Build());
                   
                }
            }
        

                    //When scenario fails
                    if (scenarioContext.TestError != null)
                    {

           
                        if (stepType == "Given")
                        {
                            _scenario.CreateNode<Given>(stepName).Fail(scenarioContext.TestError.Message,
                                MediaEntityBuilder.CreateScreenCaptureFromPath(addScreenshot(driver, scenarioContext)).Build());
                        }
                        else if (stepType == "When")
                        {
                            _scenario.CreateNode<When>(stepName).Fail(scenarioContext.TestError.Message,
                                MediaEntityBuilder.CreateScreenCaptureFromPath(addScreenshot(driver, scenarioContext)).Build());
                        }
                        else if (stepType == "Then")
                        {
                            _scenario.CreateNode<Then>(stepName).Fail(scenarioContext.TestError.Message,
                                MediaEntityBuilder.CreateScreenCaptureFromPath(addScreenshot(driver, scenarioContext)).Build());
                        }
                        else if (stepType == "And")
                        {
                            _scenario.CreateNode<And>(stepName).Fail(scenarioContext.TestError.Message,
                                MediaEntityBuilder.CreateScreenCaptureFromPath(addScreenshot(driver, scenarioContext)).Build());
                        }
                    } 
                }

            }
        }


    






