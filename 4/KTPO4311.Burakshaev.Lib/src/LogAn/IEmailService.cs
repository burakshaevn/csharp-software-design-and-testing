using System;
using System.Collections.Generic;
using System.Text;

namespace KTPO4311.Burakshaev.Lib.src.LogAn
{
    public interface IEmailService
    {
        void SendEmail(string to, string subject, string body);
    }

}
