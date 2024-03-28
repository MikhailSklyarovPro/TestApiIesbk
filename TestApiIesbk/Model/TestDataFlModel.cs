using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApiIesbk.Model
{
    public class TestDataFlModel
    {
        public string url { get; set; }
        public string type { get; set; }
        public string login { get; set; }
        public string password { get; set; }
        public Testsettings testsettings { get; set; }

        public class Testsettings
        {
            public string login { get; set; }
            public string authenticator { get; set; }
            public double balance { get; set; }
            public string device_id { get; set; }
        }

    }
}
