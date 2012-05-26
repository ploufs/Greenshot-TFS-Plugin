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
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Net;
using GreenshotPlugin.Core;
using Greenshot.IniFile;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.WorkItemTracking.Client;
using GreenshotTFSPlugin.Forms;
using System.Windows.Forms;
using GreenshotPlugin.Controls;

namespace GreenshotTFSPlugin {
	/// <summary>
	/// Description of ImgurUtils.
	/// </summary>
	public class TFSUtils {
		private static readonly log4net.ILog LOG = log4net.LogManager.GetLogger(typeof(TFSUtils));
		public static string TFS_APPLICATION_NAME = "390bea54b8ef045716dd34680d6e21ba";
		private static TFSConfiguration config = IniConfig.GetIniSection<TFSConfiguration>();

		private TFSUtils() {
		}

		public static void LoadHistory() {
			if (config.runtimeTfsHistory == null) {
				return;
			}
			if (config.TfsUploadHistory == null)
			{
				return;
			}

			if (config.runtimeTfsHistory.Count == config.TfsUploadHistory.Count) {
				return;
			}
			// Load the TFS history
			List<string> hashes = new List<string>();
			foreach (string hash in config.TfsUploadHistory.Keys)
			{
				hashes.Add(hash);
			}
			
			bool saveNeeded = false;

			foreach(string hash in hashes) {
				if (config.runtimeTfsHistory.ContainsKey(hash)) {
					// Already loaded
					continue;
				}
				try {
					TFSInfo tfsInfo = TFSUtils.RetrieveTFSInfo(hash);
					if (tfsInfo != null) {
						//TFSUtils.RetrieveTFSThumbnail(imgurInfo);
						config.runtimeTfsHistory.Add(hash, tfsInfo);
					} else {
						LOG.DebugFormat("Deleting not found TFS {0} from config.", hash);
						config.TfsUploadHistory.Remove(hash);
						saveNeeded = true;
					}
				} catch (Exception e) {
					LOG.Error("Problem loading TFS history for hash " + hash, e);
				}
			}
			if (saveNeeded) {
				// Save needed changes
				IniConfig.Save();
			}
		}

		/// <summary>
		/// Do the actual upload to TFS
		/// </summary>
		/// <param name="imageData">byte[] with image data</param>
		/// <returns>TFSResponse</returns>
		public static TFSInfo UploadToTFS(byte[] imageData, string title, string filename, string contentType)
		{
			BackgroundForm backgroundForm = BackgroundForm.ShowAndWait(TFSPlugin.Attributes.Name, Language.GetString("tfs",LangKey.communication_wait));
			AddForm addForm = null;
			try
			{
				addForm = new AddForm();
			}
			finally
			{
				backgroundForm.CloseDialog();
			}
			addForm.ImageData = imageData;
			addForm.Title = title;
			addForm.Filename = filename;
			addForm.ContentType = contentType;
			if (addForm.ShowDialog() == DialogResult.OK)
			{
				return addForm.TFSInfo;
			}
			else
			{
				return null;
			}

		}

		public static TFSInfo RetrieveTFSInfo(string id)
		{
			//return RetrieveTFSInfo(RetrieveTFSEntry(id));
			return null;
		}

		
		public static void DeleteTfsWorkItem(TFSInfo tfsInfo)
		{
			
		}
	}
}
