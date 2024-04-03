using Aspose.Html;
using NUnit.Framework.Interfaces;
using TestApiIesbk.Controller;
using TestApiIesbk.Model;

namespace TestApiIesbk.TestSuite
{
    public class LoginApiTest
    {
        //Метод, который возвращает данные из тестового набора (json файла)
        public static IEnumerable<TestCaseData> GetParametrs()
        {
            List<TestDataFL> testData = FLController.GetTestData();
            //Перебераем в цикле все вложенные элементы в секцию suite
            foreach (TestDataFL item in testData)
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

        //Вызвывается после завершения теста
        [TearDown]
        public void TearDown()
        {
            RowTableReportModel rowTable = new RowTableReportModel();
            rowTable.numberTest = "1";
            rowTable.parentClass = "test";
            rowTable.result = "result";
            rowTable.timeExecution = "time";
            rowTable.message = "message";
            GlobalMethod.ListRowTableReport.Add(rowTable);

            //if (TestContext.CurrentContext.Result.Outcome == ResultState.Ignored)
            //{

            //}

            //if(TestContext.CurrentContext.Result.Outcome == ResultState.Failure)
            //{

            //}

            //if(TestContext.CurrentContext.Result.Outcome == ResultState.Success)
            //{

            //}
        }
    }
}
