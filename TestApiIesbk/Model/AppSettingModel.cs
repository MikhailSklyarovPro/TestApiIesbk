using System.Text.Json.Serialization;

namespace TestApiIesbk.Model
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
    }
}
