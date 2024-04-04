using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApiIesbk.Model
{
    public class SenderModelLoginTech
    {
        public string Login { get; set; }
        public string Password { get; set; }

        public bool Remember_Me { get; set; } = false;
    }
}
