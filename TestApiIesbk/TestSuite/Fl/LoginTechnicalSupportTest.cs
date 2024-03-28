using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using TestApiIesbk.PageObject;

namespace TestApiIesbk.TestSuite.Fl
{
    public class LoginTechnicalSupportTest
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
        public void LoginFlTech()
        {
            MainPageFLPageObject mainPageFLPage = new MainPageFLPageObject(_webDriver);
            mainPageFLPage.ClickButtonPersonalAccountTech();
            mainPageFLPage.EnterLoginTech("Misha");
            mainPageFLPage.EnterPasswordTech("111111111");
            mainPageFLPage.ClickButtonLoginTech();
        }


        [TearDown]
        public void TearDown()
        {
            _webDriver.Close();
        }
    }
}
