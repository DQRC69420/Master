using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IlterisDictionaryLibrary.Data
{
    [Flags]
    public enum TurkicLanguage : long
    {
        None = 0,
        Anatolian_Turkish = 1,
        Azerbaijani = 2,
        Balkan_Turkic = 4,
        Turkmen = 8,
        Gagauz = 16,
        Kyrgyz = 32,
        Kazakh = 64,
        Karakalpak = 128,
        Uzbek = 256,
        Bashkortic = 512,
        Tatar = 1024,
        Uyghur = 2048,
        Crimean_Tatar = 4096,
        Chuvash = 8192,
        Southern_Altai = 16384,
        Northern_Altai = 32768,
        Salar = 65536,
        Tuvan = 131072,
        Sakhan = 262144,
        Khakassian = 524288,
        Western_Yugur = 1048576,
        Tofa = 2097152,
        Old_Uyghur = 4194304,
        Oghuz = 8388608,
        Oghur = 16777216,
        Kipchak = 33554432,
        Siberian_Turkic = 67108864,
        Karluk = 134217728,
        Köktürk = 268435456,
        Karakhanid = 536870912,
        Chagatai = 1073741824,
        Proto_Turkic = 2147483648,
        Old_Bulgar = 4294967296,
    }
}
