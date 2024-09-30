using System;
using System.Collections.Generic;
using System.Text;

namespace KTPO4311.Burakshaev.Lib.src.LogAn
{
    public class EmailServiceFactory
    {
        private static IEmailService email_ = null;
        public static IEmailService Create()
        {
            if (email_ != null)
            {
                return email_;
            }
            return new FakeEmailService();
        }
        public static void SetEmail(IEmailService email)
        {
            email_ = email;
        }
    }
}
