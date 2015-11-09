using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGBank.Models;
using System.IO;
using System.Xml;

namespace SGBank.Data
{
    public class AccountRepository
    {
        private const string FilePath = @"DataFiles\Bank.txt";

        public List<Account> GetAllAccounts()
        {
            List<Account> accounts = new List<Account>();

            var reader = File.ReadAllLines(FilePath);

            for (int i = 1; i < reader.Length; i++)
            {
                var columns = reader[i].Split(',');

                var account = new Account();

                account.AccountNumber = Int32.Parse(columns[0]);
                account.FirstName = columns[1];
                account.LastName = columns[2];
                account.Balance = Decimal.Parse(columns[3]);

                accounts.Add(account);
            }

            return accounts;
        }

        public Account LoadAccount(int accountNumber)
        {
            List<Account> accounts = GetAllAccounts();
            return accounts.FirstOrDefault(a => a.AccountNumber == accountNumber);
        }

        public void UpdateAccount(Account accountToUpdate)
        {
            var accounts = GetAllAccounts();

            var existingAccount = accounts.First(a => a.AccountNumber == accountToUpdate.AccountNumber);
            existingAccount.FirstName = accountToUpdate.FirstName;
            existingAccount.LastName = accountToUpdate.LastName;
            existingAccount.Balance = accountToUpdate.Balance;

            OverwriteFile(accounts);
        }

        public void CreateAccount(Response<CreateReceipt> toCreate)
        {
            string createNewInfo = Environment.NewLine + toCreate.Data.AccountNumber + "," + toCreate.Data.FirstName + "," +
                                   toCreate.Data.LastName + "," + toCreate.Data.InitalAmount;
            File.AppendAllText(FilePath, createNewInfo);
        }

        public void OverwriteFile(List<Account> accounts, int toDelete = 0)
        {
            File.Delete(FilePath);

            using (var writer = File.CreateText(FilePath))
            {
                writer.WriteLine("AccountNumber,FirstName,LastName,Balance");

                foreach (var account in accounts)
                {
                    if (account.AccountNumber == toDelete)
                        continue;
                    writer.WriteLine("{0},{1},{2},{3}", account.AccountNumber, account.FirstName,account.LastName,account.Balance);
                }
            }
        }
    }
}
