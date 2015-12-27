using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;


namespace PGMEWindowsUI
{
	public class MRUManager
	{
		#region Private members
		private ToolStripMenuItem ParentMenuItem;
		private Action<object, EventArgs> OnRecentFileClick;
		private Action<object, EventArgs> OnClearRecentFilesClick;
        private int MaxNumberOfFiles;

		private void _onClearRecentFiles_Click(object obj, EventArgs evt)
		{
			try
            {
                Config.settings.RecentlyUsedFiles = new LinkedList<string>();
                this.ParentMenuItem.DropDownItems.Clear();
				this.ParentMenuItem.Enabled = false;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
			}
			if (OnClearRecentFilesClick != null)
				this.OnClearRecentFilesClick(obj, evt);
		}
		
		private void _refreshRecentFilesMenu()
		{
			string s;
			ToolStripItem tSI;

			try
			{
				if (Config.settings.RecentlyUsedFiles == null)
				{
					this.ParentMenuItem.Enabled = false;
					return;
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("Cannot open recent files registry key:\n" + ex.ToString());
				return;
			}

			this.ParentMenuItem.DropDownItems.Clear();
			foreach (string value in Config.settings.RecentlyUsedFiles)
			{
				if (string.IsNullOrEmpty(value))
					continue;
				tSI = this.ParentMenuItem.DropDownItems.Add(value);
				tSI.Click += new EventHandler(this.OnRecentFileClick);
			}

			if (this.ParentMenuItem.DropDownItems.Count == 0)
			{
				this.ParentMenuItem.Enabled = false;
				return;
			}

			this.ParentMenuItem.DropDownItems.Add("-");
			tSI = this.ParentMenuItem.DropDownItems.Add("Clear list");
			tSI.Click += new EventHandler(this._onClearRecentFiles_Click);
			this.ParentMenuItem.Enabled = true;
		}
		#endregion

		#region Public members
		public void AddRecentFile(string fileNameWithFullPath)
		{
			try
			{
                if (Config.settings.RecentlyUsedFiles == null)
                    Config.settings.RecentlyUsedFiles = new LinkedList<string>();
                Config.settings.RecentlyUsedFiles.Remove(fileNameWithFullPath);
                Config.settings.RecentlyUsedFiles.AddFirst(fileNameWithFullPath);
                while (MaxNumberOfFiles != 0 && Config.settings.RecentlyUsedFiles.Count > MaxNumberOfFiles)
                    Config.settings.RecentlyUsedFiles.RemoveLast();
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
			}
			this._refreshRecentFilesMenu();
		}

		public void RemoveRecentFile(string fileNameWithFullPath)
		{
			try
			{
                Config.settings.RecentlyUsedFiles.Remove(fileNameWithFullPath);
            }
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
			}
			this._refreshRecentFilesMenu();
		}
		#endregion

		/// <exception cref="ArgumentException">If anything is null.</exception>
		public MRUManager(ToolStripMenuItem parentMenuItem, Action<object, EventArgs> onRecentFileClick, Action<object, EventArgs> onClearRecentFilesClick = null, int maxNumberOfFiles = 0)
		{
			if(parentMenuItem == null || onRecentFileClick == null)
				throw new ArgumentException("Bad argument.");

			this.ParentMenuItem = parentMenuItem;
			this.OnRecentFileClick = onRecentFileClick;
			this.OnClearRecentFilesClick = onClearRecentFilesClick;
            this.MaxNumberOfFiles = maxNumberOfFiles;

			this._refreshRecentFilesMenu();
		}
	}
}
