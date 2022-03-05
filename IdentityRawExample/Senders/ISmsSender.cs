using System.Threading.Tasks;

namespace IdentityRawExample.Senders
{
    public interface ISmsSender
    {
        public Task SendSms(string phonenumber, string message);
    }
}