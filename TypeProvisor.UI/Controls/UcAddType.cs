﻿using System;
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
            foreach (var item in this.Items)
            {
                var tn = CreateTreeNode(item);
                this.treeView1.Nodes.Add(tn);
            }
        }

        TreeNode CreateTreeNode(TypeMeta tm)
        {
            var children = tm.Properties.Select(p =>
                new TreeNode(p.Name, new[]
                {
                        new TreeNode("Type:" + p.BaseType.ToString()){Tag=p.BaseType},
                        new TreeNode("IsOptional:" + p.IsOptional.ToString()){Tag=p.IsOptional}
                }
            )
                { Tag = p }).ToArray();
            var tn = new TreeNode(tm.Name, children) { Tag = tm };
            return tn;
        }

        // assumes the replacement is because a property changed, not the parent TypeMeta
        void ReplaceTreeNode(TreeNode typeMetaNode, TypeMeta replacement, string prop)
        {
            if (!(typeMetaNode.Tag is TypeMeta)) return;
            var rNode = CreateTreeNode(replacement);
            // this isn't preserving type order
            var i = treeView1.Nodes.IndexOf(typeMetaNode);
            treeView1.Nodes[i] = rNode;
            rNode.Expand();
            treeView1.Nodes.Remove(typeMetaNode);
            if (prop.IsNonValueString())
                return;
            if (!(FindPropertyTypeNode(rNode, prop) is TreeNode propNode)) return;
            propNode.Expand();
            this.treeView1.SelectedNode = propNode;
        }

        TreeNode FindTypeMetaNode(TreeNode node)
        {
            if (node == null) return null;
            if (node.Tag is TypeMeta)
                return node;
            return FindTypeMetaNode(node.Parent);
        }

        TreeNode FindPropertyTypeNode(TreeNode node)
        {
            if (node == null) return null;
            if (node.Tag is Property)
                return node;
            return FindPropertyTypeNode(node.Parent);
        }

        TreeNode FindPropertyTypeNode(TreeNode node, string propertyName)
        {
            if (node == null) return null;
            if (node.Tag is Property p && p.Name == propertyName) return node;
            if (node.Tag is TypeMeta tm)
            {
                return node.Nodes.Cast<TreeNode>().FirstOrDefault(tn => ((tn.Tag as Property)?.Name == propertyName));
            }
            return null;
        }

        void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (FindPropertyTypeNode(this.treeView1.SelectedNode) is TreeNode typeNode && typeNode.Tag is Property p)
            {
                this.ucBaseType1.SelectedBaseType = p.BaseType;
            }
            else this.ucBaseType1.SelectedBaseType = null;
        }

        void ucBaseType1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (FindPropertyTypeNode(this.treeView1.SelectedNode) is TreeNode typeNode && typeNode.Tag is Property p
                && FindTypeMetaNode(typeNode) is TreeNode typeMetaNode && typeMetaNode.Tag is TypeMeta tm)
            {
                if (p.BaseType != this.ucBaseType1.SelectedBaseType)
                {
                    var props =
                        tm.Properties.Select(x => x.Name == p.Name
                        ? new Property(x.Name, this.ucBaseType1.SelectedBaseType, x.IsOptional, x.Comments, x.Cardinality)
                        : x).toList();
                    var rTm = new TypeMeta(tm.Name, props, tm.Comments);
                    ReplaceTreeNode(typeMetaNode, rTm, p.Name);
                }
            }
        }
    }
}
