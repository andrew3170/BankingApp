using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGBank.Models
{
    public class CreateReceipt
    {
        public int AccountNumber { get; set; }
        public decimal InitalAmount { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
