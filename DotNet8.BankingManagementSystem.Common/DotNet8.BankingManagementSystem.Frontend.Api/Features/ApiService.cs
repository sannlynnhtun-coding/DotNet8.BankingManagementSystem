namespace DotNet8.BankingManagementSystem.Frontend.Api.Features
{
    public class ApiService
    {
        private readonly EnumApiType _enumApiType;
        private readonly IAccountApi _accountApi;
        private readonly AccountService _accountService;
        private readonly IStateApi _stateApi;
        private readonly StateService _stateService;
        private readonly ITownshipApi _townshipApi;
        private readonly TownshipService _townshipService;
        private readonly ITransactionApi _transactionApi;
        private readonly TransactionService _transactionService;
        private readonly IUserApi _userApi;
        private readonly UserService _userService;
        private readonly IAdminUser _adminUser;
        private readonly AdminUserService _adminUserService;

        public ApiService(Config config, AccountService accountService,
            IAccountApi accountApi, IStateApi stateApi,
            StateService stateService, ITownshipApi townshipApi,
            TownshipService townshipService, ITransactionApi transactionApi,
            TransactionService transactionService, IUserApi userApi,
            UserService userService, IAdminUser adminUser,
            AdminUserService adminUserService)
        {
            _accountService = accountService;
            _accountApi = accountApi;
            _enumApiType = config.EnumApiType;
            _stateApi = stateApi;
            _stateService = stateService;
            _townshipApi = townshipApi;
            _townshipService = townshipService;
            _transactionApi = transactionApi;
            _transactionService = transactionService;
            _userApi = userApi;
            _userService = userService;
            _adminUser = adminUser;
            _adminUserService = adminUserService;
        }

        #region Account

        public async Task<AccountListResponseModel> GetAccounts()
        {
            return _enumApiType == EnumApiType.LocalStorage
                ? await _accountService.GetAccounts()
                : await _accountApi.GetAccounts();
        }

        public async Task<AccountListResponseModel> GetAccountList(int pageNo, int pageSize)
        {
            return _enumApiType == EnumApiType.LocalStorage
                ? await _accountService.GetAccountList(pageNo, pageSize)
                : await _accountApi.GetAccountList(pageNo, pageSize);
        }

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

        #endregion

        #region State

        public async Task<StateListResponseModel> GetStates()
        {
            return _enumApiType == EnumApiType.LocalStorage
                ? await _stateApi.GetStates()
                : await _stateService.GetStates();
        }

        public async Task<StateListResponseModel> GetStates(int pageNo, int pageSize)
        {
            return _enumApiType == EnumApiType.LocalStorage
                ? await _stateApi.GetStateList(pageNo, pageSize)
                : await _stateService.GetStateList(pageNo, pageSize);
        }

        public async Task<StateResponseModel> GetStateByCode(string stateCode)
        {
            return _enumApiType == EnumApiType.LocalStorage
                ? await _stateApi.GetStateByCode(stateCode)
                : await _stateService.GetStateByCode(stateCode);
        }

        public async Task<StateResponseModel> CreateState(StateRequestModel requestModel)
        {
            return _enumApiType == EnumApiType.LocalStorage
                ? await _stateApi.CreateState(requestModel)
                : await _stateService.CreateState(requestModel);
        }

        public async Task<StateResponseModel> UpdateState(string stateCode, StateRequestModel requestModel)
        {
            return _enumApiType == EnumApiType.LocalStorage
                ? await _stateApi.UpdateState(stateCode, requestModel)
                : await _stateService.UpdateState(stateCode, requestModel);
        }

        public async Task<StateResponseModel> DeleteState(string stateCode)
        {
            return _enumApiType == EnumApiType.LocalStorage
                ? await _stateApi.DeleteState(stateCode)
                : await _stateService.DeleteState(stateCode);
        }

        #endregion

        #region TownShip

        public async Task<TownshipListResponceModel> GetTownShipList(int pageNo, int pageSize)
        {
            return _enumApiType == EnumApiType.LocalStorage
                ? await _townshipApi.GetTownShipList(pageNo, pageSize)
                : await _townshipService.GetTownShipList(pageNo, pageSize);
        }

        public async Task<TownshipResponseModel> GetTownShipByCode(string townshipCode)
        {
            return _enumApiType == EnumApiType.LocalStorage
                ? await _townshipApi.GetTownShipByCode(townshipCode)
                : await _townshipService.GetTownShipByCode(townshipCode);
        }

        public async Task<TownshipResponseModel> CreateTownship(TownshipRequestModel requestModel)
        {
            return _enumApiType == EnumApiType.LocalStorage
                ? await _townshipApi.CreateTownship(requestModel)
                : await _townshipService.CreateTownship(requestModel);
        }

        public async Task<TownshipResponseModel> UpdateTownship(string townshipCode,
            TownshipRequestModel requestModel)
        {
            return _enumApiType == EnumApiType.LocalStorage
                ? await _townshipApi.UpdateTownship(townshipCode, requestModel)
                : await _townshipService.UpdateTownship(townshipCode, requestModel);
        }

        public async Task<TownshipResponseModel> DeleteTownship(string townshipCode)
        {
            return _enumApiType == EnumApiType.LocalStorage
                ? await _townshipApi.DeleteTownship(townshipCode)
                : await _townshipService.DeleteTownship(townshipCode);
        }

        public async Task<TownshipListResponceModel> GetTownShipByStateCode(string stateCode)
        {
            return _enumApiType == EnumApiType.LocalStorage
                ? await _townshipApi.GetTownShipByStateCode(stateCode)
                : await _townshipService.GetTownShipByStateCode(stateCode);
        }

        #endregion

        #region Transaction History

        public async Task<TransactionHistoryListResponseModel> TransactionHistory(int pageNo, int pageSize)
        {
            return _enumApiType == EnumApiType.LocalStorage
                ? await _transactionApi.TransactionHistory(pageNo, pageSize)
                : await _transactionService.TransactionHistory(pageNo, pageSize);
        }

        public async Task<TransactionHistoryListResponseModel> TransactionHistoryWithDate(
            TransactionHistorySearchModel requestModel)
        {
            return _enumApiType == EnumApiType.LocalStorage
                ? await _transactionApi.TransactionHistoryWithDate(requestModel)
                : await _transactionService.TransactionHistoryWithDate(requestModel);
        }

        public async Task<AccountResponseModel> Deposit(TransactionRequestModel requestModel)
        {
            return _enumApiType == EnumApiType.LocalStorage
                ? await _transactionApi.Deposit(requestModel)
                : await _transactionService.Deposit(requestModel);
        }

        public async Task<AccountResponseModel> Withdraw(TransactionRequestModel requestModel)
        {
            return _enumApiType == EnumApiType.LocalStorage
                ? await _transactionApi.Withdraw(requestModel)
                : await _transactionService.Withdraw(requestModel);
        }

        public async Task<TransferResponseModel> Transfer(TransferModel requestModel)
        {
            return _enumApiType == EnumApiType.LocalStorage
                ? await _transactionApi.Transfer(requestModel)
                : await _transactionService.Transfer(requestModel);
        }

        public async Task<TransactionHistoryListResponseModel> TransactionHistoryWithDateRange
            (TransactionHistorySearchModel requestModel)
        {
            return _enumApiType == EnumApiType.LocalStorage
                ? await _transactionApi.TransactionHistoryWithDateRange(requestModel)
                : await _transactionService.TransactionHistoryWithDateRange(requestModel);
        }

        #endregion

        #region User

        public async Task<UserListResponseModel> GetUserList(int pageNo, int pageSize)
        {
            return _enumApiType == EnumApiType.LocalStorage
                ? await _userApi.GetUserList(pageNo, pageSize)
                : await _userService.GetUserList(pageNo, pageSize);
        }

        public async Task<UserResponseModel> GetUserByCode(string userCode)
        {
            return _enumApiType == EnumApiType.LocalStorage
                ? await _userApi.GetUserByCode(userCode)
                : await _userService.GetUserByCode(userCode);
        }

        public async Task<UserResponseModel> CreateUser(UserRequestModel requestModel)
        {
            return _enumApiType == EnumApiType.LocalStorage
                ? await _userApi.CreateUser(requestModel)
                : await _userService.CreateUser(requestModel);
        }

        public async Task<UserResponseModel> UpdateUser(UserRequestModel requestModel)
        {
            return _enumApiType == EnumApiType.LocalStorage
                ? await _userApi.UpdateUser(requestModel)
                : await _userService.UpdateUser(requestModel);
        }

        public async Task<UserResponseModel> DeleteUser(string userCode)
        {
            return _enumApiType == EnumApiType.LocalStorage
                ? await _userApi.DeleteUser(userCode)
                : await _userService.DeleteUser(userCode);
        }

        #endregion

        #region AdminUser

        public async Task<AdminUserListResponseModel> GetAdminUsers()
        {
            return _enumApiType == EnumApiType.LocalStorage
                ? await _adminUser.GetAdminUsers()
                : await _adminUserService.GetAdminUsers();
        }

        public async Task<AdminUserListResponseModel> GetAdminUserList(int pageNo, int pageSize)
        {
            return _enumApiType == EnumApiType.LocalStorage
                ? await _adminUser.GetAdminUserList(pageNo, pageSize)
                : await _adminUserService.GetAdminUserList(pageNo, pageSize);
        }

        public async Task<AdminUserResponseModel> GetAdminUserByCode(string adminUserCode)
        {
            return _enumApiType == EnumApiType.LocalStorage
                ? await _adminUser.GetAdminUserByCode(adminUserCode)
                : await _adminUserService.GetAdminUserByCode(adminUserCode);
        }

        public async Task<AdminUserResponseModel> CreateAdminUser(AdminUserRequestModel requestModel)
        {
            return _enumApiType == EnumApiType.LocalStorage
                ? await _adminUser.CreateAdminUser(requestModel)
                : await _adminUserService.CreateAdminUser(requestModel);
        }

        public async Task<AdminUserResponseModel> UpdateAdminUser(AdminUserRequestModel requestModel)
        {
            return _enumApiType == EnumApiType.LocalStorage
                ? await _adminUser.UpdateAdminUser(requestModel)
                : await _adminUserService.UpdateAdminUser(requestModel);
        }

        public async Task<AdminUserResponseModel> DeleteAdminUser(string adminUserCode)
        {
            return _enumApiType == EnumApiType.LocalStorage
                ? await _adminUser.DeleteAdminUser(adminUserCode)
                : await _adminUserService.DeleteAdminUser(adminUserCode);
        }

        #endregion
    }
}