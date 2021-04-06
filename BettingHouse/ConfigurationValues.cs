using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingHouse
{
    public static class ConfigurationValues
    {
        public const string DataBaseName = "Betting";
        public const string MemoryKey = "xxx.xxx.xxx";
        public const int ExpirationHorus = 12;

        public static string Concat(string id) => string.Concat(MemoryKey, id);
    }
}
