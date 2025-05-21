namespace IBS_FrontendApi.Models
{
    public class TransactionHistoryModel
    {
        public int ID { get; set; }
        public int AccountID { get; set; }
        public string TransactionID { get; set; }
        public string FromAccountNo { get; set; }
        public string ToAccountNo { get; set; }
        public string SenderName { get; set; }
        public string ReceiverName { get; set; }
        public string TransactionType { get; set; }
        public string Operator { get; set; }
        public decimal TransactionAmount { get; set; }
        public string TransactionNote { get; set; }
        public string TransactionDate { get; set; }
    }
}
