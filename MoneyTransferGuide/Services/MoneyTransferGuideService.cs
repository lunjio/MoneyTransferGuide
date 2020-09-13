using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MoneyTransferGuide.Model;

namespace MoneyTransferGuide.Services
{
    public class MoneyTransferGuideService : BaseHttpService, IMoneyTransferGuideService
    {
        public MoneyTransferGuideService()
        {

        }

        private const string ApiAdress = "https://perevody-deneg.ru/api";

        public async Task<List<MoneyTransferInfoData>> GetMoneyTransferInfoAsync(string countryFrom, string countryTo, double summ, string currency)
        {
            var serviceApiAdress = $"{ApiAdress}/?type=moneytransfer&country_from={countryFrom}&country_to={countryTo}&transfersum={summ}&currency={currency}";
            var answer = await SendRequestAsync<List<MoneyTransferInfoData>>(new Uri(serviceApiAdress));
            return answer.Result;
        }

        public async Task<List<MoneyTransferSystem>> GetMoneyTransferSystemsAsync()
        {

            var serviceApiAdress = $"{ApiAdress}/?type=systeminfo&id=all";
            var answer = await SendRequestAsync<List<MoneyTransferSystem>>(new Uri(serviceApiAdress));
            return answer.Result;
        }

        public async Task<MoneyTransferSystem> GetMoneyTransferSystemDetailAsync(int id)
        {
            var serviceApiAdress = $"{ApiAdress}/?type=systeminfo&id={id}";
            var answer = await SendRequestAsync<MoneyTransferSystem>(new Uri(serviceApiAdress));
            return answer.Result;
        }
    }
}
