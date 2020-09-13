using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyTransferGuide.Model
{
    public class MoneyTransferSystem
    {
        public int Id { get; set; }

        public string Logo { get; set; }
        
        public string SystemName { get; set; }

        public string Info { get; set; }

        public string Url { get; set; }

        public string Speed { get; set; }

        public string SendTypes { get; set; }

        public string RecieveTypes { get; set; }

        public string Contacts { get; set; }

        public string SystemInfoHtml { get; set; }
    }
}
