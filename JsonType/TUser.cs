using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebT
{
    public class TUser
    {
        [JsonProperty("UserID")]
        public string UserID { get; set; }

        [JsonProperty("Password")]
        public string Password { get; set; }

        public TUser(string userID, string password)
        {
            UserID = userID;
            Password = password;
        }
    }
}