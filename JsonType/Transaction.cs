using Newtonsoft.Json;

namespace WebT
{
    public class Transaction
    {
        [JsonProperty("TransactionID")]
        public string TransactionID { get; set; }

        [JsonProperty("TransactionType")]
        public string TransactionType { get; set; }

        public Transaction(string transactionid, string transactiontype)
        {
            TransactionID = transactionid;
            TransactionType = transactiontype;
        }
    }
}