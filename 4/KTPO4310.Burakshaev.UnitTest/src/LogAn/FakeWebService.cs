using KTPO4311.Burakshaev.Lib.src.LogAn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTPO4311.Burakshaev.UnitTest.src.LogAn
{
    internal class FakeWebService : IWebService
    {
        public string last_error;
        public Exception exception_message = null;
        
        public void LogError(string error) {
            if (exception_message != null)
            {
                throw exception_message;
            }
            last_error = error; 
        }
    }
}
