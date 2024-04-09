using System.Text.Json.Serialization;


namespace TestApiIesbk.Model
{
    public class ServerResponseContractInfoULModel
    {
        [JsonPropertyName("service")]
        public long service { get; set; }

        [JsonPropertyName("service_name")]
        public string serviceName { get; set; }

        [JsonPropertyName("department_id")]
        public Guid departmentId { get; set; }

        [JsonPropertyName("division_id")]
        public Guid divisionId { get; set; }

        [JsonPropertyName("balance")]
        public double balance { get; set; }

        [JsonPropertyName("business_process_status")]
        public string businessProcessStatus { get; set; }

        [JsonPropertyName("is_tech_connection_active")]
        public bool isTechConnectionActive { get; set; }

        [JsonPropertyName("allowed_actions")]
        public List<string> allowedActions { get; set; }

        [JsonPropertyName("id")]
        public Guid id { get; set; }

        [JsonPropertyName("number")]
        public string number { get; set; }

        [JsonPropertyName("contract_date")]
        public string contractDate { get; set; }

        [JsonPropertyName("status")]
        public string status { get; set; }

        [JsonPropertyName("is_temporary")]
        public bool isTemporary { get; set; }

        [JsonPropertyName("hidden")]
        public bool hidden { get; set; }

    }
}
