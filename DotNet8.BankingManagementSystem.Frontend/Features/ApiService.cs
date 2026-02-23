namespace DotNet8.BankingManagementSystem.Frontend.Features;

public class ApiService
{
    private readonly AccountService _accountService;
    private readonly StateService _stateService;
    private readonly TownshipService _townshipService;
    private readonly TransactionService _transactionService;
    private readonly UserService _userService;
    private readonly AdminUserService _adminUserService;
    private readonly BankService _bankService;
    private readonly BranchService _branchService;

    public ApiService(AccountService accountService,
        StateService stateService,
        TownshipService townshipService,
        TransactionService transactionService,
        UserService userService,
        AdminUserService adminUserService,
        BankService bankService,
        BranchService branchService)
    {
        _accountService = accountService;
        _stateService = stateService;
        _townshipService = townshipService;
        _transactionService = transactionService;
        _userService = userService;
        _adminUserService = adminUserService;
        _bankService = bankService;
        _branchService = branchService;
    }

    #region Account
    public async Task<AccountListResponseModel> GetAccounts() => await _accountService.GetAccounts();
    public async Task<AccountListResponseModel> GetAccountList(int pageNo = 1, int pageSize = 10, string? bankCode = null, string? branchCode = null) 
        => await _accountService.GetAccountList(pageNo, pageSize, bankCode, branchCode);
    public async Task<AccountResponseModel> GetAccount(string accountNo) => await _accountService.GetAccount(accountNo);
    public async Task<AccountResponseModel> CreateAccount(AccountRequestModel requestModel) => await _accountService.CreateAccount(requestModel);
    public async Task<AccountResponseModel> UpdateAccount(string accountNo, AccountRequestModel requestModel) => await _accountService.UpdateAccount(accountNo, requestModel);
    public async Task<AccountResponseModel> DeleteAccount(string accountNo) => await _accountService.DeleteAccount(accountNo);
    #endregion

    #region State
    public async Task<StateListResponseModel> GetStates() => await _stateService.GetStates();
    public async Task<StateListResponseModel> GetStates(int pageNo, int pageSize) => await _stateService.GetStateList(pageNo, pageSize);
    public async Task<StateResponseModel> GetStateByCode(string stateCode) => await _stateService.GetStateByCode(stateCode);
    public async Task<StateResponseModel> CreateState(StateRequestModel requestModel) => await _stateService.CreateState(requestModel);
    public async Task CreateStates(List<StateRequestModel> lst) => await _stateService.CreateStates(lst);
    public async Task<StateResponseModel> UpdateState(string stateCode, StateRequestModel requestModel) => await _stateService.UpdateState(stateCode, requestModel);
    public async Task<StateResponseModel> DeleteState(string stateCode) => await _stateService.DeleteState(stateCode);
    #endregion

    #region TownShip
    public async Task<TownshipListResponceModel> GetTownShipList(int pageNo, int pageSize) => await _townshipService.GetTownShipList(pageNo, pageSize);
    public async Task<TownshipResponseModel> GetTownShipByCode(string townshipCode) => await _townshipService.GetTownShipByCode(townshipCode);
    public async Task<TownshipResponseModel> CreateTownship(TownshipRequestModel requestModel) => await _townshipService.CreateTownship(requestModel);
    public async Task CreateTownships(List<TownshipRequestModel> requestModel) => await _townshipService.CreateTownships(requestModel);
    public async Task<TownshipResponseModel> UpdateTownship(string townshipCode, TownshipRequestModel requestModel) => await _townshipService.UpdateTownship(townshipCode, requestModel);
    public async Task<TownshipResponseModel> DeleteTownship(string townshipCode) => await _townshipService.DeleteTownship(townshipCode);
    public async Task<TownshipListResponceModel> GetTownShipByStateCode(string stateCode) => await _townshipService.GetTownShipByStateCode(stateCode);
    #endregion

    #region Transaction History
    public async Task<TransactionHistoryListResponseModel> TransactionHistory(int pageNo, int pageSize) => await _transactionService.TransactionHistory(pageNo, pageSize);
    public async Task<AccountResponseModel> Deposit(AccountRequestModel requestModel) => await _transactionService.Deposit(requestModel);
    public async Task<AccountResponseModel> Withdraw(AccountRequestModel requestModel) => await _transactionService.Withdraw(requestModel);
    public async Task<TransferResponseModel> Transfer(TransferModel requestModel) => await _transactionService.Transfer(requestModel);
    public async Task<TransactionHistoryListResponseModel> TransactionHistoryWithDateRange(TransactionHistorySearchModel requestModel) => await _transactionService.TransactionHistoryWithDateRange(requestModel);
    #endregion

    #region User
    public async Task<UserListResponseModel> GetUserList(int pageNo = 1, int pageSize = 10) => await _userService.GetUserList(pageNo, pageSize);
    public async Task<UserResponseModel> GetUserByCode(string userCode) => await _userService.GetUserByCode(userCode);
    public async Task<UserResponseModel> CreateUser(UserRequestModel requestModel) => await _userService.CreateUser(requestModel);
    public async Task CreateUsers(List<UserRequestModel> lst) => await _userService.CreateUsers(lst);
    public async Task<UserResponseModel> UpdateUser(UserRequestModel requestModel) => await _userService.UpdateUser(requestModel);
    public async Task<UserResponseModel> DeleteUser(string userCode) => await _userService.DeleteUser(userCode);
    #endregion

    #region AdminUser
    public async Task<AdminUserListResponseModel> GetAdminUsers() => await _adminUserService.GetAdminUsers();
    public async Task<AdminUserListResponseModel> GetAdminUserList(int pageNo, int pageSize) => await _adminUserService.GetAdminUserList(pageNo, pageSize);
    public async Task<AdminUserResponseModel> GetAdminUserByCode(string adminUserCode) => await _adminUserService.GetAdminUserByCode(adminUserCode);
    public async Task<AdminUserResponseModel> CreateAdminUser(AdminUserRequestModel requestModel) => await _adminUserService.CreateAdminUser(requestModel);
    public async Task<AdminUserResponseModel> UpdateAdminUser(AdminUserRequestModel requestModel) => await _adminUserService.UpdateAdminUser(requestModel);
    public async Task<AdminUserResponseModel> DeleteAdminUser(string adminUserCode) => await _adminUserService.DeleteAdminUser(adminUserCode);
    #endregion

    #region Bank
    public async Task<BankListResponseModel> GetBanks() => await _bankService.GetBanks();
    public async Task<BankResponseModel> GetBankByCode(string bankCode) => await _bankService.GetBankByCode(bankCode);
    #endregion

    #region Branch
    public async Task<BranchListResponseModel> GetBranches() => await _branchService.GetBranches();
    public async Task<BranchListResponseModel> GetBranchesByBankCode(string bankCode) => await _branchService.GetBranchesByBankCode(bankCode);
    #endregion
}
