using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using NUnit.Framework;
using System.Diagnostics.Metrics;
namespace Mars_Education_Certifications.Pages
{

    public class EducationFeature
    {
        private IWebDriver _driver;

        public EducationFeature(IWebDriver driver)
        {
            _driver = driver;
        }

        private readonly By educationButtonLocator = By.XPath("//a[contains(.,'Education')][@data-tab=\"third\"]");
        IWebElement educationButton;


        private readonly By addNeweduButtonLocator = By.XPath("//div[@class='ui bottom attached tab segment tooltip-target active']//div[contains(@class,'ui teal button')][normalize-space()='Add New']");
        IWebElement addNeweduButton;

       
        private readonly By inputInstituteLocator = By.XPath("//input[@placeholder='College/University Name'][@name='instituteName']");
        IWebElement inputInstElement;


        private readonly By inputDegreeLocator = By.XPath("//input[@placeholder='Degree']");
         IWebElement inputDegElement;

       
        private readonly By addEduLocator = By.XPath("//input[@value='Add']");
        IWebElement addEdu;

        private readonly By EditbtnLocator = By.XPath("//tbody/tr/td[@class='right aligned']/span[@class='button']/i[contains(@class, 'outline write icon')]");
        IWebElement editButton;
        

        private readonly By DeletebtnLocator = By.XPath("//tbody/tr/td[@class='right aligned']/span[@class='button']/i[contains(@class, 'remove icon')]");
        IWebElement DeleteButton;


                

        public void AddEducation(String country, String universityName, String title, String degree, String year)
        {
            Console.WriteLine(universityName, country, title, degree, year);

            educationButton = _driver.FindElement(educationButtonLocator);
            educationButton.Click();

            Thread.Sleep(8000);

            addNeweduButton = _driver.FindElement(addNeweduButtonLocator);
            addNeweduButton.Click();

            Thread.Sleep(9000);


            
            inputInstElement = _driver.FindElement(inputInstituteLocator);
            inputInstElement.SendKeys(universityName);

            Thread.Sleep(7000);


            SelectElement countryLevelDropdown = new SelectElement(_driver.FindElement(By.XPath("//select[@name='country']")));
            countryLevelDropdown.SelectByText(country);

            Thread.Sleep(12000);

            SelectElement titleLevelDropdown = new SelectElement(_driver.FindElement(By.XPath("//select[@name='title']")));
              titleLevelDropdown.SelectByText(title);

           
            Thread.Sleep(9000);


            inputDegElement = _driver.FindElement(inputDegreeLocator);
             inputDegElement.SendKeys(degree);

            Thread.Sleep(7000);

            // Year of graduation (select from dropdown list)

            SelectElement yearLevelDropdown = new SelectElement(_driver.FindElement(By.XPath("//select[@name='yearOfGraduation']")));
            yearLevelDropdown.SelectByText(year);


            Thread.Sleep(12000);


           // Thread.Sleep(3000);

            // Click the "Add" button
            addEdu = _driver.FindElement(addEduLocator);
            addEdu.Click();
            Thread.Sleep(8000);
        }

         public void VerifyEducationAdded (string expectedCountry, string expectedUniversityName, string expectedTitle, string expectedDegree, string expectedYear)
         {
         
            Thread.Sleep(10000);

             // XPath to locate all rows in the language table
             string rowXPath = "//table[@class='ui fixed table']/tbody/tr";
             var rows = _driver.FindElements(By.XPath(rowXPath));


             bool found = false;

             foreach (var row in rows)

          {
                  var countryElement = row.FindElement(By.XPath("./td[1]"));
                  var universityElement = row.FindElement(By.XPath("./td[2]"));
                  var titleElement = row.FindElement(By.XPath("./td[3]"));
                  var degreeElement = row.FindElement(By.XPath("./td[4]"));
                  var yearElement = row.FindElement(By.XPath("./td[5]"));  

                

                if (  countryElement.Text == expectedCountry && universityElement.Text == expectedUniversityName  && titleElement.Text == expectedTitle && degreeElement.Text == expectedDegree && yearElement.Text == expectedYear)

              {

                  found = true;
                     break;
                 }
             }

             Assert.IsTrue(found, $"Education from  '{expectedCountry}', '{expectedUniversityName}' , '{expectedTitle}', '{expectedDegree}' , '{expectedYear}' not found.");

              DeleteButton = _driver.FindElement(DeletebtnLocator);
              DeleteButton.Click();
        }




