﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace TestApiIesbk.Model
{
    public class ServerResponseUserInfoModel
    {
        [JsonProperty("account")]
        public Account account { get; set; }

        [JsonProperty("id")]
        public Guid id { get; set; }

        [JsonProperty("first_name")]
        public string firstName { get; set; }

        [JsonProperty("second_name")]
        public string secondName { get; set; }

        [JsonProperty("last_name")]
        public string lastName { get; set; }

        [JsonProperty("phone")]
        public string phone { get; set; }

        [JsonProperty("email")]
        public string email { get; set; }

        [JsonProperty("email_doc")]
        public bool emailDoc { get; set; }

        [JsonProperty("email_info")]
        public bool emailInfo { get; set; }

        [JsonProperty("sms_info")]
        public bool smsInfo { get; set; }

        [JsonProperty("send_push")]
        public bool sendPush { get; set; }
        public class Account
        {
            [JsonProperty("devices")]
            public Device[] devices { get; set; }

            [JsonProperty("payment_links")]
            public PaymentLink[] paymentLinks { get; set; }

            [JsonProperty("connected_services")]
            public ConnectedService[] connectedServices { get; set; }

            [JsonProperty("last_bill")]
            public LastBill lLastBill { get; set; }

            [JsonProperty("id")]
            public Guid id { get; set; }

            [JsonProperty("number")]
            public string number { get; set; }

            [JsonProperty("balance")]
            public double balance { get; set; }

            [JsonProperty("accruals")]
            public Accrual[] accruals { get; set; }

            [JsonProperty("owner")]
            public string owner { get; set; }

            [JsonProperty("house")]
            public House house { get; set; }

            [JsonProperty("department_id")]
            public Guid departmentId { get; set; }

            [JsonProperty("department_name")]
            public string departmentName { get; set; }

            [JsonProperty("is_owner_registered")]
            public bool isOwnerRegistered { get; set; }

            [JsonProperty("division_id")]
            public Guid divisionId { get; set; }

            [JsonProperty("display_name")]
            public string displayName { get; set; }
        }

        public partial class Accrual
        {
            [JsonProperty("account")]
            public string account { get; set; }

            [JsonProperty("subcontractor")]
            public string Subcontractor { get; set; }

            [JsonProperty("debt")]
            public double Debt { get; set; }

            [JsonProperty("signature")]
            public string Signature { get; set; }

            [JsonProperty("signature_mobile")]
            public string SignatureMobile { get; set; }

            [JsonProperty("signature_debt")]
            public string SignatureDebt { get; set; }

            [JsonProperty("service_name")]
            public string ServiceName { get; set; }
        }

        public partial class ConnectedService
        {
            [JsonProperty("service_name")]
            public string ServiceName { get; set; }

            [JsonProperty("reconciliation_act")]
            public bool ReconciliationAct { get; set; }
        }

        public partial class Device
        {
            [JsonProperty("last_reading")]
            public LastReading LastReading { get; set; }

            [JsonProperty("id")]
            public Guid Id { get; set; }

            [JsonProperty("number")]
            public string Number { get; set; }

            [JsonProperty("type")]
            public string Type { get; set; }

            [JsonProperty("scales")]
            public Scale[] Scales { get; set; }

            [JsonProperty("trans_factor")]
            public long TransFactor { get; set; }

            [JsonProperty("accuracy")]
            public int accuracy { get; set; }

            [JsonProperty("phases")]
            public long Phases { get; set; }

            [JsonProperty("installed")]
            public DateTimeOffset Installed { get; set; }

            [JsonProperty("installed_string")]
            public string InstalledString { get; set; }

            [JsonProperty("checked")]
            public DateTimeOffset Checked { get; set; }

            [JsonProperty("checked_string")]
            public string CheckedString { get; set; }

            [JsonProperty("next_check")]
            public DateTimeOffset NextCheck { get; set; }

            [JsonProperty("next_check_string")]
            public string NextCheckString { get; set; }

            [JsonProperty("service_code")]
            public long ServiceCode { get; set; }

            [JsonProperty("service_name")]
            public string ServiceName { get; set; }

            [JsonProperty("parent_name")]
            public string ParentName { get; set; }

            [JsonProperty("service_alias_id")]
            public Guid ServiceAliasId { get; set; }

            [JsonProperty("installation_place")]
            public string InstallationPlace { get; set; }

            [JsonProperty("guid_position")]
            public Guid GuidPosition { get; set; }

            [JsonProperty("owners")]
            public Owner[] Owners { get; set; }

            [JsonProperty("is_smart")]
            public bool IsSmart { get; set; }

            [JsonProperty("status")]
            public string Status { get; set; }

            [JsonProperty("accepts_readings")]
            public bool AcceptsReadings { get; set; }

            [JsonProperty("is_interval")]
            public bool IsInterval { get; set; }

            [JsonProperty("energy_kind")]
            public string EnergyKind { get; set; }

            [JsonProperty("address")]
            public string Address { get; set; }

            [JsonProperty("is_complex")]
            public bool IsComplex { get; set; }

            [JsonProperty("readings_accept_type")]
            public string ReadingsAcceptType { get; set; }

            [JsonProperty("readings_accept_type_text")]
            public string ReadingsAcceptTypeText { get; set; }

            [JsonProperty("is_hot_water")]
            public bool IsHotWater { get; set; }

            [JsonProperty("is_installed")]
            public bool IsInstalled { get; set; }

            [JsonProperty("is_permitted")]
            public bool IsPermitted { get; set; }

            [JsonProperty("admission_status")]
            public object AdmissionStatus { get; set; }

            [JsonProperty("allow_vodomer")]
            public bool AllowVodomer { get; set; }

            [JsonProperty("show_electric_info")]
            public bool ShowElectricInfo { get; set; }
        }

        public partial class LastReading
        {
            [JsonProperty("readings")]
            public Reading[] Readings { get; set; }

            [JsonProperty("reading_slot_wrapper")]
            public ReadingSlotWrapper ReadingSlotWrapper { get; set; }
        }

        public partial class ReadingSlotWrapper
        {
            [JsonProperty("reading_slot_type")]
            public string ReadingSlotType { get; set; }

            [JsonProperty("reading_slot")]
            public ReadingSlot ReadingSlot { get; set; }
        }

        public partial class ReadingSlot
        {
            [JsonProperty("elements", NullValueHandling = NullValueHandling.Ignore)]
            public Element[] Elements { get; set; }
        }

        public partial class Element
        {
            [JsonProperty("scale_id")]
            public Guid ScaleId { get; set; }

            [JsonProperty("only_consumption")]
            public bool OnlyConsumption { get; set; }

            [JsonProperty("max_value")]
            public long MaxValue { get; set; }

            [JsonProperty("trans_factor")]
            public long TransFactor { get; set; }

            [JsonProperty("before_point")]
            public long BeforePoint { get; set; }

            [JsonProperty("after_point")]
            public long AfterPoint { get; set; }

            [JsonProperty("unit")]
            public string Unit { get; set; }

            [JsonProperty("last_reading")]
            public long LastReading { get; set; }
        }

        public partial class Reading
        {
            [JsonProperty("id")]
            public Guid Id { get; set; }

            [JsonProperty("position")]
            public long Position { get; set; }

            [JsonProperty("scale")]
            public Scale Scale { get; set; }

            [JsonProperty("value")]
            public double Value { get; set; }

            [JsonProperty("consumption")]
            public double Consumption { get; set; }

            [JsonProperty("status_transfer")]
            public string StatusTransfer { get; set; }

            [JsonProperty("status_enter")]
            public string StatusEnter { get; set; }

            [JsonProperty("approved")]
            public object Approved { get; set; }

            [JsonProperty("date")]
            public DateTimeOffset Date { get; set; }

            [JsonProperty("is_deleted")]
            public bool IsDeleted { get; set; }

            [JsonProperty("deletion_allowed")]
            public bool DeletionAllowed { get; set; }
        }

        public partial class Scale
        {
            [JsonProperty("id")]
            public Guid Id { get; set; }

            [JsonProperty("deviceid")]
            public Guid Deviceid { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("unit")]
            public string Unit { get; set; }

            [JsonProperty("only_consumption")]
            public bool OnlyConsumption { get; set; }

            [JsonProperty("before_point")]
            public long BeforePoint { get; set; }

            [JsonProperty("after_point")]
            public long AfterPoint { get; set; }

            [JsonProperty("code")]
            public string Code { get; set; }
        }

        public partial class Owner
        {
            [JsonProperty("owner_type")]
            public string OwnerType { get; set; }

            [JsonProperty("owner_name")]
            public string OwnerName { get; set; }

            [JsonProperty("owner_id")]
            public Guid OwnerId { get; set; }

            [JsonProperty("sub_owner_id")]
            public Guid SubOwnerId { get; set; }
        }

        public partial class House
        {
            [JsonProperty("address")]
            public string Address { get; set; }

            [JsonProperty("area")]
            public double Area { get; set; }

            [JsonProperty("rooms")]
            public long Rooms { get; set; }

            [JsonProperty("people")]
            public long People { get; set; }
        }

        public partial class LastBill
        {
            [JsonProperty("account_id")]
            public Guid AccountId { get; set; }

            [JsonProperty("account")]
            public string Account { get; set; }

            [JsonProperty("date")]
            public DateTimeOffset Date { get; set; }

            [JsonProperty("document")]
            public Uri Document { get; set; }
        }

        public partial class PaymentLink
        {
            [JsonProperty("actionlink")]
            public Uri Actionlink { get; set; }

            [JsonProperty("actionbody")]
            public string Actionbody { get; set; }

            [JsonProperty("type")]
            public string Type { get; set; }

            [JsonProperty("url")]
            public Uri Url { get; set; }
        }
    }
}
