using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNet8.BankingManagementSystem.Models.State;

namespace DotNet8.BankingManagementSystem.App.Api
{
    public interface IStateApi
    {
        [Get("/api/state/{pageNo}/{pageSize}")]
        Task<StateListResponseModel> GetState(int pageNo, int pageSize);
    }
}
