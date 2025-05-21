namespace IBS_BackendApi.Models.DAO
{
    public class CustomerSecurityAnswerDAO
    {
        public int ID { get; set; }
        public int CustomerID { get; set; }
        public int QuestionID { get; set; }
        public string Answer { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
