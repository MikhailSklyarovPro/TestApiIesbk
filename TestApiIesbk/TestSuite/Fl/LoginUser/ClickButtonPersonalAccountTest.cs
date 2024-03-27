using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using TestApiIesbk.PageObject;

namespace TestApiIesbk.TestSuit.Fl.LoginUser
{
    public class Tests
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
        public void OpenApplication()
        {
           MainPageFLPageObject mainPageFLPage = new MainPageFLPageObject();
           mainPageFLPage.ClickButtonPersonalAccount();
        }


        [TearDown]
        public void TearDown()
        {
            _webDriver.Close();
        }
    }
}