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

namespace TypeProvisor.UI.Controls
{
    // give the nodes a context menu https://stackoverflow.com/questions/14208944/c-sharp-right-click-on-treeview-nodes
    public partial class UcAddType : UserControl
    {
        IReadOnlyList<TypeMeta> items;
        public IReadOnlyList<TypeMeta> Items { get { return items; } set { items = value; RefreshTypes(); } }
        public UcAddType()
        {
            InitializeComponent();
            RefreshTypes();
        }

        public void RefreshTypes()
        {
            treeView1.Nodes.Clear();
            if (items == null) return;
            foreach(var item in this.Items)
            {
                var children = item.Properties.Select(p =>
                    new TreeNode(p.Name, new[]
                    {
                        new TreeNode("Type:" + p.BaseType.ToString()){Tag=p.BaseType},
                        new TreeNode("IsOptional:" + p.IsOptional.ToString()){Tag=p.IsOptional}
                    }
                )).ToArray();
                var tn = new TreeNode(item.Name, children) { Tag = item };
                this.treeView1.Nodes.Add(tn);
            }

        }
    }
}
