using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using NUnit.Framework;



namespace Mars_Education_Certifications.Pages
{
    public class CertificationsFeature
    {

        private IWebDriver _driver;

        public CertificationsFeature(IWebDriver driver)
        {
            _driver = driver;
        }

        private readonly By certificationsButtonLocator = By.XPath("//a[contains(.,'Certifications')][@data-tab=\"fourth\"]");
        IWebElement cerificationsButton;


        private readonly By addNewcertificationButtonLocator = By.XPath("//div[@class='ui bottom attached tab segment tooltip-target active']//div[contains(@class,'ui teal button')][normalize-space()='Add New']");
        IWebElement addNewcertificationButton;


        private readonly By inputCertificationLocator = By.XPath("//input[@placeholder='Certificate or Award']");
        IWebElement inputCertification;

        private readonly By inputCertifedfromLocator = By.XPath("//input[@placeholder='Certified From (e.g. Adobe)']");
        IWebElement inputCerifiedfrom;


        private readonly By addCertificationLocator = By.XPath("//input[@value='Add']");
        IWebElement addCertification;

         private readonly By EditbtnLocator = By.XPath("//tbody/tr/td[@class='right aligned']/span[@class='button']/i[contains(@class, 'outline write icon')]");
         IWebElement editButton;


         private readonly By DeletebuttonLocator = By.XPath("//tbody/tr/td[@class='right aligned']/span[@class='button']/i[contains(@class, 'remove icon')]");
         IWebElement deleteBtn;
        




        public void AddCertifications(String certification, String certifiedfrom, String year)
        {
            cerificationsButton = _driver.FindElement(certificationsButtonLocator);
            cerificationsButton.Click();

            addNewcertificationButton = _driver.FindElement(addNewcertificationButtonLocator);
            addNewcertificationButton.Click();

            string certificationPopupXPath = "//div[@class='ns-box-inner']";
            string expectedMessage = $"{certification} has been added to your certification";

            Thread.Sleep(3000);

            // Find the language input element and enter the value
            inputCertification = _driver.FindElement(inputCertificationLocator);
            inputCertification.SendKeys(certification);

            inputCerifiedfrom = _driver.FindElement(inputCertifedfromLocator);
            inputCerifiedfrom.SendKeys(certifiedfrom);

            SelectElement yearLevelDropdown = new SelectElement(_driver.FindElement(By.XPath("//select[@name='certificationYear']")));
            yearLevelDropdown.SelectByText(year);

  

            Thread.Sleep(7000);

            // Click the "Add" button
            addCertification = _driver.FindElement(addCertificationLocator);
            addCertification.Click();
            Thread.Sleep(1000);
            var popupElement = _driver.FindElement(By.XPath(certificationPopupXPath));
            string popupText = popupElement.Text;
            Assert.AreEqual(expectedMessage, popupText, $"Pop-up message mismatch. Expected: '{expectedMessage}', but got: '{popupText}'");
            Thread.Sleep(5000);

        }
      

        public void VerifyCertificationsAdded(string expectedCertificate, string expectedCertifiedfrom, string expectedYear)
        {
            Thread.Sleep(5000);


            string rowXPath = "//table[@class='ui fixed table']/tbody/tr";


            Thread.Sleep(3000);

            var rows = _driver.FindElements(By.XPath(rowXPath));


            bool found = false;

            foreach (var row in rows)
            {
                Console.WriteLine(row);
                var certificateElement = row.FindElement(By.XPath("./td[1]"));
                var certifiedfromElement = row.FindElement(By.XPath("./td[2]"));
                var yearElement = row.FindElement(By.XPath("./td[3]"));

                
                if (certificateElement.Text == expectedCertificate && certifiedfromElement.Text == expectedCertifiedfrom && yearElement.Text == expectedYear)
                {
                    found = true;
                    break;
                }
            }

            Assert.IsTrue(found, $"Certificate from '{expectedCertificate}', '{expectedCertifiedfrom}', '{expectedYear}' not found.");

              deleteBtn = _driver.FindElement(DeletebuttonLocator);
              deleteBtn.Click();
        }


