using DataPuller.BAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataPuller.BO;
using DataPuller.DAL;
using Logger;

namespace DataPullerUI
{
    public partial class Form1 : Form
    {
        IList<ProductInfo> productInfoList = null;
        DataModel siteDataModel=null;
        IDictionary<string, DataModel> templates = new Dictionary<string, DataModel>();
        int upperDistance = 85 , lastPosition = 17;
        Dictionary<string, List<string>> dbWithTables;
        ProductInfoReader reader = new ProductInfoReader();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SqlServerOperation operation = SqlServerOperation.Instance;
            dbWithTables = operation.GetDataSets();
            dataSets.DataSource = dbWithTables.Keys.ToList();
            dataSets.SelectedIndex = 0;

            tblName.DataSource = dbWithTables[dataSets.SelectedItem.ToString()];
            tblName.SelectedIndex = 0;
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog op1 = new OpenFileDialog();
            op1.Filter = "Excel Files| *.xls; *.xlsx;";
            op1.Multiselect = false;
            DialogResult result = op1.ShowDialog();
            if (result == DialogResult.OK)
            {
                fileName.Text = op1.FileName;
                try
                {
                    cmbSheets.DataSource = reader.GetSheetNames(op1.FileName);
                    cmbSheets.SelectedIndex = 0;
                    btnUpload.Enabled = true;
                    //productInfoList = reader.GetData(op1.FileName);
                }
                catch (Exception)
                {
                    MessageBox.Show("Error in Reading Excel File: " + op1.FileName);
                }
            }
            else
            {
                MessageBox.Show("Please Select File");
            }
        }
        private void btnUpload_Click(object sender, EventArgs e)
        {
            if(productInfoList != null)
            {
                productInfoList.Clear();
            }
            try
            {
                productInfoList = reader.GetData(fileName.Text , cmbSheets.SelectedItem.ToString());
                MessageBox.Show("File has been successfully read");
                btnScrapp.Enabled = true;
            }
            catch (Exception)
            {
                MessageBox.Show("Error in Reading Excel File: " + fileName.Text);
            }
        }

