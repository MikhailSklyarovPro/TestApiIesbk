using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using TestApiIesbk.Model;
using TestApiIesbk.Model.FL;

namespace TestApiIesbk.Controller
{
    public class FLController
    {
        //Получение тестовых данных ФЛ
        public static List<TestDataFL> GetTestData()
        {

            string jsonString = File.ReadAllText(GlobalMethod.GetAppSetting().pathTestDataFL);
            List<TestDataFL> testData = JsonSerializer.Deserialize<List<TestDataFL>>(jsonString)!;
            return testData;
        }

        //Вход пользователя и получение токена авторизации
        public static string LoginUser(string baseUrl, string login, string password)
        {
            //Параметры запроса(метод апи)
            string urlParametrs = "auth/login";

            //Основной путь  
            string URL = baseUrl + urlParametrs;
            //Создаем экземпляр класса для отправки запросов к веб-ресурсам
            HttpClient client = new HttpClient();
            //Задаем базовый путь до веб-ресурса
            client.BaseAddress = new Uri(URL);

            //Создаем модель для отправки тела запроса
            SenderModelLogin model = new SenderModelLogin();
            model.account = login;
            model.password = password;

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
                Assert.Fail($"Действие: Вход пользователя ЛК ФЛ. Результат: Не удалось войти! код ошибки: {errorModel.code}, текст ошибки: {errorModel.message}");
            }
            client.Dispose();
            //Возвращаем токен авторизации или пустую строку
            return token;
        }

