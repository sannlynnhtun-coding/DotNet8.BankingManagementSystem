namespace DotNet8.BankingManagementSystem.Frontend.Api.Services;

public enum EnumService
{
    Tbl_User,
    Tbl_Account,
}

public static class EnumServiceExtensions
{
    public static string GetKeyName(this EnumService key)
    {
        switch (key)
        {
            case EnumService.Tbl_User:
                return "Tbl_User";
            case EnumService.Tbl_Account:
                return "Tbl_Account";
            default:
                throw new ArgumentException("Invalid enum value", nameof(key));
        }
    }
}