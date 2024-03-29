using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApiIesbk.Model
{
    public class ServerResponseUserInfoModel
    {
        public class Account
        {
            public List<Device> devices { get; set; }
            public List<PaymentLink> payment_links { get; set; }
            public List<ConnectedService> connected_services { get; set; }
            public LastBill last_bill { get; set; }
            public string id { get; set; }
            public string number { get; set; }
            public double balance { get; set; }
            public List<Accrual> accruals { get; set; }
            public string owner { get; set; }
            public House house { get; set; }
            public string department_id { get; set; }
            public string department_name { get; set; }
            public bool is_owner_registered { get; set; }
            public string division_id { get; set; }
            public string display_name { get; set; }
        }

        public class Accrual
        {
            public string account { get; set; }
            public string subcontractor { get; set; }
            public double debt { get; set; }
            public string signature { get; set; }
            public string signature_mobile { get; set; }
            public string signature_debt { get; set; }
            public string service_name { get; set; }
        }

        public class ConnectedService
        {
            public string service_name { get; set; }
            public bool reconciliation_act { get; set; }
        }

        public class Device
        {
            public LastReading last_reading { get; set; }
            public string id { get; set; }
            public string number { get; set; }
            public string type { get; set; }
            public List<Scale> scales { get; set; }
            public double trans_factor { get; set; }
            public string accuracy { get; set; }
            public int phases { get; set; }
            public DateTime installed { get; set; }
            public string installed_string { get; set; }
            public DateTime @checked { get; set; }
            public string checked_string { get; set; }
            public DateTime next_check { get; set; }
            public string next_check_string { get; set; }
            public int service_code { get; set; }
            public string service_name { get; set; }
            public string parent_name { get; set; }
            public string service_alias_id { get; set; }
            public string installation_place { get; set; }
            public string guid_position { get; set; }
            public List<Owner> owners { get; set; }
            public bool is_smart { get; set; }
            public string status { get; set; }
            public bool accepts_readings { get; set; }
            public bool is_interval { get; set; }
            public string energy_kind { get; set; }
            public string address { get; set; }
            public bool is_complex { get; set; }
            public string readings_accept_type { get; set; }
            public string readings_accept_type_text { get; set; }
            public bool is_hot_water { get; set; }
            public bool is_installed { get; set; }
            public bool is_permitted { get; set; }
            public object admission_status { get; set; }
            public bool allow_vodomer { get; set; }
            public bool show_electric_info { get; set; }
        }

        public class Element
        {
            public string scale_id { get; set; }
            public bool only_consumption { get; set; }
            public int max_value { get; set; }
            public double trans_factor { get; set; }
            public int before_point { get; set; }
            public int after_point { get; set; }
            public string unit { get; set; }
            public double last_reading { get; set; }
        }

        public class House
        {
            public string address { get; set; }
            public double area { get; set; }
            public int rooms { get; set; }
            public int people { get; set; }
        }

        public class LastBill
        {
            public string account_id { get; set; }
            public string account { get; set; }
            public DateTime date { get; set; }
            public string document { get; set; }
        }

        public class LastReading
        {
            public List<Reading> readings { get; set; }
            public ReadingSlotWrapper reading_slot_wrapper { get; set; }
        }

        public class Owner
        {
            public string owner_type { get; set; }
            public string owner_name { get; set; }
            public string owner_id { get; set; }
            public string sub_owner_id { get; set; }
        }

        public class PaymentLink
        {
            public string actionlink { get; set; }
            public string actionbody { get; set; }
            public string type { get; set; }
            public string url { get; set; }
        }

        public class Reading
        {
            public string id { get; set; }
            public int position { get; set; }
            public Scale scale { get; set; }
            public double value { get; set; }
            public double consumption { get; set; }
            public string status_transfer { get; set; }
            public string status_enter { get; set; }
            public object approved { get; set; }
            public DateTime date { get; set; }
            public bool is_deleted { get; set; }
            public bool deletion_allowed { get; set; }
        }

        public class ReadingSlot
        {
            public List<Element> elements { get; set; }
        }

        public class ReadingSlotWrapper
        {
            public string reading_slot_type { get; set; }
            public ReadingSlot reading_slot { get; set; }
        }

        public class Root
        {
            public Account account { get; set; }
            public string id { get; set; }
            public string first_name { get; set; }
            public string second_name { get; set; }
            public string last_name { get; set; }
            public string phone { get; set; }
            public string email { get; set; }
            public bool email_doc { get; set; }
            public bool email_info { get; set; }
            public bool sms_info { get; set; }
            public bool send_push { get; set; }
        }

        public class Scale
        {
            public string id { get; set; }
            public string deviceid { get; set; }
            public string name { get; set; }
            public string unit { get; set; }
            public bool only_consumption { get; set; }
            public int before_point { get; set; }
            public int after_point { get; set; }
            public string code { get; set; }
        }

        public class Scale2
        {
            public string id { get; set; }
            public string deviceid { get; set; }
            public string name { get; set; }
            public string unit { get; set; }
            public bool only_consumption { get; set; }
            public int before_point { get; set; }
            public int after_point { get; set; }
            public string code { get; set; }
        }


    }
}
