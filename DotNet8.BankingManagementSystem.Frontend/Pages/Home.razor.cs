using Microsoft.AspNetCore.Components;
using DotNet8.BankingManagementSystem.Frontend.Features;
using DotNet8.BankingManagementSystem.Models.Account;
using DotNet8.BankingManagementSystem.Models.TransactionHistory;
using DotNet8.BankingManagementSystem.Models.Bank;
using DotNet8.BankingManagementSystem.Models.Branch;

namespace DotNet8.BankingManagementSystem.Frontend.Pages;

public partial class Home : ComponentBase
{
    private decimal TotalBalance { get; set; }
    private int TotalAccounts { get; set; }
    private int TotalCustomers { get; set; }
    private int RecentTransactionsCount { get; set; }
    private List<TransactionHistoryModel> RecentTransactions { get; set; } = new();
    private bool IsLoading { get; set; } = true;

    private List<BankModel> Banks { get; set; } = new();
    private List<BranchModel> Branches { get; set; } = new();
    private string? SelectedBankCode { get; set; }
    private string? SelectedBranchCode { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await LoadInitialData();
        await LoadDashboardData();
    }

    private async Task LoadInitialData()
    {
        var bankResponse = await ApiService.GetBanks();
        if (bankResponse.Response.IsSuccess)
        {
            Banks = bankResponse.Data;
        }
    }

    private async Task OnBankChanged(ChangeEventArgs e)
    {
        SelectedBankCode = e.Value?.ToString();
        SelectedBranchCode = null;
        Branches.Clear();
        if (!string.IsNullOrEmpty(SelectedBankCode))
        {
            var branchResponse = await ApiService.GetBranchesByBankCode(SelectedBankCode);
            if (branchResponse.Response.IsSuccess)
            {
                Branches = branchResponse.Data;
            }
        }
        await LoadDashboardData();
    }

    private async Task OnBranchChanged(ChangeEventArgs e)
    {
        SelectedBranchCode = e.Value?.ToString();
        await LoadDashboardData();
    }

    private async Task LoadDashboardData()
    {
        try
        {
            IsLoading = true;
            
            var accountsResponse = await ApiService.GetAccountList(0, 0, SelectedBankCode, SelectedBranchCode);
            if (accountsResponse?.Data != null)
            {
                TotalAccounts = accountsResponse.Data.Count;
                TotalBalance = accountsResponse.Data.Sum(x => x.Balance);
                TotalCustomers = accountsResponse.Data.Select(x => x.CustomerCode).Distinct().Count();
            }

            var requestModel = new TransactionHistorySearchModel
            {
                PageNo = 1,
                PageSize = 5,
                BankCode = SelectedBankCode,
                BranchCode = SelectedBranchCode,
                IsAll = false // Today by default
            };
            var transactionsResponse = await ApiService.TransactionHistoryWithDateRange(requestModel);
            if (transactionsResponse?.Data != null)
            {
                RecentTransactions = transactionsResponse.Data;
                RecentTransactionsCount = RecentTransactions.Count;
            }
            else
            {
                RecentTransactions = new();
                RecentTransactionsCount = 0;
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