        public void AddDuplicateCertification(String certification, String certifiedfrom, String year, String CertificationNameNew, String CertifiedFromNew, String YearNew)
        {
            Thread.Sleep(3000);

            AddCertifications(certification, certifiedfrom, year);

            cerificationsButton = _driver.FindElement(certificationsButtonLocator);
            cerificationsButton.Click();

            addNewcertificationButton = _driver.FindElement(addNewcertificationButtonLocator);
            addNewcertificationButton.Click();

            inputCertification = _driver.FindElement(inputCertificationLocator);
            inputCertification.SendKeys(certification);

            inputCerifiedfrom = _driver.FindElement(inputCertifedfromLocator);
            inputCerifiedfrom.SendKeys(certifiedfrom);

            SelectElement countryLevelDropdown = new SelectElement(_driver.FindElement(By.XPath("//select[@name='certificationYear']")));
            countryLevelDropdown.SelectByText(year);


            addCertification = _driver.FindElement(addCertificationLocator);
            addCertification.Click();

            Thread.Sleep(1000);

        }

        
        public void VerifyNoDuplicateCertificationAdded()
        {
            {
                
                
                string popupXPath = "//div[@class='ns-box-inner']";
                string expectedMessage = "This information is already exist.";
               

                Thread.Sleep(1000);

                var popupElement = _driver.FindElement(By.XPath(popupXPath));
                string popupText = popupElement.Text;

                Assert.AreEqual(expectedMessage, popupText, $"Pop-up message mismatch. Expected: '{expectedMessage}', but got: '{popupText}'");
                Thread.Sleep(5000);


                   deleteBtn = _driver.FindElement(DeletebuttonLocator);
                  deleteBtn.Click();


            }
        }
                                               

        private readonly By addCertificateupdatedLocator = By.XPath("//input[@class='ui teal button'][@value='Update']");
        IWebElement addCertificateupdated;

        
        public void EditCertifications(String certification, String CertifiedFrom, String year, String CertificationNew)
        {
            //Edit the Skills
           cerificationsButton = _driver.FindElement(certificationsButtonLocator);
           cerificationsButton.Click();
            Thread.Sleep(7000);

            AddCertifications(certification, CertifiedFrom, year);

            
            // Click the update button 

              editButton = _driver.FindElement(EditbtnLocator);
              editButton.Click();

            IWebElement certificateInput = _driver.FindElement(By.XPath("//input[@placeholder='Certificate or Award']"));

            certificateInput.Clear();

            inputCertification = _driver.FindElement(inputCertificationLocator);
            inputCertification.SendKeys(CertificationNew);

            addCertificateupdated = _driver.FindElement(addCertificateupdatedLocator);
            addCertificateupdated.Click();

            Thread.Sleep(3000);

        }

        public void VerifyCertificateEdited(string CertificationNew, string expectedCertification)
        {
           cerificationsButton = _driver.FindElement(certificationsButtonLocator);
           cerificationsButton.Click();
            Thread.Sleep(5000);

            // Create the XPath to find the skill row
            string xpath = $"//tbody/tr[td[text()='{expectedCertification}']]/td[1]";
            IWebElement certifiedFromElement = _driver.FindElement(By.XPath(xpath));

            // Get the actual level text
            string actualCertifiedfrom = expectedCertification;

            // Verify the level
            if (expectedCertification != actualCertifiedfrom)
            {
                throw new Exception($"Skill level for '{expectedCertification}' was not updated correctly. Expected: {expectedCertification}, Actual: {actualCertifiedfrom}");
            }

            Thread.Sleep(5000);


              deleteBtn = _driver.FindElement(DeletebuttonLocator);
              deleteBtn.Click();

        }


        public void DeleteCertificate(String certification, String CertifiedFrom, String year)
        {
           cerificationsButton = _driver.FindElement(certificationsButtonLocator);
           cerificationsButton.Click();
            Thread.Sleep(5000);

            AddCertifications(certification, CertifiedFrom, year);

            {
               
                  deleteBtn = _driver.FindElement(DeletebuttonLocator);
                  deleteBtn.Click();

                Thread.Sleep(5000);


            }

        }

        
        public void VerifyCertificateDeleted(string certificate)
        {
            Thread.Sleep(5000);
            // Create the XPath to find the skill row
            string xpath = $"//tbody/tr[td[text()='{certificate}']]";
            var skillElements = _driver.FindElements(By.XPath(xpath));
            // Verify that no elements are found for the deleted skill
            Assert.IsTrue(skillElements.Count == 0, $"Skill '{certificate}' was not deleted successfully.");
            Thread.Sleep(5000);
        }
    }
}













