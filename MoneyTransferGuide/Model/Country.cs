using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyTransferGuide.Model
{
    public class Country
    {
        public string Name;

        public string Code;

        public string Image;

        public override string ToString()
        {
            return Name;
        }
    }
}
