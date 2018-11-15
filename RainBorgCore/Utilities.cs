using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace RainBorg
{
    public partial class RainBorg
    {
        internal static decimal GetBalance()
        {
            try
            {
                using (WebClient client = new WebClient())
                {
                    string dl = client.DownloadString(balanceUrl);
                    JObject j = JObject.Parse(dl);
                    return (decimal)j["balance"];
                }
            }
            catch
            {
                return -1;
            }
        }

        public static decimal Floor(decimal Input)
        {
            var r = Convert.ToDecimal(Math.Pow(10, decimalPlaces));
            return Math.Floor(Input * r) / r;
        }

        public static string Format(decimal Input)
        {
            Input = Floor(Input);
            string f = "{0:#,##0.#############}";
            Console.WriteLine(string.Format(f, Input));
            return string.Format(f, Input);
        }
        public static string Format(double Input)
        {
            decimal I = Floor((decimal)Input);
            string f = "{0:#,##0.#############}";
            Console.WriteLine(string.Format(f, I));
            return string.Format(f, I);
        }
    }
}
