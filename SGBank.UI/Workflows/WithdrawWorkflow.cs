using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGBank.BLL;
using SGBank.Models;

namespace SGBank.UI.Workflows
{
    public class WithdrawWorkflow
    {
        public void Execute(Account account)
        {
            decimal amount = GetWithdrawAmount();

            var manager = new AccountManager();

            var response = manager.Withdraw(account, amount);

            if (response.Success)
            {
                Console.Clear();
                Console.WriteLine("Withdrew {0:c} to account {1}.  New Balance is {2}.",
                    response.Data.WithdrawAmount, response.Data.AccountNumber, response.Data.NewBalance);
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
            else
            {
                Console.Clear();
                Console.WriteLine("An error occurred.  {0}", response.Message);
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }

        private decimal GetWithdrawAmount()
        {
            do
            {
                Console.Write("Enter a withdraw amount: ");
                var input = Console.ReadLine();
                decimal amount;

                if (decimal.TryParse(input, out amount))
                    return amount;

                Console.WriteLine("That was not a valid amount. Please try again.");
                Console.ReadKey();
            } while (true);
        }
    }
}
