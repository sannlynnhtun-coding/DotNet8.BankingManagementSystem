using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet8.BankingManagementSystem.Models.AdminUser
{
    public class AdminUserResponseModel
    {
        public MessageResponseModel Response { get; set; } = null!;

        public AdminUserModel Data { get; set; } = null!;
    }
}