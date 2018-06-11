using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckKey
{
    public partial class KeyTrial
    {
        public int id { get; set; }
        public string main { get; set; }// longtext 
        public string cpu { get; set; } // longtext 
        public DateTime start_date { get; set; } // date 
        public DateTime end_date { get; set; } //date 
        public int expired_time { get; set; } // int(11) 
        public int status { get; set; } // varchar(20)
    }
}
