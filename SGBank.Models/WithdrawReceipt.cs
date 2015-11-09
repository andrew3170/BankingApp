using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGBank.Models
{
    public class WithdrawReciept
    {
        public int AccountNumber { get; set; }
        public decimal WithdrawAmount { get; set; }
        public decimal NewBalance { get; set; }
    }
}
