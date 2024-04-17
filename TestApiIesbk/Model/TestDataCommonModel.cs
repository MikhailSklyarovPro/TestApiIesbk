using System.Text.Json.Serialization;

namespace TestApiIesbk.Model.Common
{
    public class TestDataCommon
    {
        [JsonPropertyName("urlApi")]
        public string urlApi { get; set; }

        [JsonPropertyName("urlSite")]
        public string urlSite { get; set; }

        [JsonPropertyName("type")]
        public string type { get; set; }

        [JsonPropertyName("tech_login")]
        public string techLogin { get; set; }

        [JsonPropertyName("tech_password")]
        public string techPassword { get; set; }

        [JsonPropertyName("testsettings")]
        public Testsettings testsettings { get; set; }
    }

    public partial class Testsettings
    {
        [JsonPropertyName("email")]
        public string email { get; set; }

        [JsonPropertyName("phone")]
        public string phone { get; set; }
    }
}
