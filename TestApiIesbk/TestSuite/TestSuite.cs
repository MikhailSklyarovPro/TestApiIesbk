﻿using HtmlAgilityPack;
using NUnit.Framework.Internal;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Text.Json;
using TestApiIesbk.Model;

namespace TestApiIesbk.TestSuite
{
    [TestFixture]
    public class TestSuite
    {
        //Выполняется перед запуском всех тестов
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            string documentPath = GlobalMethod.GetAppSetting().PathReportTest;
            // Создать экземпляр HTML-документа
            HtmlDocument document = new HtmlDocument();
            // Загружаем html документ
            document.Load(documentPath);

            //Ищем секцию с началом времени тестирования
            HtmlNode sectionTimeBeginTest = document.DocumentNode.SelectSingleNode("//div[@id='timeBegin']");

            try
            {
                // Ищем старое время начала тестирования
                HtmlNode oldTimeBeginTest = document.DocumentNode.SelectSingleNode("//div[@id='timeBegin']//h1");
                //Удаляем старое время начала тестирования
                oldTimeBeginTest.Remove();
            }
            catch
            {

            }

            //Создаем новое время начала тестирования
            HtmlNode newTimeBeginTest = document.CreateElement("h1");

            //Создаем текст заголовка
            HtmlNode newValueTimeBeginTest = document.CreateTextNode($"Тестирование началось: {DateTime.Now}:{DateTime.Now.Millisecond}");
            //Добавляем заголовку новое значение
            newTimeBeginTest.AppendChild(newValueTimeBeginTest);
            //Добавляем новый заголовок начала времени тестирования
            sectionTimeBeginTest.AppendChild(newTimeBeginTest);

            try {
                // Ищем старое тело таблицы
                HtmlNode oldTbody = document.DocumentNode.SelectSingleNode("//tbody");
                // Удаляем старое тело таблицы
                oldTbody.Remove();
            }
            catch {
                
            }

            try
            {
                //true - если директория не пуста удаляем все ее содержимое
                Directory.Delete(GlobalMethod.GetAppSetting().ScreenshotFailedTest, true); 
                Directory.CreateDirectory(GlobalMethod.GetAppSetting().ScreenshotFailedTest);
            }
            catch
            {
                Directory.CreateDirectory(GlobalMethod.GetAppSetting().ScreenshotFailedTest);
            }
            //Сохраняем файл
            document.Save(documentPath);
        }

        //Метод, который возвращает данные из тестового набора (json файла) для ФЛ
        public static IEnumerable<TestCaseData> GetParametrs(string typeFice)
        {

            switch (typeFice)
            {
                case "FL":
                    break;
                case "UL":
                    break;
            }
            List<TestDataFL> testData = FLController.GetTestData();
            //Перебераем в цикле все вложенные элементы в секцию suite
            foreach (TestDataFL item in testData)
            {
                //Отправляем полученные данные на вход теста
                yield return new TestCaseData(item, ++GlobalMethod.numberTest);
            }
        }




        //Метод выполняет тест. Принимает в параметры тестовые данные.
        [Test, TestCaseSource(nameof(GetParametrs), new object[] { "FL" })]
        public void LoginFlAPI(TestDataFL testData, int numberTest)
        {
            FLController.LoginUser(testData.testSettings.login, testData.testSettings.authenticator);
            Assert.Pass("Вход пользователя в личный кабинет ФЛ API.");
        }




        [Test, TestCaseSource(nameof(GetParametrs), new object[] { "FL" })]
        public void CheckBalanceUserFLAPI(TestDataFL testData, int numberTest)
        {
            //Получаем данные пользователя от API
            ServerResponseUserInfoModel userData = FLController.GetDataUser(FLController.LoginUser(testData.testSettings.login, testData.testSettings.authenticator));
            //Проверка полученных данных от API на соответсвие тестовых данных в json файле
            if (testData.testSettings.balance != userData.account.balance.ToString()) { Assert.Fail($"Действие: Проверка на соответсвие баланса пользователя ФЛ от АПИ с тестовыми данными. Результат: Баланс: {testData.testSettings.balance} в тестовых данных не совпадает с полученным от API балансом: {userData.account.balance}"); }
            Assert.Pass("Проверка на соответсвие баланса пользователя ФЛ от АПИ с тестовыми данными.");
        }




