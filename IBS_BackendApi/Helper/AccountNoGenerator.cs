namespace IBS_BackendApi.Helper
{
    public class AccountNoGenerator
    {
        public static string GenerateAccountNo()
        {
            string initialBankCode = "0300000";
            Random random = new Random();
            string randomFiveDigitNumber = random.Next(10000, 100000).ToString();
            return initialBankCode + randomFiveDigitNumber;
        }
    }
}
