using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TypeProvisor.UI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            // TabIndexChanged 
            this.tabControl1.SelectedIndexChanged += (_, __) =>
             {
                 if (!this.tabControl1.SelectedTab.Controls.Contains(this.ucGen1))
                     return;
                 this.ucGen1.TargetNamespace = this.ucSettings1.TargetNamespace;
                 this.ucGen1.UseOptionTypes = this.ucSettings1.UseOptionTypes;
             };
            this.ucJsonImport1.ImportedTypes += (_, e) =>
            {
                var items = e.ToArray();
                this.ucAddType1.Items = items;
                this.ucGen1.Items = items;
            };
        }
    }
}
