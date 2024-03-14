// using DotNet8.BankingManagementSystem.Database.EfAppDbContextModels;
// using DotNet8.BankingManagementSystem.Mapper;
// using DotNet8.BankingManagementSystem.Models;
// using DotNet8.BankingManagementSystem.Models.TransactionHistory;
// using DotNet8.BankingManagementSystem.Models.Transfer;
// using Microsoft.EntityFrameworkCore;
//
// namespace DotNet8.BankingManagementSystem.BackendApi.Features.Transaction;
//
// public class TransactionHistoryService
// {
//     private readonly AppDbContext _dbContext;
//
//     public TransactionHistoryService(AppDbContext dbContext)
//     {
//         _dbContext = dbContext;
//     }
//
//     #region TransactionHistory
//
//     public async Task<TransactionHistoryListResponseModel> TransactionHistory(int pageNo, int pageSize)
//     {
//         TransactionHistoryListResponseModel model = new TransactionHistoryListResponseModel();
//         var query = _dbContext.TblTransactionHistories.AsNoTracking();
//         var result = await query.OrderByDescending(x => x.TransactionDate)
//             .Skip((pageNo - 1) * pageSize)
//             .Take(pageSize).ToListAsync();
//
//         var count = await query.CountAsync();
//         int pageCount = count / pageSize;
//         if (count % pageSize > 0) pageCount++;
//         var lst = result.Select(x => x.Change()).ToList();
//         model = new TransactionHistoryListResponseModel()
//         {
//             Data = lst,
//             PageSetting = new PageSettingModel(pageNo, pageSize, pageCount),
//             Response = new MessageResponseModel(true, "Success")
//         };
//         return model;
//     }
//
//     #endregion
//
//     #region generate 2years transaction history
//
//      public async Task<TransactionHistoryListResponseModel> ThreeYearsTransactionRecord()
//         {
//             int total = 0;
//             DateTime currentDate = DateTime.Now;
//             DateTime addYear;
//             List<int> years = new List<int>();
//             var userId = HttpContext.Session.GetString("LoginData");
//             var userData = await _context.UserData.FirstOrDefaultAsync(x => x.UserId == userId);
//             for (int i = 0; i < 3; i++)
//             {
//                 addYear = currentDate.AddYears(-1 * i);
//                 years.Add(addYear.Year);
//             }
//
//             Random random = new Random();
//             int totalTransactionAmount = 0;
//             bool isSuccess = true;
//             using (var transaction = await _context.Database.BeginTransactionAsync())
//             {
//                 try
//                 {
//                     for (int i = 0; i < 3; i++)
//                     {
//                         for (int j = 0; j < 365; j++)
//                         {
//                             total = i;
//                             decimal Amount = random.Next(100000, 1000000);
//                             decimal income_amount = random.Next(100000, 1000000);
//
//                             DateTime start_date = new DateTime(years[i], 1, 1);
//                             DateTime end_date = new DateTime(years[i], 12, 31);
//                             int range = (end_date - start_date).Days;
//                             int randomDays = random.Next(range);
//                             DateTime randomDate = start_date.AddDays(randomDays);
//                             string transferId = Ulid.NewUlid().ToString();
//                             int transactionAmount = random.Next(1, 101);
//                             totalTransactionAmount += transactionAmount;
//                             var transactionHistory = new TransactionHistoryModel()
//                             {
//                                 TransferId = transferId,
//                                 FromAccount = userData!.CardNumber.ToString()!,
//                                 ToAccount = GenerateRandom12DigitNumber().ToString(),
//                                 TransferDate = randomDate,
//                                 UserId = userId!,
//                                 TransitionAmount = transactionAmount,
//                             };
//                             await _context.TransactionHistory.AddAsync(transactionHistory);
//                             await _context.SaveChangesAsync();
//                         }
//                     }
//
//                     if (userData!.Balance < totalTransactionAmount)
//                     {
//                         await transaction.RollbackAsync();
//                         goto result;
//                     }
//
//                     userData.Balance -= totalTransactionAmount;
//                     _context.UserData.Update(userData);
//                     await _context.SaveChangesAsync();
//                     await transaction.CommitAsync();
//                 }
//                 catch (Exception ex)
//                 {
//                   
//                     await transaction.RollbackAsync();
//                     Console.WriteLine(ex.Message);
//                 }
//             }
//
//         result:
//         }
//
//
//     #endregion
// }