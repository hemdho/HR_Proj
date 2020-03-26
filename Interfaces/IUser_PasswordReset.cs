namespace HR.WebApi.Interfaces
{
    public interface IUser_PasswordReset
    {
        void Insert(int id, string Link);

        void ForgotPassword(string loginId);

        bool VerifyLink(string link);

        bool ResetPassword(string loginId, string password);
    }
}
