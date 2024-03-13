using DotNet8.BankingManagementSystem.Models.State;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet8.BankingManagementSystem.Models.AdminUser
{
    public class AdminUserListResponseModel
    {
        public MessageResponseModel Response { get; set; }

        public PageSettingModel pageSetting { get; set; }

        public List<AdminUserModel> Data { get; set; }
    }
}
