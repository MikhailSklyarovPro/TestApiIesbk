global using NUnit.Framework;
using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;

class GlobalMethod
{
    //��������� ����� �������� � ���������� ��� ������� �� ����� ���������� (���� �� ������������� �������� �� ��������� �������������� �� ����)
    public static IConfigurationRoot config = new ConfigurationBuilder()
        .SetBasePath("C:\\Users\\SklyarovMD\\source\\repos\\TestApiIesbk\\TestApiIesbk")
        .AddJsonFile("testdataCommon.json")
        .Build();

    ////��������� �������� ������ ��� �� � ���������� ��� ������� �� ����� ���������� (���� �� ������������� �������� �� ��������� �������������� �� ����)
    //public static IConfigurationRoot testDataFL = new ConfigurationBuilder()
    //    .SetBasePath("C:\\Users\\SklyarovMD\\source\\repos\\TestApiIesbk\\TestApiIesbk\\TestSuite\\Fl")
    //    .AddJsonFile("testdata.json")
    //    .Build();



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
}