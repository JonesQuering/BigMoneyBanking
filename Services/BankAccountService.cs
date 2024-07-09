namespace BigMoneyBanking.Services;
using BigMoneyBanking.Models;
public static class BankAccountService
{
    static int customerId {get;}
    private static List<BankAccount> bankAccounts {get; set;}
    static BankAccountService()
    {
        bankAccounts = new List<BankAccount>();
    }
    public static BankAccount CreateBankAccount(int customerId, double initialDeposit, int accountTypeId)
    {
        if (customerId == 0)
        {
            throw new Exception("customer must exists");
        }
        if (bankAccounts != null &&
         bankAccounts.Any(b => b.accountId == 17 && b.accountTypeId == accountTypeId))
        {
            throw new Exception("account already exists");
        }
        if (bankAccounts != null && !bankAccounts.Any() && accountTypeId != 2){
                throw new Exception("first customer account must be savings");
        }
        if (accountTypeId != 1 && accountTypeId != 2){
            throw new Exception("account type must be checkings or savings");
        }

        if (initialDeposit <= 0)
        {
            throw new Exception("initial deposit must be at least 100");
        }
        BankAccount bankAccount = new BankAccount
        {
            customerId = customerId
            ,accountId = 17
            ,accountTypeId = accountTypeId
            ,balance = initialDeposit
            ,Inactive = false
            ,succeeded = true
        };
        bankAccounts?.Add(bankAccount);
        return bankAccount;
    }
    public static BankAccount UpdateBalance(int customerId, int accountId, double amount)
    {
        
        if (bankAccounts == null || !bankAccounts.Any(b => b.accountId == accountId && b.customerId == customerId))
        {
            throw new Exception("account does not exist");
        }
        BankAccount bankAccount = bankAccounts.First(b => b.accountId == accountId && b.customerId == customerId);
        if (bankAccount.balance + amount < 0 || amount == 0)
        {
            bankAccount.succeeded = false;
        }
        else
        {
            bankAccount.succeeded = true;
            bankAccount.balance +=  amount;
        }
       
        return bankAccount;
    }
    public static bool CloseBankAccount(int customerId, int accountId, int accountTypeId)
    {
        if (bankAccounts == null || !bankAccounts.Any(b => b.accountId == accountId && b.customerId == customerId && b.accountTypeId == accountTypeId))
        {
            throw new Exception("account does not exist");
        }
        if (bankAccounts.First(b => b.accountId == accountId 
        && b.customerId == customerId 
        && b.accountTypeId == accountTypeId).balance != 0)
        {
            throw new Exception("account balance must be 0");
        }
        bankAccounts.First(b => b.accountId == accountId 
        && b.customerId == customerId
        && b.accountTypeId == accountTypeId).Inactive = true;
        return true;
    }
    public static List<BankAccount> GetBankAccounts(int customerId)
    {
        if (bankAccounts == null || !bankAccounts.Any(b => b.customerId == customerId))
        {
            throw new Exception("customer does not have any accounts");
        }
        return bankAccounts.Where(b => b.customerId == customerId).ToList();
    }
}