using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
namespace TestApiIesbk.PageObject
{

    public class MainPageFLPageObject
    {
        private IWebDriver _webDriver;
        public static string LocatMainPageFL = GlobalMethod.config["SiteURLFL"]!; //путь до главной страницы сайта физ. лица
        private readonly By _buttonPersonalAccount = By.XPath("//button[@id='idModalLoginWindow']"); //путь до кнопки личный кабинет на главной странице

        public MainPageFLPageObject(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }

        //Нажатие на кнопку личный кабинет
        public void ClickButtonPersonalAccount()
        {
            //Ищем кнопку входа в личный кабинет
            if (!GlobalMethod.WaitFindElement(_buttonPersonalAccount, _webDriver, 5)) { Assert.Fail("Не удалось найти кнопку входа в личный кабинет"); }
            //Нажимаем кнопку входа в личный кабинет
            if (!GlobalMethod.WaitClick(_webDriver.FindElement(_buttonPersonalAccount), 3)) { Assert.Fail("Кнопка входа в личный кабинет не кликабельна"); }

        }

        //Выбрать радиобатон

        //Нажать на кнопку далее

        //Выбрать радобатон по логину и паролю

        //Нажать на кнопку далее

        //Ввести намер лицевого

        //Ввести пароль

        //Нажать на кнопку войти

    }
}
