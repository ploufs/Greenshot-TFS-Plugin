namespace GreenshotTFSPlugin.Forms
{
    using System;
    using System.Collections;
    using System.IO;
    using System.Net;
    using System.Text;
    using System.Windows.Forms;
    using GreenshotPlugin.Controls;
    using GreenshotPlugin.Core;
    using IniFile;
    using Microsoft.TeamFoundation.Client;
    using Microsoft.TeamFoundation.WorkItemTracking.Client;

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

                    ILanguage lang = Language.GetInstance();
                    BackgroundForm backgroundForm = BackgroundForm.ShowAndWait(TFSPlugin.Attributes.Name, lang.GetString(LangKey.communication_wait));

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

                if (combobox_AssignTo.SelectedItem != null)
                {
                    workItem.Fields["System.AssignedTo"].Value = combobox_AssignTo.Text;
                }

                if (combobox_severity.SelectedItem != null)
                {
                    workItem.Fields["Microsoft.VSTS.Common.Severity"].Value = combobox_severity.Text;
                }

                workItem.Fields["Microsoft.VSTS.Common.Priority"].Value = textbox_Priority.Text;

                if (combobox_AreaPath.SelectedItem != null)
                {
                    workItem.AreaPath = combobox_AreaPath.Text;
                }

                if (combobox_IterationPath.SelectedItem != null)
                {
                    workItem.IterationPath = combobox_IterationPath.Text;
                }

                if (combobox_state.SelectedItem != null)
                {
                    workItem.Fields["System.State"].Value = combobox_state.Text;
                }

                if (combobox_reason.SelectedItem != null)
                {
                    workItem.Fields["System.Reason"].Value = combobox_reason.Text;
                }

                if (workItem.Fields.Contains("Microsoft.VSTS.TCM.SystemInfo"))
                {
                    workItem.Fields["Microsoft.VSTS.TCM.SystemInfo"].Value = textbox_ReproStep.Text;
                }

                if (workItem.Fields.Contains("Microsoft.VSTS.TCM.ReproStreps"))
                {
                    workItem.Fields["Microsoft.VSTS.TCS.ReproStreps"].Value = textbox_SystemInfo.Text;
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
                    // workItem.Save();

                    if (this.TFSInfo == null)
                    {
                        this.TFSInfo = new TFSInfo();
                    }

                    this.TFSInfo.ID = workItem.Id.ToString();
                    this.TFSInfo.Title = workItem.Title;
                    this.TFSInfo.Description = workItem.Description;
                    this.TFSInfo.WebUrl = workItem.Uri.ToString();


                    this.SetLastValue();
                    DialogResult = DialogResult.OK;
                }
                else
                {
                    string errrorMsg = "Validation Error in field\n";
                    foreach (Field field in validationErrors)
                    {
                        errrorMsg += field.ReferenceName + "\n";
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
            this.BindComboBox(combobox_AssignTo, CoreField.AssignedTo);
            combobox_AssignTo.SelectedValue = config.TfsAssignedTo;

            // this.bindComboBox(combobox_severity,CoreField.se);
            combobox_severity.SelectedValue = config.TfsSeverity;
            if (combobox_severity.SelectedItem == null && combobox_severity.Items.Count > 0)
            {
                combobox_severity.SelectedIndex = 0;
            }

            this.BindComboBox(combobox_state, CoreField.State);
            combobox_state.SelectedValue = config.TfsState;
            if (combobox_state.SelectedItem == null && combobox_state.Items.Count > 0)
            {
                combobox_state.SelectedIndex = 0;
            }

            this.BindComboBox(combobox_reason, CoreField.Reason);
            combobox_reason.SelectedValue = config.TfsState;
            if (combobox_reason.SelectedItem == null && combobox_reason.Items.Count > 0)
            {
                combobox_reason.SelectedIndex = 0;
            }
        }

        private void combobox_workItemType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ILanguage lang = Language.GetInstance();
            BackgroundForm backgroundForm = BackgroundForm.ShowAndWait(TFSPlugin.Attributes.Name, lang.GetString(LangKey.communication_wait));

            try
            {
                this.BindScreenByItemType();
            }
            finally
            {
                backgroundForm.CloseDialog();
            }
        }
    }
}