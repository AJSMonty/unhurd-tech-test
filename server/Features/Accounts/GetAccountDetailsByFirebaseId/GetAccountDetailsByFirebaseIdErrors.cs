using Unhurd.Infrastructure.Common;

public static class AccountErrors
{
    public static readonly Error NotFound = new("Account.NotFound", "Account not found.");
    
    public static readonly Error AccountNotCreated = new("Account.AccountNotCreated", "Account not created.");
}
