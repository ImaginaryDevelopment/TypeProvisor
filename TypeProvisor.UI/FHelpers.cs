using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.FSharp.Collections;
using Microsoft.FSharp.Core;

namespace TypeProvisor.UI
{
    public static class FHelpers
    {
        public static FSharpMap<TKey, TValue> ToMap<TKey, TValue>(this IEnumerable<Tuple<TKey, TValue>> items)
             => new FSharpMap<TKey, TValue>(items);
        public static bool IsValueString(this string x)
            => BReusable.StringPatterns.isValueString.Invoke(x);
        public static FSharpList<T> toList<T>(this IEnumerable<T> x) => SeqModule.ToList(x); //new FSharpList<T>()
        public static bool IsNonValueString(this string x) => string.IsNullOrWhiteSpace(x);
    }

}
