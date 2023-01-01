using SuchByte.MacroDeck.GUI.CustomControls;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.ComponentModel;
using System.Text;
using SuchByte.MacroDeck.Plugins;
using System.Drawing;
using System.Reflection;
using SuchByte.MacroDeck.ActionButton;
using SuchByte.MacroDeck.GUI;
using System.Xml.Linq;
using System.Runtime.InteropServices;
using SuchByte.MacroDeck.Logging;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Newtonsoft.Json.Linq;

namespace Sephless.WinampControl
{

    partial class WinampConfigControl
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
        /// 


        private void InitializeComponent()
        {
            this.WinampChoice_ComboBox = new SuchByte.MacroDeck.GUI.CustomControls.RoundedComboBox();
            this.SuspendLayout();
            // 
            // WinampChoice_ComboBox
            // 
            this.WinampChoice_ComboBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.WinampChoice_ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.WinampChoice_ComboBox.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.WinampChoice_ComboBox.Icon = null;
            this.WinampChoice_ComboBox.Location = new System.Drawing.Point(18, 22);
            this.WinampChoice_ComboBox.Name = "WinampChoice_ComboBox";
            this.WinampChoice_ComboBox.Padding = new System.Windows.Forms.Padding(8, 2, 8, 2);
            this.WinampChoice_ComboBox.SelectedIndex = -1;
            this.WinampChoice_ComboBox.SelectedItem = null;
            this.WinampChoice_ComboBox.Size = new System.Drawing.Size(561, 31);
            this.WinampChoice_ComboBox.TabIndex = 0;
            // 
            // WinampConfigControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.WinampChoice_ComboBox);
            this.Name = "WinampConfigControl";
            this.Size = new System.Drawing.Size(600, 299);
            this.ResumeLayout(false);

        }

        #endregion

        public void populateComboBox()
        {
            foreach (Winamp_Command wc in WinampAPI.Winamp_Command_Array)
            {
                this.WinampChoice_ComboBox.Items.Add(wc.text_prompt);
            }

            int index = 0;
            try
            {
                string strConfig = _macroDeckAction.Configuration.ToString();
                index = WinampAPI.getIndexFromString(strConfig);
            }
            catch { }
            this.WinampChoice_ComboBox.Text = WinampAPI.Winamp_Command_Array[index].text_prompt;
        }

        private SuchByte.MacroDeck.GUI.CustomControls.RoundedComboBox WinampChoice_ComboBox;
    }


}
