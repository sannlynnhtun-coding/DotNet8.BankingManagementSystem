using DotNet8.BankingManagementSystem.Shared;
using System.Text.Json;
using System.Text;

namespace DotNet8.BankingManagementSystem.Frontend.Services;

public class DataGenerationService
{
    private readonly IndexedDbService _indexedDbService;
    private readonly Random _random = new Random();

    public DataGenerationService(IndexedDbService indexedDbService)
    {
        _indexedDbService = indexedDbService;
    }

    public async Task GenerateData(int accountCount, DateTime fromDate, DateTime toDate, IProgress<(int Percentage, string Message)> progress)
    {
        try
        {
            var tables = EnumServiceExtensions.GetStoreNames();
            await _indexedDbService.InitializeAsync(tables);
            
            foreach (var table in tables)
            {
                await _indexedDbService.ClearStoreAsync(table);
            }

            progress.Report((5, "Generating setup data (States & Townships)..."));

            // 1. Generate States
            var states = new List<TblPlaceState>
            {
                new() { StateCode = "YGN", StateName = "Yangon" },
                new() { StateCode = "MDY", StateName = "Mandalay" },
                new() { StateCode = "NPT", StateName = "Naypyidaw" },
                new() { StateCode = "SHN", StateName = "Shan" }
            };
            await _indexedDbService.AddObjectsAsync("Tbl_State", states);

            // 2. Generate Townships
            var townships = new List<TblPlaceTownship>
            {
                new() { StateCode = "YGN", TownshipCode = "T001", TownshipName = "Latha" },
                new() { StateCode = "YGN", TownshipCode = "T002", TownshipName = "Lanmadaw" },
                new() { StateCode = "MDY", TownshipCode = "T003", TownshipName = "Chanayethazan" },
                new() { StateCode = "MDY", TownshipCode = "T004", TownshipName = "Mahaaungmyay" },
                new() { StateCode = "NPT", TownshipCode = "T005", TownshipName = "Zabuthiri" },
                new() { StateCode = "SHN", TownshipCode = "T006", TownshipName = "Taunggyi" }
            };
            await _indexedDbService.AddObjectsAsync("Tbl_Township", townships);

            progress.Report((10, "Generating Banks and Branches..."));

            // 3. Generate Banks
            var banks = new List<TblBank>
            {
                new() { BankCode = "AYA", BankName = "Aya Bank" },
                new() { BankCode = "KBZ", BankName = "KBZ Bank" },
                new() { BankCode = "CB", BankName = "CB Bank" },
                new() { BankCode = "UAB", BankName = "UAB Bank" }
            };
            await _indexedDbService.AddObjectsAsync("Tbl_Bank", banks);

            // 4. Generate Branches
            var branches = new List<TblBranch>();
            foreach (var bank in banks)
            {
                int bCount = _random.Next(1, 3);
                for (int b = 0; b < bCount; b++)
                {
                    var township = townships[_random.Next(townships.Count)];
                    branches.Add(new TblBranch
                    {
                        BankCode = bank.BankCode,
                        BranchCode = $"{bank.BankCode}-{township.TownshipCode}-{b}",
                        BranchName = $"{bank.BankName} - {township.TownshipName} Branch",
                        StateCode = township.StateCode,
                        TownshipCode = township.TownshipCode
                    });
                }
            }
            await _indexedDbService.AddObjectsAsync("Tbl_Branch", branches);

            // 5. Generate Admin User
            var adminUsers = new List<TblAdminUser>
            {
                new() { AdminUserCode = "ADM001", AdminUserName = "System Admin", MobileNo = "09123456789", UserRoleCode = "Admin" }
            };
            await _indexedDbService.AddObjectsAsync("Tbl_AdminUser", adminUsers);

            progress.Report((20, "Generating users and accounts..."));

            List<TblAccount> accounts = new();
            List<TblUser> users = new();

            for (int i = 0; i < accountCount; i++)
            {
                var userCode = "U" + (1000 + i);
                var fullName = GenerateCustomerName();
                var branch = branches[_random.Next(branches.Count)];

                var user = new TblUser
                {
                    UserCode = userCode,
                    UserName = fullName.Replace(" ", "").ToLower() + i,
                    FullName = fullName,
                    MobileNo = "09" + _random.Next(10000000, 99999999),
                    Email = fullName.Replace(" ", ".").ToLower() + i + "@example.com",
                    Nrc = $"{_random.Next(1, 15)}/ABC(N){_random.Next(100000, 999999)}",
                    Address = $"Street {_random.Next(1, 100)}",
                    StateCode = branch.StateCode,
                    TownshipCode = branch.TownshipCode,
                    BranchCode = branch.BranchCode,
                    CustomerId = "C" + _random.Next(1000000, 9999999)
                };
                users.Add(user);

                var account = new TblAccount
                {
                    CustomerName = user.FullName,
                    CustomerCode = user.CustomerId,
                    Balance = GenerateRandomAmount(100000, 1000000), // Initial balance
                    AccountNo = GenerateIBAN(),
                    BranchCode = branch.BranchCode
                };
                accounts.Add(account);

                if (i % 10 == 0)
                {
                    int p = 20 + (int)((float)i / accountCount * 30);
                    progress.Report((p, $"Generating user & account {i + 1}/{accountCount}..."));
                }
            }

            await _indexedDbService.AddObjectsAsync("Tbl_User", users);
            await _indexedDbService.AddObjectsAsync("Tbl_Account", accounts);
            progress.Report((50, "Users and Accounts saved."));

            // 6. Generate Transaction History
            List<TblTransactionHistory> transactions = new();
            progress.Report((55, "Generating transactions..."));

            for (int i = 0; i < accounts.Count; i++)
            {
                var account = accounts[i];
                int txCount = _random.Next(5, 11); // 5 to 10 transactions

                for (int j = 0; j < txCount; j++)
                {
                    var type = GenerateTransactionType();
                    var amount = GenerateRandomAmount(1000, 50000);
                    var txDate = GetRandomDate(fromDate, toDate);

                    var toAccount = accounts[_random.Next(accounts.Count)];
                    
                    var transaction = new TblTransactionHistory
                    {
                        FromAccountNo = account.AccountNo!,
                        ToAccountNo = (type == "Transfer" ? toAccount.AccountNo! : account.AccountNo!),
                        Amount = amount,
                        TransactionDate = txDate,
                        AdminUserCode = adminUsers[0].AdminUserCode,
                        TransactionType = type
                    };
                    transactions.Add(transaction);

                    // Update balances
                    if (type == "Deposit")
                    {
                        account.Balance += amount;
                    }
                    else if (type == "Withdraw")
                    {
                        account.Balance -= amount;
                    }
                    else if (type == "Transfer")
                    {
                        account.Balance -= amount;
                        toAccount.Balance += amount;
                    }
                }

                if (i % 50 == 0)
                {
                    int p = 55 + (int)((float)i / accounts.Count * 40);
                    progress.Report((p, $"Processing transactions for account {i + 1}/{accounts.Count}..."));
                }
            }

            // Sync updated account balances back to DB
            await _indexedDbService.ClearStoreAsync("Tbl_Account");
            await _indexedDbService.AddObjectsAsync("Tbl_Account", accounts);

            await _indexedDbService.AddObjectsAsync("Tbl_TransactionHistory", transactions);
            progress.Report((100, "Data generation complete. All tables initialized."));
        }
        catch (Exception ex)
        {
            progress.Report((0, $"Error: {ex.Message}"));
        }
    }

