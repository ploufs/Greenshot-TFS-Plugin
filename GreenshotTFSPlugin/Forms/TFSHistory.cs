﻿/*
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
using System.Text;
using System.Windows.Forms;

using GreenshotPlugin.Controls;
using GreenshotPlugin.Core;
using IniFile;

namespace GreenshotTFSPlugin.Forms {
	/// <summary>
	/// Description of ImgurHistory.
	/// </summary>
	public partial class TFSHistory : Form {
		private static readonly log4net.ILog LOG = log4net.LogManager.GetLogger(typeof(TFSHistory));
		private ListViewColumnSorter columnSorter;
		private static TFSConfiguration config = IniConfig.GetIniSection<TFSConfiguration>();
		private ILanguage lang = Language.GetInstance();
		private static TFSHistory instance;
		
		public static void ShowHistory() {
			if (instance == null) {
				instance = new TFSHistory();
			}
			instance.Show();
			instance.redraw();
		}
		
		private TFSHistory() {
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();

			// Init sorting
			columnSorter = new ListViewColumnSorter();
			this.listview_Picasa_uploads.ListViewItemSorter = columnSorter;
			columnSorter.SortColumn = 2; //sort by date
			columnSorter.Order = SortOrder.Descending;
			redraw();
			if (listview_Picasa_uploads.Items.Count > 0) {
				listview_Picasa_uploads.Items[0].Selected = true;
			}
		}

		private void redraw() {
			listview_Picasa_uploads.BeginUpdate();
			listview_Picasa_uploads.Items.Clear();
			listview_Picasa_uploads.Columns.Clear();
			string[] columns = { "Id", "Title", "Date"};
			foreach (string column in columns) {
				listview_Picasa_uploads.Columns.Add(column);
			}
			foreach (TFSInfo imgurInfo in config.runtimePicasaHistory.Values) {
				ListViewItem item = new ListViewItem(imgurInfo.ID);
				item.Tag = imgurInfo;
				item.SubItems.Add(imgurInfo.Title);
				item.SubItems.Add(imgurInfo.Timestamp.ToString());
				listview_Picasa_uploads.Items.Add(item);
			}
			for (int i = 0; i < columns.Length; i++) {
				listview_Picasa_uploads.AutoResizeColumn(i, ColumnHeaderAutoResizeStyle.ColumnContent);
			}
	
			listview_Picasa_uploads.EndUpdate();
			listview_Picasa_uploads.Refresh();
			deleteButton.Enabled = false;
			openButton.Enabled = false;
			clipboardButton.Enabled = false;
		}

		private void Listview_Picasa_uploadsSelectedIndexChanged(object sender, EventArgs e) {
			pictureBox_Picasa.Image = null;
			if (listview_Picasa_uploads.SelectedItems != null && listview_Picasa_uploads.SelectedItems.Count > 0) {
				deleteButton.Enabled = true;
				openButton.Enabled = true;
				clipboardButton.Enabled = true;
				if (listview_Picasa_uploads.SelectedItems.Count == 1) {
					TFSInfo imgurInfo = (TFSInfo)listview_Picasa_uploads.SelectedItems[0].Tag;
					pictureBox_Picasa.Image = imgurInfo.Image;
				}
			} else {
				pictureBox_Picasa.Image = null;
				deleteButton.Enabled = false;
				openButton.Enabled = false;
				clipboardButton.Enabled = false;
			}
		}

		private void DeleteButtonClick(object sender, EventArgs e) {
			this.deletePicture();
		}

		private void ClipboardButtonClick(object sender, EventArgs e) {
			this.clipboardUrl(config.PictureDisplaySize);
		}

		private void FinishedButtonClick(object sender, EventArgs e) {
			this.Close();
		}

		private void OpenButtonClick(object sender, EventArgs e) {
			this.openPicture(config.PictureDisplaySize);
		}
		
		private void listview_imgur_uploads_ColumnClick(object sender, ColumnClickEventArgs e) {
			// Determine if clicked column is already the column that is being sorted.
			if (e.Column == columnSorter.SortColumn) {
				// Reverse the current sort direction for this column.
				if (columnSorter.Order == SortOrder.Ascending) {
					columnSorter.Order = SortOrder.Descending;
				} else {
					columnSorter.Order = SortOrder.Ascending;
				}
			} else {
				// Set the column number that is to be sorted; default to ascending.
				columnSorter.SortColumn = e.Column;
				columnSorter.Order = SortOrder.Ascending;
			}

			// Perform the sort with these new sort options.
			this.listview_Picasa_uploads.Sort();
		}

		
		void ImgurHistoryFormClosing(object sender, FormClosingEventArgs e)
		{
			instance = null;
		}

		private void ToolStripMenuItem_Delete_Click(object sender, EventArgs e)
		{
			this.deletePicture();
		}

		private void ToolStripMenuItem_Open_webUrl_Click(object sender, EventArgs e)
		{
			this.openPicture(PictureDisplaySize.WebUrl);
		}

		private void ToolStripMenuItem_Open_SquareThumbnailUrl_Click(object sender, EventArgs e)
		{
			this.openPicture(PictureDisplaySize.SquareThumbnailUrl);
		}

		private void ToolStripMenuItem_Open_OriginalUrl_Click(object sender, EventArgs e)
		{
			this.openPicture(PictureDisplaySize.OriginalUrl);
		}

		private void ToolStripMenuItem_Copy_WebUrl_Click(object sender, EventArgs e)
		{
			this.clipboardUrl(PictureDisplaySize.WebUrl);
		}

		private void ToolStripMenuItem_Copy_SquareThumbnailUrl_Click(object sender, EventArgs e)
		{
			this.clipboardUrl(PictureDisplaySize.SquareThumbnailUrl);
		}

		private void ToolStripMenuItem_Copy_OriginalUrl_Click(object sender, EventArgs e)
		{
			this.clipboardUrl(PictureDisplaySize.OriginalUrl);
		}

			

		private void deletePicture()
		{
			if (listview_Picasa_uploads.SelectedItems != null && listview_Picasa_uploads.SelectedItems.Count > 0)
			{
				for (int i = 0; i < listview_Picasa_uploads.SelectedItems.Count; i++)
				{
					TFSInfo imgurInfo = (TFSInfo)listview_Picasa_uploads.SelectedItems[i].Tag;
					DialogResult result = MessageBox.Show(lang.GetFormattedString(LangKey.delete_question, imgurInfo.Title), lang.GetFormattedString(LangKey.delete_title, imgurInfo.ID), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
					if (result == DialogResult.Yes)
					{
						BackgroundForm backgroundForm = BackgroundForm.ShowAndWait(TFSPlugin.Attributes.Name, lang.GetString(LangKey.communication_wait));
						try
						{
							TFSUtils.DeletePicasaImage(imgurInfo);
						}
						catch (Exception ex)
						{
							LOG.Warn("Problem communicating with Picasa: ", ex);
						}
						finally
						{
							backgroundForm.CloseDialog();
						}

						imgurInfo.Dispose();
					}
				}
			}
			redraw();
		}

		private void openPicture(PictureDisplaySize pictureDisplaySize)
		{
			if (listview_Picasa_uploads.SelectedItems != null && listview_Picasa_uploads.SelectedItems.Count > 0)
			{
				for (int i = 0; i < listview_Picasa_uploads.SelectedItems.Count; i++)
				{
					TFSInfo imgurInfo = (TFSInfo)listview_Picasa_uploads.SelectedItems[i].Tag;

					System.Diagnostics.Process.Start(imgurInfo.LinkUrl(pictureDisplaySize));
				}
			}
		}

		private void clipboardUrl(PictureDisplaySize pictureDisplaySize)
		{
			StringBuilder links = new StringBuilder();
			if (listview_Picasa_uploads.SelectedItems != null && listview_Picasa_uploads.SelectedItems.Count > 0)
			{
				for (int i = 0; i < listview_Picasa_uploads.SelectedItems.Count; i++)
				{
					TFSInfo PicasaInfo = (TFSInfo)listview_Picasa_uploads.SelectedItems[i].Tag;

					links.AppendLine(PicasaInfo.LinkUrl(pictureDisplaySize));
				}
			}
			try
			{
				Clipboard.SetText(links.ToString());
			}
			catch (Exception ex)
			{
				LOG.Error("Can't write to clipboard: ", ex);
			}
		}

		private void pictureBox_Picasa_Click(object sender, EventArgs e)
		{
			this.openPicture(config.PictureDisplaySize);
		}

	}
}