        [Test, TestCaseSource(nameof(GetParametrs), new object[] { "FL" })]
        public void CheckIdDeviceFLAPI(TestDataFL testData, int numberTest)
        {
            //Получаем данные пользователя от API
            List<ServerResponseDevicesModel> devices = FLController.GetDevices(FLController.LoginUser(testData.testSettings.login, testData.testSettings.authenticator));

            //Прибор учета с переданным id найден?
            string foundDeviceId = "";
            //Список id всех приборов учета через запятую
            string allDeviceId = "";
            //Перебираем все приборы учета принадлежащие пользователю
            foreach (ServerResponseDevicesModel device in devices)
            {
                if (device.Id.ToString() == testData.testSettings.deviceId) { foundDeviceId = device.Id.ToString(); }
                allDeviceId = $"{allDeviceId}, {device.Id}";
            }
            //Проверка полученных данных от API на соответсвие тестовых данных в json файле
            if (foundDeviceId == "") { Assert.Fail($"Действие: Проверка на соответсвие id прибора учета пользователя ФЛ от АПИ с тестовыми данными. Результат: Прибор учета с id: {testData.testSettings.deviceId} в тестовых данных не совпадает с полученным(и) от API id: {allDeviceId}"); }
            Assert.Pass("Проверка на соответсвие id прибора учета пользователя ФЛ от АПИ с тестовыми данными.");
        }




        [Test, TestCaseSource(nameof(GetParametrs), new object[] { "FL" })]
        public void LoginFlFront(TestDataFL testData, int numberTest)
        {
            string message = "Действие: Вход пользователя в ЛК ФЛ FRONT. Результат:";
            IWebDriver _webDriver = new ChromeDriver();
            //Открываем окно бразуера на весь экран
            _webDriver.Manage().Window.Maximize();
            //Переходим на главну. страницу сайта ФЛ
            _webDriver.Navigate().GoToUrl(MainPageFLPageObject.LocatMainPageFL);
            //Создаем экземпляр главной страницы сайта ФЛ
            MainPageFLPageObject mainPageFLPage = new MainPageFLPageObject(_webDriver);
            //Нажимаем на кнопку личный кабинет
            mainPageFLPage.ClickButtonPersonalAccount(message);
            //Выбираем радиобаттон частным лицам
            mainPageFLPage.ChoiceRadioButtonFL(message);
            //Выбираем вход по логину и паролю
            mainPageFLPage.ChoiceRadioButtonMethodLogin(message);
            //Вводим логин
            mainPageFLPage.EnterNumberAccount(testData.testSettings.login, message);
            //Вводим пароль
            mainPageFLPage.EnterPassword(testData.testSettings.authenticator, message);
            //Нажимаем на кнопку войти
            mainPageFLPage.ClickButtonLogin(message);
            //Уничтожаем вебдрайвер
            _webDriver.Quit();
            Assert.Pass("Вход пользователя в ЛК ФЛ FRONT.");
        }




        [Test, TestCaseSource(nameof(GetParametrs), new object[] { "FL" })]
        public void LoginTechFlAPI(TestDataFL testData, int numberTest)
        {
            FLController.LoginUserTech(testData.techLogin, testData.techPassword);
            Assert.Pass("Вход техподдержки в личный кабинет ФЛ API.");
        }




        [Test, TestCaseSource(nameof(GetParametrs), new object[] { "FL" })]
        public void LoginUserFromTechFlAPI(TestDataFL testData, int numberTest)
        {
            FLController.LoginUserFromTech(testData.testSettings.login, testData.testSettings.authenticator, FLController.LoginUserTech(testData.techLogin, testData.techPassword));
            Assert.Pass("Вход пользователя в ЛК ФЛ из под ЛК техподдержки API.");
        }



        [Test, TestCaseSource(nameof(GetParametrs), new object[] { "FL" })]
        public void CheckBalanceUserFromTechFLAPI(TestDataFL testData, int numberTest)
        {
            //Получаем данные пользователя от API
            ServerResponseUserInfoModel userData = FLController.GetDataUser(FLController.LoginUserFromTech(testData.testSettings.login, testData.testSettings.authenticator, FLController.LoginUserTech(testData.techLogin, testData.techPassword)));
            //Проверка полученных данных от API на соответсвие тестовых данных в json файле
            if (testData.testSettings.balance != userData.account.balance.ToString()) { Assert.Fail($"Действие: Проверка на соответсвие баланса пользователя ФЛ от АПИ с тестовыми данными, авторизированного из под ЛК техподдержки с помощью 'ЛК ФЛ (Служба поддержки)'. Результат: Баланс: {testData.testSettings.balance} в тестовых данных не совпадает с полученным от API балансом: {userData.account.balance}"); }
            Assert.Pass("Проверка на соответсвие тестовых данных и API данных баланса пользователя ФЛ, авторизированного из под ЛК техподдержки с помощью 'ЛК ФЛ (Служба поддержки)'");
        }




