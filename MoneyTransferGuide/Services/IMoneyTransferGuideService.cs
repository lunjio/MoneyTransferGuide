using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MoneyTransferGuide.Model;

namespace MoneyTransferGuide.Services
{
    public interface IMoneyTransferGuideService
    {
        Task<List<MoneyTransferInfoData>> GetMoneyTransferInfoAsync(string countryFrom, string countryTo, double summ, string currency);

        Task<List<MoneyTransferSystem>> GetMoneyTransferSystemsAsync();

        Task<MoneyTransferSystem> GetMoneyTransferSystemDetailAsync(int id);
    }
}
