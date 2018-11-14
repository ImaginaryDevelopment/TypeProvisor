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
        public UcBaseType()
        {
            InitializeComponent();
            this.ucComboBox1.Items = BaseTypeModule.getNames();
        }
    }
}
