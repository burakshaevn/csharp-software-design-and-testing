using KTPO4311.Burakshaev.Lib.src.LogAn;
using System;
using System.Collections.Generic;
using System.Text;

namespace KTPO4311.Burakshaev.Lib.src.LogAn
{
    public class FakeEmailService : IEmailService
    {
        public string To;
        public string Subject;
        public string Body;
        public void SendEmail(string to, string subject, string body)
        {
            To = to;
            Subject = subject;
            Body = body;
        }
    }
}
