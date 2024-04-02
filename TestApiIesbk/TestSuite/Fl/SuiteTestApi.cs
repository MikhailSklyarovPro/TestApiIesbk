//using Microsoft.Extensions.Configuration;
//using System.Text.Json;
//using OpenQA.Selenium;
//using TestApiIesbk.Controller;
//using TestApiIesbk.Model;
//using NUnit.Framework.Interfaces;
//using System;
//using static TestApiIesbk.Model.ServerResponseUserInfoModel;

//namespace TestApiIesbk.TestSuite.Fl
//{
//    public class SuiteTestApi
//    {

//        public class TestApiFL
//        {
//            private IWebDriver _webDriver;
//            //Метод, который возвращает данные из тестового набора (json файла)
//            public static IEnumerable<TestCaseData> GetTestData()
//            {

//                string jsonString = File.ReadAllText("C:\\Users\\SklyarovMD\\source\\repos\\TestApiIesbk\\TestApiIesbk\\testdataFl.json");
//                List<TestData> testData = JsonSerializer.Deserialize<List<TestData>>(jsonString)!;
//                //Перебераем в цикле все вложенные элементы в секцию suite
//                foreach (TestData item in testData)
//                {
//                    //Получаем значения из json
//                    string url = item.url;
//                    string type = item.type;
//                    string techLogin = item.techLogin;
//                    string techPassword = item.techPassword;
//                    string login = item.testSettings.login;
//                    string password = item.testSettings.authenticator;
//                    string balance = item.testSettings.balance;
//                    string deviceId = item.testSettings.deviceId;
//                    //Отправляем полученные данные на вход теста
//                    yield return new TestCaseData(url, type, techLogin, techPassword, login, password, balance, deviceId);
//                }
//            }


//            //Метод выполняет тест. Принимает в параметры тестовые данные.
//            [Test, TestCaseSource(nameof(GetTestData))]
//            public void LoginUser(string login, string password)
//            {
//                FLController.LoginUser(login, password);
//            }



//            [Test, TestCaseSource(nameof(GetTestData))]
//            public void CheckDataUser(string url, string type, string techLogin, string techPassword, string login, string password, string balance, string deviceId)
//            {
//                //Получаем данные пользователя от API
//                ServerResponseUserInfoModel userData = FLController.GetDataUser(FLController.LoginUser(login, password));
//                //Проверка полученных данных от API на соответсвие тестовых данных в json файле
//                if (balance != userData.account.balance.ToString()) { Assert.Fail($"Баланс: {balance} в тестовых данных не совпадает с полученным от API балансом: {userData.account.balance}"); }
//            }



//            [Test, TestCaseSource(nameof(GetTestData))]
//            public void CheckIdDevice(string url, string type, string techLogin, string techPassword, string login, string password, string balance, string deviceId)
//            {
//                //Получаем данные пользователя от API
//                List<ServerResponseDevicesModel> devices = FLController.GetDevices(FLController.LoginUser(login, password));

//                //Прибор учета с переданным id найден?
//                string foundDeviceId = "";
//                //Список id всех приборов учета через запятую
//                string allDeviceId = "";
//                //Перебираем все приборы учета принадлежащие пользователю
//                foreach (ServerResponseDevicesModel device in devices)
//                {
//                    if (device.Id.ToString() == deviceId) { foundDeviceId = device.Id.ToString(); }
//                    allDeviceId = allDeviceId + ", " + " " + device.Id;
//                }
//                //Проверка полученных данных от API на соответсвие тестовых данных в json файле
//                if (foundDeviceId == "") { Assert.Fail($"Прибор учета с id: {deviceId} в тестовых данных не совпадает с полученным(и) от API id: {allDeviceId}"); }
//            }
//        }
//    }
//}
