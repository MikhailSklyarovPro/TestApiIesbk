using System.Text;
using System.Text.Json;
using TestApiIesbk.Model;
using TestApiIesbk.Model.Common;

namespace TestApiIesbk.Controller
{
    public class CommonController
    {

        //Получение общих тестовых данных
        public static List<TestDataCommon> GetTestData()
        {
            //Читаем из json
            string jsonString = File.ReadAllText(GlobalMethod.GetAppSetting().PathTestDataCommon);
            //Десериализуем в модель
            List<TestDataCommon> testData = JsonSerializer.Deserialize<List<TestDataCommon>>(jsonString)!;
            return testData;
        }

        //Вход техподдержки и получение токена авторизации
        public static string LoginCommonTech(string login, string password)
        {
            //Параметры запроса(метод апи)
            string urlParametrs = "service/tech/auth/login";

            //Основной путь  
            string URL = GlobalMethod.GetAppSetting().ApiUrlUL + urlParametrs;
            //Создаем экземпляр класса для отправки запросов к веб-ресурсам
            HttpClient client = new HttpClient();
            //Задаем базовый путь до веб-ресурса
            client.BaseAddress = new Uri(URL);

            //Создаем модель для отправки тела запроса
            SenderModelLoginTech model = new SenderModelLoginTech();
            model.Login = login;
            model.Password = password;

            //Серилизуем модель в json
            string json = JsonSerializer.Serialize(model);

            //Добавляем к телу запроса
            StringContent data = new StringContent(json, Encoding.UTF8, "application/json");

            //Делаем запрос к веб-ресурсу по пути URL+urlParameters. Result возращает результат выполнения запроса.
            HttpResponseMessage response = client.PostAsync(URL, data).Result;

            //Будет хранит токен авторизации
            string token = "";

            //Делаем проверку если ответ пришел усешный 200-300
            if (response.IsSuccessStatusCode)
            {
                //Записываем токен авторизации пришедший от сервера в перемнную
                token = response.Headers.GetValues("Set-Cookie").First();
            }
            else
            {
                //Записывает ответ в виде json
                string jsonResult = response.Content.ReadAsStringAsync().Result;
                //Записываем ответ от сервера в модель 
                ServerResponseErrorModel errorModel = JsonSerializer.Deserialize<ServerResponseErrorModel>(jsonResult)!;
                //Выводим ошибку от сервера
                Assert.Fail($"Действие: Вход техподдержки в ЛК. Результат: Не удалось войти! код ошибки: {errorModel.code}, текст ошибки: {errorModel.message}");
            }
            client.Dispose();
            //Возвращаем токен авторизации или пустую строку
            return token;
        }

        //Отправка тестового письма на email
        public static void CheckSendTestLetter(string tokenUser, string email)
        {
            //Параметры запроса(метод апи)
            string urlParametrs = "service/tech/check/email";

            //Основной путь  
            string URL = GlobalMethod.GetAppSetting().ApiUrlUL + urlParametrs;
            //Создаем экземпляр класса для отправки запросов к веб-ресурсам
            HttpClient client = new HttpClient();
            //Задаем базовый путь до веб-ресурса
            client.BaseAddress = new Uri(URL);

            //Создаем модель для отправки тела запроса
            EmailModel model = new EmailModel();
            model.email = email;

            //Серилизуем модель в json
            string json = JsonSerializer.Serialize(model);

            //Добавляем к телу запроса
            StringContent data = new StringContent(json, Encoding.UTF8, "application/json");
            // Добавляем в заголовок куки с токеном авторизации
            client.DefaultRequestHeaders.Add("Cookie", tokenUser);

            //Делаем запрос к веб-ресурсу по пути URL+urlParameters. Result возращает результат выполнения запроса.
            HttpResponseMessage response = client.PostAsync(URL, data).Result;

            //Делаем проверку если ответ пришел усешный 200-300
            if (!response.IsSuccessStatusCode)
            {
                //Ожидаем пока не получим значение. После получения читаем ответ как строку (в итоге будет json в виде строки)
                string jsonResult = response.Content.ReadAsStringAsync().Result;
                //Записываем ответ от сервера в модель 
                ServerResponseErrorModel errorModel = JsonSerializer.Deserialize<ServerResponseErrorModel>(jsonResult)!;
                //Выводим ошибку от сервера
                Assert.Fail($"Действие: Отправка тестового письма на email: {email}. Результат: Не удалось отправить письмо! Код ошибки: {errorModel.code}, текст ошибки: {errorModel.message}");
            }
            client.Dispose();
        }


        //Отправка тестового sms на телефон
        public static void SendTestSms(string tokenTech, string phone)
        {
            //Параметры запроса(метод апи)
            string urlParametrs = "service/tech/check/sms";

            //Основной путь  
            string URL = GlobalMethod.GetAppSetting().ApiUrlUL + urlParametrs;
            //Создаем экземпляр класса для отправки запросов к веб-ресурсам
            HttpClient client = new HttpClient();
            //Задаем базовый путь до веб-ресурса
            client.BaseAddress = new Uri(URL);

            //Создаем модель для отправки тела запроса
            PhoneModel model = new PhoneModel();
            model.phone = phone;

            //Серилизуем модель в json
            string json = JsonSerializer.Serialize(model);

            //Добавляем к телу запроса
            StringContent data = new StringContent(json, Encoding.UTF8, "application/json");
            // Добавляем в заголовок куки с токеном авторизации
            client.DefaultRequestHeaders.Add("Cookie", tokenTech);

            //Делаем запрос к веб-ресурсу по пути URL+urlParameters. Result возращает результат выполнения запроса.
            HttpResponseMessage response = client.PostAsync(URL, data).Result;

            //Делаем проверку если ответ пришел усешный 200-300
            if (!response.IsSuccessStatusCode)
            {
                //Записываем ответ в виде json
                string jsonResult = response.Content.ReadAsStringAsync().Result;
                //Записываем ответ от сервера в модель 
                ServerResponseErrorModel errorModel = JsonSerializer.Deserialize<ServerResponseErrorModel>(jsonResult)!;
                //Выводим ошибку от сервера
                Assert.Fail($"Действие: Отправка тестового sms на телфон:{phone} Результат: Не удалось отправить sms! Код ошибки: {errorModel.code}, текст ошибки: {errorModel.message}");
            }
            client.Dispose();
        }
    }
}
