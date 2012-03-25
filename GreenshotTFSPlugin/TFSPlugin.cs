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
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using System.Windows.Forms;
using System.Xml;

using Greenshot.Plugin;
using GreenshotTFSPlugin.Forms;
using GreenshotPlugin.Controls;
using GreenshotPlugin.Core;
using IniFile;

namespace GreenshotTFSPlugin
{
    /// <summary>
    /// This is the TFS base code
    /// </summary>
    public class TFSPlugin : IGreenshotPlugin
    {
        private static readonly log4net.ILog LOG = log4net.LogManager.GetLogger(typeof(TFSPlugin));
        private static TFSConfiguration config;
        public static PluginAttribute Attributes;
        private ILanguage lang = Language.GetInstance();
        private IGreenshotHost host;
        private ComponentResourceManager resources;
      
        public TFSPlugin()
        {
        }

        public IEnumerable<IDestination> Destinations()
        {
            yield return new TFSDestination(this);
        }


        public IEnumerable<IProcessor> Processors()
        {
            yield break;
        }

        /// <summary>
        /// Implementation of the IGreenshotPlugin.Initialize
        /// </summary>
        /// <param name="host">Use the IGreenshotPluginHost interface to register events</param>
        /// <param name="captureHost">Use the ICaptureHost interface to register in the MainContextMenu</param>
        /// <param name="pluginAttribute">My own attributes</param>
        public virtual bool Initialize(IGreenshotHost pluginHost, PluginAttribute myAttributes)
        {
            this.host = (IGreenshotHost)pluginHost;
            Attributes = myAttributes;

            // Get configuration
            config = IniConfig.GetIniSection<TFSConfiguration>();
            resources = new ComponentResourceManager(typeof(TFSPlugin));

            ToolStripMenuItem itemPlugInRoot = new ToolStripMenuItem();
            itemPlugInRoot.Text = "TFS";
            itemPlugInRoot.Tag = host;
            //itemPlugInRoot.Image = (Image)resources.GetObject("TFS");

             ToolStripMenuItem itemPlugInHistory = new ToolStripMenuItem();
            itemPlugInHistory.Text = lang.GetString(LangKey.History);
            itemPlugInHistory.Tag = host;
            itemPlugInHistory.Click += new System.EventHandler(HistoryMenuClick);
            itemPlugInRoot.DropDownItems.Add(itemPlugInHistory);

            ToolStripMenuItem itemPlugInConfig = new ToolStripMenuItem();
            itemPlugInConfig.Text = lang.GetString(LangKey.Configure);
            itemPlugInConfig.Tag = host;
            itemPlugInConfig.Click += new System.EventHandler(ConfigMenuClick);
            itemPlugInRoot.DropDownItems.Add(itemPlugInConfig);

            PluginUtils.AddToContextMenu(host, itemPlugInRoot);
            
            return true;
        }

        public virtual void Shutdown()
        {
            LOG.Debug("TFS Plugin shutdown.");
            //host.OnImageEditorOpen -= new OnImageEditorOpenHandler(ImageEditorOpened);
        }

        /// <summary>
        /// Implementation of the IPlugin.Configure
        /// </summary>
        public virtual void Configure()
        {
            config.ShowConfigDialog();
        }

        /// <summary>
        /// This will be called when Greenshot is shutting down
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Closing(object sender, FormClosingEventArgs e)
        {
            LOG.Debug("Application closing, de-registering TFS Plugin!");
            Shutdown();
        }

        public void HistoryMenuClick(object sender, EventArgs eventArgs)
        {
            TFSUtils.LoadHistory();
            TFSHistory.ShowHistory();
        }

        public void ConfigMenuClick(object sender, EventArgs eventArgs)
        {
            config.ShowConfigDialog();
        }

        public bool Upload(ICaptureDetails captureDetails, Image image)
        {
            using (MemoryStream stream = new MemoryStream())
            {
               // BackgroundForm backgroundForm = BackgroundForm.ShowAndWait(Attributes.Name, lang.GetString(LangKey.communication_wait));

                host.SaveToStream(image, stream, config.UploadFormat, config.UploadJpegQuality);
                byte[] buffer = stream.GetBuffer();

                try
                {
                    string filename = Path.GetFileName(host.GetFilename(config.UploadFormat, captureDetails));
                    string contentType = "image/" + config.UploadFormat.ToString();
                    TFSInfo tfsInfo = TFSUtils.UploadToTFS(buffer, captureDetails.DateTime.ToString(), filename, contentType);
                    if (tfsInfo == null)
                    {
                        return false;
                    }
                    else
                    {
                        if (config.TfsUploadHistory == null)
                        {
                            config.TfsUploadHistory = new Dictionary<string, string>();
                        }

                        if (tfsInfo.ID != null)
                        {
                            LOG.InfoFormat("Storing TFS upload for id {0}", tfsInfo.ID);

                            config.TfsUploadHistory.Add(tfsInfo.ID, tfsInfo.ID);
                            config.runtimeTfsHistory.Add(tfsInfo.ID, tfsInfo);
                        }

                        // Make sure the configuration is save, so we don't lose the deleteHash
                        IniConfig.Save();
                        // Make sure the history is loaded, will be done only once
                        TFSUtils.LoadHistory();

                        // Show
                        if (config.AfterUploadOpenHistory)
                        {
                            TFSHistory.ShowHistory();
                        }

                        if (config.AfterUploadLinkToClipBoard)
                        {
                            Clipboard.SetText(tfsInfo.WebUrl);
                        }
                        if (config.AfterUploadOpenWorkItem)
                        {

                        }
                        return true;
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(lang.GetString(LangKey.upload_failure) + " " + e.ToString());
                }
                finally
                {
                    //backgroundForm.CloseDialog();
                }
            }
            return false;
        }

    }
}
