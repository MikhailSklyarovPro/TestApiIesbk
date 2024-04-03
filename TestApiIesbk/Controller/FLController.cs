using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using TestApiIesbk.Model;

namespace TestApiIesbk.Controller
{
    public class FLController
    {
        //Получение тестовых данных
        public static List<TestData> GetTestData()
        {

            string jsonString = File.ReadAllText("C:\\Users\\SklyarovMD\\source\\repos\\TestApiIesbk\\TestApiIesbk\\testdataFl.json");
            List<TestData> testData = JsonSerializer.Deserialize<List<TestData>>(jsonString)!;
            return testData;
        }

        //Вход пользователя и получение токена авторизации
        public static string LoginUser(string login, string password)
        {
            //Параметры запроса(метод апи)
            string urlParametrs = "auth/login";

            //Основной путь  
            string URL = GlobalMethod.GetAppSetting().ApiUrl + urlParametrs;
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
                //Ожидаем пока не получим значение. После получения читаем ответ как строку (в итоге будет json в виде строки)
                string jsonResult = response.Content.ReadAsStringAsync().Result;
                //Записываем ответ от сервера в модель 
                ServerResponseErrorModel errorModel = JsonSerializer.Deserialize<ServerResponseErrorModel>(jsonResult)!;
                //Выводим ошибку от сервера
                Assert.Fail($"Не удалось войти! код ошибки: {errorModel.code}, текст ошибки: {errorModel.message}");
            }
            client.Dispose();
            //Возвращаем токен авторизации или пустую строку
            return token;
        }

        //Получаем все приборы учета пользователя по токену авторизации
        public static List<ServerResponseDevicesModel> GetDevices(string token)
        {
            //Получаем настройки приложения
            string jsonString = File.ReadAllText(@"C:\Users\SklyarovMD\source\repos\TestApiIesbk\TestApiIesbk\appsetting.json");
            AppSettingModel setting = JsonSerializer.Deserialize<AppSettingModel>(jsonString)!;
            //Полученные данные
            List<ServerResponseDevicesModel> devices = new List<ServerResponseDevicesModel>();
            //Параметры запроса(метод апи)
            string urlParametrs = "account/devices";
            //Основной путь  
            string URL = setting.ApiUrl;
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

                //TODO: здесь ошибка. После преобразования получаеться пустой список
                devices = JsonSerializer.Deserialize<List<ServerResponseDevicesModel>>(jsonResult)!;
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
            return devices;
        }

        //Получение данных пользователя
        public static ServerResponseUserInfoModel GetDataUser(string token)
        {
            //Полученные данные
            ServerResponseUserInfoModel userInfo = new ServerResponseUserInfoModel();
            //Параметры запроса(метод апи)
            string urlParametrs = "user/info";
            //Основной путь  
            string URL = GlobalMethod.GetAppSetting().ApiUrl;
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
                userInfo = JsonSerializer.Deserialize<ServerResponseUserInfoModel>(jsonResult)!;
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
            return userInfo;
        }
    }
}
