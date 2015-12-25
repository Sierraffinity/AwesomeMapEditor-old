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
                PGMEBackend.Config.settings.RecentlyUsedFiles = new LinkedList<string>();
                PGMEBackend.Config.WriteConfig();
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
			ToolStripItem tSI;

			try
			{
				if (PGMEBackend.Config.settings.RecentlyUsedFiles == null)
				{
					this.ParentMenuItem.Enabled = false;
					return;
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
				return;
			}

			this.ParentMenuItem.DropDownItems.Clear();
            int i = 1;
			foreach (string value in PGMEBackend.Config.settings.RecentlyUsedFiles)
			{
				if (string.IsNullOrEmpty(value))
					continue;
                if (i < 10)
                    tSI = this.ParentMenuItem.DropDownItems.Add("&" + i++ + " " + value);
                else if (i == 10)
                    tSI = this.ParentMenuItem.DropDownItems.Add("1&0 " + value);
                else
                    tSI = this.ParentMenuItem.DropDownItems.Add(i++ + " " + value);
                tSI.Click += new EventHandler(this.OnRecentFileClick);
			}

			if (this.ParentMenuItem.DropDownItems.Count == 0)
			{
				this.ParentMenuItem.Enabled = false;
				return;
			}

			this.ParentMenuItem.DropDownItems.Add("-");
			tSI = this.ParentMenuItem.DropDownItems.Add(PGMEBackend.Program.rmInternalStrings.GetString("ClearList"));
			tSI.Click += new EventHandler(this._onClearRecentFiles_Click);
			this.ParentMenuItem.Enabled = true;
		}
		#endregion

		#region Public members
		public void AddRecentFile(string fileNameWithFullPath)
		{
			try
			{
                if (PGMEBackend.Config.settings.RecentlyUsedFiles == null)
                    PGMEBackend.Config.settings.RecentlyUsedFiles = new LinkedList<string>();
                PGMEBackend.Config.settings.RecentlyUsedFiles.Remove(fileNameWithFullPath);
                PGMEBackend.Config.settings.RecentlyUsedFiles.AddFirst(fileNameWithFullPath);
                while (MaxNumberOfFiles != 0 && PGMEBackend.Config.settings.RecentlyUsedFiles.Count > MaxNumberOfFiles)
                    PGMEBackend.Config.settings.RecentlyUsedFiles.RemoveLast();
                PGMEBackend.Config.WriteConfig();
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
                PGMEBackend.Config.settings.RecentlyUsedFiles.Remove(fileNameWithFullPath);
                PGMEBackend.Config.WriteConfig();
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

            this.MaxNumberOfFiles = maxNumberOfFiles;
            this.ParentMenuItem = parentMenuItem;
			this.OnRecentFileClick = onRecentFileClick;
			this.OnClearRecentFilesClick = onClearRecentFilesClick;

			this._refreshRecentFilesMenu();
		}
	}
}
