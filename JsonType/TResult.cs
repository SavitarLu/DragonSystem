using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebT
{
    public class TResult
    {
        [JsonProperty("Result")]
        public string Result { get; set; }

        [JsonProperty("Message")]
        public string Message { get; set; }

        [JsonProperty("Data")]
        public object Data { get; set; }

        public TResult(string result, string message, object data)
        {
            Result = result;
            Message = message;
            Data = data;
        }
    }

}