using OpenQA.Selenium;

namespace TestApiIesbk.PageObject
{

    public class MainPageFLPageObject
    {
        private IWebDriver _webDriver;
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
            if (!GlobalMethod.WaitFindElement(_buttonPersonalAccount, _webDriver, 5)) { GlobalMethod.FrontTestFailed($"{message} Не удалось найти кнопку входа в личный кабинет", _webDriver); }
            //Нажимаем кнопку входа в личный кабинет
            if (!GlobalMethod.WaitClick(_webDriver.FindElement(_buttonPersonalAccount), 3))  { GlobalMethod.FrontTestFailed($"{message} Кнопка входа в личный кабинет не кликабельна", _webDriver); }
        }

        //Выбираем радиобатон (частным лицам /для бизнеса)
        public void ChoiceRadioButtonFL(string message)
        {
            //Ищем радиобаттон
            if (!GlobalMethod.WaitFindElement(_radioButtonFL, _webDriver, 5)) { GlobalMethod.FrontTestFailed($"{message} Не удалось найти радиобаттон частным лицам", _webDriver); }
            //Нажимаем на радиобаттон
            if (!GlobalMethod.WaitClick(_webDriver.FindElement(_radioButtonFL), 3)) { GlobalMethod.FrontTestFailed($"{message} Радиобаттон частным лицам не кликабелен", _webDriver); }


            //Ищем кнопку далее
            if (!GlobalMethod.WaitFindElement(_buttonBackChoiceFL, _webDriver, 5)) { GlobalMethod.FrontTestFailed($"{message} Не удалось найти кнопку далее", _webDriver); }
            //Нажимаем на кнопку далее
            if (!GlobalMethod.WaitClick(_webDriver.FindElement(_buttonBackChoiceFL), 3)) { GlobalMethod.FrontTestFailed($"{message} Кнопка далее не кликабельна", _webDriver); }
        }

        //Выбираем радиобатон способа входа (по логину и паролю)
        public void ChoiceRadioButtonMethodLogin(string message)
        {
            //Ищем радиобаттон
            if (!GlobalMethod.WaitFindElement(_radioButtonMethodLogin, _webDriver, 5)) { GlobalMethod.FrontTestFailed($"{message} Не удалось найти радиобаттон частным лицам", _webDriver); }
            //Нажимаем на радиобаттон
            if (!GlobalMethod.WaitClick(_webDriver.FindElement(_radioButtonMethodLogin), 3)) { GlobalMethod.FrontTestFailed($"{message} Радиобаттон частным лицам не кликабелен", _webDriver); }


            //Ищем кнопку далее
            if (!GlobalMethod.WaitFindElement(_buttonBackChoiceMethodLogin, _webDriver, 5)) { GlobalMethod.FrontTestFailed($"{message} Не удалось найти кнопку далее", _webDriver); }
            //Нажимаем на кнопку далее
            if (!GlobalMethod.WaitClick(_webDriver.FindElement(_buttonBackChoiceMethodLogin), 3)) { GlobalMethod.FrontTestFailed($"{message} Кнопка далее не кликабельна", _webDriver); }
        }

        //Вводим номер лицевого счета
        public void EnterNumberAccount(string login, string message)
        {
            //Ищем поле для ввода номера лс
            if (!GlobalMethod.WaitFindElement(_inputNumberAccount, _webDriver, 5)) { GlobalMethod.FrontTestFailed($"{message} Не удалось найти поле для ввода", _webDriver); }
            //Устанавливаем значение в поле для ввода
            if (!GlobalMethod.WaitSendKey(_inputNumberAccount, _webDriver, login)) { GlobalMethod.FrontTestFailed($"{message} Не удалось установить значение в поле для ввода номера лицевого счета", _webDriver); }
        }

        //Вводим пароль
        public void EnterPassword(string password, string message)
        {
            //Ищем поле для ввода пароля
            if (!GlobalMethod.WaitFindElement(_inputPassword, _webDriver, 5)) { GlobalMethod.FrontTestFailed($"{message} Не удалось найти поле для ввода", _webDriver); }
            //Устанавливаем значение в поле для ввода
            if (!GlobalMethod.WaitSendKey(_inputPassword, _webDriver, password)) { GlobalMethod.FrontTestFailed($"{message} Не удалось установить значение в поле для ввода пароля", _webDriver); }
        }

