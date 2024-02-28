using DotNet8.BankingManagementSystem.Models.State;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet8.BankingManagementSystem.Models.TownShip
{
    public class TownShipResponseModel
    {
        public MessageResponseModel Response { get; set; }
        public TownShipModel Data { get; set; }
    }
}
