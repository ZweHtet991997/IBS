namespace IBS_BackendApi.Models.DAO
{
    public class AdminDAO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public string Department { get; set; }
        public string Position { get; set; }
        public string UserRole { get; set; }
        public string City { get; set; }
        public string Township { get; set; }
        public string Address { get; set; }
        public string ManagerName { get; set; }
        public string Password { get; set; }
        public bool FirstTimeUserFlag { get; set; }
        public bool ForceChangePasswordFlag { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
