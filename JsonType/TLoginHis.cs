using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebT
{
    public class TLoginHis
    {
        [JsonProperty("UserID")]
        public string UserID { get; set; }

        [JsonProperty("IPAddr")]
        public string IPAddr { get; set; }

        public TLoginHis(string userid, string ipaddr)
        {
            UserID = userid;
            IPAddr = ipaddr;
        }
    }
}