        public void AddingEmptyEducation(String country)
           {
                educationButton = _driver.FindElement(educationButtonLocator);
                educationButton.Click();

                Thread.Sleep(8000);
  
              addNeweduButton = _driver.FindElement(addNeweduButtonLocator);
              addNeweduButton.Click();

            SelectElement countryLevelDropdown = new SelectElement(_driver.FindElement(By.XPath("//select[@name='country']")));
            countryLevelDropdown.SelectByText(country);

  
            addEdu = _driver.FindElement(addEduLocator);
            addEdu.Click();

           }

           public void VerifyNoEmptyEducationAdded()
           {
               string popupXPath = "//div[@class='ns-box-inner']";
               string expectedMessage = "Please enter all the fields";
           
               Thread.Sleep(1000);

               var popupElement = _driver.FindElement(By.XPath(popupXPath));
               string popupText = popupElement.Text;

               Assert.AreEqual(expectedMessage, popupText, $"Pop-up message mismatch. Expected: '{expectedMessage}', but got: '{popupText}'");
               Thread.Sleep(5000);
           }

           private readonly By addUpdatedLocator = By.XPath("//input[@class='ui teal button'][@value='Update']");
        
           IWebElement addUpdated;
           private object language;
           private double expectedLevel;


        
           public void EditEducation(String country, String universityName, String title, String degree, String year, String YearNew)
           {

              AddEducation(country,universityName, title, degree, year);

            Thread.Sleep(12000);

              editButton = _driver.FindElement(EditbtnLocator);
              editButton.Click();

               SelectElement yearDropdown = new SelectElement(_driver.FindElement(By.XPath("//select[@name='yearOfGraduation']")));
               yearDropdown.SelectByText(YearNew);

            // Optionally, save the changes by clicking the update button

             addUpdated = _driver.FindElement(addUpdatedLocator);
             addUpdated.Click();
              Thread.Sleep(3000);
        
           }


         public void VerifyEducationLeveleditted(String year, String expectedyear)
           {
            Thread.Sleep(7000);

            // Create the XPath to find the eduction row
            string xpath = $"//tbody/tr[td[text()='{expectedyear}']]/td[5]";
         
                       
            IWebElement levelElement = _driver.FindElement(By.XPath(xpath));

            Thread.Sleep(5000);

               // Get the actual level text
               string actualyear = levelElement.Text;

            Thread.Sleep(5000);

            // Verify the level
            Assert.AreEqual(expectedyear, actualyear, $" Year of Graduation for was not updated correctly.");

            Thread.Sleep(5000);

            DeleteButton = _driver.FindElement(DeletebtnLocator);
            DeleteButton.Click();

        }

                
           public void DeleteEducation(String country, String universityName, String title, String degree, String year)
           {
                educationButton = _driver.FindElement(educationButtonLocator);
                educationButton.Click();

            AddEducation(country, universityName, title, degree, year);


            Thread.Sleep(5000);

              DeleteButton = _driver.FindElement(DeletebtnLocator);
              DeleteButton.Click();

            Thread.Sleep(5000);

                          
           }

           public void VerifyEducationDeleted(string DeletedTitle)
           {
               Thread.Sleep(5000);
               // Create the XPath to find the education row
               string xpath = $"//tbody/tr[td[text()='{DeletedTitle}']]";
               var educationElements = _driver.FindElements(By.XPath(xpath));
               // Verify that no elements are found for the deleted education
               Assert.IsTrue(educationElements.Count == 0, $"B.Sc '{DeletedTitle}' was not deleted successfully.");
               Thread.Sleep(8000);
           } 


    }
}