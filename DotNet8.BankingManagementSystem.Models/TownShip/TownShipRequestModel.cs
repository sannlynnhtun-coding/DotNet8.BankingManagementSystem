using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet8.BankingManagementSystem.Models.Township
{
    public class TownshipRequestModel
    {
        public string TownshipCode { get; set; } = null!;
        public string TownshipName { get; set; } = null!;
        public string StateCode { get; set; } = null!;
    }
}
