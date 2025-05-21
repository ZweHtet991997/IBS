namespace IBS_BackendApi.Helper
{
    public class CustomerIDGenerator
    {
        public static string GenerateCIFID(int customerCount)
        {
            customerCount++;
            string customerCountFormatted = customerCount.ToString("D6");
            string customerID = "CUS-" + customerCountFormatted;
            return customerID;
        }
    }
}
