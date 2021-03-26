using System;
using System.Collections.Generic;
using System.Text;

namespace HRM.Models
{
    public class HttpResponsiveViewModel
    {
        public object Data { get; set; }
        public bool Result { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }

    }
}
