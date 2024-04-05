namespace DotNet8.BankingManagementSystem.Frontend.Api.Services;

public enum EnumService
{
    Tbl_User,
    Tbl_Account,
    Tbl_State,
    Tbl_Township,
    Tbl_AdminUser,
    Tbl_TransactionHistory
}

public static class EnumServiceExtensions
{
    public static string GetKeyName(this EnumService key)
    {
        return key.ToString();
    }
}