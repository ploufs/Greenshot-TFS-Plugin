namespace GreenshotTFSPlugin.Forms
{
    using System;
    using System.Collections;
    using System.IO;
    using System.Net;
    using System.Text;
    using System.Linq;
    using System.Windows.Forms;
    using GreenshotPlugin.Controls;
    using GreenshotPlugin.Core;
    using Greenshot.IniFile;
    using Microsoft.TeamFoundation.Client;
    using Microsoft.TeamFoundation.WorkItemTracking.Client;
    using Microsoft.TeamFoundation;
    using Microsoft.Win32;

    public partial class AddForm : Form
    {
        private static TFSConfiguration config = IniConfig.GetIniSection<TFSConfiguration>();

        public AddForm()
        {
            InitializeComponent();

            button_save.Enabled = false;
            if (!string.IsNullOrEmpty(config.TfsDefaultProject))
            {
                textbox_tfsUrl.Text = config.TfsServerUrl;
                textbox_defaultProject.Text = config.TfsDefaultProject;
                button_save.Enabled = true;
            }

            this.BindScreen();

            // genereate system info
            StringBuilder systemInfo = new StringBuilder();

            OperatingSystem os = Environment.OSVersion;
            systemInfo.AppendFormat("OS Version : {0}{1}", os.VersionString.ToString(), Environment.NewLine);
            systemInfo.AppendFormat("Computer name : {0}{1}", SystemInformation.ComputerName, Environment.NewLine);
            systemInfo.AppendFormat("Monitor count : {0}{1}", SystemInformation.MonitorCount, Environment.NewLine);
            systemInfo.AppendFormat("Monitors same display format : {0}{1}", SystemInformation.MonitorsSameDisplayFormat, Environment.NewLine);
            systemInfo.AppendFormat("Primary monitor size : {0} x {1} {2}", SystemInformation.PrimaryMonitorSize.Height.ToString(), SystemInformation.PrimaryMonitorSize.Width.ToString(), Environment.NewLine);
            systemInfo.AppendFormat("Terminal server session : {0}{1}", SystemInformation.TerminalServerSession, Environment.NewLine);
            systemInfo.AppendFormat("Working Area : {0} X {1} {2}", SystemInformation.WorkingArea.Height.ToString(), SystemInformation.WorkingArea.Width.ToString(), Environment.NewLine);

            WebBrowser browser = new WebBrowser();
            systemInfo.AppendFormat("Internet explorer version : {0}{1}", browser.Version, SystemInformation.WorkingArea.Width.ToString(), Environment.NewLine);
            browser.Dispose();

            //version firefox
            var regKeyFirefox = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Mozilla\Mozilla Firefox", false);
            if (regKeyFirefox != null)
            {
                systemInfo.AppendFormat("{0}Firefox version :{1}",Environment.NewLine, regKeyFirefox.GetValue("CurrentVersion"));
            }

            //version chrome
            var regKeyChrome = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Uninstall\Google Chrome", false);
            if (regKeyChrome != null)
            {
                systemInfo.AppendFormat("{0}Chrome version : {1}",Environment.NewLine, regKeyChrome.GetValue("Version"));
            }


            textbox_SystemInfo.Text = systemInfo.ToString();
        }

        public TFSInfo TFSInfo { get; set; }

        public string ContentType { get; set; }

        public string Filename { get; set; }

        public string Title
        {
            get { return textbox_title.Text; }
            set { textbox_title.Text = value; }
        }

        public byte[] ImageData { get; set; }

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            button_save.Enabled = false;
            using (TeamProjectPicker tpp = new TeamProjectPicker(TeamProjectPickerMode.SingleProject, false))
            {
                DialogResult result = tpp.ShowDialog();
                if (result == DialogResult.OK)
                {
                    textbox_tfsUrl.Text = tpp.SelectedTeamProjectCollection.Uri.ToString();
                    textbox_defaultProject.Text = tpp.SelectedProjects[0].Name;
                    button_save.Enabled = true;

                    BackgroundForm backgroundForm = BackgroundForm.ShowAndWait(TFSPlugin.Attributes.Name, Language.GetString("tfs",LangKey.communication_wait));

                    try
                    {
                        this.BindScreen();
                    }
                    finally
                    {
                        backgroundForm.CloseDialog();
                    }
                }
            }
        }

        private void button_save_Click(object sender, EventArgs e)
        {
            try
            {
                ////http://social.technet.microsoft.com/wiki/contents/articles/3280.tfs-2010-api-create-workitems-bugs.aspx
                TfsTeamProjectCollection tfs = new TfsTeamProjectCollection(new Uri(this.textbox_tfsUrl.Text));

                WorkItemStore workItemStore = (WorkItemStore)tfs.GetService(typeof(WorkItemStore));

                WorkItemTypeCollection workItemTypes = workItemStore.Projects[this.textbox_defaultProject.Text].WorkItemTypes;

                WorkItemType workItemType = workItemTypes[combobox_workItemType.Text];

                // Assign values to each mandatory field
                var workItem = new WorkItem(workItemType);

                workItem.Title = textbox_title.Text;
                workItem.Description = textbox_description.Text;

                var fieldAssignTo = "System.AssignedTo";
                if (combobox_AssignTo.SelectedItem != null && workItem.Fields.Contains(fieldAssignTo))
                {
                    workItem.Fields[fieldAssignTo].Value = combobox_AssignTo.Text;
                }

                var fieldSeverity="Microsoft.VSTS.Common.Severity";
                if (combobox_severity.SelectedItem != null &&  workItem.Fields.Contains(fieldSeverity))
                {
                    workItem.Fields[fieldSeverity].Value = combobox_severity.Text;
                }

                var fieldPriority = "Microsoft.VSTS.Common.Priority";
                if (workItem.Fields.Contains(fieldPriority))
                {
                    workItem.Fields[fieldPriority].Value = textbox_Priority.Text;
                }

                if (combobox_AreaPath.SelectedItem != null)
                {
                    workItem.AreaPath = combobox_AreaPath.Text;
                }

                if (combobox_IterationPath.SelectedItem != null)
                {
                    workItem.IterationPath = combobox_IterationPath.Text;
                }

                var fieldState = "System.State";
                if (combobox_state.SelectedItem != null && workItem.Fields.Contains(fieldState))
                {
                    workItem.Fields[fieldState].Value = combobox_state.Text;
                }

                var fieldReason = "System.Reason";
                if (combobox_reason.SelectedItem != null && workItem.Fields.Contains(fieldReason))
                {
                    workItem.Fields["System.Reason"].Value = combobox_reason.Text;
                }

                string fieldSystenInfo = "Microsoft.VSTS.TCM.SystemInfo";
                if (workItem.Fields.Contains(fieldSystenInfo))
                {
                    if (workItem.Fields[fieldSystenInfo].FieldDefinition.FieldType == FieldType.Html)
                    {
                        workItem.Fields[fieldSystenInfo].Value = textbox_SystemInfo.Text.Replace(Environment.NewLine,"<br/>");
                    }
                    else
                    {
                        workItem.Fields[fieldSystenInfo].Value = textbox_SystemInfo.Text;
                    }

                }

                string fieldsReproStreps = "Microsoft.VSTS.TCM.ReproSteps";
                if (workItem.Fields.Contains(fieldsReproStreps))
                {
                    workItem.Fields[fieldsReproStreps].Value = textbox_ReproStep.Text;
                    if (string.IsNullOrEmpty(textbox_ReproStep.Text))
                    {
                        workItem.Fields[fieldsReproStreps].Value = workItem.Description;
                    }
                }

                // add image
                string tempFile = Path.Combine(Environment.CurrentDirectory, this.Filename);
                File.WriteAllBytes(tempFile, this.ImageData);

                workItem.Attachments.Add(new Attachment(tempFile));

                // Link the failed test case to the Bug
                // workItem.Links.Add(new RelatedLink(testcaseID));

                // Check for validation errors before saving the Bug
                ArrayList validationErrors = workItem.Validate();

                if (validationErrors.Count == 0)
                {
                    workItem.Save();

                    if (this.TFSInfo == null)
                    {
                        this.TFSInfo = new TFSInfo();
                    }

                    this.TFSInfo.ID = workItem.Id.ToString();
                    this.TFSInfo.Title = workItem.Title;
                    this.TFSInfo.Description = workItem.Description;

                    // http://stackoverflow.com/questions/6466441/how-to-map-a-tfs-item-url-to-something-viewable
                    var testManagementService = tfs.GetService<ILinking>();
                    this.TFSInfo.WebDetailUrl = testManagementService.GetArtifactUrlExternal(workItem.Uri.ToString());
                    
                    var myService = tfs.GetService<TswaClientHyperlinkService>();
                   this.TFSInfo.WebEditUrl = myService.GetWorkItemEditorUrl(workItem.Id).ToString();
                    

                    if (checkbox_description_addImage.Checked)
                    {
                        if (workItem.Fields["System.Description"].FieldDefinition.FieldType == FieldType.Html)
                        {
                            workItem.Description += string.Format("<br/><a href=\"{0}\"><img src=\"{0}\" /></a>", workItem.Attachments[0].Uri.ToString());
                        }
                        else
                        {
                            workItem.Description += System.Environment.NewLine + workItem.Attachments[0].Uri.ToString();
                        }
                    }

                    if (checkbox_reproStep_AddImage.Checked && (workItem.Fields.Contains(fieldsReproStreps)))
                    {
                        if (workItem.Fields[fieldsReproStreps].FieldDefinition.FieldType == FieldType.Html)
                        {
                            workItem.Fields[fieldsReproStreps].Value += string.Format("<br/><a href=\"{0}\"><img src=\"{0}\" /></a>", workItem.Attachments[0].Uri.ToString());
                        }
                        else
                        {
                            workItem.Fields[fieldsReproStreps].Value += System.Environment.NewLine + workItem.Attachments[0].Uri.ToString();
                        }
                    }

                    workItem.Save();

                    this.SetLastValue();
                    DialogResult = DialogResult.OK;
                }
                else
                {
                    string errrorMsg = "Validation Error in field\n";
                    foreach (Field field in validationErrors)
                    {
                        errrorMsg += field.Name + "\n";
                    }

                    MessageBox.Show(errrorMsg);
                }

                tfs.Dispose();

                // delete temps images
                System.IO.File.Delete(tempFile);
            }
            catch (Exception eError)
            {
                MessageBox.Show(eError.ToString());
            }
        }

        /// <summary>
        /// Bind combobox to tfs core field
        /// </summary>
        /// <param name="comboBox"></param>
        /// <param name="coreField"></param>
        private void BindComboBox(ComboBox comboBox, CoreField coreField)
        {
            using (var tfs = new TfsTeamProjectCollection(new Uri(this.textbox_tfsUrl.Text)))
            {
                WorkItemStore workItemStore = (WorkItemStore)tfs.GetService(typeof(WorkItemStore));

                Project project = workItemStore.Projects[this.textbox_defaultProject.Text];


                // workType
                WorkItemTypeCollection workItemTypes = project.WorkItemTypes;

                WorkItemType workItemType = workItemTypes[combobox_workItemType.Text];
                if (workItemType != null)
                {
                    var allowedValues = workItemType.FieldDefinitions[coreField].AllowedValues;
                    comboBox.Items.Clear();
                    foreach (string value in allowedValues)
                    {
                        comboBox.Items.Add(value);
                    }
                }
            }
        }

        private void SetLastValue()
        {
            if (combobox_AreaPath.SelectedItem != null)
            {
                config.TfsAreaPath = combobox_AreaPath.Text;
            }

            if (combobox_AssignTo.SelectedItem != null)
            {
                config.TfsAssignedTo = combobox_AssignTo.Text;
            }

            config.TfsDefaultProject = textbox_defaultProject.Text;

            if (combobox_IterationPath.SelectedItem != null)
            {
                config.TfsIterationPath = combobox_IterationPath.Text;
            }

            config.TfsPriority = (int)textbox_Priority.Value;
            config.TfsServerUrl = textbox_tfsUrl.Text;

            if (combobox_severity.SelectedItem != null)
            {
                config.TfsSeverity = combobox_severity.Text;
            }

            if (combobox_state.SelectedItem != null)
            {
                config.TfsState = combobox_state.Text;
            }

            if (combobox_workItemType.SelectedItem != null)
            {
                config.TfsWorkItemType = combobox_workItemType.Text;
            }

            if (combobox_workItemType.SelectedItem != null)
            {
                config.TfsReason = combobox_reason.Text;
            }
        }

        private void BindScreen()
        {
            if (!string.IsNullOrEmpty(this.textbox_tfsUrl.Text))
            {
                try
                {
                   using (var tfs = new TfsTeamProjectCollection(new Uri(this.textbox_tfsUrl.Text)))
                    {
                        WorkItemStore workItemStore = (WorkItemStore)tfs.GetService(typeof(WorkItemStore));
                        Project project = workItemStore.Projects[this.textbox_defaultProject.Text];

                        // workType
                        WorkItemTypeCollection workItemTypes = project.WorkItemTypes;
                        combobox_workItemType.Items.Clear();
                        foreach (WorkItemType workItemType in workItemTypes)
                        {
                            combobox_workItemType.Items.Add(workItemType.Name);
                        }

                        combobox_workItemType.SelectedValue = config.TfsWorkItemType;
                        if (combobox_workItemType.SelectedItem == null)
                        {
                            combobox_workItemType.SelectedIndex = 0;
                        }

                        this.BindScreenByItemType();

                        // area
                        combobox_AreaPath.Items.Clear();
                        combobox_AreaPath.Items.Add(project.Name);
                        this.BindComboBoxTree(combobox_AreaPath, project.Name, project.AreaRootNodes);
                        combobox_AreaPath.SelectedValue = config.TfsAreaPath;
                        if (combobox_AreaPath.SelectedItem == null && combobox_AreaPath.Items.Count > 0)
                        {
                            combobox_AreaPath.SelectedIndex = 0;
                        }

                        // iteration
                        combobox_IterationPath.Items.Clear();
                        combobox_IterationPath.Items.Add(project.Name);
                        this.BindComboBoxTree(combobox_IterationPath, project.Name, project.IterationRootNodes);
                        combobox_IterationPath.SelectedValue = config.TfsIterationPath;
                        if (combobox_IterationPath.SelectedItem == null && combobox_IterationPath.Items.Count > 0)
                        {
                            combobox_IterationPath.SelectedIndex = 0;
                        }

                        textbox_Priority.Value = config.TfsPriority;
                    }
                }
                catch (Microsoft.TeamFoundation.TeamFoundationServerUnauthorizedException eError)
                {
                    MessageBox.Show("Login fail");
                }
            }
        }


        /// <summary>
        /// Lood child and create combobox item
        /// </summary>
        /// <param name="parentTitle"></param>
        /// <param name="parentNode"></param>
        /// <param name="nodeCollection"></param>
        private void BindComboBoxTree(ComboBox combobox, string parentTitle, NodeCollection nodeCollection)
        {
            foreach (Node item in nodeCollection)
            {
                if (!string.IsNullOrEmpty(item.Name))
                {
                    string itemText = parentTitle + "/" + item.Name;
                    combobox.Items.Add(itemText);
                    this.BindComboBoxTree(combobox, itemText, item.ChildNodes);
                }
            }
        }

        private void BindScreenByItemType()
        {
            //disabled control not in workItem and bind combobox
            using (var tfs = new TfsTeamProjectCollection(new Uri(this.textbox_tfsUrl.Text)))
            {
                WorkItemStore workItemStore = (WorkItemStore)tfs.GetService(typeof(WorkItemStore));
                WorkItemTypeCollection workItemTypes = workItemStore.Projects[this.textbox_defaultProject.Text].WorkItemTypes;
                WorkItemType workItemType = workItemTypes[combobox_workItemType.Text];

                foreach (TabPage tabPage in tabControl_Field.TabPages)
                {
                    foreach (Control ctl in tabPage.Controls)
                    {
                        if (ctl.Tag != null)
                        {
                            ctl.Enabled = false;
                            if (workItemType.FieldDefinitions.Contains(ctl.Tag.ToString()))
                            {
                                var fieldDefinition = workItemType.FieldDefinitions[ctl.Tag.ToString()];
                                ctl.Enabled = (fieldDefinition.IsEditable && ! fieldDefinition.IsComputed);
                                
                                if(! string.IsNullOrEmpty(fieldDefinition.HelpText))
                                {
                                    tooltip_Main.SetToolTip(ctl, fieldDefinition.HelpText);
                                }

                                if (ctl is ComboBox)
                                {
                                    ((ComboBox)ctl).Items.Clear();
                                    foreach (var allowedValue in fieldDefinition.AllowedValues)
                                    {
                                        ((ComboBox)ctl).Items.Add(allowedValue);
                                    }
                                }
                            }
                        }
                    }

                }
            }

            combobox_AssignTo.SelectedValue = config.TfsAssignedTo;

            if (combobox_severity.SelectedItem == null && combobox_severity.Items.Count > 0)
            {
                combobox_severity.SelectedIndex = 0;
            }

            combobox_state.SelectedValue = config.TfsState;
            if (combobox_state.SelectedItem == null && combobox_state.Items.Count > 0)
            {
                combobox_state.SelectedIndex = 0;
            }

            combobox_reason.SelectedValue = config.TfsState;
            if (combobox_reason.SelectedItem == null && combobox_reason.Items.Count > 0)
            {
                // set to new if exist
                combobox_reason.SelectedItem = "New";

                if (combobox_reason.SelectedItem == null && combobox_reason.Items.Count > 0)
                {
                    combobox_reason.SelectedIndex = 0;
                }
            }
        }

        private void combobox_workItemType_SelectedIndexChanged(object sender, EventArgs e)
        {
            BackgroundForm backgroundForm = BackgroundForm.ShowAndWait(TFSPlugin.Attributes.Name, Language.GetString("tfs", LangKey.communication_wait));

            try
            {
                this.BindScreenByItemType();
            }
            finally
            {
                backgroundForm.CloseDialog();
            }
        }

        private void control_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel_main.Text = tooltip_Main.GetToolTip((Control)sender);
        }

        private void control_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel_main.Text = string.Empty;
        }
    }
}