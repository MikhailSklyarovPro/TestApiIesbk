using System.Text.Json.Serialization;


namespace TestApiIesbk.Model
{
    public class TestDataFL
    {
        [JsonPropertyName("url")]
        public string url { get; set; }

        [JsonPropertyName("type")]
        public string type { get; set; }

        [JsonPropertyName("tech_login")]
        public string techLogin { get; set; }

        [JsonPropertyName("tech_password")]
        public string techPassword { get; set; }

        [JsonPropertyName("testsettings")]
        public Testsettings testSettings { get; set; }


        public class Testsettings
        {
            [JsonPropertyName("login")]
            public string login { get; set; }

            [JsonPropertyName("authenticator")]
            public string authenticator { get; set; }

            [JsonPropertyName("balance")]
            public string balance { get; set; }

            [JsonPropertyName("device_id")]
            public string deviceId { get; set; }
        }
    }
}
