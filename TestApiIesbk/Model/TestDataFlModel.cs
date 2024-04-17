﻿using System.Text.Json.Serialization;


namespace TestApiIesbk.Model.FL
{
    public class TestDataFL
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
        public Testsettings testSettings { get; set; }


        public partial class Testsettings
        {
            [JsonPropertyName("login")]
            public string login { get; set; }

            [JsonPropertyName("authenticator")]
            public string authenticator { get; set; }

            [JsonPropertyName("password")]
            public string password { get; set; }

            [JsonPropertyName("balance")]
            public string balance { get; set; }

            [JsonPropertyName("device_id")]
            public string deviceId { get; set; }
        }
    }
}
