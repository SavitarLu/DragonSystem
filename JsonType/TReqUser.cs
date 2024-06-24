using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebT
{
    public class TReqUser
    {

        [JsonProperty("Username")]
        public string Username { get; set; }

        public TReqUser(string username)
        {
            Username = username;
        }
    }
}