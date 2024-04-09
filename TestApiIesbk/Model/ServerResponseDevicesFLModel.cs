using System.Text.Json.Serialization;

namespace TestApiIesbk.Model.FL
{
    public class ServerResponseDevicesFLModel
    {
        [JsonPropertyName("last_reading")]
        public LastReading lastReading { get; set; }

        [JsonPropertyName("id")]
        public Guid id { get; set; }

        [JsonPropertyName("number")]
        public string number { get; set; }

        [JsonPropertyName("type")]
        public string type { get; set; }

        [JsonPropertyName("scales")]
        public List<Scale> scales { get; set; }

        [JsonPropertyName("trans_factor")]
        public double transFactor { get; set; }

        [JsonPropertyName("accuracy")]
        public string accuracy { get; set; }

        [JsonPropertyName("phases")]
        public long phases { get; set; }

        [JsonPropertyName("installed")]
        public DateTimeOffset installed { get; set; }

        [JsonPropertyName("installed_string")]
        public string installedString { get; set; }

        [JsonPropertyName("checked")]
        public DateTimeOffset checkedValue { get; set; }

        [JsonPropertyName("checked_string")]
        public string checkedString { get; set; }

        [JsonPropertyName("next_check")]
        public DateTimeOffset nextCheck { get; set; }

        [JsonPropertyName("next_check_string")]
        public string nextCheckString { get; set; }

        [JsonPropertyName("service_code")]
        public long serviceCode { get; set; }

        [JsonPropertyName("service_name")]
        public string serviceName { get; set; }

        [JsonPropertyName("parent_name")]
        public string parentName { get; set; }

        [JsonPropertyName("service_alias_id")]
        public Guid serviceAliasId { get; set; }

        [JsonPropertyName("installation_place")]
        public string installationPlace { get; set; }

        [JsonPropertyName("guid_position")]
        public Guid guidPosition { get; set; }

        [JsonPropertyName("owners")]
        public List<Owner> owners { get; set; }

        [JsonPropertyName("is_smart")]
        public bool isSmart { get; set; }

        [JsonPropertyName("status")]
        public string status { get; set; }

        [JsonPropertyName("accepts_readings")]
        public bool acceptsReadings { get; set; }

        [JsonPropertyName("is_interval")]
        public bool isInterval { get; set; }

        [JsonPropertyName("energy_kind")]
        public string energyKind { get; set; }

        [JsonPropertyName("address")]
        public string address { get; set; }

        [JsonPropertyName("is_complex")]
        public bool isComplex { get; set; }

        [JsonPropertyName("readings_accept_type")]
        public string readingsAcceptType { get; set; }

        [JsonPropertyName("readings_accept_type_text")]
        public string readingsAcceptTypeText { get; set; }

        [JsonPropertyName("is_hot_water")]
        public bool isHotWater { get; set; }

        [JsonPropertyName("is_installed")]
        public bool isInstalled { get; set; }

        [JsonPropertyName("is_permitted")]
        public bool isPermitted { get; set; }

        [JsonPropertyName("admission_status")]
        public object admissionStatus { get; set; }

        [JsonPropertyName("allow_vodomer")]
        public bool allowVodomer { get; set; }

        [JsonPropertyName("show_electric_info")]
        public bool showElectricInfo { get; set; }
    }

        public partial class LastReading
        {
            [JsonPropertyName("readings")]
            public List<Reading> Readings { get; set; }

            [JsonPropertyName("reading_slot_wrapper")]
            public ReadingSlotWrapper ReadingSlotWrapper { get; set; }
        }

        public partial class ReadingSlotWrapper
        {
            [JsonPropertyName("reading_slot_type")]
            public string ReadingSlotType { get; set; }

            [JsonPropertyName("reading_slot")]
            public ReadingSlot ReadingSlot { get; set; }
        }

        public partial class ReadingSlot
        {
            [JsonPropertyName("elements")]
            public List<Element> elements { get; set; }
        }

        public partial class Element
        {
            [JsonPropertyName("scale_id")]
            public Guid scaleId { get; set; }

            [JsonPropertyName("only_consumption")]
            public bool onlyConsumption { get; set; }

            [JsonPropertyName("max_value")]
            public int maxValue { get; set; }

            [JsonPropertyName("trans_factor")]
            public double transFactor { get; set; }

            [JsonPropertyName("before_point")]
            public int beforePoint { get; set; }

            [JsonPropertyName("after_point")]
            public int afterPoint { get; set; }

            [JsonPropertyName("unit")]
            public string unit { get; set; }

            [JsonPropertyName("last_reading")]
            public double lastReading { get; set; }
        }

        public partial class Reading
        {
            [JsonPropertyName("id")]
            public Guid Id { get; set; }

            [JsonPropertyName("position")]
            public int Position { get; set; }

            [JsonPropertyName("scale")]
            public Scale Scale { get; set; }

            [JsonPropertyName("value")]
            public double Value { get; set; }

            [JsonPropertyName("consumption")]
            public double Consumption { get; set; }

            [JsonPropertyName("status_transfer")]
            public string StatusTransfer { get; set; }

            [JsonPropertyName("status_enter")]
            public string StatusEnter { get; set; }

            [JsonPropertyName("approved")]
            public object Approved { get; set; }

            [JsonPropertyName("date")]
            public DateTimeOffset Date { get; set; }

            [JsonPropertyName("is_deleted")]
            public bool IsDeleted { get; set; }

            [JsonPropertyName("deletion_allowed")]
            public bool DeletionAllowed { get; set; }
        }

        public partial class Scale
        {
            [JsonPropertyName("id")]
            public Guid Id { get; set; }

            [JsonPropertyName("deviceid")]
            public Guid Deviceid { get; set; }

            [JsonPropertyName("name")]
            public string Name { get; set; }

            [JsonPropertyName("unit")]
            public string Unit { get; set; }

            [JsonPropertyName("only_consumption")]
            public bool OnlyConsumption { get; set; }

            [JsonPropertyName("before_point")]
            public int BeforePoint { get; set; }

            [JsonPropertyName("after_point")]
            public int AfterPoint { get; set; }

            [JsonPropertyName("code")]
            public string Code { get; set; }
        }

        public partial class Owner
        {
            [JsonPropertyName("owner_type")]
            public string OwnerType { get; set; }

            [JsonPropertyName("owner_name")]
            public string OwnerName { get; set; }

            [JsonPropertyName("owner_id")]
            public Guid OwnerId { get; set; }

            [JsonPropertyName("sub_owner_id")]
            public Guid SubOwnerId { get; set; }
        }
    }