        private void btnScrapp_Click(object sender, EventArgs e)
        {
            if (productInfoList.Count == 0)
            {
                MessageBox.Show("Selected Sheet is empty");
                return;
            }
            SqlServerOperation operation = SqlServerOperation.Instance;
            operation.SetDataSets(dataSets.SelectedItem.ToString(), tblName.SelectedItem.ToString());
            
            bool updateFlag = updCheck.Checked;
            Browser browser = new Browser(Browser.Type.CHROME, false);
            Puller dataPuller = new Puller();

            foreach (var info in productInfoList)
            {
                try
                {
                    if (!templates.ContainsKey(info.Template))
                    {
                        if(!ReadTemplate(info.Template))
                        {
                            Log.Exception("Scrapping failed for " + info.Url);
                            continue;
                        }
                    }
                    DataModel dataModel = templates[info.Template];
                    if(staticRb.Checked)
                    {
                        StaticSite target = new StaticSite(browser, dataModel.name, info.Url, dataModel, 1, info.LastPageNo, info.Name, dataModel.nextPageSelectorElement, int.Parse(siteLoad.Text), int.Parse(pageLoad.Text) , dataModel.siteActions, dataModel.pageActions);
                        dynamic sitena = dataPuller.PullData(target, updateFlag);
                    }
                    else
                    {
                        DynamicSite target = new DynamicSite(browser, dataModel.name , info.Url, dataModel, info.Url, urlSplitterTb.Text , int.Parse(incTb.Text) , info.Name, int.Parse(siteLoad.Text), int.Parse(pageLoad.Text), dataModel.siteActions, dataModel.pageActions, 1, info.LastPageNo);
                        dynamic sitena = dataPuller.PullData(target, updateFlag);
                    }
                    
                }
                catch (Exception ex)
                {
                    Log.Exception("Scrapping failed for " + info.Url);
                }
            }
            MessageBox.Show("Scrapping has been completed !!");
        }
        bool ReadTemplate(string template)
        {
            IDataAccessLayer readFile = DataAccessFactory.Create(0);
            DataModel data = readFile.GetTemplate<DataModel>(template);
            data.siteActions = data.siteActionsList.ToDictionary(pair=>pair.Key , pair=>pair.Value);
            data.pageActions = data.pageActionsList.ToDictionary(pair=>pair.Key , pair=>pair.Value);
            if(data != null)
            {
                templates.Add(template , data);
                return true;
            }
            return false;
        }
        private void updSchema_Click(object sender, EventArgs e)
        {
            var dbName = dataSets.SelectedItem;
            var tbl = tblName.SelectedItem;
            
            SqlServerOperation operation = SqlServerOperation.Instance;
            operation.SetDataSets(dbName.ToString() , tbl.ToString());
            if(siteDataModel == null)
            {
                GetDataFromControls();
            }
            if(operation.CreateTable(siteDataModel))
            {
                MessageBox.Show("Schema Updated");
            }
            
        }
        private void radioButtons_CheckedChanged(object sender, EventArgs e)
        {
            //RadioButton radioButton = sender as RadioButton;

            if (staticRb.Checked)
            {
                urlsplitter.Visible = false;
                urlSplitterTb.Visible = false;
                inc.Visible = false;
                incTb.Visible = false;

                int x = btnScrapp.Location.X;
                int y = btnScrapp.Location.Y;
                btnScrapp.Location = new Point(x , y-55);
            }
            else if (dynamicRb.Checked)
            {
                urlsplitter.Visible = true;
                urlSplitterTb.Visible = true;
                inc.Visible = true;
                incTb.Visible = true;

                int x = btnScrapp.Location.X;
                int y = btnScrapp.Location.Y;
                btnScrapp.Location = new Point(x, y + 55);
            }
        }
        private void btnCreate_Click(object sender, EventArgs e)
        {
            InitializePannel();
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            OpenFileDialog op1 = new OpenFileDialog();
            op1.Filter = "Json Files| *.json;";
            op1.Multiselect = false;
            DialogResult result = op1.ShowDialog();
            DataModel data = null;
            if (result == DialogResult.OK)
            {
                string fName = op1.FileName;
                try
                {
                    IDataAccessLayer readFile = DataAccessFactory.Create(0);
                    data = readFile.Get<DataModel>(fName);
                    ShowDataTocontrols(data);
                }
                catch (Exception)
                {
                    MessageBox.Show("Error in Reading File: " + op1.FileName);
                }
            }
            else
            {
                MessageBox.Show("Please Select File");
            }
        }
        void ShowDataTocontrols(DataModel data)
        {
            InitializePannel();
            siteNameText.Text = data.name;
            parentSelectorTextBox.Text = data.parentElement.htmlSelector.selectorValue;
            if(data.parentElement.attributeValue != null)
            {
                parentTag.Key.Checked = true;
                parentTag.Value.Text = data.parentElement.attributeValue;
            }
            for(int i = 0; i < data.fieldsList.Count; i++)
            {
                DrawLabel(lastPosition, upperDistance, "Field:");
                lastPosition = lastPosition + 10;
                DrawTextBox(lastPosition, upperDistance, 0 , data.fieldsList.ElementAt(i).fieldName);
                lastPosition = lastPosition + 20;

                DrawLabel(lastPosition, upperDistance, "Selector:");
                lastPosition = lastPosition + 10;
                DrawTextBox(lastPosition, upperDistance, 1 , data.fieldsList.ElementAt(i).htmlElement.htmlSelector.selectorValue);

                lastPosition = lastPosition + 20;
                DrawCheckBox(lastPosition, upperDistance , tagsCheckBoxList);

                if(data.fieldsList.ElementAt(i).htmlElement.attributeValue != null)
                {
                    tagsCheckBoxList.ElementAt(i).Key.Checked = true;
                    tagsCheckBoxList.ElementAt(i).Value.Text = data.fieldsList.ElementAt(i).htmlElement.attributeValue;
                }
                upperDistance = upperDistance + 60;
                lastPosition = 17;
            }
            for(int i = 0; i < data.siteActionsList.Count; i++)
            {
                DrawLabel(lastPosition, upperDistance, "Action:");
                lastPosition = lastPosition + 10;
                DrawTextBox(lastPosition, upperDistance, 2 , data.siteActionsList.ElementAt(i).Key.htmlSelector.selectorValue);
                lastPosition = lastPosition + 20;

                DrawPanelWithRadioButtons(lastPosition, upperDistance);

                upperDistance = upperDistance + 60;
                lastPosition = 17;
            }
            for (int i = 0; i < data.pageActionsList.Count; i++)
            {
                DrawLabel(lastPosition, upperDistance, "Action:");
                lastPosition = lastPosition + 10;
                DrawTextBox(lastPosition, upperDistance, 2, data.pageActionsList.ElementAt(i).Key.htmlSelector.selectorValue);
                lastPosition = lastPosition + 20;

                DrawPanelWithRadioButtons(lastPosition, upperDistance);
                RadioButton radio = actionsRadioButtonsList.Last().Controls.OfType<RadioButton>().FirstOrDefault(x => x.Name.Equals("page"));
                radio.Checked = true;

                upperDistance = upperDistance + 60;
                lastPosition = 17;
            }
        }
        void InitializePannel()
        {
            if(panel1 != null)
            {
                panel1.Controls.Clear();
                fieldSelection.Controls.Remove(panel1);
                fieldSelection.Controls.Remove(siteName);
                fieldSelection.Controls.Remove(siteNameText);
                lastPosition = 17;
                upperDistance = 85;
                panel1.Dispose();
                panel1 = null;
                siteName = null;
                siteNameText = null;
            }
            panel1 = new Panel();
            this.btnAddAction = new System.Windows.Forms.Button();
            this.btnAddField = new System.Windows.Forms.Button();
            this.btnSaveTemplate = new System.Windows.Forms.Button();
            this.fieldsTextBoxList = new List<System.Windows.Forms.TextBox>();
            this.selectorsTextBoxList = new List<System.Windows.Forms.TextBox>();
            this.actionsTextBoxList = new List<System.Windows.Forms.TextBox>();
            this.tagsCheckBoxList = new Dictionary<CheckBox, TextBox>();
            this.actionsRadioButtonsList = new List<Panel>();
            this.siteName = new Label();
            this.siteNameText = new TextBox();
            


            InitializeButtons();
            DrawLabel(lastPosition, upperDistance, "Parent Selector:");
            lastPosition = lastPosition + 10;
            DrawTextBox(lastPosition , upperDistance , 3);
            lastPosition = lastPosition + 20;
            DrawCheckBoxForParent(lastPosition , upperDistance);
            lastPosition = 17;
            upperDistance = upperDistance + 60;

            this.panel1.Controls.Add(this.btnAddAction);
            this.panel1.Controls.Add(this.btnAddField);
            this.fieldSelection.Controls.Add(this.btnSaveTemplate);
            this.fieldSelection.Controls.Add(this.siteName);
            this.fieldSelection.Controls.Add(this.siteNameText);

            panel1.Location = new Point(17, 147);
            panel1.Size = new Size(610, 482);
            panel1.BackColor = Color.White;
            panel1.BorderStyle = BorderStyle.Fixed3D;
            panel1.AutoScroll = true;
            panel1.Visible = true;
            this.fieldSelection.Controls.Add(this.panel1);
            this.panel1.PerformLayout();
        }
        void InitializeButtons()
        {
            this.btnAddAction.BackColor = System.Drawing.Color.Transparent;
            this.btnAddAction.Font = new System.Drawing.Font("Comic Sans MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddAction.Location = new System.Drawing.Point(338, 14);
            this.btnAddAction.Name = "btnAddAction";
            this.btnAddAction.Size = new System.Drawing.Size(126, 39);
            this.btnAddAction.TabIndex = 1;
            this.btnAddAction.Text = "Add Action";
            this.btnAddAction.UseVisualStyleBackColor = false;

            this.btnAddField.BackColor = System.Drawing.Color.Transparent;
            this.btnAddField.Font = new System.Drawing.Font("Comic Sans MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddField.Location = new System.Drawing.Point(145, 14);
            this.btnAddField.Name = "btnAddField";
            this.btnAddField.Size = new System.Drawing.Size(126, 39);
            this.btnAddField.TabIndex = 0;
            this.btnAddField.Text = "Add Field";
            this.btnAddField.UseVisualStyleBackColor = false;

            this.btnSaveTemplate.BackColor = System.Drawing.Color.Transparent;
            this.btnSaveTemplate.Font = new System.Drawing.Font("Comic Sans MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveTemplate.Location = new System.Drawing.Point(504, 651);
            this.btnSaveTemplate.Name = "button2";
            this.btnSaveTemplate.Size = new System.Drawing.Size(126, 39);
            this.btnSaveTemplate.TabIndex = 2;
            this.btnSaveTemplate.Text = "Save Template";
            this.btnSaveTemplate.UseVisualStyleBackColor = false;

            this.siteName.AutoSize = true;
            this.siteName.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.siteName.ForeColor = System.Drawing.Color.White;
            this.siteName.Location = new System.Drawing.Point(15, 657);
            this.siteName.Size = new System.Drawing.Size(78, 16);
            this.siteName.Text = "Site Name:";

            this.siteNameText.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.siteNameText.Location = new System.Drawing.Point(110, 651);
            this.siteNameText.Size = new System.Drawing.Size(136, 22);
            RegisterEvents();
        }
        void RegisterEvents()
        {
            this.btnAddField.Click += new System.EventHandler(this.btnAddField_Click);
            this.btnAddAction.Click += new System.EventHandler(this.btnAddAction_Click);
            this.btnSaveTemplate.Click += new System.EventHandler(this.btnSaveTemplate_Click);
        }
        void DrawLabel(int x , int y , string text)
        {
            Label label = new Label();
            label.AutoSize = true;
            label.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

            label.Size = new System.Drawing.Size(44, 16);
            label.Text = text;
            label.Location = new System.Drawing.Point(x, y);
            this.panel1.Controls.Add(label);
            lastPosition = lastPosition + label.Size.Width;
        }
        void DrawTextBox(int x, int y , int tBoxFor , string text = "")
        {
            TextBox textBox = new TextBox();
            textBox.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            textBox.Location = new System.Drawing.Point(x,y);
            textBox.Size = new System.Drawing.Size(136, 22);
            textBox.Text = text;
            this.panel1.Controls.Add(textBox);
            
            lastPosition = lastPosition + textBox.Size.Width;

            if(tBoxFor == 0)
            {
                fieldsTextBoxList.Add(textBox);
            }
            else if(tBoxFor == 1)
            {
                selectorsTextBoxList.Add(textBox);
            }
            else if (tBoxFor == 2)
            {
                actionsTextBoxList.Add(textBox);
            }
            else
            {
                this.parentSelectorTextBox = textBox;
            }
        }

        void DrawCheckBoxForParent(int x, int y)
        {
            CheckBox checkBoxParent = new CheckBox();
            checkBoxParent.AutoSize = true;
            checkBoxParent.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            checkBoxParent.Location = new System.Drawing.Point(x, y + 5);
            checkBoxParent.Size = new System.Drawing.Size(15, 14);
            checkBoxParent.UseVisualStyleBackColor = true;
            lastPosition = lastPosition + checkBoxParent.Size.Width;
            this.panel1.Controls.Add(checkBoxParent);
            this.parentTag = new KeyValuePair<CheckBox, TextBox>(checkBoxParent, null);
            checkBoxParent.CheckedChanged += new EventHandler(checkBoxParent_CheckedChanged);
        }

        private void checkBoxParent_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            switch (checkBox.CheckState)
            {
                case CheckState.Checked:
                    TextBox textBox = new TextBox();

                    textBox.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    textBox.Location = new System.Drawing.Point(checkBox.Location.X + checkBox.Size.Width + 10, checkBox.Location.Y - 5);
                    textBox.Size = new System.Drawing.Size(75, 22);
                    this.panel1.Controls.Add(textBox);
                    this.parentTag = new KeyValuePair<CheckBox, TextBox>(checkBox , textBox);
                    break;
                case CheckState.Unchecked:
                    this.panel1.Controls.Remove(parentTag.Value);
                    break;
            }

        }

        void DrawCheckBox(int x, int y , Dictionary<System.Windows.Forms.CheckBox, System.Windows.Forms.TextBox> ActionCheckBoxList)
        {
            CheckBox checkBox = new CheckBox();
            checkBox.AutoSize = true;
            checkBox.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            checkBox.Location = new System.Drawing.Point(x, y+5);
            checkBox.Size = new System.Drawing.Size(15, 14);
            checkBox.UseVisualStyleBackColor = true;
            lastPosition = lastPosition + checkBox.Size.Width;
            this.panel1.Controls.Add(checkBox);
            ActionCheckBoxList.Add(checkBox , null);
            checkBox.CheckedChanged += new EventHandler((s, e) => checkBox_CheckedChanged(s, e, ActionCheckBoxList));
        }

        private void checkBox_CheckedChanged(object sender, EventArgs e , Dictionary<System.Windows.Forms.CheckBox, System.Windows.Forms.TextBox> ActionCheckBoxList)
        {
            CheckBox checkBox = sender as CheckBox;
            switch(checkBox.CheckState)
            {
                case CheckState.Checked:
                    TextBox textBox = new TextBox();

                    textBox.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    textBox.Location = new System.Drawing.Point(checkBox.Location.X + checkBox.Size.Width + 10, checkBox.Location.Y - 5);
                    textBox.Size = new System.Drawing.Size(75, 22);
                    this.panel1.Controls.Add(textBox);
                    ActionCheckBoxList[checkBox] = textBox;
                    break;
                case CheckState.Unchecked:
                    this.panel1.Controls.Remove(ActionCheckBoxList[checkBox]);
                    ActionCheckBoxList[checkBox] = null;
                    break;
            }
            
        }
        void DrawPanelWithRadioButtons(int x, int y)
        {
            Panel actionPanel = new Panel();
            RadioButton pageAction = new RadioButton();
            RadioButton siteAction = new RadioButton();

            siteAction.AutoSize = true;
            siteAction.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            siteAction.Location = new System.Drawing.Point(3, 3);
            siteAction.Name = "site";
            siteAction.Size = new System.Drawing.Size(95, 20);
            siteAction.TabStop = true;
            siteAction.Text = "Site Action";
            siteAction.UseVisualStyleBackColor = true;
            siteAction.Checked = true;
            // 
            // radioButton1
            // 
            pageAction.AutoSize = true;
            pageAction.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            pageAction.Location = new System.Drawing.Point(115, 3);
            pageAction.Name = "page";
            pageAction.Size = new System.Drawing.Size(103, 20);
            pageAction.TabStop = true;
            pageAction.Text = "Page Action";
            pageAction.UseVisualStyleBackColor = true;

            actionPanel.BackColor = System.Drawing.Color.Transparent;
            actionPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            actionPanel.Controls.Add(siteAction);
            actionPanel.Controls.Add(pageAction);
            actionPanel.Location = new System.Drawing.Point(x , y);
            actionPanel.Size = new System.Drawing.Size(221, 27);
            lastPosition = lastPosition + actionPanel.Size.Width;
            this.panel1.Controls.Add(actionPanel);

            actionsRadioButtonsList.Add(actionPanel);
        }
        private void btnAddField_Click(object sender, EventArgs e)
        {
            DrawLabel(lastPosition, upperDistance , "Field:");
            lastPosition = lastPosition + 10;
            DrawTextBox(lastPosition , upperDistance , 0);
            lastPosition = lastPosition + 20;

            DrawLabel(lastPosition , upperDistance, "Selector:");
            lastPosition = lastPosition + 10;
            DrawTextBox(lastPosition, upperDistance , 1);
            lastPosition = lastPosition + 20;

            DrawCheckBox(lastPosition , upperDistance , tagsCheckBoxList);
            upperDistance = upperDistance + 60;
            lastPosition = 17;
        }
        void DrawDelete(int x, int y)
        {
            LinkLabel deleteLabel = new LinkLabel();
            deleteLabel.AutoSize = true;
            deleteLabel.Location = new System.Drawing.Point(x,y);
            deleteLabel.Size = new System.Drawing.Size(38, 13);
            deleteLabel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            deleteLabel.TabStop = true;
            deleteLabel.Text = "Delete";
            deleteLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.deleteLabel_LinkClicked);

            this.panel1.Controls.Add(deleteLabel);
            lastPosition = lastPosition + deleteLabel.Size.Width;
        }

        private void deleteLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
        }

