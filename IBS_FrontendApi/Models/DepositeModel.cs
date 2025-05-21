namespace IBS_FrontendApi.Models
{
    public class DepositeModel
    {
        public int ID { get; set; }
        public string CustomerID { get; set; }
        public int AccountID { get; set; }
        public string AccountNo { get; set; }
        public decimal DepositeAmount { get; set; }
        public string Description { get; set; }
        public string DepositeDate { get; set; }
        public string CreatedUserID { get; set; }
    }
}
