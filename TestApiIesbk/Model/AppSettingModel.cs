using System.Text.Json.Serialization;

namespace TestIesbk
{
    public class AppSettingModel
    {
        [JsonPropertyName("ApiUrl")]
        public string ApiUrl { get; set; }

        [JsonPropertyName("SiteURLFL")]
        public string SiteURLFL { get; set; }

        [JsonPropertyName("SiteURLUL")]
        public string SiteURLUL { get; set; }

        [JsonPropertyName("SiteURLCommon")]
        public string SiteURLCommon { get; set; }

        [JsonPropertyName("PathReportTest")]
        public string PathReportTest { get; set; }

        [JsonPropertyName("ScreenshotFailedTest")]
        public string ScreenshotFailedTest { get; set; }

        [JsonPropertyName("PathTestDataFL")]
        public string PathTestDataFL { get; set; }

        [JsonPropertyName("PathTestDataUL")]
        public string PathTestDataUL { get; set; }

        [JsonPropertyName("PathTestDataCommon")]
        public string PathTestDataCommon { get; set; }

    }
}
