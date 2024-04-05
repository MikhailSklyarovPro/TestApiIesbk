
using System.Text.Json;
using TestApiIesbk;

namespace TestIesbk
{
    public class CommonController
    {

        //Получение тестовых данных
        public static List<TestDataCommon> GetTestData()
        {

            string jsonString = File.ReadAllText(GlobalMethod.GetAppSetting().PathTestDataUL);
            List<TestDataCommon> testData = JsonSerializer.Deserialize<List<TestDataCommon>>(jsonString)!;
            return testData;
        }

        //Вход пользователя за техподдержку с логином и паролем
        public void LoginTech()
        {

        }



        //Вход пользователя с логином и паролем 
        public void Login()
        {

        }



        //Отправка тестового письма электронной почты на адрес 
        public void SendTestLetterEmail()
        {

        }



        //Отправка тестового смс-сообщения на телефон 
        public void SendTestLetterSms()
        {

        }
    }
}
