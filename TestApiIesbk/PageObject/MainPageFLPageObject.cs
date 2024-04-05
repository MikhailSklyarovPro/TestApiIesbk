﻿using OpenQA.Selenium;
using TestApiIesbk;

namespace TestIesbk
{

    public class MainPageFLPageObject
    {
        private IWebDriver _webDriver;
        public static string LocatMainPageFL = GlobalMethod.GetAppSetting().SiteURLFL; //путь до главной страницы сайта физ. лица
        private readonly By _buttonPersonalAccount = By.XPath("//button[@id='idModalLoginWindow']"); //путь до кнопки личный кабинет на главной странице
        private readonly By _radioButtonFL = By.XPath("//label[text()='Частным лицам']"); //путь до радиобаттона частным лицам
        private readonly By _buttonBackChoiceFL = By.XPath("//div[@class='AuthScreenChooseUserTypeTemplate']/button[text()='Далее']"); //путь до кнопки далее при выборе(ФЛ/ЮЛ)
        private readonly By _radioButtonMethodLogin = By.XPath("//label[text()='По логину и паролю']"); //путь до радиобаттона выбора способа входа (По логину и паролю)
        private readonly By _buttonBackChoiceMethodLogin = By.XPath("//div[@class='AuthScreenSelectLoginMethodTemplate']//button[text()='Далее']"); //путь до кнопки далее при выборе способа входа (По логину и паролю)
        private readonly By _inputNumberAccount = By.XPath("//label[text()='Номер лицевого счета / договора']/../input"); //путь до поля ввода номера лицевого счета
        private readonly By _inputPassword = By.XPath("//label[text()='Фамилия / пароль']/../..//input"); //путь до поля ввода пароля
        private readonly By _buttonLogin= By.XPath("//div[@class='AuthScreenEnterPasswordPLTemplate']//button[text()='Войти']"); //путь до кнопки войти
        private readonly By _buttonPersonalAccountTech = By.XPath("//a[@title='Вход для администраторов и контент-менеджеров']"); //путь до кнопки входа в личный кабинет администратора
        private readonly By _inputLoginTech = By.XPath("//input[@id='LogonForm_Uid']"); //путь до поля ввода логина администратора
        private readonly By _inputPasswordTech = By.XPath("//input[@id='LogonForm_Pwd']"); //путь до поля ввода пароля администратора
        private readonly By _buttonLoginTech = By.XPath("//form[@name='LogonForm']//button[text()='Войти']"); //путь до кнопки войти для администратора
        private readonly By _adminPanel = By.XPath("//div[@class='vtSideSlideBar']"); //путь до боковой панели администратора
        private readonly By _titlePersonalAccount = By.XPath("//h1[text()='Личный кабинет физического лица']"); //путь до заголовка лчиного кабинета
                                                                                                                                                                                   
        public MainPageFLPageObject(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }

        //Нажатие на кнопку личный кабинет
        public void ClickButtonPersonalAccount(string message)
        {
            //Ищем кнопку входа в личный кабинет
            if (!GlobalMethod.WaitFindElement(_buttonPersonalAccount, _webDriver, 5)) { _webDriver.Quit(); Assert.Fail($"{message} Не удалось найти кнопку входа в личный кабинет"); }
            //Нажимаем кнопку входа в личный кабинет
            if (!GlobalMethod.WaitClick(_webDriver.FindElement(_buttonPersonalAccount), 3)) { _webDriver.Quit(); Assert.Fail($"{message} Кнопка входа в личный кабинет не кликабельна"); }
        }

        //Выбираем радиобатон (частным лицам /для бизнеса)
        public void ChoiceRadioButtonFL(string message)
        {
            //Ищем радиобаттон
            if (!GlobalMethod.WaitFindElement(_radioButtonFL, _webDriver, 5)) { _webDriver.Quit(); Assert.Fail($"{message} Не удалось найти радиобаттон частным лицам"); }
            //Нажимаем на радиобаттон
            if (!GlobalMethod.WaitClick(_webDriver.FindElement(_radioButtonFL), 3)) { _webDriver.Quit(); Assert.Fail($"{message} Радиобаттон частным лицам не кликабелен"); }


            //Ищем кнопку далее
            if (!GlobalMethod.WaitFindElement(_buttonBackChoiceFL, _webDriver, 5)) { _webDriver.Quit(); Assert.Fail($"{message} Не удалось найти кнопку далее"); }
            //Нажимаем на кнопку далее
            if (!GlobalMethod.WaitClick(_webDriver.FindElement(_buttonBackChoiceFL), 3)) { _webDriver.Quit(); Assert.Fail($"{message} Кнопка далее не кликабельна"); }
        }

        //Выбираем радиобатон способа входа (по логину и паролю)
        public void ChoiceRadioButtonMethodLogin(string message)
        {
            //Ищем радиобаттон
            if (!GlobalMethod.WaitFindElement(_radioButtonMethodLogin, _webDriver, 5)) { _webDriver.Quit(); Assert.Fail($"{message} Не удалось найти радиобаттон частным лицам"); }
            //Нажимаем на радиобаттон
            if (!GlobalMethod.WaitClick(_webDriver.FindElement(_radioButtonMethodLogin), 3)) { _webDriver.Quit(); Assert.Fail($"{message} Радиобаттон частным лицам не кликабелен"); }


            //Ищем кнопку далее
            if (!GlobalMethod.WaitFindElement(_buttonBackChoiceMethodLogin, _webDriver, 5)) { _webDriver.Quit(); Assert.Fail($"{message} Не удалось найти кнопку далее"); }
            //Нажимаем на кнопку далее
            if (!GlobalMethod.WaitClick(_webDriver.FindElement(_buttonBackChoiceMethodLogin), 3)) { _webDriver.Quit(); Assert.Fail($"{message} Кнопка далее не кликабельна"); }
        }

