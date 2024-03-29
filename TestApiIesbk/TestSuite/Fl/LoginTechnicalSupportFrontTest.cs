using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using TestApiIesbk.PageObject;
using Microsoft.Extensions.Configuration;

namespace TestApiIesbk.TestSuite.Fl
{
    public class LoginTechnicalSupportFrontTest
    {
        private IWebDriver _webDriver;
        //Метод, который возвращает из набора тестовых данных (json файл) логин и пароль пользователя техподдержки.
        private static IEnumerable<TestCaseData> GetTestData()
        {
            //Получаем из файла секцию suite 
            IConfigurationSection valuesSection = GlobalMethod.testDataFL.GetSection("suite");

            //Перебераем в цикле все вложенные элементы в секцию suite
            foreach (IConfigurationSection section in valuesSection.GetChildren())
            {
                //Получаем значение секции логина
                string login = section.GetSection("tech_login").Value!;
                //Получаем значение секции пароля
                string password = section.GetSection("tech_password").Value!;
                //Отправляем полученные логин и пароль в качестве параметров на на выполнение теста
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
