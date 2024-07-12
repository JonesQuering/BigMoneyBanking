using BigMoneyBanking.Models;
using BigMoneyBanking.Services;
using Microsoft.AspNetCore.Mvc;

namespace BigMoneyBanking.Controllers;

[ApiController]
[Route("[controller]")]
public class BankAccountController : ControllerBase
{
    private readonly BankAccountService _bankAccountService;
    public BankAccountController(BankAccountService bankAccountService)
    {
        _bankAccountService = bankAccountService;
    }
    
    [HttpPost("{customerId}")]
    public IActionResult CreateBankAccount(int customerId, AccountCreationRequest request)
    {
        ///The endpoint will receive the following JSON:
        // {
        // 	customerId: 5,
        // 	initialDeposit: 525.00,
        // 	accountTypeId: 1 
        // }
        //         And should return:
        // {
        // 	customerId: 5,
        // 	accountId: 17,
        // 	accountTypeId: 1,
        // 	balance: 525.00,
        // 	succeeded: true
        // }

        try
        {
            var bankAccount = _bankAccountService.CreateBankAccount(new BankAccount
            {
                customerId = request.CustomerId,
                balance = request.InitialDeposit,
                accountId = 17,
                accountTypeId = request.AccountTypeId
            });
            return Ok(bankAccount);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    [HttpPut("{customerId}")]
    public IActionResult UpdateBalance(int customerId, BalanceChangeRequest request)
    {
        ///make a deposit
        ///The endpoint will receive the following JSON:
        // {
        // 	customerId: 5,
        // 	accountId: 17,
        // 	amount: 112.00
        // }
        //make a withdrawal
        ///The endpoint will receive the following JSON:
        // {
        // 	customerId: 5,
        // 	accountId: 17,
        // 	amount: 112.00
        // }
    ///And should return:
    // {
    // 	customerId: 5,
    // 	accountId: 17,
    // 	balance: 2287.13,
    // 	succeeded: true
    // }

        try
        {
            var bankAccount = _bankAccountService.UpdateBalance(new BankAccount
            {
                customerId = request.CustomerId,
                accountId = request.AccountId,
                balance = request.Amount
            });
            return Ok(bankAccount);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [HttpDelete("{customerId}")]
    public IActionResult CloseAccount(int customerId, CloseAccountRequest request)
    {
        ///close an account 
        ///The endpoint will receive the following JSON:
        // {
        // 	customerId: 5,
        // 	accountId: 17
        // }
        ///And should return:
        // {
        // 	customerId: 5,
        // 	accountId: 17,
        // 	succeeded: true
        // }

        try
        {
            var bankAccount = _bankAccountService.CloseAccount(new BankAccount
            {
                customerId = customerId,
                accountId = request.AccountId
            });
            
            return Ok(bankAccount);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [HttpGet("{customerId}")]
    public IActionResult GetBankAccounts(int customerId)
    {
        try
        {
            List<BankAccount> bankAccounts = _bankAccountService.GetBankAccounts(customerId);
            return Ok(bankAccounts);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}