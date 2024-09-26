using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsEducationCertificationsSpecflow.Pages
{
    public class LoginPage
    {
        private IWebDriver _driver;

        public LoginPage(IWebDriver driver)
        {
            _driver = driver;
        }

        private readonly By signinButtonLocator = By.XPath("//A[@class='item'][text()='Sign In']");
        IWebElement signinButton;
        private readonly By usernametextBoxLocator = By.XPath("(//INPUT[@type='text'])[2]");
        IWebElement usernametextBox;
        private readonly By passwordtextBoxLocator = By.XPath("//INPUT[@type='password']");
        IWebElement passwordtextBox;
        private readonly By loginButtonLocator = By.XPath("//BUTTON[@class='fluid ui teal button'][text()='Login']");
        IWebElement loginButton;
        public void LoginActions(string username, string password)
        {
            _driver.Navigate().GoToUrl("http://localhost:5000/");

            _driver.Manage().Window.Maximize();

            //SignIn into Mars portal
            signinButton = _driver.FindElement(signinButtonLocator);
            signinButton.Click();

            //Enter login credentials, emailid & password
            usernametextBox = _driver.FindElement(usernametextBoxLocator);
            usernametextBox.SendKeys(username);

            passwordtextBox = _driver.FindElement(passwordtextBoxLocator);
            passwordtextBox.SendKeys(password);

            //Click on LoginButton
            loginButton = _driver.FindElement(loginButtonLocator);
            loginButton.Click();
            Thread.Sleep(5000);
        }

    }
}
