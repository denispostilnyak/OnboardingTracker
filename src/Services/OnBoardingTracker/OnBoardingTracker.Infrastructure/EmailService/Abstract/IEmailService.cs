using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnBoardingTracker.Infrastructure.EmailService.Abstract
{
    public interface IEmailService
    {
        Task Send(IEnumerable<string> to, string subject, string body);
    }
}
