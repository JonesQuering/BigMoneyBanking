using BigMoneyBanking.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace BigMoneyBanking.Repositories
{
    public class BankAccountRepository : IBankAccount
    {
        private readonly string _connectionString;
        public List<BankAccount> BankAccounts = new List<BankAccount>();

        public BankAccountRepository(string connectionString)
        {
            BankAccounts = new List<BankAccount>();
            _connectionString = connectionString;
        }

        public BankAccount CreateBankAccount(BankAccount bankAccount)
        {
            //try
            //    {
            //    using (var connection = new SqlConnection(_connectionString))
            //    {
            //        await connection.OpenAsync();
            //        using (var command = new SqlCommand("p.CreateBankAccount", connection))
            //        {
            //            command.CommandType = CommandType.StoredProcedure;
            //            var customerIdParam = new SqlParameter("@customerId", SqlDbType.Int)
            //            {
            //                Value = bankAccount.customerId,
            //                Direction = ParameterDirection.Input
            //            };
            //            var initialDepositParam = new SqlParameter("@deposit", SqlDbType.Money)
            //            {
            //                Value = bankAccount.balance,
            //                Direction = ParameterDirection.Input
            //            };
            //            var accountTypeIdParam = new SqlParameter("@accountTypeId", SqlDbType.Int)
            //            {
            //                Value = bankAccount.accountTypeId,
            //                Direction = ParameterDirection.Input
            //            };
            //            command.Parameters.AddRange(new[] { customerIdParam, initialDepositParam, accountTypeIdParam });
            //            var result = await command.ExecuteNonQueryAsync();

            //            if (result > 0)
            //            {
            //                bankAccount.succeeded = false;
            //            }
            //            else
            //            {
            //                bankAccount.succeeded = true;
            //            }

            //        }
            //    }

            //}
            //catch (SqlException ex)
            //{
            //    Console.WriteLine($"SQL {ex.Message}");
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //    throw;
            //}
            bankAccount.succeeded = true;
            BankAccounts.Add(bankAccount);
            return bankAccount;
        }
        public  BankAccount UpdateBalance(BankAccount bankAccount)
        {
            var prevBalanceAccount = BankAccounts.First(b => b.accountId == bankAccount.accountId && b.customerId == bankAccount.customerId);
            prevBalanceAccount.balance += bankAccount.balance;
            bankAccount = prevBalanceAccount;
            bankAccount.succeeded = true;
            //try
            //{
            //    using (var connection = new SqlConnection(_connectionString))
            //    {
            //        await connection.OpenAsync();
            //        using (var command = new SqlCommand("p.UpdateBalance", connection))
            //        {
            //            command.CommandType = CommandType.StoredProcedure;
            //            var customerIdParam = new SqlParameter("@customerId", SqlDbType.Int)
            //            {
            //                Value = bankAccount.customerId,
            //                Direction = ParameterDirection.Input
            //            };
            //            var accountIdParam = new SqlParameter("@accountId", SqlDbType.Int)
            //            {
            //                Value = bankAccount.accountId,
            //                Direction = ParameterDirection.Input
            //            };
            //            var amountParam = new SqlParameter("@amount", SqlDbType.Money)
            //            {
            //                Value = bankAccount.balance,
            //                Direction = ParameterDirection.Input
            //            };
            //            command.Parameters.AddRange(new[] { customerIdParam, accountIdParam, amountParam });
            //            var result = await command.ExecuteNonQueryAsync();
            //            if (result > 0)
            //            {
            //                bankAccount.succeeded = false;
            //            }
            //            else
            //            {
            //                bankAccount.succeeded = true;
            //            }

            //        }
            //    }

            //}
            //catch (SqlException ex)
            //{
            //    Console.WriteLine($"SQL Error: {ex.Message}");
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //    throw;
            //}
            return bankAccount;
        }

        public BankAccount CloseAccount(BankAccount bankAccount)
        {

            var account = BankAccounts.First(b => b.accountId == bankAccount.accountId && b.customerId == bankAccount.customerId);
            account.Inactive = true;
            account.succeeded = true;
            bankAccount = account;

            //try
            //{
            //    using (var connection = new SqlConnection(_connectionString))
            //    {
            //        await connection.OpenAsync();
            //        using (var command = new SqlCommand("p.CloseBankAccount", connection))
            //        {
            //            command.CommandType = CommandType.StoredProcedure;
            //            var customerIdParam = new SqlParameter("@customerId", SqlDbType.Int)
            //            {
            //                Value = bankAccount.customerId,
            //                Direction = ParameterDirection.Input
            //            };
            //            var accountIdParam = new SqlParameter("@accountId", SqlDbType.Int)
            //            {
            //                Value = bankAccount.accountId,
            //                Direction = ParameterDirection.Input
            //            };
            //            var accountTypeIdParam = new SqlParameter("@accountTypeId", SqlDbType.Int)
            //            {
            //                Value = bankAccount.accountTypeId,
            //                Direction = ParameterDirection.Input
            //            };
            //            command.Parameters.AddRange(new[] { customerIdParam, accountIdParam, accountTypeIdParam });
            //            var result = await command.ExecuteNonQueryAsync();

            //            if (result > 0)
            //            {
            //                bankAccount.succeeded = false;
            //            }
            //            else
            //            {
            //                bankAccount.Inactive = true;
            //                bankAccount.succeeded = true;
            //            }
            //        }
            //    }

            //}
            //catch (SqlException ex)
            //{
            //    Console.WriteLine($"SQL Error: {ex.Message}");
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //    throw;
            //}
            return bankAccount;
        }

        public  List<BankAccount> GetBankAccounts(int customerId)
        {
            
            return BankAccounts.Where(b => b.customerId == customerId).ToList();
            //try
            //{
            //    using (var connection = new SqlConnection(_connectionString))
            //    {
            //        await connection.OpenAsync();
            //        using (var command = new SqlCommand("p.GetBankAccounts", connection))
            //        {
            //            command.CommandType = CommandType.StoredProcedure;
            //            var customerIdParam = new SqlParameter("@customerId", SqlDbType.Int);
            //            command.Parameters.Add(customerIdParam);
            //            using (var reader = await command.ExecuteReaderAsync())
            //            {
            //                while (await reader.ReadAsync())
            //                {
            //                    bankAccounts.Add(new BankAccount
            //                    {
            //                        customerId = reader["customerId"] != DBNull.Value ? Convert.ToInt32(reader["customerId"]) : 0,
            //                        accountId = reader["accountId"] != DBNull.Value ? Convert.ToInt32(reader["accountId"]) : 0,
            //                        accountTypeId = reader["accountTypeId"] != DBNull.Value ? Convert.ToInt32(reader["accountTypeId"]) : 0,
            //                        balance = reader["balance"] != DBNull.Value ? Convert.ToDouble(reader["balance"]) : 0
            //                    });
            //                }
            //                return bankAccounts;
            //            }
            //        }
            //    }
            //}
            //catch (SqlException ex)
            //{
            //    Console.WriteLine($"SQL Error: {ex.Message}");
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //    throw;
            //}
         
        }



    }
}