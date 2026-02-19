using Microsoft.AspNetCore.Components;
using DotNet8.BankingManagementSystem.Frontend.Api.Features;
using DotNet8.BankingManagementSystem.Models.Account;
using DotNet8.BankingManagementSystem.Models.TransactionHistory;

namespace DotNet8.BankingManagementSystem.Frontend.Pages;

public partial class Home : ComponentBase
{
    private decimal TotalBalance { get; set; }
    private int TotalAccounts { get; set; }
    private int TotalCustomers { get; set; }
    private int RecentTransactionsCount { get; set; }
    private List<TransactionHistoryModel> RecentTransactions { get; set; } = new();
    private bool IsLoading { get; set; } = true;

    protected override async Task OnInitializedAsync()
    {
        await LoadDashboardData();
    }

    private async Task LoadDashboardData()
    {
        try
        {
            IsLoading = true;
            
            // In a real app, we might have a dedicated Dashboard API.
            // For now, we aggregate from existing endpoints.
            var accountsResponse = await ApiService.GetAccounts();
            if (accountsResponse?.Data != null)
            {
                TotalAccounts = accountsResponse.Data.Count;
                TotalBalance = accountsResponse.Data.Sum(x => x.Balance);
                TotalCustomers = accountsResponse.Data.Select(x => x.CustomerCode).Distinct().Count();
            }

            var transactionsResponse = await ApiService.TransactionHistory(1, 5);
            if (transactionsResponse?.Data != null)
            {
                RecentTransactions = transactionsResponse.Data;
                RecentTransactionsCount = RecentTransactions.Count;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading dashboard: {ex}");
        }
        finally
        {
            IsLoading = false;
            StateHasChanged();
        }
    }
}
