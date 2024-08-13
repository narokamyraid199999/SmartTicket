namespace SmartTicket.Services
{
    public interface IEmailService
    {
        public Task<string?> sendMailAsync(string maailTo, string subject, string body);
    }
}
