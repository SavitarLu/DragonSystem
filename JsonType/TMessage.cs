using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebT
{
    public class TMessage
    {
       // [JsonProperty("Transaction")]
        public Transaction Transaction { get; set; }

       // [JsonProperty("Data")]
        //public string Data { get; set; }
        public dynamic Data { get; set; }

        //public TMessage(string transaction, dynamic data)
        //{
        //    Transaction = transaction;
        //    Data = data;
        //}
    }
}