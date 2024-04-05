using System.Text;
using System.Text.Json;
using TestApiIesbk;

namespace TestIesbk
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


        //Вход пользователя за техподдержку с логином и паролем
        public void LoginTech()
        {

        }



        //Вход пользователя и получение токена авторизации
        public static string LoginUser(string login, string password)
        {
            //Параметры запроса(метод апи)
            string urlParametrs = "auth/login_tech";

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



        //Получение данных по договору 
        public void GetDataByContract()
        {
 
        }



        //Получение данных о приборах учета
        public void GetDeviceInfo()
        {

        }
    }
}