        private void btnAddAction_Click(object sender, EventArgs e)
        {
            DrawLabel(lastPosition, upperDistance, "Action:");
            lastPosition = lastPosition + 10;
            DrawTextBox(lastPosition, upperDistance, 2);
            lastPosition = lastPosition + 20;

            DrawPanelWithRadioButtons(lastPosition, upperDistance);

            upperDistance = upperDistance + 60;
            lastPosition = 17;
        }
        private void btnSaveTemplate_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveTemplateDialog = new SaveFileDialog();
            saveTemplateDialog.ShowDialog();
            saveTemplateDialog.Title = "Save Site Template";
            saveTemplateDialog.Filter = "Json Files| *.json;";
            saveTemplateDialog.InitialDirectory = @"C:\";
            saveTemplateDialog.CheckFileExists = true;
            saveTemplateDialog.CheckPathExists = true;
            
            if(saveTemplateDialog.FileName != "")
            {
                string filename = saveTemplateDialog.FileName;
                if(siteDataModel == null)
                { 
                    GetDataFromControls();
                }
                IDataAccessLayer writeFile = DataAccessFactory.Create(0);
                writeFile.Save(filename , siteDataModel);
                MessageBox.Show("Template saved successfully");
            }
            else
            {
                MessageBox.Show("Please Enter Filename");
            }
        }

        private void dataSets_SelectedIndexChanged(object sender, EventArgs e)
        {
            tblName.DataSource = dbWithTables[dataSets.SelectedItem.ToString()];
            tblName.SelectedIndex = 0;
        }

        private void import_Click(object sender, EventArgs e)
        {

        }

        void GetDataFromControls()
        {
            Element parentElement = null;
            HtmlSelector parentSelector = null;
            
            if (!parentTag.Key.Checked)
            {                
                parentSelector = new HtmlSelector(parentSelectorTextBox.Text, SelectBy.CSS_SELECTOR);
                parentElement = new Element(parentSelector);
            }
            else
            {
                parentElement = new Element(parentSelector , parentTag.Value.Text);
            }
     
            HtmlSelector elementSelector;
            Element element = null;
            List<Field> fieldsList = new List<Field>();
            for (int i = 0; i < tagsCheckBoxList.Count; i++)
            {
                if(fieldsTextBoxList.ElementAt(i).Text == "" || selectorsTextBoxList.ElementAt(i).Text == "")
                {
                    continue;
                }
                elementSelector = new HtmlSelector(selectorsTextBoxList.ElementAt(i).Text, SelectBy.CSS_SELECTOR);
                if (!tagsCheckBoxList.ElementAt(i).Key.Checked)
                {
                    
                    element = new Element(elementSelector, AttributeType.TEXT);
                }
                else
                {
                    element = new Element(elementSelector, tagsCheckBoxList.ElementAt(i).Value.Text);
                }
                fieldsList.Add(new Field(fieldsTextBoxList.ElementAt(i).Text , element));
            }

            HtmlSelector Action = null;
            Element ActionElement = null;
            DomAction clickAction = new DomAction(DomAction.ActionType.CLICK, "");
            IDictionary<Element, DomAction> siteActions = new Dictionary<Element, DomAction>();
            IDictionary<Element, DomAction> pageActions = new Dictionary<Element, DomAction>();
            for (int i = 0; i < actionsTextBoxList.Count; i++)
            {
                if (actionsTextBoxList.ElementAt(i).Text == "")
                {
                    continue;
                }
                
                Action = new HtmlSelector(actionsTextBoxList.ElementAt(i).Text, SelectBy.CSS_SELECTOR);
                ActionElement = new Element(Action, AttributeType.TEXT);
                RadioButton radio = actionsRadioButtonsList.ElementAt(i).Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked);
                if (radio.Name.Equals("page"))
                {
                    pageActions.Add(ActionElement, clickAction);
                }
                else
                {
                    siteActions.Add(ActionElement, clickAction);
                }
                
            }
            siteDataModel = new DataModel(siteNameText.Text, parentElement, fieldsList, null , pageActions , siteActions);
        }
    }
}