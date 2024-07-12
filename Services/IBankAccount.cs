using BigMoneyBanking.Models;

namespace BigMoneyBanking;

public interface IBankAccount
{
    BankAccount CreateBankAccount(BankAccount bankAccount);
    BankAccount UpdateBalance(BankAccount bankAccount);
    BankAccount CloseAccount(BankAccount bankAccount);
    List<BankAccount> GetBankAccounts(int customerId);
}