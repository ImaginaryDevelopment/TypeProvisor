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

namespace TypeProvisor.UI
{

    // make the output scroll wheel font size adjustable: https://stackoverflow.com/questions/4429901/how-to-capture-mouse-wheel-on-panel
    public partial class UcGen : UserControl
    {
        IReadOnlyList<TypeMeta> items;
        public bool UseOptionTypes { get; set; }
        public bool Writable { get; set; }
        public string TargetNamespace { get; set; }

        public UcGen()
        {
            InitializeComponent();
            Enum.GetNames(typeof(GenerationType))
                .Select(v => Enum.Parse(typeof(GenerationType), v))
                .ToList()
                .ForEach(x => cbType.Items.Add(x));
        }

        public IReadOnlyList<TypeMeta> Items
        {
            get
            {
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
            if (cbType.SelectedItem is GenerationType gt)
            {
                // if null comes back, don't set the text
                if (CodeVisorAdapter.Generate(GetItems(), this.Writable, this.TargetNamespace, gt) is string result)
                    this.tbOutput.Text = result;
            }
        }
    }
}
