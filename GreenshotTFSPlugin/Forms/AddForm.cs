namespace GreenshotTFSPlugin.Forms
{
    using System;
    using System.Collections;
    using System.IO;
    using System.Windows.Forms;
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
                    this.BindScreen();
                }
            }
        }

        private void button_save_Click(object sender, EventArgs e)
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

            // add image
            StreamWriter imgWrite = new StreamWriter(this.Filename);
            imgWrite.Write(this.ImageData);
            imgWrite.Dispose();

            workItem.Attachments.Add(new Attachment(this.Filename));

            // Link the failed test case to the Bug
            // workItem.Links.Add(new RelatedLink(testcaseID));

            // Check for validation errors before saving the Bug
            ArrayList validationErrors = workItem.Validate();

            if (validationErrors.Count == 0)
            {
                workItem.Save();
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

            if (this.TFSInfo == null)
            {
                this.TFSInfo = new TFSInfo();
            }

            this.TFSInfo.ID = workItem.Id.ToString();
            this.TFSInfo.Title = workItem.Title;
            this.TFSInfo.Description = workItem.Description;
            this.TFSInfo.WebUrl = workItem.Uri.ToString();
            DialogResult = DialogResult.OK;
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
                var allowedValues = workItemStore.FieldDefinitions[coreField].AllowedValues;

                comboBox.Items.Clear();
                foreach (string value in allowedValues)
                {
                    comboBox.Items.Add(value);
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

            if (combobox_workItemType != null)
            {
                config.TfsWorkItemType = combobox_workItemType.Text;
            }
        }

        private void BindScreen()
        {
            if (!string.IsNullOrEmpty(this.textbox_tfsUrl.Text))
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

                    // area
                    combobox_AreaPath.Items.Clear();
                    foreach (Node area in project.AreaRootNodes)
                    {
                        combobox_AreaPath.Items.Add(area.Name);
                        foreach (Node item in area.ChildNodes)
                        {
                            combobox_AreaPath.Items.Add(item.Name);
                        }
                    }

                    combobox_AreaPath.SelectedValue = config.TfsAreaPath;

                    // iteration
                    combobox_IterationPath.Items.Clear();
                    foreach (Node node in project.IterationRootNodes)
                    {
                        combobox_IterationPath.Items.Add(node.Name);
                        foreach (Node item in node.ChildNodes)
                        {
                            combobox_IterationPath.Items.Add(item.Name);
                        }
                    }

                    combobox_IterationPath.SelectedValue = config.TfsIterationPath;

                    this.BindComboBox(combobox_AssignTo, CoreField.AssignedTo);
                    combobox_AssignTo.SelectedValue = config.TfsAssignedTo;

                    // this.bindComboBox(combobox_severity,CoreField.se);
                    combobox_severity.SelectedValue = config.TfsSeverity;

                    this.BindComboBox(combobox_state, CoreField.State);
                    combobox_state.SelectedValue = config.TfsState;

                    textbox_Priority.Value = config.TfsPriority;
                }
            }
        }
    }
}