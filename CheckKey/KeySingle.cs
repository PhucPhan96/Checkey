using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckKey
{
    public partial class KeySingle
    {
        public int id { get; set; }
        public string key_single { get; set; }// longtext 
        public string ex_main { get; set; } // longtext 
        public string ex_cpu { get; set; }// longtext 
        public Nullable<System.DateTime> start_date { get; set; } // date 
        public Nullable<System.DateTime> end_date { get; set; } //date 
        public Nullable<int> key_log { get; set; } // int(11) 
        public string status_key { get; set; } // varchar(20)
    }
}
