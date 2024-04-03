using Aspose.Html;
using NUnit.Framework.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApiIesbk.Model;

namespace TestApiIesbk.TestSuite
{
    [TestFixture]
    public class TestSuite
    {
        //Выполняется перед запуском всех тестов
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            try {
                string documentPath = GlobalMethod.GetAppSetting().PathReportTest;
                // Создать экземпляр HTML-документа
                HTMLDocument document = new HTMLDocument(documentPath);
                // Ищем старое тело таблицы
                var oldTbody = document.GetElementsByTagName("tbody").First();
                // Удаляем старое тело таблицы
                oldTbody.Remove();

                //Сохраняем файл
                document.Save(documentPath);
            }
            catch { }

        }

        //Метод, который возвращает данные из тестового набора (json файла) для ФЛ
        public static IEnumerable<TestCaseData> GetParametrs(string typeFice)
        {

            switch (typeFice)
            {
                case "FL":
                    break;
                case "UL":
                    break;
            }
            List<TestDataFL> testData = FLController.GetTestData();
            //Перебераем в цикле все вложенные элементы в секцию suite
            foreach (TestDataFL item in testData)
            {
                //Отправляем полученные данные на вход теста
                yield return new TestCaseData(item, ++GlobalMethod.numberTest);
            }
        }

        //Метод выполняет тест. Принимает в параметры тестовые данные.
        [Test, TestCaseSource(nameof(GetParametrs), new object[] { "FL" })]
        public void LoginUser(TestDataFL testData, int numberTest)
        {
            FLController.LoginUser(testData.testSettings.login, testData.testSettings.authenticator);
        }

        //Вызвывается после завершения каждого теста
        [TearDown]
        public void TearDown()
        {
            string message = TestContext.CurrentContext.Result.Message == "" ? "Успешно пройден" : TestContext.CurrentContext.Result.Message;
            
            RowTableReportModel rowTable = new RowTableReportModel();
            rowTable.numberTest = TestContext.CurrentContext.Test.ID;
            rowTable.parentClass = TestContext.CurrentContext.Test.FullName; //Полное имя теста с параметрами
            rowTable.result = TestContext.CurrentContext.Result.Outcome.ToString(); //Результат теста
            rowTable.timeExecution = TestExecutionContext.CurrentContext.Duration.ToString() + " сек."; 
            rowTable.message = message; //Ошибка теста (Если есть)
            GlobalMethod.ListRowTableReport.Add(rowTable);

           
            //TestContext.CurrentContext.Test.MethodName! - LoginUser
            //TestContext.CurrentContext.Test.ClassName! - TestApiIesbk.TestSuite.FlTestSuite
        }

        //Выполняется после выполнения всех тестов
        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            string documentPath = GlobalMethod.GetAppSetting().PathReportTest;
            // Создать экземпляр HTML-документа
            HTMLDocument document = new HTMLDocument(documentPath);

            // Создаем тело таблицы
            var newBody = document.CreateElement("tbody");
            foreach (RowTableReportModel rowTableReport in GlobalMethod.ListRowTableReport)
            {

                // Создаем строку таблицы
                var newRow = document.CreateElement("tr");
                // Создаем ячейки таблицы
                var numberTestCell = document.CreateElement("td");
                var parentClassCell = document.CreateElement("td");
                var resultCell = document.CreateElement("td");
                var timeExecutionCell = document.CreateElement("td");
                var messageCell = document.CreateElement("td");

                // Создаем значение для ячеек
                var numberTestValue = document.CreateTextNode(rowTableReport.numberTest);
                var parentClassValue = document.CreateTextNode(rowTableReport.parentClass);
                var resultValue = document.CreateTextNode(rowTableReport.result);
                var timeExecutionValue = document.CreateTextNode(rowTableReport.timeExecution);
                var messageValue = document.CreateTextNode(rowTableReport.message);

                //Добавляем в ячейки значения
                numberTestCell.AppendChild(numberTestValue);
                parentClassCell.AppendChild(parentClassValue);
                resultCell.AppendChild(resultValue);
                timeExecutionCell.AppendChild(timeExecutionValue);
                messageCell.AppendChild(messageValue);

                //Добавляем в строку ячейки
                newRow.AppendChild(numberTestCell);
                newRow.AppendChild(parentClassCell);
                newRow.AppendChild(resultCell);
                newRow.AppendChild(timeExecutionCell);
                newRow.AppendChild(messageCell);

                //Добавляем в тело таблицы строку
                newBody.AppendChild(newRow);
            }

            // Ищем старую таблицу
            var oldTable = document.GetElementsByTagName("table").First();

            //Добавляем к таблице новое тело
            oldTable.AppendChild(newBody);

            //Сохраняем файл
            document.Save(documentPath);
        }

    }
}
