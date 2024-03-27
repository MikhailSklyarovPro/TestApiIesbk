using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApiIesbk.PageObject
{
    public class MainPageFLPageObject
    {
        private IWebDriver _webDriver;
        public static string LocatMainPageFL = "https://psoreliz.ie.corp/RequestInformationSystem"; //путь до главной страницы сайта физ. лица

        private readonly By _buttonPersonalAccount = By.XPath("//button[@id='idModalLoginWindow']"); //путь до кнопки личный кабинет на главной странице

        //Нажатие на кнопку личный кабинет
        public void ClickButtonPersonalAccount()
        {
            //Ищем кнопку входа в личный кабинет
            if(!GlobalMethod.WaitFindElement(_buttonPersonalAccount, _webDriver, 5)) { Assert.Fail("Не удалось найти кнопку входа в личный кабинет"); }
            //Нажимаем кнопку входа в личный кабинет
            if (!GlobalMethod.WaitClick(_webDriver.FindElement(_buttonPersonalAccount), 3)) { Assert.Fail("Кнопка входа в личный кабинет не кликабельна"); }
        }

    }
}
