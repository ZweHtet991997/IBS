namespace IBS_BackendApi.Models.ResponseModel
{
    public class CustomerResponseModel
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
        public string CreatedDate { get; set; }
    }
}
