using System.Text.Json.Serialization;

namespace BigMoneyBanking.Models;
public class BankAccount{
    public int customerId {get; set;}
    public int accountId {get; set;}
    public int accountTypeId {get; set;}
    public double balance {get; set;}
    [JsonIgnore]
    public bool Inactive {get; set;}
    public bool succeeded {get; set;}

}
public class CloseBankAccountRequest{
    public int customerId {get; set;}
    public int accountId {get; set;}
    [JsonIgnore]
    public int accountTypeId {get; set;}
    public bool succeeded {get; set;}

}
public class BankAccountRequest{
    public int customerId {get; set;}
    public double initialDeposit {get; set;}
    public int accountId {get; set;}
    public int accountTypeId {get; set;}
    public double amount {get; set;}
}