namespace GrandeTravel.Data
{
    public interface IEmailSender
    {
        void SendEmail(string from, string to, string subject, string message);
    }
}