        //Нажимаем на кнопку войти
        public void ClickButtonLogin(string message)
        {
            //Ищем кнопку войти
            if (!GlobalMethod.WaitFindElement(_buttonLogin, _webDriver, 5)) { GlobalMethod.FrontTestFailed($"{message} Не удалось найти кнопку войти", _webDriver); }
            //Нажимаем на кнопку
            if (!GlobalMethod.WaitClick(_webDriver.FindElement(_buttonLogin))) { GlobalMethod.FrontTestFailed($"{message} Кнопка войти не кликабельна", _webDriver); }
            //Ищем заголовок Личный кабинет физического лица чтобы проверить вошли или нет
            if (!GlobalMethod.WaitFindElement(_titlePersonalAccount, _webDriver, 5)) { GlobalMethod.FrontTestFailed($"{message} Не удалось войти в личный кабинет физического лица", _webDriver); }
            
        }

        //Кликаем на кнопку входа для администраторов
        public void ClickButtonPersonalAccountTech(string message)
        {
            //Ищем кнопку входа в личный кабинет
            if (!GlobalMethod.WaitFindElement(_buttonPersonalAccountTech, _webDriver, 5)) { GlobalMethod.FrontTestFailed($"{message} Не удалось найти кнопку входа в личный кабинет администратора", _webDriver); }
            //Нажимаем кнопку входа в личный кабинет
            if (!GlobalMethod.WaitClick(_webDriver.FindElement(_buttonPersonalAccountTech), 3)) { GlobalMethod.FrontTestFailed($"{message} Кнопка входа в личный кабинет администратора не кликабельна", _webDriver); }
        }

        //Вводим логин администратора
        public void EnterLoginTech(string loginTech, string message)
        {
            //Ищем поле для ввода логина администратора
            if (!GlobalMethod.WaitFindElement(_inputLoginTech, _webDriver, 5)) { GlobalMethod.FrontTestFailed($"{message} Не удалось найти поле для ввода логина администратора", _webDriver); }
            //Устанавливаем значение в поле для ввода
            if (!GlobalMethod.WaitSendKey(_inputLoginTech, _webDriver, loginTech)) { GlobalMethod.FrontTestFailed($"{message} Не удалось установить значение в поле для ввода логина администратора", _webDriver); }
        }

        //Вводим пароль администратора
        public void EnterPasswordTech(string passwordTech, string message)
        {
            //Ищем поле для ввода пароля администратора
            if (!GlobalMethod.WaitFindElement(_inputPasswordTech, _webDriver, 5)) { GlobalMethod.FrontTestFailed($"{message} Не удалось найти поле для ввода пароля администратора", _webDriver); }
            //Устанавливаем значение в поле для ввода
            if (!GlobalMethod.WaitSendKey(_inputPasswordTech, _webDriver, passwordTech)) { GlobalMethod.FrontTestFailed($"{message} Не удалось установить значение в поле для ввода пароля администратора", _webDriver); }
        }

        //Нажимаем на кнопку войти
        public void ClickButtonLoginTech(string message)
        {
            //Ищем кнопку войти
            if (!GlobalMethod.WaitFindElement(_buttonLoginTech, _webDriver, 5)) { GlobalMethod.FrontTestFailed($"{message} Не удалось найти кнопку войти", _webDriver); }
            //Нажимаем на кнопку
            if (!GlobalMethod.WaitClick(_webDriver.FindElement(_buttonLoginTech))) { GlobalMethod.FrontTestFailed($"{message} Кнопка войти не кликабельна", _webDriver); }
            //Ищем боковую панель администратора для того чтобы проверить вошли или нет
            if (!GlobalMethod.WaitFindElement(_adminPanel, _webDriver, 7)) { GlobalMethod.FrontTestFailed($"{message} Не удалось войти в учетную запись администратора", _webDriver); }
        }
    }
}
