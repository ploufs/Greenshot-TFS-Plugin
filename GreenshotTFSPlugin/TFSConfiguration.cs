/*
 * Greenshot - a free and open source screenshot tool
 * Copyright (C) 2012  Francis  Noel
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
using System.Collections.Generic;
using System.Windows.Forms;

using GreenshotPlugin.Controls;
using GreenshotPlugin.Core;
using IniFile;

namespace GreenshotTFSPlugin
{
    /// <summary>
    /// Description of ImgurConfiguration.
    /// </summary>
    [IniSection("TFS", Description = "Greenshot TFS Plugin configuration")]
    public class TFSConfiguration : IniSection
    {
        [IniProperty("UploadFormat", Description = "What file type to use for uploading", DefaultValue = "png")]
        public OutputFormat UploadFormat;

        [IniProperty("UploadJpegQuality", Description = "JPEG file save quality in %.", DefaultValue = "80")]
        public int UploadJpegQuality;

        [IniProperty("AfterUploadOpenWorkItem", Description = "After upload open work item.", DefaultValue = "false")]
        public bool AfterUploadOpenWorkItem;

        [IniProperty("AfterUploadOpenHistory", Description = "After upload open history.", DefaultValue = "false")]
        public bool AfterUploadOpenHistory;

        [IniProperty("AfterUploadLinkToClipBoard", Description = "After upload send TFS link to clipboard.", DefaultValue = "true")]
        public bool AfterUploadLinkToClipBoard;

       
        [IniProperty("TfsServerUrl", Description = "Tfs Server Url", DefaultValue = "")]
        public string TfsServerUrl;

        [IniProperty("TfsDefaultProject", Description = "Tfs default project", DefaultValue = "")]
        public string TfsDefaultProject;

        [IniProperty("TfsWorkItemType", Description = "Tfs last work item type", DefaultValue = "Bug")]
        public string TfsWorkItemType;

        [IniProperty("TfsState", Description = "Tfs last state", DefaultValue = "Open")]
        public string TfsState;

        [IniProperty("TfsAssignedTo", Description = "Tfs last AssignedTo", DefaultValue = "")]
        public string TfsAssignedTo;

        [IniProperty("TfsAreaPath", Description = "Tfs last area path", DefaultValue = "")]
        public string TfsAreaPath;

        [IniProperty("TfsIterationPath", Description = "Tfs last iteration path", DefaultValue = "")]
        public string TfsIterationPath;

        [IniProperty("TfsSeverity", Description = "Tfs last severity", DefaultValue = "")]
        public string TfsSeverity;

        [IniProperty("TfsPriority", Description = "Tfs last priority", DefaultValue = "1")]
        public int TfsPriority;

        [IniProperty("TfsUploadHistory", Description = "TFS upload history (TFSUploadHistory.hash=deleteHash)")]
        public Dictionary<string, string> TfsUploadHistory;



        // Not stored, only run-time!
        public Dictionary<string, TFSInfo> runtimeTfsHistory = new Dictionary<string, TFSInfo>();

        /// <summary>
        /// Supply values we can't put as defaults
        /// </summary>
        /// <param name="property">The property to return a default for</param>
        /// <returns>object with the default value for the supplied property</returns>
        public override object GetDefault(string property)
        {
            switch (property)
            {
                case "TfsUploadHistory":
                    return new Dictionary<string, string>();
            }
            return null;
        }
        /// <summary>
        /// A form for token
        /// </summary>
        /// <returns>bool true if OK was pressed, false if cancel</returns>
        public bool ShowConfigDialog()
        {
            SettingsForm settingsForm;
            ILanguage lang = Language.GetInstance();

            BackgroundForm backgroundForm = BackgroundForm.ShowAndWait(TFSPlugin.Attributes.Name, lang.GetString(LangKey.communication_wait));
            try
            {
                settingsForm = new SettingsForm(this);
            }
            finally
            {
                backgroundForm.CloseDialog();
            }
            settingsForm.UploadFormat = this.UploadFormat.ToString();
            settingsForm.AfterUploadOpenHistory = this.AfterUploadOpenHistory;
            settingsForm.AfterUploadLinkToClipBoard = this.AfterUploadLinkToClipBoard;
            settingsForm.AfterUploadOpenWorkItem = this.AfterUploadOpenWorkItem;
            settingsForm.ServerUrl = this.TfsServerUrl;
            settingsForm.DefaultProject = this.TfsDefaultProject;
            DialogResult result = settingsForm.ShowDialog();
            if (result == DialogResult.OK)
            {

                this.UploadFormat = (OutputFormat)Enum.Parse(typeof(OutputFormat), settingsForm.UploadFormat.ToLower());

                this.AfterUploadOpenHistory = settingsForm.AfterUploadOpenHistory;
                this.AfterUploadLinkToClipBoard = settingsForm.AfterUploadLinkToClipBoard;
                this.AfterUploadOpenWorkItem = settingsForm.AfterUploadOpenWorkItem;

                this.TfsServerUrl = settingsForm.ServerUrl;
                this.TfsDefaultProject = settingsForm.DefaultProject;

                IniConfig.Save();
                return true;
            }
            return false;
        }

    }
}
