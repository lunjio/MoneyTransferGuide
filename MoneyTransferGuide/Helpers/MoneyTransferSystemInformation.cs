using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyTransferGuide.Helpers
{
    public class MoneyTransferSystemInformation
    {
        public string Title { get; }

        public string Content { get; }

        public MoneyTransferSystemInformation(string title, string content)
        {
            Title = title;
            Content = content;
        }
    }
}
