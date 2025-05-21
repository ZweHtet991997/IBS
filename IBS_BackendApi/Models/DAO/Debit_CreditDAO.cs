namespace IBS_BackendApi.Models.DAO
{
    public class Debit_CreditDAO
    {
        public int ID { get; set; }
        public string TransactionID { get; set; }
        public string AccountNo { get; set; }
        public string Flash { get; set; }
        public string Description { get; set; }
        public string TransactionDate { get; set; }
    }
}
