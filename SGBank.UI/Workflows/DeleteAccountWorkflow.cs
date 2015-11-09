using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGBank.BLL;
using SGBank.Models;

namespace SGBank.UI.Workflows
{
    public class DeleteAccountWorkflow
    {
        public void DeleteAccount()
        {
            var manager = new AccountManager();

            Console.WriteLine("Please provide the account number you wish to delete: ");
            int toDelete = Int32.Parse(Console.ReadLine());

            var response = manager.Delete(toDelete);

            if (response.Success)
            {
                Console.WriteLine("Account number {0} has been deleted.", toDelete);
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

            manager.Delete(toDelete);


        }
    }
}
