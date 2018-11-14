using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TypeProvisor.UI.Controls
{
    [Designer(typeof(UcBaseTypeDesigner))]
    public partial class UcComboBox : UserControl
    {
        public Control DesignHandle => this.label1;
        public UcComboBox()
        {
            InitializeComponent();
        }

        public object SelectedItem
        {
            get { return this.comboBox1.SelectedItem; }
            set { this.comboBox1.SelectedItem = value; }
        }

        public string LabelText
        {
            get { return this.label1.Text; }
            set { this.label1.Text = value; }
        }

        public System.Collections.IEnumerable Items { set { this.comboBox1.Items.Clear(); foreach (var x in value) this.comboBox1.Items.Add(x); } }
        public event EventHandler SelectedIndexChanged
        {
            add
            {
                this.comboBox1.SelectedIndexChanged += value;
            }
            remove
            {
                this.comboBox1.SelectedIndexChanged -= value;
            }
        }
        public event EventHandler SelectedValueChanged
        {
            add
            {
                this.comboBox1.SelectedValueChanged += value;
            }
            remove
            {
                this.comboBox1.SelectedValueChanged -= value;
            }
        }
    }
}