        [Test, TestCaseSource(nameof(GetParametrs), new object[] { "FL" })]
        public void CheckIdDeviceFromTechFLAPI(TestDataFL testData, int numberTest)
        {
            //Получаем данные пользователя от API
            List<ServerResponseDevicesModel> devices = FLController.GetDevices(FLController.LoginUserFromTech(testData.testSettings.login, testData.testSettings.authenticator, FLController.LoginUserTech(testData.techLogin, testData.techPassword)));

            //Прибор учета с переданным id найден?
            string foundDeviceId = "";
            //Список id всех приборов учета через запятую
            string allDeviceId = "";
            //Перебираем все приборы учета принадлежащие пользователю
            foreach (ServerResponseDevicesModel device in devices)
            {
                if (device.Id.ToString() == testData.testSettings.deviceId) { foundDeviceId = device.Id.ToString(); }
                allDeviceId = $"{allDeviceId}, {device.Id}";
            }
            //Проверка полученных данных от API на соответсвие тестовых данных в json файле
            if (foundDeviceId == "") { Assert.Fail($"Действие: Проверка на соответсвие id прибора учета пользователя ФЛ от АПИ с тестовыми данными, авторизированного из под ЛК техподдержки с помощью 'ЛК ФЛ (Служба поддержки)'. Результат: Прибор учета с id: {testData.testSettings.deviceId} в тестовых данных не совпадает с полученным(и) от API id: {allDeviceId}"); }
            Assert.Pass("Проверка на соответсвие тестовых данных и API данных id прибора учета пользователя ФЛ, авторизированного из под ЛК техподдержки с помощью 'ЛК ФЛ (Служба поддержки)'");
        }




        //Вызвывается после завершения каждого теста
        [TearDown]
        public void TearDown()
        {
            RowTableReportModel rowTable = new RowTableReportModel();
            rowTable.testData = JsonSerializer.Serialize(TestContext.CurrentContext.Test.Arguments[0]!); //Аргуметы теста
            rowTable.numberTest = TestContext.CurrentContext.Test.Arguments[1]!.ToString()!; //Номер теста
            rowTable.parentClass = TestContext.CurrentContext.Test.MethodName!; //Полное имя теста с параметрами
            rowTable.result = TestContext.CurrentContext.Result.Outcome.ToString(); //Результат теста
            rowTable.timeExecution = TestExecutionContext.CurrentContext.Duration.ToString() + " сек."; //Длительность теста
            rowTable.message = TestContext.CurrentContext.Result.Message; //Ошибка теста или описание действия (Если есть)
            GlobalMethod.ListRowTableReport.Add(rowTable);
        }

        //Выполняется после выполнения всех тестов
        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            string documentPath = GlobalMethod.GetAppSetting().PathReportTest;
            // Создать экземпляр HTML-документа
            HtmlDocument document = new HtmlDocument();
            // Загружаем html документ
            document.Load(documentPath);

