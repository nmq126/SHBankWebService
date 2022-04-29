using SHBankWebService.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace SHBankWebService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        private Context db;
        public bool Deposit(string token, double amount)
        {
            var existAccount = CheckToken(token);
            if (existAccount == null || amount <=0)
            {
                return false;
            }
            using (DbContextTransaction transaction = db.Database.BeginTransaction())
            {
                try
                {
                    existAccount.Balance += amount;
                    db.Accounts.AddOrUpdate(existAccount);
                    TransactionHistory transactionHistory = new TransactionHistory()
                    {
                        Type = 2,
                        Amount = amount,
                        SenderAccountNumber = existAccount.AccountNumber,
                        ReceiverAccountNumber = existAccount.AccountNumber,
                        Status = 1,
                        CreatedAt = DateTime.Now
                    };
                    db.TransactionHistories.Add(transactionHistory);
                    db.SaveChanges();
                    transaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }

        public string Login(string accountNumber, string code)
        {
            var existAccount = CheckValidAccount(accountNumber);
            if (existAccount.SecurityCode != code)
            {
                return null;
            }
            existAccount.Token = CreateToken(accountNumber);
            db.SaveChanges();
            return existAccount.Token;
        }

        public bool Transfer(string token, string receiverAccountNumber, double amount)
        {
            var senderAccount = CheckToken(token);
            var receiverAccount = CheckValidAccount(receiverAccountNumber);
            if (senderAccount == receiverAccount || senderAccount == null || receiverAccount == null || amount <=0 || amount > senderAccount.Balance)
            {
                return false;
            }
            using (DbContextTransaction transaction = db.Database.BeginTransaction())
            {
                try
                {
                    senderAccount.Balance -= amount;
                    receiverAccount.Balance += amount;
                    db.Accounts.AddOrUpdate(senderAccount);
                    db.Accounts.AddOrUpdate(receiverAccount);
                    TransactionHistory transactionHistory = new TransactionHistory()
                    {
                        Type = 3,
                        Amount = amount,
                        SenderAccountNumber = senderAccount.AccountNumber,
                        ReceiverAccountNumber = receiverAccountNumber,
                        Status = 1,
                        CreatedAt = DateTime.Now
                    };
                    db.TransactionHistories.Add(transactionHistory);
                    db.SaveChanges();
                    transaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }
        public bool Withdraw(string token, double amount)
        {
            var existAccount = CheckToken(token);
            if (existAccount == null || amount <= 0 || amount > existAccount.Balance)
            {
                return false;
            }
            using (DbContextTransaction transaction = db.Database.BeginTransaction())
            {
                try
                {
                    existAccount.Balance -= amount;
                    db.Accounts.AddOrUpdate(existAccount);
                    TransactionHistory transactionHistory = new TransactionHistory()
                    {
                        Type = 1,
                        Amount = amount,
                        SenderAccountNumber = existAccount.AccountNumber,
                        ReceiverAccountNumber = existAccount.AccountNumber,
                        Status = 1,
                        CreatedAt = DateTime.Now
                    };
                    db.TransactionHistories.Add(transactionHistory);
                    db.SaveChanges();
                    transaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }
        public Account CheckValidAccount(string accountNumber)
        {
            var existAccount = db.Accounts.Find(accountNumber);
            if (existAccount == null || existAccount.Status == -1)
            {
                return null;
            }
            return existAccount;
        }
        public Account CheckToken(string token)
        {
            var account = db.Accounts.Where(a => a.Token == token).First();
            return account;
        }

        public string CreateToken(string accountNumber)
        {
            string random = Guid.NewGuid().ToString();
            string token = accountNumber + random;
            return token;
        }

        public List<TransactionHistory> FindTransactionHistoriesByToken(string token)
        {
            var existAccount = CheckToken(token);
            if (existAccount == null)
            {
                return null;
            }
            List<TransactionHistory> transactionHistories = (List<TransactionHistory>)db.TransactionHistories.Where(t =>
            (t.SenderAccountNumber == existAccount.AccountNumber) &&
            (t.ReceiverAccountNumber == existAccount.AccountNumber));
            return transactionHistories;
        }
    }
}
