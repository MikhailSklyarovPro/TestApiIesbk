using TestApiIesbk.Controller;
using TestApiIesbk.Model;

namespace TestApiIesbk.TestSuite
{
    public class GetDataDeviceApiTest
    {
        //Метод, который возвращает из набора тестовых данных (json файл) логин, пароль и id прибора учета
        private static IEnumerable<TestCaseData> GetTestData()
        {
            List<TestDataFL> testData = FLController.GetTestData();
            //Перебераем в цикле все вложенные элементы в секцию suite
            foreach (TestDataFL item in testData)
            {
                string login = item.testSettings.login;
                string password = item.testSettings.authenticator;
                string deviceId = item.testSettings.deviceId;

                //Отправляем полученные данные на вход теста
                yield return new TestCaseData(login, password, deviceId);
            }
        }

        //Метод выполняет тест. Принимает параметры из вне(логин, пароль и id прибора учета).
        [Test, TestCaseSource(nameof(GetTestData))]
        public void CheckIdDevice(string login, string password, string deviceId)
        {
            //Получаем данные пользователя от API
            List<ServerResponseDevicesModel> devices = FLController.GetDevices(FLController.LoginUser(login, password));

            //Прибор учета с переданным id найден?
            string foundDeviceId = "";
            //Список id всех приборов учета через запятую
            string allDeviceId = "";
            //Перебираем все приборы учета принадлежащие пользователю
            foreach (ServerResponseDevicesModel device in devices)
            {
                if (device.Id.ToString() == deviceId) { foundDeviceId = device.Id.ToString(); }
                allDeviceId = $"{allDeviceId}, {device.Id}";
            }
            //Проверка полученных данных от API на соответсвие тестовых данных в json файле
            if (foundDeviceId == "") { Assert.Fail($"Прибор учета с id: {deviceId} в тестовых данных не совпадает с полученным(и) от API id: {allDeviceId}"); }
        }
    }
}
