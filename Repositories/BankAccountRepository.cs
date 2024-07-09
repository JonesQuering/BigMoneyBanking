using BigMoneyBanking.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace BigMoneyBanking.Repositories
{
    public class BankAccountRepository : IBankAccountRepository
    {
        private readonly string _connectionString;

        public BankAccountRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<int> CloseBankAccount(int customerId, int accountId, int accountTypeId)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    using (var command = new SqlCommand("p.CloseBankAccount", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        var customerIdParam = new SqlParameter("@customerId", SqlDbType.Int);
                        var accountIdParam = new SqlParameter("@accountId", SqlDbType.Int);
                        var accountTypeIdParam = new SqlParameter("@accountTypeId", SqlDbType.Int);
                        command.Parameters.AddRange(new[] { customerIdParam, accountIdParam, accountTypeIdParam });
                        return await command.ExecuteNonQueryAsync();
                    }
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> CreateBankAccount(int customerId, double initialDeposit, int accountTypeId)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    using (var command = new SqlCommand("p.CreateBankAccount", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        var customerIdParam = new SqlParameter("@customerId", SqlDbType.Int);
                        var initialDepositParam = new SqlParameter("@deposit", SqlDbType.Money);
                        var accountTypeIdParam = new SqlParameter("@accountTypeId", SqlDbType.Int);
                        command.Parameters.AddRange(new[] { customerIdParam, initialDepositParam, accountTypeIdParam });
                        return await command.ExecuteNonQueryAsync();
                    }
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<BankAccount>> GetBankAccounts(int customerId)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    using (var command = new SqlCommand("p.GetBankAccounts", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        var customerIdParam = new SqlParameter("@customerId", SqlDbType.Int);
                        command.Parameters.Add(customerIdParam);
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            var bankAccounts = new List<BankAccount>();
                            while (await reader.ReadAsync())
                            {
                                bankAccounts.Add(new BankAccount
                                {
                                    customerId = reader["customerId"] != DBNull.Value ? Convert.ToInt32(reader["customerId"]) : 0,
                                    accountId = reader["accountId"] != DBNull.Value ? Convert.ToInt32(reader["accountId"]) : 0,
                                    accountTypeId = reader["accountTypeId"] != DBNull.Value ? Convert.ToInt32(reader["accountTypeId"]) : 0,
                                    balance = reader["balance"] != DBNull.Value ? Convert.ToDouble(reader["balance"]) : 0
                                });
                            }
                            return bankAccounts;
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<int> UpdateBalance(int customerId, int accountId, double amount)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    using (var command = new SqlCommand("p.UpdateBalance", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        var customerIdParam = new SqlParameter("@customerId", SqlDbType.Int);
                        var accountIdParam = new SqlParameter("@accountId", SqlDbType.Int);
                        var amountParam = new SqlParameter("@amount", SqlDbType.Money);
                        command.Parameters.AddRange(new[] { customerIdParam, accountIdParam, amountParam });
                        return await command.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}