using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CodeGeneration.UI
{
    public partial class UcSettings : UserControl
    {
        // consider making these private once you have the generate settings method
        public string TargetProjectName => txtTargetProjectName.Text;
        public string TargetNamespace => txtTargetNamespace.Text;
        public bool UseOptionTypes => cbUseOptionTypes.Checked;
        public bool IncludeNonDboSchemaInNamespace => cbIncludeNonDboSchemaInNamespace.Checked;

        public UcSettings()
        {
            InitializeComponent();
        }


    }
}
