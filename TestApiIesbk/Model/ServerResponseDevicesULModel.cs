using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TestApiIesbk.UL
{
    public class ServerResponseDevicesULModel
    {
        [JsonPropertyName("last_reading")]
        public LastReading LastReading { get; set; }

        [JsonPropertyName("last_vzlet_file_upload_date")]
        public DateTimeOffset LastVzletFileUploadDate { get; set; }

        [JsonPropertyName("has_vzlet_file_in_period")]
        public bool HasVzletFileInPeriod { get; set; }

        [JsonPropertyName("last_vzlet_data_date")]
        public DateTimeOffset LastVzletDataDate { get; set; }

        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("number")]
        public string Number { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("scales")]
        public List<Scale> Scales { get; set; }

        [JsonPropertyName("trans_factor")]
        public double TransFactor { get; set; }

        [JsonPropertyName("accuracy")]
        public string Accuracy { get; set; }

        [JsonPropertyName("phases")]
        public long Phases { get; set; }

        [JsonPropertyName("installed")]
        public DateTimeOffset Installed { get; set; }

        [JsonPropertyName("installed_string")]
        public string InstalledString { get; set; }

        [JsonPropertyName("checked")]
        public DateTimeOffset Checked { get; set; }

        [JsonPropertyName("checked_string")]
        public string CheckedString { get; set; }

        [JsonPropertyName("next_check")]
        public DateTimeOffset NextCheck { get; set; }

        [JsonPropertyName("next_check_string")]
        public string NextCheckString { get; set; }

        [JsonPropertyName("service_code")]
        public long ServiceCode { get; set; }

        [JsonPropertyName("service_name")]
        public string ServiceName { get; set; }

        [JsonPropertyName("parent_name")]
        public string ParentName { get; set; }

        [JsonPropertyName("service_alias_id")]
        public Guid ServiceAliasId { get; set; }

        [JsonPropertyName("installation_place")]
        public string InstallationPlace { get; set; }

        [JsonPropertyName("guid_position")]
        public Guid GuidPosition { get; set; }

        [JsonPropertyName("owners")]
        public List<Owner> Owners { get; set; }

        [JsonPropertyName("is_smart")]
        public bool IsSmart { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("accepts_readings")]
        public bool AcceptsReadings { get; set; }

        [JsonPropertyName("is_interval")]
        public bool IsInterval { get; set; }

        [JsonPropertyName("energy_kind")]
        public string EnergyKind { get; set; }

        [JsonPropertyName("address")]
        public string Address { get; set; }

        [JsonPropertyName("is_complex")]
        public bool IsComplex { get; set; }

        [JsonPropertyName("readings_accept_type")]
        public string ReadingsAcceptType { get; set; }

        [JsonPropertyName("readings_accept_type_text")]
        public string ReadingsAcceptTypeText { get; set; }

        [JsonPropertyName("is_hot_water")]
        public bool IsHotWater { get; set; }

        [JsonPropertyName("is_installed")]
        public bool IsInstalled { get; set; }

        [JsonPropertyName("is_permitted")]
        public bool IsPermitted { get; set; }

        [JsonPropertyName("admission_status")]
        public string AdmissionStatus { get; set; }

        [JsonPropertyName("allow_vodomer")]
        public bool AllowVodomer { get; set; }

        [JsonPropertyName("show_electric_info")]
        public bool ShowElectricInfo { get; set; }
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
        [JsonPropertyName("slot_date")]
        public DateTimeOffset SlotDate { get; set; }

        [JsonPropertyName("slot_identifier")]
        public string SlotIdentifier { get; set; }

        [JsonPropertyName("vzlet_file_name")]
        public string VzletFileName { get; set; }

        [JsonPropertyName("vzlet_file_date")]
        public string VzletFileDate { get; set; }
    }

    public partial class Reading
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("position")]
        public long Position { get; set; }

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
        public bool Approved { get; set; }

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
        public long BeforePoint { get; set; }

        [JsonPropertyName("after_point")]
        public long AfterPoint { get; set; }

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
