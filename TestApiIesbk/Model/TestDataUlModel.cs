using System.Text.Json.Serialization;


namespace TestIesbk
{
    public class TestDataUL
    {

        [JsonPropertyName("url")]
        public Uri Url { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("tech_login")]
        public string TechLogin { get; set; }

        [JsonPropertyName("tech_password")]
        public long TechPassword { get; set; }

        [JsonPropertyName("testsettings")]
        public Testsettings Testsettings { get; set; }
    }

    public partial class Testsettings
    {
        [JsonPropertyName("login")]
        public string Login { get; set; }

        [JsonPropertyName("authenticator")]
        public string Authenticator { get; set; }

        [JsonPropertyName("contract_id")]
        public Guid ContractId { get; set; }

        [JsonPropertyName("balance")]
        public string Balance { get; set; }

        [JsonPropertyName("device_id")]
        public Guid DeviceId { get; set; }
    }
}
