using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityRawExample.Senders
{
    public class SendSms : ISmsSender
    {
        async Task ISmsSender.SendSms(string phonenumber, string message)
        {
            
        }
    }
}
