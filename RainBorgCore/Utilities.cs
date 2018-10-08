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
    }
}
