using System;
using System.Collections;
using System.IO;
using System.Windows.Forms;
using IniFile;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.WorkItemTracking.Client;

namespace GreenshotTFSPlugin.Forms
{
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
			this.bindScreen();
		}

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
					this.bindScreen();
				}
			}
		}

		private void button_save_Click(object sender, EventArgs e)
		{
			//http://social.technet.microsoft.com/wiki/contents/articles/3280.tfs-2010-api-create-workitems-bugs.aspx
			TfsTeamProjectCollection tfs = new TfsTeamProjectCollection(new Uri(this.textbox_tfsUrl.Text));

			WorkItemStore workItemStore = (WorkItemStore)tfs.GetService(typeof(WorkItemStore));
			
			WorkItemTypeCollection workItemTypes = workItemStore.Projects[this.textbox_defaultProject.Text].WorkItemTypes;

			// Define the Work Item type as Bug

			WorkItemType workItemType = workItemTypes[combobox_workItemType.Text];

			// Assign values to each mandatory field
			var workItem = new WorkItem(workItemType);

		  workItem.Title=textbox_title.Text;
			  workItem.Description=textbox_description.Text;
		   

			workItem.Fields["System.AssignedTo"].Value = combobox_AssignTo.Text;

			if(combobox_severity.SelectedItem !=null)
			{
				workItem.Fields["Microsoft.VSTS.Common.Severity"].Value = combobox_severity.Text;
			}

			workItem.Fields["Microsoft.VSTS.Common.Priority"].Value = textbox_Priority.Text;

			 workItem.AreaPath =combobox_AreaPath.Text ;

			workItem.IterationPath = combobox_IterationPath.Text;

			workItem.Fields["System.State"].Value = combobox_state.Text;

			//add image
			StreamWriter imgWrite=new StreamWriter(this.Filename);
			imgWrite.Write(this.ImageData);
			imgWrite.Dispose();

			workItem.Attachments.Add(new Attachment(this.Filename));

			// Link the failed test case to the Bug
			//workItem.Links.Add(new RelatedLink(testcaseID));

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
					errrorMsg += field.ReferenceName;
				}
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
		private void bindComboBox(ComboBox comboBox,CoreField coreField)
		{
			using(var tfs = new TfsTeamProjectCollection(new Uri(this.textbox_tfsUrl.Text)))
			{
				WorkItemStore workItemStore = (WorkItemStore)tfs.GetService(typeof(WorkItemStore));
				var allowedValues = workItemStore.FieldDefinitions[coreField].AllowedValues;

				comboBox.Items.Clear();
				foreach (String value in allowedValues)
				{
					comboBox.Items.Add(value);
				}
			}
		}

		private void bindScreen()
		{
			if (!string.IsNullOrEmpty(this.textbox_tfsUrl.Text))
			{
				using (var tfs = new TfsTeamProjectCollection(new Uri(this.textbox_tfsUrl.Text)))
				{
					WorkItemStore workItemStore = (WorkItemStore)tfs.GetService(typeof(WorkItemStore));
					WorkItemTypeCollection workItemTypes = workItemStore.Projects[this.textbox_defaultProject.Text].WorkItemTypes;

					//workType
					combobox_workItemType.Items.Clear();
					foreach (WorkItemType workItemType in workItemTypes)
					{
						combobox_workItemType.Items.Add(workItemType.Name);
					}

					this.bindComboBox(combobox_AreaPath, CoreField.AreaPath);
					this.bindComboBox(combobox_AssignTo, CoreField.AssignedTo);
					this.bindComboBox(combobox_IterationPath, CoreField.IterationPath);
					//this.bindComboBox(combobox_severity,CoreField.se);
					this.bindComboBox(combobox_state, CoreField.State);

				}
			}
		}
	
		public  TFSInfo TFSInfo { get; set; }
		public  string ContentType { get; set; }
		public  string Filename { get; set; }
		public  string Title 
		{ 
			get{return textbox_title.Text;} 
			set{textbox_title.Text=value;} 
		}

		public  byte[] ImageData { get; set; }

	}
}
