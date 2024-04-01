using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using TestApiIesbk.Model;

namespace TestApiIesbk.TestSuite.Fl
{
    public class GetDataDeviceApiTest
    {
        //Метод, который возвращает из набора тестовых данных (json файл) логин, пароль и id прибора учета
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
                //Получаем значени секции id прибора учета
                string deviceId = section.GetSection("testsettings:device_id").Value!;
                //Отправляем полученные логин и пароль в качестве параметров на на выполнение теста
                yield return new TestCaseData(login, password, deviceId);
            }
        }

        //Получаем все приборы учета пользователя по токену авторизации
        public List<ServerResponseDevicesModel> GetDevices(string token)
        {

            //Полученные данные
            List<ServerResponseDevicesModel> devices = new List<ServerResponseDevicesModel>();
            //Параметры запроса(метод апи)
            string urlParametrs = "account/devices";
            //Основной путь  
            string URL = GlobalMethod.config["ApiUrl"]!;
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
                devices = JsonConvert.DeserializeObject<List<ServerResponseDevicesModel>>(jsonResult)!;
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
            return devices;
        }

        //Метод выполняет тест. Принимает параметры из вне(логин, пароль и id прибора учета).
        [Test, TestCaseSource(nameof(GetTestData))]
        public void CheckIdDevice(string login, string password, string deviceId)
        {
            //Получаем данные пользователя от API
            List<ServerResponseDevicesModel> devices = GetDevices(GlobalMethod.LoginUser(login, password));

            //Прибор учета с переданным id найден?
            string foundDeviceId = "";
            //Список id всех приборов учета через запятую
            string allDeviceId = "";
            //Перебираем все приборы учета принадлежащие пользователю
            foreach (ServerResponseDevicesModel device in devices)
            {
                if (device.Id == deviceId) { foundDeviceId = device.Id; }
                allDeviceId = $"{allDeviceId}, {device.Id}";
            }
            //Проверка полученных данных от API на соответсвие тестовых данных в json файле
            if (foundDeviceId == "") { Assert.Fail($"Прибор учета с id: {deviceId} в тестовых данных не совпадает с полученным(и) от API id: {allDeviceId}"); }
        }
    }
}
