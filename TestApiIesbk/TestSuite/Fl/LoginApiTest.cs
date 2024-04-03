using TestApiIesbk.Controller;
using TestApiIesbk.Model;

namespace TestApiIesbk.TestSuite.Fl
{
    public class LoginApiTest
    {
        //Метод, который возвращает данные из тестового набора (json файла)
        public static IEnumerable<TestCaseData> GetParametrs()
        {
            List<TestData> testData = FLController.GetTestData();
            //Перебераем в цикле все вложенные элементы в секцию suite
            foreach (TestData item in testData)
            {
                string login = item.testSettings.login;
                string password = item.testSettings.authenticator;

                //Отправляем полученные данные на вход теста
                yield return new TestCaseData(login, password);
            }
        }

        //Метод выполняет тест. Принимает в параметры тестовые данные.
        [Test, TestCaseSource(nameof(GetParametrs))]
        public void LoginUser(string login, string password)
        {
            FLController.LoginUser(login, password);
        }
    }
}
