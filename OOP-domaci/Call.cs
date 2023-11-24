using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_domaci
{
    public class Call
    {
        public Call(DateTime callingTime, string status)
        {
            CallingTime = callingTime;
            Status = status;
        }
        public DateTime CallingTime { get; set; }
        public string Status { get; set; }
    }
}
