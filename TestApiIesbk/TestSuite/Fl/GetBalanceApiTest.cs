using TestApiIesbk.Controller;
using TestApiIesbk.Model;

namespace TestApiIesbk.TestSuite.Fl
{
    public class GetBalanceApiTest
    {
        //Метод, который возвращает из набора тестовых данных (json файл) логин, пароль и id прибора учета
        private static IEnumerable<TestCaseData> GetTestData()
        {
            List<TestData> testData = FLController.GetTestData();
            //Перебераем в цикле все вложенные элементы в секцию suite
            foreach (TestData item in testData)
            {
                string login = item.testSettings.login;
                string password = item.testSettings.authenticator;
                string balance = item.testSettings.balance;

                //Отправляем полученные данные на вход теста
                yield return new TestCaseData(login, password, balance);
            }
        }

        [Test, TestCaseSource(nameof(GetTestData))]
        public void CheckDataUser(string login, string password, string balance)
        {
            //Получаем данные пользователя от API
            ServerResponseUserInfoModel userData = FLController.GetDataUser(FLController.LoginUser(login, password));
            //Проверка полученных данных от API на соответсвие тестовых данных в json файле
            if (balance != userData.account.balance.ToString()) { Assert.Fail($"Баланс: {balance} в тестовых данных не совпадает с полученным от API балансом: {userData.account.balance}"); }
        }
    }
}
