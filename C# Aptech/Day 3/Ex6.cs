public class BankAccount(int accountNumber, string owner, double balance)
{
    public int AccountNumber {get;} = accountNumber;
    private string Owner  {get;} = !string.IsNullOrWhiteSpace(owner) ? owner : throw new ArgumentException("Owner cannot be empty", nameof(owner));
    
}