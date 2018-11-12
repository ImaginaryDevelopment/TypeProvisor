using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace CodeGeneration.UI
{
    static class Helpers
    {
        public static string Serialize(this object x, Formatting formatting = Formatting.Indented)
        => JsonConvert.SerializeObject(x, formatting);
        public static T Deserialize<T>(this string x) => JsonConvert.DeserializeObject<T>(x);

    }
}
