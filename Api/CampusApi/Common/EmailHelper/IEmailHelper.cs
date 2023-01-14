using Core.Models;

namespace Common.EmailHelper
{
    public interface IEmailHelper
    {
        dynamic SendEmail(EmailModel emailDto);
    }
}
