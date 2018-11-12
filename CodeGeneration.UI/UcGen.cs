using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using TypeProvisor;

namespace CodeGeneration.UI
{
    public enum GenerationTypes
    {
        FSharpRecord,
        CSharpClass
    }

    public partial class UcGen : UserControl
    {
        IReadOnlyList<TypeMeta> items;
        public IReadOnlyList<TypeMeta> Items
        {
            get {
                return items;
            }
            set
            {
                items = value;
                if (items == null) return;
                cbName.Items.Clear();
                foreach (var x in items)
                {
                    cbName.Items.Add(x.Name);
                }
            }
        }
        public UcGen()
        {
            InitializeComponent();
            Enum.GetNames(typeof(GenerationTypes))
                .Select(v => Enum.Parse(typeof(GenerationTypes), v))
                .ToList()
                .ForEach(x => cbType.Items.Add(x));
        }
        IEnumerable<TypeMeta> GetItems()
        {
            return this.Items
                .Where(x => string.IsNullOrWhiteSpace(cbName?.Text) || x.Name == cbName?.Text);
        }

        void btnGenerate_Click(object sender, EventArgs e)
        {
            if (this.Items == null || this.Items.Count < 1)
            {
                MessageBox.Show("No items imported");
                return;
            }
            if (cbType.SelectedIndex < 0 || cbType.Text.IsValueString() == false)
            {
                MessageBox.Show("Select the type of generation you wish to use");
                return;
            }
            switch (cbType.SelectedItem as GenerationTypes?)
            {
                case GenerationTypes.FSharpRecord:
                    this.tbOutput.Text = GenerateRecords(cbName.Text);
                    break;
                case GenerationTypes.CSharpClass:
                    this.tbOutput.Text = GenerateCClass(cbName.Text);
                    break;

                default:
                    MessageBox.Show("Not implemented");
                    break;

            }
        }
        string GenerateItems(Func<TypeMeta,IEnumerable<Tuple<int,string>>> f)
        {
            var mapped = GetItems()
                .SelectMany(f)
                .Select(x => IndentationImpl.toString("    ", x.Item1, x.Item2))
                .Aggregate((s1, s2) => s1 + Environment.NewLine + s2);
            return mapped;
        }

        string GenerateRecords(string name = null)
        {
            return GenerateItems(x => FSharp.generateRecord(true,x));
        }
        string GenerateCClass(string name = null)
        {
            return GenerateItems(x => CSharp.generateClass(true, x));
        }
    }
}
