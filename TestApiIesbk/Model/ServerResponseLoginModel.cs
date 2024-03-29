using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApiIesbk.Model
{
    public class ServerResponseLoginModel
    {
        public string access_token { get; set; }
        public string refresh_token { get; set; }
        public string token_type { get; set; }
        public string expires_in { get; set; }

        [JsonProperty(".issued")]
        public string issued { get; set; }

        [JsonProperty(".expires")]
        public string expires { get; set; }
        public string refresh_expires { get; set; }
        public string id { get; set; }
        public List<string> main_user_roles { get; set; }
        public List<string> sub_user_roles { get; set; }
    }
}
