using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGBank.Models
{
    public class TransferReceipt
    {
        public int AccountNumberFrom { get; set; }
        public int AccountNumberTo { get; set; }
        public decimal TransferAmount { get; set; }
        public decimal NewBalance { get; set; }
    }
}
