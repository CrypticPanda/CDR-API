using System;

namespace CDR_BIP.Model
{
    public class CDR
    {
        public string caller_id { get; set; }
        public string recipient { get; set; }
        public DateTime call_date { get; set; }
        public DateTime end_time { get; set; }
        public int duration { get; set; }
        public decimal cost { get; set; }
        public string reference { get; set; }
        public string currency { get; set; }
    }
}
