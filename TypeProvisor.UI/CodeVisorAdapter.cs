using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TypeProvisor.UI
{
    public enum GenerationLanguages
    {
        CSharp,
        FSharp,
        // not implemented yet:
        // TypeScript,
        // SQL,
    }

    public enum GenerationType
    {
        FSharpRecord,
        CSharpClass,
        CSharpFile,
    }

    public static class CodeVisorAdapter
    {
        public static string Generate(IEnumerable<TypeMeta> items, bool writable, string targetNamespace, GenerationType gt)
        {
            if (items == null || items.Count() < 1)
            {
                MessageBox.Show("No items imported");
                return null;
            }
            switch (gt)
            {
                case GenerationType.FSharpRecord:
                    return MapItems(items, x => FSharp.generateRecord(writable,x));
                case GenerationType.CSharpClass:
                    return MapItems(items, x => CSharp.generateClass(writable, x));
                case GenerationType.CSharpFile:
                    return MapItems(items, x => CSharp.generateClassFile(targetNamespace, writable, x));
                default:
                    MessageBox.Show("Not implemented");
                    return null;
            }
        }

        static string MapItems(IEnumerable<TypeMeta> items, Func<TypeMeta, IEnumerable<Tuple<int, string>>> f)
        {
            var mapped = items
                .SelectMany(f)
                .Select(x => IndentationImpl.toString("    ", x.Item1, x.Item2))
                .Aggregate((s1, s2) => s1 + Environment.NewLine + s2);
            return mapped;
        }

    }
}
