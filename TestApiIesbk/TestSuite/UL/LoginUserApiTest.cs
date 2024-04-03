//using TestApiIesbk.Controller;
//using TestApiIesbk.Model;

//namespace TestApiIesbk.TestSuite.UL
//{
//    public class LoginUserApiTest
//    {
//        //Метод, который возвращает из набора тестовых данных (json файл) логин, пароль
//        private static IEnumerable<TestCaseData> GetTestData()
//        {
//            List<TestDataFL> testData = UlController.GetTestData();
//            //Перебераем в цикле все вложенные элементы в секцию suite
//            foreach (TestDataFL item in testData)
//            {
//                string login = item.testSettings.login;
//                string password = item.testSettings.authenticator;

//                //Отправляем полученные данные на вход теста
//                yield return new TestCaseData(login, password);
//            }
//        }

//        //Метод выполняет тест. Принимает параметры из вне(логин, пароль).
//        [Test, TestCaseSource(nameof(GetTestData))]
//        public void LoginUser(string login, string password)
//        {
//            UlController.LoginUser(login, password);
//        }
//    }
//}
