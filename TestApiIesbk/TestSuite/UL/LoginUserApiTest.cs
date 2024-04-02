//using Microsoft.Extensions.Configuration;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace TestApiIesbk.TestSuite.UL
//{
//    public class LoginUserApiTest
//    {
//        //Метод, который возвращает из набора тестовых данных (json файл) логин, пароль
//        private static IEnumerable<TestCaseData> GetTestData()
//        {
//            //Получаем из файла секцию suite 
//            IConfigurationSection valuesSection = GlobalMethod.testDataUL.GetSection("suite");

//            //Перебераем в цикле все вложенные элементы в секцию suite
//            foreach (IConfigurationSection section in valuesSection.GetChildren())
//            {
//                //Получаем значение секции логина
//                string login = section.GetSection("testsettings:login").Value!;
//                //Получаем значение секции пароля
//                string password = section.GetSection("testsettings:authenticator").Value!;
//                //Отправляем полученные логин и пароль в качестве параметров на на выполнение теста
//                yield return new TestCaseData(login, password);
//            }
//        }

//        //Метод выполняет тест. Принимает параметры из вне(логин, пароль).
//        [Test, TestCaseSource(nameof(GetTestData))]
//        public void LoginUserTech(string login, string password)
//        {
//            GlobalMethod.LoginUserTech(login, password);
//        }
//    }
//}
