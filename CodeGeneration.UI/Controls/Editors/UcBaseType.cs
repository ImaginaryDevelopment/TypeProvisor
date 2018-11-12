using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CodeGeneration.UI.Controls
{
    [Designer(typeof(UcBaseTypeDesigner))]
    public partial class UcBaseType : UserControl
    {
        public Control DesignHandle => this.label1;
        public UcBaseType()
        {
            InitializeComponent();
        }
    }
}
