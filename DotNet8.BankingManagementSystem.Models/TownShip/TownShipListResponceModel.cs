using DotNet8.BankingManagementSystem.Models.State;
using DotNet8.BankingManagementSystem.Models.Township;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet8.BankingManagementSystem.Models.TownShip
{
    public class TownshipListResponceModel
    {
        public MessageResponseModel Response { get; set; }
        public PageSettingModel PageSetting { get; set; }
        public List<TownshipModel> Data { get; set; }
    }
}
