using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;

namespace TestApiIesbk.TestSuit.Fl
{
    public class Tests
    {

        private IWebDriver _webDriver;
        
        [SetUp]
        public void Setup()
        {
            _webDriver = new ChromeDriver();
            _webDriver.Manage().Window.Maximize();
            _webDriver.Navigate().GoToUrl("https://cian.ru");
        }

        [Test]
        public void OpenApplication()
        {
            Assert.Fail("Работает");
        }


        [TearDown]
        public void TearDown()
        {
            _webDriver.Close();
        }
    }
}