        //Вход техподдержки и получение токена авторизации
        public static string LoginUserTech(string baseUrl, string login, string password)
        {
            //Параметры запроса(метод апи)
            string urlParametrs = "service/tech/auth/login";

            //Основной путь  
            string URL = baseUrl + urlParametrs;
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
                //Записываем ответ в виде json
                string jsonResult = response.Content.ReadAsStringAsync().Result;
                //Записываем ответ от сервера в модель 
                ServerResponseErrorModel errorModel = JsonSerializer.Deserialize<ServerResponseErrorModel>(jsonResult)!;
                //Выводим ошибку от сервера
                Assert.Fail($"Действие: Вход техподдержки в ЛК ФЛ. Результат: Не удалось войти! код ошибки: {errorModel.code}, текст ошибки: {errorModel.message}");
            }
            client.Dispose();
            //Возвращаем токен авторизации или пустую строку
            return token;
        }

        //Вход пользователя из под ЛК техподдержки и получение токена авторизации
        public static string LoginUserFromTech(string baseUrl, string login, string password, string tokenTech)
        {
            //Параметры запроса(метод апи)
            string urlParametrs = "auth/login_tech";

            //Основной путь  
            string URL = baseUrl + urlParametrs;
            //Создаем экземпляр класса для отправки запросов к веб-ресурсам
            HttpClient client = new HttpClient();
            //Задаем базовый путь до веб-ресурса
            client.BaseAddress = new Uri(URL);

            //Создаем модель для отправки тела запроса
            SenderModelLogin model = new SenderModelLogin();
            model.account = login;
            model.password = password;

            //Серилизуем модель в json
            string json = JsonSerializer.Serialize(model);

            //Добавляем к телу запроса
            StringContent data = new StringContent(json, Encoding.UTF8, "application/json");

            // Добавляем в заголовок куки с токеном авторизации
            client.DefaultRequestHeaders.Add("Cookie", tokenTech);

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
                //Записываем ответ в виде json
                string jsonResult = response.Content.ReadAsStringAsync().Result;
                //Записываем ответ от сервера в модель 
                ServerResponseErrorModel errorModel = JsonSerializer.Deserialize<ServerResponseErrorModel>(jsonResult)!;
                //Выводим ошибку от сервера
                Assert.Fail($"Действие: Вход пользователя в ЛК из под ЛК техподдержки. Результат: Не удалось войти! код ошибки: {errorModel.code}, текст ошибки: {errorModel.message}");
            }
            client.Dispose();
            //Возвращаем токен авторизации или пустую строку
            return token;
        }

        //Получаем все приборы учета пользователя по токену авторизации
        public static List<ServerResponseDevicesFLModel> GetDevices(string baseUrl, string token)
        {
            //Полученные данные
            List<ServerResponseDevicesFLModel> devices = new List<ServerResponseDevicesFLModel>();
            //Параметры запроса(метод апи)
            string urlParametrs = "account/devices";
            //Основной путь  
            string URL = baseUrl;
            //Создаем экземпляр класса для отправки запросов к веб-ресурсам
            HttpClient client = new HttpClient();
            //Задаем базовый путь до веб-ресурса
            client.BaseAddress = new Uri(URL);

            // Добавляем заголовки к нашему запросу
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            // Добавляем в заголовок куки с токеном авторизации
            client.DefaultRequestHeaders.Add("Cookie", token);

            //Делаем запрос к веб-ресурсу по пути URL+urlParameters. Result возращает результат выполнения запроса.
            HttpResponseMessage response = client.GetAsync(urlParametrs).Result;
            //Делаем проверку если ответ пришел усешный 200-300
            if (response.IsSuccessStatusCode)
            {
                //Ожидаем пока не получим значение. После получения читаем ответ как строку (в итоге будет json в виде строки)
                string jsonResult = response.Content.ReadAsStringAsync().Result;

                devices = JsonSerializer.Deserialize<List<ServerResponseDevicesFLModel>>(jsonResult)!;
            }
            else
            {
                //Записывает ответ в виде json
                string jsonResult = response.Content.ReadAsStringAsync().Result;
                //Записываем ответ от сервера в модель 
                ServerResponseErrorModel errorModel = JsonSerializer.Deserialize<ServerResponseErrorModel>(jsonResult)!;
                //Выводим ошибку от сервера
                Assert.Fail($"Действие: Получение приборов учета пользователя, зайденного в ЛК ФЛ из под ЛК техподдержки. Результат: Произошла ошибка! код ошибки: {errorModel.code}, текст ошибки: {errorModel.message} URL:{URL}");
            }
            client.Dispose();
            return devices;
        }

        //Получение данных пользователя
        public static ServerResponseUserInfoFLModel GetDataUser(string baseUrl, string token)
        {
            //Полученные данные
            ServerResponseUserInfoFLModel userInfo = new ServerResponseUserInfoFLModel();
            //Параметры запроса(метод апи)
            string urlParametrs = "user/info";
            //Основной путь  
            string URL = baseUrl;
            //Создаем экземпляр класса для отправки запросов к веб-ресурсам
            HttpClient client = new HttpClient();
            //Задаем базовый путь до веб-ресурса
            client.BaseAddress = new Uri(URL);

            // Добавляем заголовки к нашему запросу
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            // Добавляем в заголовок куки с токеном авторизации
            client.DefaultRequestHeaders.Add("Cookie", token);

            //Делаем запрос к веб-ресурсу по пути URL+urlParameters. Result возращает результат выполнения запроса.
            HttpResponseMessage response = client.GetAsync(urlParametrs).Result;
            //Делаем проверку если ответ пришел усешный 200-300
            if (response.IsSuccessStatusCode)
            {
                //Ожидаем пока не получим значение. После получения читаем ответ как строку (в итоге будет json в виде строки)
                string jsonResult = response.Content.ReadAsStringAsync().Result;
                userInfo = JsonSerializer.Deserialize<ServerResponseUserInfoFLModel>(jsonResult)!;
            }
            else
            {
                //Записываем ответ в виде json
                string jsonResult = response.Content.ReadAsStringAsync().Result;
                //Записываем ответ от сервера в модель 
                ServerResponseErrorModel errorModel = JsonSerializer.Deserialize<ServerResponseErrorModel>(jsonResult)!;
                //Выводим ошибку от сервера
                Assert.Fail($"Действие: Получение данных пользователя (баланса), зайденного в ЛК ФЛ из под ЛК техподдержки. Результат: Произошла ошибка! код ошибки: {errorModel.code}, текст ошибки: {errorModel.message} URL:{URL}");
            }
            client.Dispose();
            return userInfo;
        }
    }
}
