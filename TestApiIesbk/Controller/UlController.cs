using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using TestApiIesbk;
using TestApiIesbk.UL;
using TestIesbk.FL;

namespace TestIesbk.UL
{
    public class ULController
    {
        //Получение тестовых данных
        public static List<TestDataUL> GetTestData()
        {

            string jsonString = File.ReadAllText(GlobalMethod.GetAppSetting().PathTestDataUL);
            List<TestDataUL> testData = JsonSerializer.Deserialize<List<TestDataUL>>(jsonString)!;
            return testData;
        }

        //Вход техподдержки и получение токена авторизации
        public static string LoginUserTech(string login, string password)
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
                //Ожидаем пока не получим значение. После получения читаем ответ как строку (в итоге будет json в виде строки)
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

        //Вход пользователя из под ЛК техподдержки и получение токена авторизации
        public static string LoginUserFromTech(string login, string password, string tokenTech)
        {
            //Параметры запроса(метод апи)
            string urlParametrs = "auth/login_tech";

            //Основной путь  
            string URL = GlobalMethod.GetAppSetting().ApiUrlUL + urlParametrs;
            //Создаем экземпляр класса для отправки запросов к веб-ресурсам
            HttpClient client = new HttpClient();
            //Задаем базовый путь до веб-ресурса
            client.BaseAddress = new Uri(URL);

            //Создаем модель для отправки тела запроса
            SenderModelLoginUL model = new SenderModelLoginUL();
            model.contract = login;
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
                //Ожидаем пока не получим значение. После получения читаем ответ как строку (в итоге будет json в виде строки)
                string jsonResult = response.Content.ReadAsStringAsync().Result;
                //Записываем ответ от сервера в модель 
                ServerResponseErrorModel errorModel = JsonSerializer.Deserialize<ServerResponseErrorModel>(jsonResult)!;
                //Выводим ошибку от сервера
                Assert.Fail($"Действие: Вход пользователя в ЛК ЮЛ из под ЛК техподдержки. Результат: Не удалось войти! код ошибки: {errorModel.code}, текст ошибки: {errorModel.message}");
            }
            client.Dispose();
            //Возвращаем токен авторизации или пустую строку
            return token;
        }

        //Получение данных по договору 
        public static List<ServerResponseContractInfoULModel> GetDataByContract(string token)
        {
            //Полученные данные
            List<ServerResponseContractInfoULModel> listContract = new List<ServerResponseContractInfoULModel>();
            //Параметры запроса(метод апи)
            string urlParametrs = "contracts";
            //Основной путь  
            string URL = GlobalMethod.GetAppSetting().ApiUrlUL;
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

                listContract = JsonSerializer.Deserialize<List<ServerResponseContractInfoULModel>>(jsonResult)!;
            }
            else
            {
                //Ожидаем пока не получим значение. После получения читаем ответ как строку (в итоге будет json в виде строки)
                string jsonResult = response.Content.ReadAsStringAsync().Result;
                //Записываем ответ от сервера в модель 
                ServerResponseErrorModel errorModel = JsonSerializer.Deserialize<ServerResponseErrorModel>(jsonResult)!;
                //Выводим ошибку от сервера
                Assert.Fail($"Произошла ошибка! код ошибки: {errorModel.code}, текст ошибки: {errorModel.message} URL:{URL}");
            }
            client.Dispose();
            return listContract;
        }


        //Получение данных о приборах учета
        public static List<ServerResponseDevicesULModel> GetInfoDevices(string contractId, string token)
        {
            //Полученные данные
            List<ServerResponseDevicesULModel> devices = new List<ServerResponseDevicesULModel>();
            //Параметры запроса(метод апи)
            string urlParametrs = $"contract/{contractId}/devices";
            //Основной путь  
            string URL = GlobalMethod.GetAppSetting().ApiUrlUL;
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

                devices = JsonSerializer.Deserialize<List<ServerResponseDevicesULModel>>(jsonResult)!;
            }
            else
            {
                //Ожидаем пока не получим значение. После получения читаем ответ как строку (в итоге будет json в виде строки)
                string jsonResult = response.Content.ReadAsStringAsync().Result;
                //Записываем ответ от сервера в модель 
                ServerResponseErrorModel errorModel = JsonSerializer.Deserialize<ServerResponseErrorModel>(jsonResult)!;
                //Выводим ошибку от сервера
                Assert.Fail($"Действие: Получение приборов учета пользователя, зайденного в ЛК ЮЛ из под ЛК техподдержки. Результат: Произошла ошибка! код ошибки: {errorModel.code}, текст ошибки: {errorModel.message} URL:{URL}");
            }
            client.Dispose();
            return devices;
        }
    }
}
