using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using MoneyTransferGuide.Model;
using Xamarin.Forms;
using XF.Material.Forms.UI;

namespace MoneyTransferGuide.Helpers
{
    public class InfoTypeDataTemplateSelector : DataTemplateSelector
    {

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            DataTemplate result;
            var moneyTransferSystemInformation =  (MoneyTransferSystemInformation)item;
            if (moneyTransferSystemInformation.Title == "О системе")
            {
                result = new DataTemplate(() =>
                {
                    var label = new MaterialLabel() { Text = moneyTransferSystemInformation.Content, TextType = TextType.Html, FontSize = new OnIdiom<double> {
                    Tablet = 20, Phone = 18}};
                    return new ScrollView() {Content = label};
                });
            }
            else
            {
                result = new DataTemplate(() =>
                {
                    var label = new MaterialLabel()
                    {
                        Text = HtmlToPlainText(moneyTransferSystemInformation.Content),
                        TextType = TextType.Html,
                        FontSize = new OnIdiom<double>
                        {
                            Tablet = 20,
                            Phone = 18
                        }
                    };
                    return new ScrollView() { Content = label };
                });
            }

            return result;
        }

        private static string HtmlToPlainText(string html)
        {
            const string tagWhiteSpace = @"(>|$)(\W|\n|\r)+<";//matches one or more (white space or line breaks) between '>' and '<'
            const string stripFormatting = @"<[^>]*(>|$)";//match any character between '<' and '>', even when end tag is missing
            const string lineBreak = @"<(br|BR)\s{0,1}\/{0,1}>";//matches: <br>,<br/>,<br />,<BR>,<BR/>,<BR />
            var lineBreakRegex = new Regex(lineBreak, RegexOptions.Multiline);
            var stripFormattingRegex = new Regex(stripFormatting, RegexOptions.Multiline);
            var tagWhiteSpaceRegex = new Regex(tagWhiteSpace, RegexOptions.Multiline);

            var text = html;
            //Decode html specific characters
            text = System.Net.WebUtility.HtmlDecode(text);
            //Remove tag whitespace/line breaks
            text = tagWhiteSpaceRegex.Replace(text, "><");
            //Replace <br /> with line breaks
            text = lineBreakRegex.Replace(text, Environment.NewLine);
            //Strip formatting
            text = stripFormattingRegex.Replace(text, string.Empty);

            return text;
        }
    }
}
