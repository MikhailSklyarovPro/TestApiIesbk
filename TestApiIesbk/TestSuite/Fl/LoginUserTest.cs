using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApiIesbk.PageObject;

namespace TestApiIesbk.TestSuite.Fl
{
    public class LoginUserTest
    {
        private IWebDriver _webDriver;

        [SetUp]
        public void Setup()
        {
            _webDriver = new ChromeDriver();
            _webDriver.Manage().Window.Maximize();
            _webDriver.Navigate().GoToUrl(MainPageFLPageObject.LocatMainPageFL);
        }

        [Test]
        public void ClickButtonPersonalAccount()
        {
            MainPageFLPageObject mainPageFLPage = new MainPageFLPageObject(_webDriver);
            mainPageFLPage.ClickButtonPersonalAccount();
        }


        [TearDown]
        public void TearDown()
        {
            _webDriver.Close();
        }
    }
}