    private decimal GenerateRandomAmount(long min, long max)
    {
        // Generate a number in steps of 1000 to ensure suffix 000
        long range = (max - min) / 1000;
        long value = min / 1000 + (long)(_random.NextDouble() * range);
        return (decimal)(value * 1000);
    }

    private DateTime GetRandomDate(DateTime fromDate, DateTime toDate)
    {
        int range = (toDate - fromDate).Days;
        if (range <= 0) return fromDate;
        return fromDate.AddDays(_random.Next(range + 1)).AddHours(_random.Next(24)).AddMinutes(_random.Next(60));
    }

    private string GenerateTransactionType()
    {
        string[] types = { "Deposit", "Withdraw", "Transfer" };
        return types[_random.Next(types.Length)];
    }

    private string GenerateCustomerName()
    {
        string[] firstNames = { "John", "Alice", "Michael", "Emily", "David", "Sarah", "Liam", "Sophia", "James", "Emma" };
        string[] lastNames = { "Smith", "Johnson", "Williams", "Brown", "Jones", "Garcia", "Miller", "Davis", "Rodriguez", "Martinez" };
        return firstNames[_random.Next(firstNames.Length)] + " " + lastNames[_random.Next(lastNames.Length)];
    }

    private string GenerateIBAN()
    {
        StringBuilder ibanBuilder = new StringBuilder();
        ibanBuilder.Append(GetRandomLetters(2));
        ibanBuilder.Append(_random.Next(10));
        ibanBuilder.Append(_random.Next(10));
        for (int i = 0; i < 22; i++)
        {
            ibanBuilder.Append(_random.Next(10));
        }
        return ibanBuilder.ToString();
    }

    private string GetRandomLetters(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        return new string(Enumerable.Repeat(chars, length)
          .Select(s => s[_random.Next(s.Length)]).ToArray());
    }
}
