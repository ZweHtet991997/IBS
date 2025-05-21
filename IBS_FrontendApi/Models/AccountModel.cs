namespace IBS_FrontendApi.Models
{
    public class AccountModel
    {
        public int ID { get; set; }
        public string CustomerID { get; set; }
        public string AccountNo { get; set; }
        public string AccountType { get; set; }
        public decimal Balance { get; set; }
        public string AccountDescription { get; set; }
        public string AccountStatus { get; set; }
        public int CreatedUserID { get; set; }
        public DateTime CreatedDate { get; set; }
        public int UpdatedUserID { get; set; }
        public DateTime UpdatedDate { get; set; }
        public DateTime InactiveDate { get; set; }
    }

    public class InactiveAccountModel
    {
        public string AccountNo { get; set; }
    }
}
