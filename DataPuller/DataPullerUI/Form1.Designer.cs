using System.Collections.Generic;

namespace DataPullerUI
{
    partial class Form1
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.fieldSelection = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnCreate = new System.Windows.Forms.Button();
            this.import = new System.Windows.Forms.TabPage();
            this.pageLoad = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.siteLoad = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.incTb = new System.Windows.Forms.TextBox();
            this.inc = new System.Windows.Forms.Label();
            this.urlSplitterTb = new System.Windows.Forms.TextBox();
            this.urlsplitter = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.dynamicRb = new System.Windows.Forms.RadioButton();
            this.staticRb = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tblName = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dataSets = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnScrapp = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnUpload = new System.Windows.Forms.Button();
            this.cmbSheets = new System.Windows.Forms.ComboBox();
            this.updCheck = new System.Windows.Forms.CheckBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.fileName = new System.Windows.Forms.TextBox();
            this.tabControl1.SuspendLayout();
            this.fieldSelection.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.import.SuspendLayout();
            this.panel4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.fieldSelection);
            this.tabControl1.Controls.Add(this.import);
            this.tabControl1.Location = new System.Drawing.Point(-7, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(649, 793);
            this.tabControl1.TabIndex = 0;
            // 
            // fieldSelection
            // 
            this.fieldSelection.BackColor = System.Drawing.Color.DarkGray;
            this.fieldSelection.Controls.Add(this.groupBox3);
            this.fieldSelection.Location = new System.Drawing.Point(4, 22);
            this.fieldSelection.Name = "fieldSelection";
            this.fieldSelection.Size = new System.Drawing.Size(641, 767);
            this.fieldSelection.TabIndex = 0;
            this.fieldSelection.Text = "Template Customization";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnEdit);
            this.groupBox3.Controls.Add(this.btnCreate);
            this.groupBox3.Font = new System.Drawing.Font("Rockwell", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.ForeColor = System.Drawing.Color.White;
            this.groupBox3.Location = new System.Drawing.Point(135, 23);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(365, 108);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Selection Options";
            // 
            // btnEdit
            // 
            this.btnEdit.Font = new System.Drawing.Font("Comic Sans MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEdit.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnEdit.Location = new System.Drawing.Point(203, 38);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(126, 39);
            this.btnEdit.TabIndex = 1;
            this.btnEdit.Text = "Edit";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnCreate
            // 
            this.btnCreate.Font = new System.Drawing.Font("Comic Sans MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCreate.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnCreate.Location = new System.Drawing.Point(33, 38);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(126, 39);
            this.btnCreate.TabIndex = 0;
            this.btnCreate.Text = "Create New";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // import
            // 
            this.import.BackColor = System.Drawing.Color.DarkGray;
            this.import.Controls.Add(this.pageLoad);
            this.import.Controls.Add(this.label6);
            this.import.Controls.Add(this.siteLoad);
            this.import.Controls.Add(this.label5);
            this.import.Controls.Add(this.incTb);
            this.import.Controls.Add(this.inc);
            this.import.Controls.Add(this.urlSplitterTb);
            this.import.Controls.Add(this.urlsplitter);
            this.import.Controls.Add(this.panel4);
            this.import.Controls.Add(this.groupBox2);
            this.import.Controls.Add(this.btnScrapp);
            this.import.Controls.Add(this.groupBox1);
            this.import.Location = new System.Drawing.Point(4, 22);
            this.import.Name = "import";
            this.import.Padding = new System.Windows.Forms.Padding(3);
            this.import.Size = new System.Drawing.Size(641, 767);
            this.import.TabIndex = 1;
            this.import.Text = "Import Data";
            this.import.Click += new System.EventHandler(this.import_Click);
            // 
            // pageLoad
            // 
            this.pageLoad.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pageLoad.Location = new System.Drawing.Point(490, 473);
            this.pageLoad.Name = "pageLoad";
            this.pageLoad.Size = new System.Drawing.Size(40, 22);
            this.pageLoad.TabIndex = 27;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(332, 473);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(152, 16);
            this.label6.TabIndex = 26;
            this.label6.Text = "Page Load Time(secs):";
            // 
            // siteLoad
            // 
            this.siteLoad.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.siteLoad.Location = new System.Drawing.Point(245, 473);
            this.siteLoad.Name = "siteLoad";
            this.siteLoad.Size = new System.Drawing.Size(40, 22);
            this.siteLoad.TabIndex = 25;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(95, 473);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(144, 16);
            this.label5.TabIndex = 24;
            this.label5.Text = "Site Load Time(secs):";
            // 
            // incTb
            // 
            this.incTb.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.incTb.Location = new System.Drawing.Point(234, 626);
            this.incTb.Name = "incTb";
            this.incTb.Size = new System.Drawing.Size(136, 22);
            this.incTb.TabIndex = 23;
            // 
            // inc
            // 
            this.inc.AutoSize = true;
            this.inc.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.inc.ForeColor = System.Drawing.Color.White;
            this.inc.Location = new System.Drawing.Point(133, 626);
            this.inc.Name = "inc";
            this.inc.Size = new System.Drawing.Size(76, 16);
            this.inc.TabIndex = 22;
            this.inc.Text = "Increment:";
            // 
            // urlSplitterTb
            // 
            this.urlSplitterTb.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.urlSplitterTb.Location = new System.Drawing.Point(234, 582);
            this.urlSplitterTb.Name = "urlSplitterTb";
            this.urlSplitterTb.Size = new System.Drawing.Size(136, 22);
            this.urlSplitterTb.TabIndex = 21;
            // 
            // urlsplitter
            // 
            this.urlsplitter.AutoSize = true;
            this.urlsplitter.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.urlsplitter.ForeColor = System.Drawing.Color.White;
            this.urlsplitter.Location = new System.Drawing.Point(133, 582);
            this.urlsplitter.Name = "urlsplitter";
            this.urlsplitter.Size = new System.Drawing.Size(88, 16);
            this.urlsplitter.TabIndex = 20;
            this.urlsplitter.Text = "URL Splitter:";
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.White;
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel4.Controls.Add(this.dynamicRb);
            this.panel4.Controls.Add(this.staticRb);
            this.panel4.Location = new System.Drawing.Point(136, 520);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(235, 27);
            this.panel4.TabIndex = 19;
            // 
            // dynamicRb
            // 
            this.dynamicRb.AutoSize = true;
            this.dynamicRb.Checked = true;
            this.dynamicRb.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dynamicRb.Location = new System.Drawing.Point(122, 3);
            this.dynamicRb.Name = "dynamicRb";
            this.dynamicRb.Size = new System.Drawing.Size(110, 20);
            this.dynamicRb.TabIndex = 1;
            this.dynamicRb.TabStop = true;
            this.dynamicRb.Text = "Dynamic Site";
            this.dynamicRb.UseVisualStyleBackColor = true;
            this.dynamicRb.CheckedChanged += new System.EventHandler(this.radioButtons_CheckedChanged);
            // 
            // staticRb
            // 
            this.staticRb.AutoSize = true;
            this.staticRb.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.staticRb.Location = new System.Drawing.Point(3, 3);
            this.staticRb.Name = "staticRb";
            this.staticRb.Size = new System.Drawing.Size(91, 20);
            this.staticRb.TabIndex = 0;
            this.staticRb.TabStop = true;
            this.staticRb.Text = "Static Site";
            this.staticRb.UseVisualStyleBackColor = true;
            this.staticRb.CheckedChanged += new System.EventHandler(this.radioButtons_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tblName);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.dataSets);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Font = new System.Drawing.Font("Rockwell", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.Color.White;
            this.groupBox2.Location = new System.Drawing.Point(136, 283);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(365, 147);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "DataSets";
            // 
            // tblName
            // 
            this.tblName.DropDownWidth = 205;
            this.tblName.Font = new System.Drawing.Font("Comic Sans MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tblName.FormattingEnabled = true;
            this.tblName.Location = new System.Drawing.Point(109, 89);
            this.tblName.Name = "tblName";
            this.tblName.Size = new System.Drawing.Size(206, 26);
            this.tblName.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Comic Sans MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(19, 89);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 18);
            this.label2.TabIndex = 3;
            this.label2.Text = "Table Name:";
            // 
            // dataSets
            // 
            this.dataSets.DropDownWidth = 205;
            this.dataSets.Font = new System.Drawing.Font("Comic Sans MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataSets.FormattingEnabled = true;
            this.dataSets.Location = new System.Drawing.Point(109, 41);
            this.dataSets.Name = "dataSets";
            this.dataSets.Size = new System.Drawing.Size(206, 26);
            this.dataSets.TabIndex = 1;
            this.dataSets.SelectedIndexChanged += new System.EventHandler(this.dataSets_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Comic Sans MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(16, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "DataSets:";
            // 
            // btnScrapp
            // 
            this.btnScrapp.Enabled = false;
            this.btnScrapp.Font = new System.Drawing.Font("Comic Sans MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnScrapp.Location = new System.Drawing.Point(136, 682);
            this.btnScrapp.Name = "btnScrapp";
            this.btnScrapp.Size = new System.Drawing.Size(115, 44);
            this.btnScrapp.TabIndex = 1;
            this.btnScrapp.Text = "Start Scrapping";
            this.btnScrapp.UseVisualStyleBackColor = true;
            this.btnScrapp.Click += new System.EventHandler(this.btnScrapp_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.btnUpload);
            this.groupBox1.Controls.Add(this.cmbSheets);
            this.groupBox1.Controls.Add(this.updCheck);
            this.groupBox1.Controls.Add(this.btnBrowse);
            this.groupBox1.Controls.Add(this.fileName);
            this.groupBox1.Font = new System.Drawing.Font("Rockwell", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(136, 58);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(365, 176);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "File Upload";
            // 
            // btnUpload
            // 
            this.btnUpload.BackColor = System.Drawing.Color.Transparent;
            this.btnUpload.Enabled = false;
            this.btnUpload.Font = new System.Drawing.Font("Comic Sans MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpload.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnUpload.Location = new System.Drawing.Point(273, 83);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(75, 26);
            this.btnUpload.TabIndex = 5;
            this.btnUpload.Text = "Upload";
            this.btnUpload.UseVisualStyleBackColor = false;
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // cmbSheets
            // 
            this.cmbSheets.DropDownWidth = 205;
            this.cmbSheets.Font = new System.Drawing.Font("Comic Sans MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbSheets.FormattingEnabled = true;
            this.cmbSheets.Location = new System.Drawing.Point(19, 84);
            this.cmbSheets.Name = "cmbSheets";
            this.cmbSheets.Size = new System.Drawing.Size(237, 26);
            this.cmbSheets.TabIndex = 4;
            // 
            // updCheck
            // 
            this.updCheck.AutoSize = true;
            this.updCheck.Font = new System.Drawing.Font("Comic Sans MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.updCheck.Location = new System.Drawing.Point(22, 125);
            this.updCheck.Name = "updCheck";
            this.updCheck.Size = new System.Drawing.Size(165, 22);
            this.updCheck.TabIndex = 3;
            this.updCheck.Text = "Update Scrapped Data";
            this.updCheck.UseVisualStyleBackColor = true;
            // 
            // btnBrowse
            // 
            this.btnBrowse.BackColor = System.Drawing.Color.Transparent;
            this.btnBrowse.Font = new System.Drawing.Font("Comic Sans MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBrowse.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnBrowse.Location = new System.Drawing.Point(273, 35);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(75, 26);
            this.btnBrowse.TabIndex = 1;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.UseVisualStyleBackColor = false;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // fileName
            // 
            this.fileName.Font = new System.Drawing.Font("Comic Sans MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fileName.Location = new System.Drawing.Point(18, 35);
            this.fileName.Name = "fileName";
            this.fileName.Size = new System.Drawing.Size(237, 26);
            this.fileName.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(638, 788);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Import Data Form";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.fieldSelection.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.import.ResumeLayout(false);
            this.import.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage import;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox fileName;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Button btnScrapp;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox dataSets;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox updCheck;
        private System.Windows.Forms.TabPage fieldSelection;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnAddAction;
        private System.Windows.Forms.Button btnAddField;
        private System.Windows.Forms.Button btnSaveTemplate;
        private System.Windows.Forms.Label siteName;
        private System.Windows.Forms.TextBox siteNameText;

        private System.Windows.Forms.TextBox parentSelectorTextBox;
        private KeyValuePair<System.Windows.Forms.CheckBox , System.Windows.Forms.TextBox> parentTag;

        private List<System.Windows.Forms.TextBox> fieldsTextBoxList;
        
        private List<System.Windows.Forms.TextBox> selectorsTextBoxList;
        private Dictionary<System.Windows.Forms.CheckBox, System.Windows.Forms.TextBox> tagsCheckBoxList;

        private List<System.Windows.Forms.TextBox> actionsTextBoxList;
        private List<System.Windows.Forms.Panel> actionsRadioButtonsList;
        private System.Windows.Forms.ComboBox tblName;
        private System.Windows.Forms.ComboBox cmbSheets;
        private System.Windows.Forms.Button btnUpload;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.RadioButton dynamicRb;
        private System.Windows.Forms.RadioButton staticRb;
        private System.Windows.Forms.Label urlsplitter;
        private System.Windows.Forms.TextBox urlSplitterTb;
        private System.Windows.Forms.Label inc;
        private System.Windows.Forms.TextBox incTb;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox siteLoad;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox pageLoad;
    }
}

