using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using SGBank.BLL;
using SGBank.Models;

namespace SGBank.UI.Workflows
{
    class TransferWorkflow
    {
        public void Execute(Account accountFrom, Account accountTo)
        {

            decimal amount = GetTransferAmount();

            var manager = new AccountManager();

            var response = manager.Transfer(accountFrom, accountTo, amount);

            if (response.Success)
            {
                Console.Clear();
                Console.WriteLine("Transfered {0:c} from account {1} to account {2}. New Balance is {3}.", response.Data.TransferAmount, response.Data.AccountNumberFrom, response.Data.AccountNumberTo, response.Data.NewBalance);
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
            else
            {
                Console.Clear();
                Console.WriteLine("An error occurred.  {0)", response.Message);
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }

        private decimal GetTransferAmount()
        {
            do
            {
                Console.Write("Enter a transfer amount: ");
                var input = Console.ReadLine();
                decimal amount;

                if (decimal.TryParse(input, out amount))
                    return amount;

                Console.WriteLine("That was not a valid amount. Please try again.");
                Console.ReadKey();
            } while (true);
        }

        public int GetTransferNumberFromUser()
        {
            do
            {
                //Console.Clear();
                Console.WriteLine("Enter the account number you wish to transfer to: ");
                string input = Console.ReadLine();
                int accountNumber;

                if (int.TryParse(input, out accountNumber))
                    return accountNumber;

                Console.WriteLine("That was not a valid account number.  Press any key to try again...");
                Console.ReadKey();
            } while (true);
        }
    }
}
