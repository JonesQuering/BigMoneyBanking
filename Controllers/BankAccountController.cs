using BigMoneyBanking.Models;
using BigMoneyBanking.Services;
using Microsoft.AspNetCore.Mvc;

namespace BigMoneyBanking.Controllers;

[ApiController]
[Route("[controller]")]
public class BankAccountController : ControllerBase
{
    [HttpPost("{customerId}")]
    public IActionResult CreateBankAccount(int customerId, BankAccountRequest bankAccountRequest)
    {
        try
        {
            BankAccount bankAccount = BankAccountService.CreateBankAccount(customerId, bankAccountRequest.initialDeposit, bankAccountRequest.accountTypeId);
            return Ok(bankAccount);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    [HttpPut("{customerId}")]
    public IActionResult UpdateBalance(int customerId, BankAccountRequest bankAccountRequest)
    {
        try
        {
            BankAccount bankAccount = BankAccountService.UpdateBalance(customerId, bankAccountRequest.accountId, bankAccountRequest.amount);
            return Ok(bankAccount);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [HttpDelete("{customerId}")]
    public IActionResult CloseBankAccount(int customerId, CloseBankAccountRequest closeBankAccount)
    {
        try
        {
            bool result = BankAccountService.CloseBankAccount(customerId, closeBankAccount.accountId, closeBankAccount.accountTypeId);
            return Ok(result);
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
            List<BankAccount> bankAccounts = BankAccountService.GetBankAccounts(customerId);
            return Ok(bankAccounts);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}