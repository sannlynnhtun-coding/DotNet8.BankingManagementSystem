using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet8.BankingManagementSystem.Models.State
{
    public class StateResponseModel
    {
        public MessageResponseModel Response { get; set; }
        public StateModel Data { get; set; }
    }
}
