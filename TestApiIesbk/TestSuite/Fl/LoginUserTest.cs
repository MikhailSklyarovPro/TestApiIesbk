using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using TestApiIesbk.PageObject;
using TestApiIesbk.Model;
using System.Text.Json;

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
        public void LoginFl()
        {
            MainPageFLPageObject mainPageFLPage = new MainPageFLPageObject(_webDriver);
            mainPageFLPage.ClickButtonPersonalAccount();
            mainPageFLPage.ChoiceRadioButtonFL();
            mainPageFLPage.ChoiceRadioButtonMethodLogin();
            mainPageFLPage.EnterNumberAccount("10103005224");
            mainPageFLPage.EnterPassword("111111111");
            mainPageFLPage.ClickButtonLogin();
        }


        [TearDown]
        public void TearDown()
        {
           _webDriver.Close();
        }
    }
}
