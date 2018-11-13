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
    // perhaps one day, make it able to import other types besides just dataModels
    public partial class UcJsonImport : UserControl
    {
        public event EventHandler<IReadOnlyList<TypeMeta>> ImportedTypes;

        TypeMeta[] _types;
        public IEnumerable<TypeMeta> Types => _types;

        public UcJsonImport()
        {
            InitializeComponent();
        }

        void btnImport_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.textBox1.Text))
                return;
            try
            {
                _types = this.textBox1.Text.Deserialize<TypeMeta[]>();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            this.ImportedTypes?.Invoke(this, _types);

            this.textBox1.Text = _types.Serialize();
            this.btnImport.Enabled = false;
        }

        void textBox1_TextChanged(object sender, EventArgs e)
        {
            btnImport.Enabled = true;
        }
    }
}
