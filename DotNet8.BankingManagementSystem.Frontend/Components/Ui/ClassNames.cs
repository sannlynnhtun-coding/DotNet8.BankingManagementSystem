namespace DotNet8.BankingManagementSystem.Frontend.Components.Ui;

public static class ClassNames
{
    public static string Merge(params string?[] values)
        => string.Join(" ", values.Where(v => !string.IsNullOrWhiteSpace(v)).Select(v => v!.Trim()));
}

