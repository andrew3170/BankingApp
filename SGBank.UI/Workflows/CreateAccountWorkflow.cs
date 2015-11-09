using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using SGBank.BLL;

namespace SGBank.UI.Workflows
{
    public class CreateAccountWorkflow
    {
        public void CreateNewAccount()
        {
            var manager = new AccountManager();

            string[] newAccountInfo = new string[3];

            Console.WriteLine("Please enter the first name of the customer: ");
            newAccountInfo[0] = Console.ReadLine();
            Console.WriteLine("Please enter the last name of the customer: ");
            newAccountInfo[1] = Console.ReadLine();
            Console.WriteLine("Please enter the initial amount of the first deposit: ");
            newAccountInfo[2] = Console.ReadLine();
            Console.Clear();

            manager.Create(newAccountInfo);
        }
    }
}
