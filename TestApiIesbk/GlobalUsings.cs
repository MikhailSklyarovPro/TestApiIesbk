global using NUnit.Framework;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using OpenQA.Selenium;
using System.Text;
using TestApiIesbk.Model;

class GlobalMethod
{
    //��������� ����� �������� � ���������� ��� ������� �� ����� ���������� (���� �� ������������� �������� �� ��������� �������������� �� ����)
    public static IConfigurationRoot config = new ConfigurationBuilder()
        .SetBasePath("C:\\Users\\SklyarovMD\\source\\repos\\TestApiIesbk\\TestApiIesbk")
        .AddJsonFile("testdatacommon.json")
        .Build();

    //��������� �������� ������ ��� �� � ���������� ��� ������� �� ����� ����������(���� �� ������������� �������� �� ��������� �������������� �� ����)
    public static IConfigurationRoot testDataFL = new ConfigurationBuilder()
        .SetBasePath("C:\\Users\\SklyarovMD\\source\\repos\\TestApiIesbk\\TestApiIesbk")
        .AddJsonFile("testdataFL.json")
        .Build();



    //��������� ���� �� ������� �� �������� ��� ���
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

    //������� �������� �� timeout � ���� ������� ������� �� ���������� ��� ����� �������� ������
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

    //������� ���� ������� ������ ������������ � ������� � ����. ���� ������� ���� �������� ������ ������
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

    //������� ���� ������� �� ������ ������������� ��� ��������� �������� � ���� (Input).���� ������� ���� �������� ������ ������
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
        Assert.Fail($"�� ������� ����������� �������� {value} � �������� ����!");
        return false;
    }

    //������� ���� ������� ���� �������� �� ������ ������ �����������.
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



    //-----------��� ������������ api-----------//

    //���� ������������ � ��������� ������ �����������
    public static string LoginUser(string login, string password)
    {
        //��������� �������(����� ���)
        string urlParametrs = "auth/login";
        //�������� ����  
        string URL = GlobalMethod.config["ApiUrl"]! + urlParametrs;
        //������� ��������� ������ ��� �������� �������� � ���-��������
        HttpClient client = new HttpClient();
        //������ ������� ���� �� ���-�������
        client.BaseAddress = new Uri(URL);

        //������� ������ ��� �������� ���� �������
        SenderModelLogin model = new SenderModelLogin();
        model.account = login;
        model.password = password;

        //���������� ������ � json
        string json = JsonConvert.SerializeObject(model);
        StringContent data = new StringContent(json, Encoding.UTF8, "application/json");

        //������ ������ � ���-������� �� ���� URL+urlParameters. Result ��������� ��������� ���������� �������.
        HttpResponseMessage response = client.PostAsync(URL, data).Result;

        //����� ������ ����� �����������
        string token = "";

        //������ �������� ���� ����� ������ ������� 200-300
        if (response.IsSuccessStatusCode)
        {
            //���������� ����� ����������� ��������� �� ������� � ���������
            token = response.Headers.GetValues("Set-Cookie").First();
        }
        else
        {
            //������� ���� �� ������� ��������. ����� ��������� ������ ����� ��� ������ (� ����� ����� json � ���� ������)
            string jsonResult = response.Content.ReadAsStringAsync().Result;
            //���������� ����� �� ������� � ������ 
            ServerResponseErrorModel errorModel = JsonConvert.DeserializeObject<ServerResponseErrorModel>(jsonResult)!;
            //������� ������ �� �������
            Assert.Fail($"�� ������� �����! ��� ������: {errorModel.code}, ����� ������: {errorModel.message}");
        }
        client.Dispose();
        //���������� ����� ����������� ��� ������ ������
        return token;
    }

}