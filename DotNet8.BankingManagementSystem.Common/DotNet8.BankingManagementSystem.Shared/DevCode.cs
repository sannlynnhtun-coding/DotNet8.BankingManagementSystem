namespace DotNet8.BankingManagementSystem.Shared;

public static class DevCode
{
    public static DateTime ToDateTime(this DateTime? dt)
    {
        return Convert.ToDateTime(dt);
    }

    public static string GenerateBankAccountNo()
    {
        Random random = new Random();
        int accountNumber = random.Next(100000, 999999); // Generates a number between 100000 and 999999
        string formattedAccountNumber = accountNumber.ToString("D6");
        return formattedAccountNumber;
    }
}