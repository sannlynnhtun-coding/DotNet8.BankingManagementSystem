namespace DotNet8.BankingManagementSystem.Shared;

public static class DevCode
{
    public static DateTime ToDateTime(this DateTime? dt)
    {
        return Convert.ToDateTime(dt);
    }
}