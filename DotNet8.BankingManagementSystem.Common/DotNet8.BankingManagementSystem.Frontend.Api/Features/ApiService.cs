using DotNet8.BankingManagementSystem.Frontend.Api.Features.Account;
using DotNet8.BankingManagementSystem.Frontend.Api.Services;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet8.BankingManagementSystem.Frontend.Api.Features
{
    public class ApiService
    {
        private readonly EnumApiType _enumApiType;
        private readonly IAccountApi _accountApi;
        private readonly AccountService _accountService;

        public ApiService(Config config, AccountService accountService, IAccountApi accountApi)
        {
            _accountService = accountService;
            _accountApi = accountApi;
            _enumApiType = config.EnumApiType;
        }

        public async Task<AccountListResponseModel> GetAccounts()
        {
            return _enumApiType == EnumApiType.LocalStorage
                ? await _accountService.GetAccounts()
                : await _accountApi.GetAccounts();
        }

        //Task<AccountListResponseModel> GetAccountList(int pageNo, int pageSize);

        public async Task<AccountResponseModel> GetAccount(string accountNo)
        {
            return _enumApiType == EnumApiType.LocalStorage
                ? await _accountService.GetAccount(accountNo)
                : await _accountApi.GetAccount(accountNo);
        }

        public async Task<AccountResponseModel> CreateAccount(AccountRequestModel requestModel)
        {
            return _enumApiType == EnumApiType.LocalStorage
                ? await _accountService.CreateAccount(requestModel)
                : await _accountApi.CreateAccount(requestModel);
        }

        public async Task<AccountResponseModel> UpdateAccount(string accountNo, AccountRequestModel requestModel)
        {
            return _enumApiType == EnumApiType.LocalStorage
                ? await _accountService.UpdateAccount(accountNo, requestModel)
                : await _accountApi.UpdateAccount(accountNo, requestModel);
        }

        public async Task<AccountResponseModel> DeleteAccount(string accountNo)
        {
            return _enumApiType == EnumApiType.LocalStorage
                ? await _accountService.DeleteAccount(accountNo)
                : await _accountApi.DeleteAccount(accountNo);
        }
    }
}