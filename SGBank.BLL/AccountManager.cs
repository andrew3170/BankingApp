using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using SGBank.Data;
using SGBank.Models;

namespace SGBank.BLL
{
    public class AccountManager
    {
        public Response<Account> GetAccount(int accountNumber)
        {
            var repo = new AccountRepository();
            var response = new Response<Account>();

            try
            {
                var account = repo.LoadAccount(accountNumber);

                if (account == null)
                {
                    response.Success = false;
                    response.Message = "Account was not found!";
                }
                else
                {
                    response.Success = true;
                    response.Data = account;
                }
            }
            catch (Exception ex)
            {
                // log the exception
                response.Success = false;
                response.Message = "There was an error.  Please try again later.";
            }

            return response;
        }

        public Response<DepositReciept> Deposit(Account account, decimal amount)
        {
            
            var response = new Response<DepositReciept>();

            try
            {
                if (amount <= 0)
                {
                    response.Success = false;
                    response.Message = "Must deposit a positive value.";
                }
                else
                {
                    account.Balance += amount;
                    var repo = new AccountRepository();
                    repo.UpdateAccount(account);
                    response.Success = true;

                    response.Data = new DepositReciept();
                    response.Data.AccountNumber = account.AccountNumber;
                    response.Data.DepositAmount = amount;
                    response.Data.NewBalance = account.Balance;
                }
            }

            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public Response<WithdrawReciept> Withdraw(Account account, decimal amount)
        {

            var response = new Response<WithdrawReciept>();

            try
            {
                if (amount <= 0 )
                {
                    response.Success = false;
                    response.Message = "Must withdraw a positive value.";
                }
                else if (amount > account.Balance)
                {
                    response.Success = false;
                    response.Message = "Insufficient Funds";
                }
                else
                {
                    account.Balance -= amount;
                    var repo = new AccountRepository();
                    repo.UpdateAccount(account);
                    response.Success = true;

                    response.Data = new WithdrawReciept();
                    response.Data.AccountNumber = account.AccountNumber;
                    response.Data.WithdrawAmount = amount;
                    response.Data.NewBalance = account.Balance;
                }
            }

            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public Response<TransferReceipt> Transfer(Account accountFrom, Account accountTo, decimal amount)
        {
            var response = new Response<TransferReceipt>();

            try
            {
                if (amount <= 0)
                {
                    response.Success = false;
                    response.Message = "Must transfer a positive value.";
                }
                else
                {
                    accountFrom.Balance -= amount;
                    accountTo.Balance += amount;
                    var repoFrom = new AccountRepository();
                    repoFrom.UpdateAccount(accountFrom);
                    repoFrom.UpdateAccount(accountTo);
                    response.Success = true;

                    response.Data = new TransferReceipt();
                    response.Data.AccountNumberFrom = accountFrom.AccountNumber;
                    response.Data.TransferAmount = amount;
                    response.Data.NewBalance = accountFrom.Balance;

                    response.Data = new TransferReceipt();
                    response.Data.AccountNumberTo = accountTo.AccountNumber;
                    response.Data.TransferAmount = amount;
                    response.Data.NewBalance = accountTo.Balance;
                }
            }

            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public Response<CreateReceipt> Create(string[] newAccount)
        {
            AccountRepository repo = new AccountRepository();
            var response = new Response<CreateReceipt>();

            var allAccounts = repo.GetAllAccounts();
            List<int> accountNums = new List<int>();

            foreach (var accout in allAccounts)
            {
               accountNums.Add(accout.AccountNumber);
            }

            int freshAccountNumber = accountNums.Max() + 1;

            try
            {
                if (Decimal.Parse(newAccount[2]) <= 0)
                {
                    response.Success = false;
                    response.Message = "Must initially deposit a positive value.";
                }
                else
                {
                    response.Data = new CreateReceipt();
                    response.Data.AccountNumber = freshAccountNumber;
                    response.Data.FirstName = newAccount[0];
                    response.Data.LastName = newAccount[1];
                    response.Data.InitalAmount = Decimal.Parse(newAccount[2]);
                    response.Success = true;
                    response.Message = "Account successfully created.  Press any key to continue...";
                    repo.CreateAccount(response);
                }
            }

            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public Response<DeleteReceipt> Delete(int accountNumber)
        {
            var repo = new AccountRepository();
            var response = new Response<DeleteReceipt>();

            try
            {
                var account = repo.GetAllAccounts();

                if (account == null)
                {
                    response.Success = false;
                    response.Message = "Account was not found!";
                }
                else
                {
                    response.Success = true;
                    repo.OverwriteFile(account, accountNumber);
                }
            }
            catch (Exception ex)
            {
                // log the exception
                response.Success = false;
                response.Message = "There was an error.  Please try again later.";
            }

            return response;
        } 
    }
}
