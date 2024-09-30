using System;
using System.Collections.Generic;
using System.Text;

namespace KTPO4311.Burakshaev.Lib.src.LogAn
{
    public class WebServiceFactory
    {
        private static IWebService webService = null;

        public static IWebService Create()
        {
            if (webService != null)
            {
                return webService;
            }
            return new WebService();
        }
        public static void SetService(IWebService service)
        {
            webService = service;
        }
    }
}
