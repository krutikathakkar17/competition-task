using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace MarsEducationCertificationsSpecflow.Utilities
{
    
    
    public class CommonDriver
    {
        public IWebDriver driver { get; private set; }

        public void Initialize()
        {
            driver = new ChromeDriver();
            Thread.Sleep(5000);
            driver.Manage().Window.Maximize();
        }

        public void Cleanup()
        {
            if (driver != null)
            {
                driver.Quit();
                driver.Dispose();
            }
        }
    }
}
