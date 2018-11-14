using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TypeProvisor.UI.Controls.Editors
{
    public partial class UcBaseType : UserControl
    {
        public BaseType SelectedBaseType
        {
            get
            {
                if (this.ucComboBox1.SelectedItem is BaseType bt)
                    return bt;
                return this.ucComboBox1.SelectedItem as BaseType;
            }
            set { this.ucComboBox1.SelectedItem = value; }
        }


        public UcBaseType()
        {
            InitializeComponent();
            this.ucComboBox1.Items = BaseTypeModule.getDefaults();
        }
        public event EventHandler SelectedIndexChanged
        {
            add
            {
                this.ucComboBox1.SelectedIndexChanged += value;
            }
            remove
            {
                this.ucComboBox1.SelectedIndexChanged -= value;
            }
        }
        public event EventHandler SelectedValueChanged
        {
            add
            {
                this.ucComboBox1.SelectedValueChanged += value;
            }
            remove
            {
                this.ucComboBox1.SelectedValueChanged -= value;
            }
        }
    }
}
