/*
 * Greenshot - a free and open source screenshot tool
 * Copyright (C) 2012  Francis Noel
 * 
 * For more information see: http://getgreenshot.org/
 * The Greenshot project is hosted on Sourceforge: http://sourceforge.net/projects/greenshot/
 * 
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 1 of the License, or
 * (at your option) any later version.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */
using System;
using System.Drawing;
using System.Windows.Forms;

using Greenshot.Plugin;
using GreenshotTFSPlugin.Forms;
using GreenshotPlugin.Controls;
using GreenshotPlugin.Core;
using Microsoft.TeamFoundation.Client;


namespace GreenshotTFSPlugin {
	/// <summary>
	/// Description of PasswordRequestForm.
	/// </summary>
	public partial class SettingsForm : Form {
		private ILanguage lang = Language.GetInstance();
		private string TFSFrob = string.Empty;

		public SettingsForm(TFSConfiguration config) {
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			InitializeTexts();
			
			combobox_uploadimageformat.Items.Clear();
			foreach(OutputFormat format in Enum.GetValues(typeof(OutputFormat))) {
				combobox_uploadimageformat.Items.Add(format.ToString());
			}

			TFSUtils.LoadHistory();

			if (config.runtimeTfsHistory.Count > 0) {
				historyButton.Enabled = true;
			} else {
				historyButton.Enabled = false;
			}
		}
				
		private void InitializeTexts() {
			this.buttonOK.Text = lang.GetString(LangKey.OK);
			
			this.buttonCancel.Text = lang.GetString(LangKey.CANCEL);
			this.Text = lang.GetString(LangKey.settings_title);
			this.label_upload_format.Text = lang.GetString(LangKey.label_upload_format);
			this.label_AfterUpload.Text = lang.GetString(LangKey.label_AfterUpload);
			this.checkboxAfterUploadOpenHistory.Text = lang.GetString(LangKey.label_AfterUploadOpenHistory);
			this.checkboxAfterUploadLinkToClipBoard.Text = lang.GetString(LangKey.label_AfterUploadLinkToClipBoard);
		}

		public bool AfterUploadOpenHistory {
			get { return checkboxAfterUploadOpenHistory.Checked; }
			set { checkboxAfterUploadOpenHistory.Checked = value; }
		}
		public bool AfterUploadLinkToClipBoard
		{
			get { return checkboxAfterUploadLinkToClipBoard.Checked; }
			set { checkboxAfterUploadLinkToClipBoard.Checked = value; }
		}

		public bool AfterUploadOpenWorkItem 
		{
			get { return checkbox_openWorkItem.Checked; }
			set { checkbox_openWorkItem.Checked = value; } 
		}

		/// <summary>
		/// Tfs server url
		/// </summary>
		public string ServerUrl
		{
			get { return textbox_tfsUrl.Text; }
			set { textbox_tfsUrl.Text = value; }
		}

		/// <summary>
		/// Tfs default project
		/// </summary>
		public string DefaultProject
		{
			get { return textbox_defaultProject.Text; }
			set { textbox_defaultProject.Text = value; }
		}

		public string UploadFormat
		{
			get { return combobox_uploadimageformat.Text; }
			set { combobox_uploadimageformat.Text = value; }
		}

		void ButtonOKClick(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.OK;
		}
		
		void ButtonCancelClick(object sender, System.EventArgs e) {
			this.DialogResult = DialogResult.Cancel;
		}
		
		void ButtonHistoryClick(object sender, EventArgs e) {
			TFSHistory.ShowHistory();
		}

		private void buttonConnect_Click(object sender, EventArgs e)
		{
			//http://blogs.msdn.com/b/team_foundation/archive/2010/04/20/using-the-teamprojectpicker-api-in-tfs-2010.aspx
			using (TeamProjectPicker tpp = new TeamProjectPicker(TeamProjectPickerMode.SingleProject, false))
			{
				DialogResult result = tpp.ShowDialog();
				if (result == DialogResult.OK)
				{
					textbox_tfsUrl.Text= tpp.SelectedTeamProjectCollection.Uri.ToString();
				   textbox_defaultProject.Text= tpp.SelectedProjects[0].Name;
					
				}
			}
		}

	}
}
