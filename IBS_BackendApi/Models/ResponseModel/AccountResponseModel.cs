namespace IBS_BackendApi.Models.ResponseModel
{
    public class AccountResponseModel
    {
        public int ID { get; set; }
        public string CIFID { get; set; }
        public string FullName { get; set; }
        public string NRC { get; set; }
        public string PhoneNo { get; set; }
        public string AccountNo { get; set; }
        public string AccountType { get; set; }
        public string AccountStatus { get; set; }
        public string AccountDescription { get; set; }
        public decimal Balance { get; set; }
    }

    public class AccountByCustomerID
    {
        public string AccountNo { get; set;}
    }
}
