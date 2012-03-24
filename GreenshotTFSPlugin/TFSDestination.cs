
using System.ComponentModel;
using System.Drawing;
using Greenshot.Plugin;
using GreenshotPlugin.Core;
using IniFile;

namespace GreenshotTFSPlugin
{
	public class TFSDestination :AbstractDestination
	{
		private static log4net.ILog LOG = log4net.LogManager.GetLogger(typeof(TFSDestination));
		private static TFSConfiguration config = IniConfig.GetIniSection<TFSConfiguration>();
		private ILanguage lang = Language.GetInstance();

		private TFSPlugin plugin = null;
		public TFSDestination(TFSPlugin plugin) {
			this.plugin = plugin;
		}
		
		public override string Designation {
			get {
				return "TFS";
			}
		}

		public override string Description {
			get {
				return lang.GetString(LangKey.upload_menu_item);
			}
		}

		public override Image DisplayIcon {
			get {
				ComponentResourceManager resources = new ComponentResourceManager(typeof(TFSPlugin));
				return (Image)resources.GetObject("Tfs");
			}
		}

		public override bool ExportCapture(ISurface surface, ICaptureDetails captureDetails) {
			using (Image image = surface.GetImageForExport()) {
				bool uploaded = plugin.Upload(captureDetails, image);
				if (uploaded) {
					surface.SendMessageEvent(this, SurfaceMessageTyp.Info, "Exported to TFS");
					surface.Modified = false;
				}
				return uploaded;
			}
		}
	}
}
