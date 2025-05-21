namespace IBS_FrontendApi.Models
{
    public class CustomerModel
    {
        public int ID { get; set; }
        public string CIFID { get; set; }
        public string FullName { get; set; }
        public string NRC { get; set; }
        public string PhoneNo { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public string Township { get; set; }
        public string Address { get; set; }
        public string DOB { get; set; }
        public string Gender { get; set; }
        public string JobTitle { get; set; }
        public string ProfilePhotoPath { get; set; }
        public string Password { get; set; }
        public bool FirstTimeUserFlag { get; set; }
        public bool ForceChangePasswordFlag { get; set; }
        public int CreatedUserID { get; set; }
        public DateTime CreatedDate { get; set; }
        public int UpdatedUserID { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
