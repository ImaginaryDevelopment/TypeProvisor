﻿namespace TypeProvisor.UI.Controls.Editors
{
    partial class UcBaseType
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ucComboBox1 = new TypeProvisor.UI.Controls.UcComboBox();
            this.SuspendLayout();
            // 
            // ucComboBox1
            // 
            this.ucComboBox1.LabelText = "PropertyType";
            this.ucComboBox1.Location = new System.Drawing.Point(0, 0);
            this.ucComboBox1.Name = "ucComboBox1";
            this.ucComboBox1.Size = new System.Drawing.Size(166, 28);
            this.ucComboBox1.TabIndex = 0;
            // 
            // UcBaseType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ucComboBox1);
            this.Name = "UcBaseType";
            this.Size = new System.Drawing.Size(166, 28);
            this.ResumeLayout(false);

        }

        #endregion

        private UcComboBox ucComboBox1;
    }
}
