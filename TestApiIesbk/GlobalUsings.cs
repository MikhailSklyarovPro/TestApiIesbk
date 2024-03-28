global using NUnit.Framework;
using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;

class GlobalMethod
{
    //ƒобавл€ем файлы настроек в переменную дл€ доступа из всего приложени€ (путь до родительского каталога по умолчанию переопределили на свой)
    public static IConfigurationRoot config = new ConfigurationBuilder()
        .SetBasePath("C:\\Users\\SklyarovMD\\source\\repos\\TestApiIesbk\\TestApiIesbk")
        .AddJsonFile("testdataCommon.json")
        .Build();

    ////ƒобавл€ем тестовые данные дл€ ‘Ћ в переменную дл€ доступа из всего приложени€ (путь до родительского каталога по умолчанию переопределили на свой)
    //public static IConfigurationRoot testDataFL = new ConfigurationBuilder()
    //    .SetBasePath("C:\\Users\\SklyarovMD\\source\\repos\\TestApiIesbk\\TestApiIesbk\\TestSuite\\Fl")
    //    .AddJsonFile("testdata.json")
    //    .Build();



    //ѕровер€ет есть ли элемент на странице или нет
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

    //ќжидает загрузки до timeout и если элемент нашелс€ то возвращает его иначе вызывает ошибку
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

    //ќжидает пока элемент станет кликабельным и кликает н него. ≈сли истечет срок ожидани€ вернет ошибку
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

    //ќжидает пока элемент не станет интерактивным дл€ установки значени€ в него (Input).≈сли истечет срок ожидани€ вернет ошибку
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
        Assert.Fail($"Ќе удалось установаить значение {value} в тестовое поле!");
        return false;
    }

    //ќжидает пока текущий путь браузера не станет равным переданному.
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
}