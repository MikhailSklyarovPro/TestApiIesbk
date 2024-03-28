using System.Net.Http.Headers;

namespace TestApiIesbk.Controller
{
    public class FLController
    {

        //Вход пользователя за техподдержку с логином и паролем
        public void LoginTech()
        {

        }



        //Вход пользователя с логином и паролем 
        public void Login() 
        {

        }



        //Получение данных о пользователе
        public void GetUserInfo()
        {
            string URL = GlobalMethod.config["ApiUrl"]!; //Путь до апи
            string urlParameters = "/user/info"; //Доп параметры 
            HttpClient client = new HttpClient(); //Создаем экземпляр класса для отправки запросов к веб-ресурсам
            client.BaseAddress = new Uri(URL); //Задаем базовый путь до веб-ресурса

            // Добавляем заголовки к нашему запросу
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")); //Добавляем какой тип ответа ожидаем - application/json

            HttpResponseMessage response = client.GetAsync(urlParameters).Result;  //Делаем асинхронный запрос к веб-ресурсу по пути URL+urlParameters. Result возращает результат выполнения запроса.
            if (response.IsSuccessStatusCode) //Делаем проверку если ответ пришел усешный 200-300
            {
                var result = response.Content.ReadAsStringAsync().Result; //ожидаем пока не получим значение. После получения читаем ответ как строку (в итоге будет json в виде строки)
                Assert.Fail(result);
            }
            else
            {
                Assert.Fail("ошибка");
            }
            client.Dispose();
        }



        //Получение данных о приборах учета
        public void GetDeviceInfo()
        {

        }
    }
}
