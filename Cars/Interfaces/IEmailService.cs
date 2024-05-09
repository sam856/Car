using Cars.Helper;

namespace Cars.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(Mailrequest mailrequest,string emailBody);
    }
}
