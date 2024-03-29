using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using TestApiIesbk.Model;

namespace TestApiIesbk.TestSuite.Fl
{
    public class GetDataUserApiTest
    {
        //Метод, который возвращает из набора тестовых данных (json файл) логин, пароль и баланс
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
                //Получаем значени секции баланс
                string balance = section.GetSection("testsettings:balance").Value!;
                //Отправляем полученные логин и пароль в качестве параметров на на выполнение теста
                yield return new TestCaseData(login, password, balance);
            }
        }

        //Вход пользователя и получение токена авторизации
        public string LoginUserAsync(string login, string password)
        {
            //Параметры запроса(метод апи)
            string urlParametrs = "auth/login";
            //Основной путь  
            string URL = GlobalMethod.config["ApiUrl"]! + urlParametrs;
            //Создаем экземпляр класса для отправки запросов к веб-ресурсам
            HttpClient client = new HttpClient();
            //Задаем базовый путь до веб-ресурса
            client.BaseAddress = new Uri(URL); 

            //Создаем модель для отправки тела запроса
            SenderModel model = new SenderModel();
            model.account = login;
            model.password = password;

            //Серилизуем модель в json
            var json = JsonConvert.SerializeObject(model);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            //Делаем запрос к веб-ресурсу по пути URL+urlParameters. Result возращает результат выполнения запроса.
            var response = client.PostAsync(URL, data).Result;
            //Создаем модель в которую запишем ответ от сервера
            ServerResponseLoginModel modelResult = new ServerResponseLoginModel();
            //Делаем проверку если ответ пришел усешный 200-300
            if (response.IsSuccessStatusCode) 
            {
                //Ожидаем пока не получим значение. После получения читаем ответ как строку (в итоге будет json в виде строки)
                string jsonResult = response.Content.ReadAsStringAsync().Result;
                //Записываем ответ от сервера в модель 
                modelResult = JsonConvert.DeserializeObject<ServerResponseLoginModel>(jsonResult)!;
            }
            else
            {
                //Ожидаем пока не получим значение. После получения читаем ответ как строку (в итоге будет json в виде строки)
                string jsonResult = response.Content.ReadAsStringAsync().Result;
                //Записываем ответ от сервера в модель 
                ServerResponseErrorModel errorModel = JsonConvert.DeserializeObject<ServerResponseErrorModel>(jsonResult)!;
                //Выводим ошибку от сервера
                Assert.Fail($"Произошла ошибка! код ошибки: {errorModel.code}, текст ошибки: {errorModel.message}");
            }
            client.Dispose();
            //Возвращаем токен авторизации или пустую строку
            return modelResult.access_token;
        }

        //TODO:Получение данных пользователя
        public ServerResponseUserInfoModel GetDataUser(string token)
        {
            //Полученные данные
            ServerResponseUserInfoModel userInfo = new ServerResponseUserInfoModel();
            //Параметры запроса(метод апи)
            string urlParametrs = "user/info";
            //Основной путь  
            string URL = GlobalMethod.config["ApiUrl"]!;
            //Создаем экземпляр класса для отправки запросов к веб-ресурсам
            HttpClient client = new HttpClient();
            //Задаем базовый путь до веб-ресурса
            client.BaseAddress = new Uri(URL);

            // Добавляем заголовки к нашему запросу
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")); //Добавляем какой тип ответа ожидаем - application/json
            client.DefaultRequestHeaders.Add("Cookie", token);
            //TODO:Authorization


            //Делаем запрос к веб-ресурсу по пути URL+urlParameters. Result возращает результат выполнения запроса.
            HttpResponseMessage response = client.GetAsync(urlParametrs).Result;
            //Делаем проверку если ответ пришел усешный 200-300
            if (response.IsSuccessStatusCode)
            {
                //Ожидаем пока не получим значение. После получения читаем ответ как строку (в итоге будет json в виде строки)
                string jsonResult = response.Content.ReadAsStringAsync().Result;
                userInfo = JsonConvert.DeserializeObject<ServerResponseUserInfoModel>(jsonResult)!;
            }
            else
            {
                //Ожидаем пока не получим значение. После получения читаем ответ как строку (в итоге будет json в виде строки)
                string jsonResult = response.Content.ReadAsStringAsync().Result;
                //Записываем ответ от сервера в модель 
                ServerResponseErrorModel errorModel = JsonConvert.DeserializeObject<ServerResponseErrorModel>(jsonResult)!;
                //Выводим ошибку от сервера
                Assert.Fail($"Произошла ошибка! код ошибки: {errorModel.code}, текст ошибки: {errorModel.message} URL:{URL}");
            }
            client.Dispose();
            return userInfo;
        }



        //Метод выполняем тест. Принимает параметры из вне(логин, пароль и баланс).
        [Test, TestCaseSource(nameof(GetTestData))]
        public void CheckDataUser(string login, string password, string balance)
        {
            //Получаем данные пользователя от API
            ServerResponseUserInfoModel userData = GetDataUser(LoginUserAsync(login, password));
            //Проверка полученных данных от API на соответсвие тестовых данных в json файле
            //if (balance != "userData.balance") { Assert.Fail($"Баланс: {balance} в тестовых данных не совпадает с полученным от API балансом: userData.balance"); }
        }
    }


    public class SenderModel
    {
        public string? account { get; set; }
        public string? password { get; set; }
    }
}
