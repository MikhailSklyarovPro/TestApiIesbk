using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using TestApiIesbk.PageObject;
using TestApiIesbk.Controller;
using TestApiIesbk.Model;

namespace TestApiIesbk.TestSuite.Fl
{
    public class LoginTechnicalSupportFrontTest
    {
        private IWebDriver _webDriver;
        //Метод, который возвращает из набора тестовых данных (json файл) логин и пароль пользователя техподдержки.
        private static IEnumerable<TestCaseData> GetTestData()
        {
            List<TestData> testData = FLController.GetTestData();

            //Перебераем в цикле все вложенные элементы в секцию
            foreach (TestData item in testData)
            {
                string login = item.testSettings.login;
                string password = item.testSettings.authenticator;

                //Отправляем полученные данные на вход теста
                yield return new TestCaseData(login, password);
            }
        }

        //Метод выполняет действия перед запуском теста
        [SetUp]
        public void Setup()
        {
            _webDriver = new ChromeDriver();
            //Открываем окно бразуера на весь экран
            _webDriver.Manage().Window.Maximize();
            //Переходим на главну. страницу сайта ФЛ
            _webDriver.Navigate().GoToUrl(MainPageFLPageObject.LocatMainPageFL);
        }

        //Метод выполняем тест. Принимает параметры из вне(логин и пароль).
        [Test, TestCaseSource(nameof(GetTestData))]
        public void LoginFlTech(string login, string password)
        {
            //Создаем экземпляр главной страницы сайта ФЛ
            MainPageFLPageObject mainPageFLPage = new MainPageFLPageObject(_webDriver);
            //Нажимаем на кнопку личный кабинет для теходдержки
            mainPageFLPage.ClickButtonPersonalAccountTech();
            //Вводим логин
            mainPageFLPage.EnterLoginTech(login);
            //Вводим пароль
            mainPageFLPage.EnterPasswordTech(password);
            //Нажимаем на кнопку войти
            mainPageFLPage.ClickButtonLoginTech();

        }

        //Метод выполняет действия после выполнения теста
        [TearDown]
        public void TearDown()
        {
            _webDriver.Close();
        }
    }
}