            // Создаем тело таблицы
            HtmlNode newBody = document.CreateElement("tbody");
            foreach (RowTableReportModel rowTableReport in GlobalMethod.ListRowTableReport)
            {
                // Создаем строку таблицы
                HtmlNode newRow = document.CreateElement("tr");
                if (rowTableReport.result == "Passed"){ newRow.SetAttributeValue("style", "color:green;"); }
                if (rowTableReport.result == "Failed") { newRow.SetAttributeValue("style", "color:red; font-weight: bold;"); }
                //TODO:Проверить правильное название Ignore
                if (rowTableReport.result == "Ignore") { newRow.SetAttributeValue("style", "color:yellow;"); }

                // Создаем ячейки таблицы
                HtmlNode numberTestCell = document.CreateElement("td");
                HtmlNode parentClassCell = document.CreateElement("td");
                HtmlNode resultCell = document.CreateElement("td");
                HtmlNode timeExecutionCell = document.CreateElement("td");
                HtmlNode messageCell = document.CreateElement("td");
                HtmlNode inputDataCell = document.CreateElement("td");
                HtmlNode screenshotCell = document.CreateElement("td");

                // Создаем значение для ячеек
                HtmlTextNode numberTestValue = document.CreateTextNode(rowTableReport.numberTest);
                HtmlTextNode parentClassValue = document.CreateTextNode(rowTableReport.parentClass);
                HtmlTextNode resultValue = document.CreateTextNode(rowTableReport.result);
                HtmlTextNode timeExecutionValue = document.CreateTextNode(rowTableReport.timeExecution);
                HtmlTextNode messageValue = document.CreateTextNode(rowTableReport.message);
                HtmlTextNode inputDataValue = document.CreateTextNode(rowTableReport.testData);

                //Добавляем в ячейки значения
                numberTestCell.AppendChild(numberTestValue);
                parentClassCell.AppendChild(parentClassValue);
                resultCell.AppendChild(resultValue);
                timeExecutionCell.AppendChild(timeExecutionValue);
                messageCell.AppendChild(messageValue);
                inputDataCell.AppendChild(inputDataValue);

                //Добавляем в строку ячейки
                newRow.AppendChild(numberTestCell);
                newRow.AppendChild(parentClassCell);
                newRow.AppendChild(resultCell);
                newRow.AppendChild(timeExecutionCell);
                newRow.AppendChild(messageCell);
                newRow.AppendChild(inputDataCell);

                //Проверяем существует ли файл снимка ошибки 
                if (File.Exists($"{GlobalMethod.GetAppSetting().ScreenshotFailedTest}\\{rowTableReport.numberTest}.jpg"))
                {
                    HtmlNode screenshotLink = document.CreateElement("a");
                    HtmlTextNode screenshotValue = document.CreateTextNode("Смотреть");
                    screenshotLink.SetAttributeValue("href", $"{GlobalMethod.GetAppSetting().ScreenshotFailedTest}\\{rowTableReport.numberTest}.jpg");
                    screenshotLink.AppendChild(screenshotValue);
                    screenshotCell.AppendChild(screenshotLink);
                    newRow.AppendChild(screenshotCell);
                }
                else
                {
                    HtmlTextNode screenshotValue = document.CreateTextNode("-");
                    screenshotCell.AppendChild(screenshotValue);
                    newRow.AppendChild(screenshotCell);
                }

                //Добавляем в тело таблицы строку
                newBody.AppendChild(newRow);
            }

            // Ищем старую таблицу
            HtmlNode oldTable = document.DocumentNode.SelectSingleNode("//table");

            //Добавляем к таблице новое тело
            oldTable.AppendChild(newBody);

            //Ищем секцию с началом времени тестирования
            HtmlNode sectionInfoTest = document.DocumentNode.SelectSingleNode("//div[@id='infoTest']");

            try
            {
                // Ищем старое время начала тестирования
                HtmlNode oldInfoTest = document.DocumentNode.SelectSingleNode("//div[@id='infoTest']//p");
                //Удаляем старое время начала тестирования
                oldInfoTest.Remove();
            }
            catch
            {

            }

            //Создаем новое время начала тестирования
            HtmlNode newInfoTest = document.CreateElement("p");

            //Создаем текст заголовка
            HtmlNode newValueInfoTest = document.CreateTextNode($"Всего тестов запущено: {TestContext.CurrentContext.Result.FailCount + TestContext.CurrentContext.Result.PassCount} | Успешно: {TestContext.CurrentContext.Result.PassCount} | Провалено: {TestContext.CurrentContext.Result.FailCount} | Процент пройденных {TestContext.CurrentContext.Result.PassCount * 100 / (TestContext.CurrentContext.Result.FailCount + TestContext.CurrentContext.Result.PassCount)}% | Тестирование завершено: {DateTime.Now}:{DateTime.Now.Millisecond}");
            //Добавляем заголовку новое значение
            newInfoTest.AppendChild(newValueInfoTest);
            //Добавляем новый заголовок начала времени тестирования
            sectionInfoTest.AppendChild(newInfoTest);
            //Сохраняем файл
            document.Save(documentPath);
        }

    }
}
