﻿namespace DotNet8.BankingManagementSystem.Models.Account;

public class AccountModel
{
    public int AccountId { get; set; }

    public string? AccountNo { get; set; }

    public string CustomerCode { get; set; } = null!;

    public decimal Balance { get; set; }
}