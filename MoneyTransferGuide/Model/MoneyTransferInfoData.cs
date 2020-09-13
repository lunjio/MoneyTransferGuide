using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace MoneyTransferGuide.Model
{
    public class MoneyTransferInfoData
    {
        public string System { get; set; }

        public string Comission { get; set; }
        
        public string Logo { get; set; }

        [JsonProperty("country_to_addresses_link")]
        public string CountryToAddressesLink { get; set; }

        [JsonProperty("country_from_addresses_link")]
        public string CountryFromAddressesLink { get; set; }

    }
}
