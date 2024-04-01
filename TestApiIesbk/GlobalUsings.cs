global using NUnit.Framework;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using OpenQA.Selenium;
using System.Text;
using TestApiIesbk.Model;

class GlobalMethod
{
    //Добавляем файлы настроек в переменную для доступа из всего приложения (путь до родительского каталога по умолчанию переопределили на свой)
    public static IConfigurationRoot config = new ConfigurationBuilder()
        .SetBasePath("C:\\Users\\SklyarovMD\\source\\repos\\TestApiIesbk\\TestApiIesbk")
        .AddJsonFile("testdatacommon.json")
        .Build();

    //Добавляем тестовые данные для ФЛ в переменную для доступа из всего приложения(путь до родительского каталога по умолчанию переопределили на свой)
    public static IConfigurationRoot testDataFL = new ConfigurationBuilder()
        .SetBasePath("C:\\Users\\SklyarovMD\\source\\repos\\TestApiIesbk\\TestApiIesbk")
        .AddJsonFile("testdataFL.json")
        .Build();



    //Проверяет есть ли элемент на странице или нет
    public static bool IsElementExists(By locator, IWebDriver webDriver)
    {
        try
        {
            webDriver.FindElement(locator);
            return true;
        }
        catch (Exception)
        {
            return false;
        }

    }

    //Ожидает загрузки до timeout и если элемент нашелся то возвращает его иначе вызывает ошибку
    public static bool WaitFindElement(By locator, IWebDriver webDriver, int timeout = 10)
    {
        double countTime = 0;
        while (countTime < timeout)
        {
            if (!IsElementExists(locator, webDriver))
            {
                countTime = countTime + 0.5;
                Thread.Sleep(500);
            }
            else
            {
                return true;
            }
        }
        return false;
    }

    //Ожидает пока элемент станет кликабельным и кликает н него. Если истечет срок ожидания вернет ошибку
    public static bool WaitClick(IWebElement element, int timeout = 10)
    {
        double countTime = 0;
        while (countTime < timeout)
        {
            try
            {
                element.Click();
                return true;
            }
            catch (Exception)
            {
                countTime = countTime + 0.5;
                Thread.Sleep(500);
            }
        }
        return false;
    }

    //Ожидает пока элемент не станет интерактивным для установки значения в него (Input).Если истечет срок ожидания вернет ошибку
    public static bool WaitSendKey(By locator, IWebDriver webDriver, string value, int timeout = 10)
    {
        double countTime = 0;
        while (countTime < timeout)
        {
            try
            {
                webDriver.FindElement(locator).SendKeys(value);
                return true;
            }
            catch (Exception)
            {
                countTime = countTime + 0.5;
                Thread.Sleep(500);
            }
        }
        Assert.Fail($"Не удалось установаить значение {value} в тестовое поле!");
        return false;
    }

    //Ожидает пока текущий путь браузера не станет равным переданному.
    public static bool WaitLodingPage(IWebDriver webDriver, string url, int timeout = 15)
    {
        double countTime = 0;
        while (countTime < timeout)
        {
            if (webDriver.Url == url)
            {
                return true;
            }
        }
        return false;
    }



    //-----------Для тестирования api-----------//

    //Вход пользователя и получение токена авторизации
    public static string LoginUser(string login, string password)
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
        SenderModelLogin model = new SenderModelLogin();
        model.account = login;
        model.password = password;

        //Серилизуем модель в json
        string json = JsonConvert.SerializeObject(model);
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
            ServerResponseErrorModel errorModel = JsonConvert.DeserializeObject<ServerResponseErrorModel>(jsonResult)!;
            //Выводим ошибку от сервера
            Assert.Fail($"Не удалось войти! код ошибки: {errorModel.code}, текст ошибки: {errorModel.message}");
        }
        client.Dispose();
        //Возвращаем токен авторизации или пустую строку
        return token;
    }

}