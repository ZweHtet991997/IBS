namespace IBS_BackendApi.Models
{
    public class ResponseMessage
    {
        public static string RegsiterSuccess()
        {
            return "Register Successfully";
        }

        public static string LoginSuccess()
        {
            return "Login Success";
        }

        public static string AccountCreateSuccess()
        {
            return "Your Account have been successfully created";
        }

        public static string TransactionSuccess()
        {
            return "Your transaction has been successfully";
        }

        public static string InsufficientBalance()
        {
            return "Sorry,You have insufficient balance to make this transaction";
        }

        public static string AccountNotAllowed()
        {
            return "Saving Account cannot be transfer";
        }

        public static string DepositeFailed()
        {
            return "Account No {0} is currently cannot be deposite or Account is Inactive";
        }

        public static string AdminRegisterFailed()
        {
            return "Your Email {0} is already registered in this system";
        }

        public static string CustomerRegisterFailed()
        {
            return "Your NRC No {0} is already registered in this system";
        }

        public static string AccountOpeningFailed()
        {
            return "Sorry,you cannot create account at this time";
        }

        public static string LoginFailed()
        {
            return "UserName or Password is invalid.Please try again.";
        }

        public static string UserDuplicate(string? NRC)
        {
            return "NRC No " + NRC + " is already registered.Please use another NRC No";
        }

        public static string UpdateSuccess()
        {
            return "Successfully Updated";
        }

        public static string UpdateFailed()
        {
            return "Failed to Update.Please try again";
        }

        public static string DeleteSuccess()
        {
            return "Successfully Deleted";
        }

        public static string DeleteFailed()
        {
            return "Failed to Delete.Please try again";
        }
    }
}
