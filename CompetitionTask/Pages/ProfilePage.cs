using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsEducationCertificationsSpecflow.Pages
{
    public class ProfilePage
    {
        private IWebDriver _driver;

        public ProfilePage(IWebDriver driver)
        {
            _driver = driver;
        }
        public void VerifyLoggedinUser()
        {
            //Verify whether user has loggedin or not
            IWebElement hiKrutikalink = _driver.FindElement(By.XPath("/html/body/div[1]/div/div[1]/div[2]/div/span"));

            if (hiKrutikalink.Text == "Hi Krutika")
            {
                Console.WriteLine("User has logged in successfully");
            }
            else
            {
                Console.WriteLine("User hasn't been logged in");
            }



            Thread.Sleep(5000);

        }



    }


}
