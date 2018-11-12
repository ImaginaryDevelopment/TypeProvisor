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
    public partial class UcGen : UserControl
    {
        IReadOnlyList<TypeMeta> items;
        public IReadOnlyList<TypeMeta> Items
        {
            get { return items; }
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
            cbType.Items.Add("FSharp Record");
        }

        void btnGenerate_Click(object sender, EventArgs e)
        {
            if (this.Items == null || this.Items.Count < 1)
            {
                MessageBox.Show("No items imported");
                return;
            }
            if (cbName.SelectedIndex < 0 || cbName.Text.IsValueString() == false)
            {
                MessageBox.Show("Select the name of the item you wish to import");
                return;
            }
            if (cbType.SelectedIndex < 0 || cbType.Text.IsValueString() == false)
            {
                MessageBox.Show("Select the type of generation you wish to use");
                return;
            }
            var records = this.Items
                .SelectMany(x => FSharp.generateRecord(true, x))
                .Select(x => IndentationImpl.toString("    ",x.Item1, x.Item2))
                .Aggregate((s1,s2) => s1 + Environment.NewLine + s2);
            tbOutput.Text = records;

        }
    }
}
