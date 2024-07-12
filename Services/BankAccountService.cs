using BigMoneyBanking.Models;
using BigMoneyBanking.Repositories;

namespace BigMoneyBanking.Services;

public class BankAccountService : IBankAccount
{
    private readonly BankAccountRepository _bankAccountRepository;
    /// <summary>
    /// Constructor takes in a BankAccountRepository using dependency injection
    /// </summary>
    /// <param name="bankAccountRepository"></param>
    public BankAccountService(BankAccountRepository bankAccountRepository)
    {
        _bankAccountRepository = bankAccountRepository;

    }
    /// <summary>
    /// GetAccount method returns a BankAccount object from the repository
    /// </summary>
    /// <param name="bankAccount"></param>
    /// <returns></returns>
    private BankAccount GetAccount(BankAccount bankAccount) => _bankAccountRepository.BankAccounts
        ?.FirstOrDefault(b => b.accountId == bankAccount.accountId
        && b.customerId == bankAccount.customerId) ?? new BankAccount();
    /// <summary>
    /// AccountExists method checks if a BankAccount object exists in the repository
    /// </summary>
    /// <param name="bankAccount"></param>
    /// <returns></returns>
    private bool AccountExists (BankAccount bankAccount)
    {
        return _bankAccountRepository.BankAccounts != null &&
         _bankAccountRepository.BankAccounts
         .Any(b => b.accountId == bankAccount.accountId
         && b.customerId == bankAccount.customerId);

    }
    /// <summary>
    /// CreateBankAccount method creates a BankAccount object in the repository
    /// validate the customer exists
    /// , validate the account does not exist
    /// , validate the account if first account is a savings account
    /// , validate the account type is checkings or savings -- this probably should be forced before we get here
    /// , validate the initial deposit is at least 100
    /// </summary>
    /// <param name="bankAccount"></param>
    /// <returns></returns>
    public BankAccount CreateBankAccount(BankAccount bankAccount)
    {
        if (bankAccount.customerId == 0)
        {
            bankAccount.succeeded = false;
            bankAccount.ErrorMessage = "Customer must exist";
        }
        else if (AccountExists(bankAccount))
        {
            bankAccount.succeeded = false;
            bankAccount.ErrorMessage = "Account already exists";
        }
        else if (_bankAccountRepository.BankAccounts != null 
        && !_bankAccountRepository.BankAccounts
        .Any(b=> b.customerId == bankAccount.customerId) 
        && bankAccount.accountTypeId != 2)
        {
            bankAccount.succeeded = false;
            bankAccount.ErrorMessage = "Customer must open savings first";
        }
        else if (bankAccount.accountTypeId != 1 
        && bankAccount.accountTypeId != 2)
        {
            bankAccount.succeeded = false;
            bankAccount.ErrorMessage = "Account type must be checkings or savings";
        }

        else if (bankAccount.balance < 100)
        {
            bankAccount.succeeded = false;
            bankAccount.ErrorMessage = "Initial deposit must be at least 100";
        }
        else
        return _bankAccountRepository.CreateBankAccount(bankAccount);

        return bankAccount;
    }
/// <summary>
/// UpdateBalance method updates the balance of a BankAccount object in the repository
/// validate the account exists
/// , validate if a withdrawl is being made that the account has sufficient funds
/// ,validate a deposit is greater than 0
/// 
/// </summary>
/// <param name="bankAccount"></param>
/// <returns></returns>
    public BankAccount UpdateBalance(BankAccount bankAccount)
    {
        if (!AccountExists(bankAccount))
        {
            bankAccount.ErrorMessage = "Account does not exist";
            bankAccount.succeeded = false;
            return bankAccount;
        }
       
        var oldAccount = GetAccount(bankAccount);
        if (oldAccount != null 
        && oldAccount.balance != 0
        && ((oldAccount.balance + bankAccount.balance) < 0))
        {
            bankAccount.ErrorMessage = "Insufficient funds";
            bankAccount.succeeded = false;
            bankAccount.balance = oldAccount.balance;
        }
        else if (bankAccount.balance == 0)
        {
            bankAccount.ErrorMessage = "Amount must be greater than 0";
            bankAccount.succeeded = false;
        }
        else
        {
            return _bankAccountRepository.UpdateBalance(bankAccount);
        }
        return bankAccount;
    }
/// <summary>
/// CloseAccount method set a BankAccount object invalid in the repository
/// validate a customers account 
/// , validate the account balance is 0
/// </summary>
/// <param name="bankAccount"></param>
/// <returns></returns>
    public BankAccount CloseAccount(BankAccount bankAccount)
    {
        if (!AccountExists(bankAccount))
        {
            bankAccount.ErrorMessage = "Account does not exist";
            bankAccount.succeeded = false;
            return bankAccount;
        }
        var currentAccount = GetAccount(bankAccount);
        if (currentAccount.balance != 0)
        {

            currentAccount.ErrorMessage = "Account balance must be 0";
            currentAccount.succeeded = false;
            return currentAccount;
        }
 
        return _bankAccountRepository.CloseAccount(bankAccount);
    }
    /// <summary>
    /// GetBankAccounts method returns a list of BankAccount objects from the repository
    /// check balance for the accounts of the customer 
    /// </summary>
    /// <param name="customerId"></param>
    /// <returns></returns>
    public List<BankAccount> GetBankAccounts(int customerId)
    {
        return _bankAccountRepository.GetBankAccounts(customerId);
    }
}