using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using static PGMEBackend.Config;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

namespace PGMEWindowsUI
{
    public partial class SettingsDialog : Form
    {
        MainWindow parent;
        string scriptEditor = settings.ScriptEditor;

        public SettingsDialog(MainWindow par)
        {
            parent = par;
            InitializeComponent();
            trackBarPermTrans.Value = settings.PermissionTranslucency;
            LoadScriptEditorStuff(scriptEditor);
            SetCheckedLanguage(settings.Language);
            checkBoxBackUpROM.Checked = settings.CreateBackups;
            switch (settings.HexPrefix)
            {
                case "0x":
                    radioButton0x.Checked = true;
                    break;
                case "$":
                    radioButtonDollar.Checked = true;
                    break;
                case "&H":
                    radioButtonAndH.Checked = true;
                    break;
                default:
                    radioButtonOtherPrefix.Checked = true;
                    textBoxOtherPrefix.Text = settings.HexPrefix;
                    break;
            }
        }
        
        private void trackBarPermTrans_ValueChanged(object sender, EventArgs e)
        {
            labelPermTrans.Text = trackBarPermTrans.Value + "%";
        }

        private void trackBarPermTrans_Scroll(object sender, EventArgs e)
        {
            parent.PreviewPermTranslucency(trackBarPermTrans.Value);
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            settings.PermissionTranslucency = trackBarPermTrans.Value;
            settings.ScriptEditor = scriptEditor;
            settings.CreateBackups = checkBoxBackUpROM.Checked;
            SetLanguage();
            if (radioButton0x.Checked == true)
                settings.HexPrefix = "0x";
            else if (radioButtonDollar.Checked == true)
                settings.HexPrefix = "$";
            else if (radioButtonAndH.Checked == true)
                settings.HexPrefix = "&H";
            else if(!string.IsNullOrEmpty(textBoxOtherPrefix.Text))
                settings.HexPrefix = textBoxOtherPrefix.Text;
            WriteConfig();
            DialogResult = DialogResult.OK;
        }

        private void btnChooseScriptEditor_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Title = "Choose Script Editor";
            open.Filter = "Executables (*.exe)|*.exe|All Files (*.*)|*.*";
            open.Multiselect = false;
            open.CheckFileExists = true;
            open.CheckPathExists = true;
            if(open.ShowDialog() == DialogResult.OK)
                LoadScriptEditorStuff(open.FileName);
        }

        private void LoadScriptEditorStuff(string editor)
        {
            if (File.Exists(editor))
            {
                Icon iconForFile = Icon.ExtractAssociatedIcon(editor);
                pictureBoxScriptEditorIcon.Image = Bitmap.FromHicon(new Icon(iconForFile, new Size(48, 48)).Handle);
                var info = System.Diagnostics.FileVersionInfo.GetVersionInfo(editor);
                labelScriptEditor.Text = editor;
                labelScriptEditorName.Text = info.ProductName;
                scriptEditor = editor;
            }
        }

        private void radioButtonPrefix_CheckedChanged(object sender, EventArgs e)
        {
            textBoxOtherPrefix.Enabled = radioButtonOtherPrefix.Checked;
        }

        private void languageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetCheckedLanguage((sender as ToolStripMenuItem).Tag as string);
        }

        public void SetLanguage()
        {
            foreach (ToolStripMenuItem item in cmsLanguages.Items)
            {
                if (item.Checked && (item.Tag as string) != settings.Language)
                {
                    settings.Language = (item.Tag as string);
                    MessageBox.Show(PGMEBackend.Program.rmInternalStrings.GetString("RestartToSaveLanguage", new CultureInfo(settings.Language)), PGMEBackend.Program.rmInternalStrings.GetString("RestartToSaveLanguageTitle", new CultureInfo(settings.Language)), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                }
            }
        }

        private void SetCheckedLanguage(string lang)
        {
            foreach (ToolStripMenuItem item in cmsLanguages.Items)
            {
                if (item.Tag.Equals(lang))
                {
                    buttonLanguage.Text = item.Text;
                    item.Checked = true;
                }
                else
                    item.Checked = false;
            }
        }
    }
}
