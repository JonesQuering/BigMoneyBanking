using BigMoneyBanking.Models;

namespace BigMoneyBanking.Repositories;
public interface IBankAccountRepository
{
    Task<int> CreateBankAccount(int customerId, double initialDeposit, int accountTypeId);
    Task<int> UpdateBalance(int customerId, int accountId, double amount);
    Task<int> CloseBankAccount(int customerId, int accountId, int accountTypeId);
    Task<List<BankAccount>> GetBankAccounts(int customerId);

}