using System.Text.Json.Serialization;

namespace TestApiIesbk.Model
{
    public class AppSettingModel
    {
        [JsonPropertyName("pathReportTest")]
        public string pathReportTest { get; set; }

        [JsonPropertyName("screenshotFailedTest")]
        public string screenshotFailedTest { get; set; }

        [JsonPropertyName("pathTestDataFL")]
        public string pathTestDataFL { get; set; }

        [JsonPropertyName("pathTestDataUL")]
        public string pathTestDataUL { get; set; }

        [JsonPropertyName("pathTestDataCommon")]
        public string pathTestDataCommon { get; set; }

        [JsonPropertyName("listURL")]
        public List<ListURL> listURL { get; set; }

    }


    public class ListURL
    {
        [JsonPropertyName("name")]
        public string name { get; set; }

        [JsonPropertyName("urlApiFL")]
        public string urlApiFL { get; set; }

        [JsonPropertyName("urlApiUL")]
        public string urlApiUL { get; set; }

        [JsonPropertyName("urlSiteFL")]
        public string urlSiteFL { get; set; }

        [JsonPropertyName("urlSiteUL")]
        public string urlSiteUL { get; set; }
    }
}
