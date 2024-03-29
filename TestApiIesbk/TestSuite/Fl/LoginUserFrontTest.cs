using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using TestApiIesbk.PageObject;
using Microsoft.Extensions.Configuration;

namespace TestApiIesbk.TestSuite.Fl
{
    public class LoginUserFrontTest
    {
        private IWebDriver _webDriver;

        //Метод, который возвращает из набора тестовых данных (json файл) логин и пароль пользователя.
        private static IEnumerable<TestCaseData> GetTestData()
        {
            //Получаем из файла секцию suite 
            IConfigurationSection valuesSection = GlobalMethod.testDataFL.GetSection("suite");

            //Перебераем в цикле все вложенные элементы в секцию suite
            foreach (IConfigurationSection section in valuesSection.GetChildren())
            {
                //Получаем значение секции логина
                string login = section.GetSection("testsettings:login").Value!;
                //Получаем значение секции пароля
                string password = section.GetSection("testsettings:authenticator").Value!;
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
        public void LoginFl(string login, string password)
        {
            //Создаем экземпляр главной страницы сайта ФЛ
            MainPageFLPageObject mainPageFLPage = new MainPageFLPageObject(_webDriver);
            //Нажимаем на кнопку личный кабинет
            mainPageFLPage.ClickButtonPersonalAccount();
            //Выбираем радиобаттон частным лицам
            mainPageFLPage.ChoiceRadioButtonFL();
            //Выбираем вход по логину и паролю
            mainPageFLPage.ChoiceRadioButtonMethodLogin();
            //Вводим логин
            mainPageFLPage.EnterNumberAccount(login);
            //Вводим пароль
            mainPageFLPage.EnterPassword(password);
            //Нажимаем на кнопку войти
            mainPageFLPage.ClickButtonLogin();
        }

        //Метод выполняет действия после выполнения теста
        [TearDown]
        public void TearDown()
        {
            _webDriver.Close();
        }
    }
}