        //Вводим номер лицевого счета
        public void EnterNumberAccount(string login, string message)
        {
            //Ищем поле для ввода номера лс
            if (!GlobalMethod.WaitFindElement(_inputNumberAccount, _webDriver, 5)) { _webDriver.Quit(); Assert.Fail($"{message} Не удалось найти поле для ввода"); }
            //Устанавливаем значение в поле для ввода
            if (!GlobalMethod.WaitSendKey(_inputNumberAccount, _webDriver, login)) { _webDriver.Quit(); Assert.Fail($"{message} Не удалось установить значение в поле для ввода номера лицевого счета"); }
        }

        //Вводим пароль
        public void EnterPassword(string password, string message)
        {
            //Ищем поле для ввода пароля
            if (!GlobalMethod.WaitFindElement(_inputPassword, _webDriver, 5)) { _webDriver.Quit(); Assert.Fail($"{message} Не удалось найти поле для ввода"); }
            //Устанавливаем значение в поле для ввода
            if (!GlobalMethod.WaitSendKey(_inputPassword, _webDriver, password)) { _webDriver.Quit(); Assert.Fail($"{message} Не удалось установить значение в поле для ввода пароля"); }
        }

        //Нажимаем на кнопку войти
        public void ClickButtonLogin(string message)
        {
            //Ищем кнопку войти
            if (!GlobalMethod.WaitFindElement(_buttonLogin, _webDriver, 5)) { _webDriver.Quit(); Assert.Fail($"{message} Не удалось найти кнопку войти"); }
            //Нажимаем на кнопку
            if (!GlobalMethod.WaitClick(_webDriver.FindElement(_buttonLogin))) { _webDriver.Quit(); Assert.Fail($"{message} Кнопка войти не кликабельна"); }
            //Ищем заголовок Личный кабинет физического лица чтобы проверить вошли или нет
            if (!GlobalMethod.WaitFindElement(_titlePersonalAccount, _webDriver, 5)) 
            {
                Screenshot screenshot = ((ITakesScreenshot)_webDriver).GetScreenshot();
                screenshot.SaveAsFile($"{GlobalMethod.GetAppSetting().ScreenshotFailedTest}\\{GlobalMethod.numberTest}.jpg");
                _webDriver.Quit(); 
                Assert.Fail($"{message} Не удалось войти в личный кабинет физического лица"); 
            }
            
        }

        //Кликаем на кнопку входа для администраторов
        public void ClickButtonPersonalAccountTech(string message)
        {
            //Ищем кнопку входа в личный кабинет
            if (!GlobalMethod.WaitFindElement(_buttonPersonalAccountTech, _webDriver, 5)) { _webDriver.Quit(); Assert.Fail($"{message} Не удалось найти кнопку входа в личный кабинет администратора"); }
            //Нажимаем кнопку входа в личный кабинет
            if (!GlobalMethod.WaitClick(_webDriver.FindElement(_buttonPersonalAccountTech), 3)) { _webDriver.Quit(); Assert.Fail($"{message} Кнопка входа в личный кабинет администратора не кликабельна"); }
        }

        //Вводим логин администратора
        public void EnterLoginTech(string loginTech, string message)
        {
            //Ищем поле для ввода логина администратора
            if (!GlobalMethod.WaitFindElement(_inputLoginTech, _webDriver, 5)) { _webDriver.Quit(); Assert.Fail($"{message} Не удалось найти поле для ввода логина администратора"); }
            //Устанавливаем значение в поле для ввода
            if (!GlobalMethod.WaitSendKey(_inputLoginTech, _webDriver, loginTech)) { _webDriver.Quit(); Assert.Fail($"{message} Не удалось установить значение в поле для ввода логина администратора"); }
        }

        //Вводим пароль администратора
        public void EnterPasswordTech(string passwordTech, string message)
        {
            //Ищем поле для ввода пароля администратора
            if (!GlobalMethod.WaitFindElement(_inputPasswordTech, _webDriver, 5)) { _webDriver.Quit(); Assert.Fail($"{message} Не удалось найти поле для ввода пароля администратора"); }
            //Устанавливаем значение в поле для ввода
            if (!GlobalMethod.WaitSendKey(_inputPasswordTech, _webDriver, passwordTech)) { _webDriver.Quit(); Assert.Fail($"{message} Не удалось установить значение в поле для ввода пароля администратора"); }
        }

        //Нажимаем на кнопку войти
        public void ClickButtonLoginTech(string message)
        {
            //Ищем кнопку войти
            if (!GlobalMethod.WaitFindElement(_buttonLoginTech, _webDriver, 5)) { _webDriver.Quit(); Assert.Fail($"{message}Не удалось найти кнопку войти"); }
            //Нажимаем на кнопку
            if (!GlobalMethod.WaitClick(_webDriver.FindElement(_buttonLoginTech))) { _webDriver.Quit(); Assert.Fail($"{message}Кнопка войти не кликабельна"); }
            //Ищем боковую панель администратора для того чтобы проверить вошли или нет
            if (!GlobalMethod.WaitFindElement(_adminPanel, _webDriver, 5)) { _webDriver.Quit(); Assert.Fail($"{message} Не удалось войти в учетную запись администратора"); }
        }
    }
}
