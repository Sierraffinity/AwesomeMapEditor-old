using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Soap;
using System.IO;
using System.Xml.Schema;
using System.Xml;

namespace SappyXMLEditor
{
    public partial class Form1 : Form
    {
        ISappySettingFile fileToEdit;

        List<NumericUpDown> songNumbers;
        List<TextBox> songNames;
        List<Button> songDeletes;

        int dy;

        IROM currentRom;
        IPlaylist currentPlaylist;
        private IROM CurrentRom
        {
            get { return currentRom; }
        }
        private IPlaylist CurrentPlaylist
        {
            get { return currentPlaylist; }
        }

        public Form1()
        {
            InitializeComponent();
            fileToEdit = new SappyXmlSettingFile();

            songDeletes = new List<Button>();
            songDeletes.Add(buttonDeleteSong1);
            songDeletes.Add(buttonDeleteSong2);
            buttonDeleteSong1.Click += new EventHandler(songButton_press);
            buttonDeleteSong2.Click += new EventHandler(songButton_press);
            dy = buttonDeleteSong2.Location.Y - buttonDeleteSong1.Location.Y;

            songNames = new List<TextBox>();
            songNames.Add(textBoxSongName1);
            songNames.Add(textBoxSongName2);
            textBoxSongName1.TextChanged += new EventHandler(songTextBox_textChanged);
            textBoxSongName2.TextChanged += new EventHandler(songTextBox_textChanged);

            songNumbers = new List<NumericUpDown>();
            songNumbers.Add(numericUpDownSongNumber1);
            songNumbers.Add(numericUpDownSongNumber2);
            numericUpDownSongNumber1.ValueChanged += new EventHandler(songUpDown_valueChanged);
            numericUpDownSongNumber2.ValueChanged += new EventHandler(songUpDown_valueChanged);

            comboBoxRom.TextChanged += new EventHandler(comboBoxRom_TextChanged);
            comboBoxPlaylist.TextChanged += new EventHandler(comboBoxPlaylist_TextChanged);
        }

        private void UpdateGUI()
        {
            IROM[] roms = fileToEdit.GetROMs();
            comboBoxRom.Items.Clear();
            comboBoxRom.Items.AddRange(roms);
            currentRom = roms[0];
            comboBoxRom.SelectedIndex = 0;
            //SetRomInfo(CurrentRom);
        }

        private void SetSongInfo(IPlaylist playlist)
        {
            ISong[] songs = playlist.GetSongs();

            panelPlaylist.SuspendLayout();

            //disable unused controls
            int beg = 0;
            if (songs != null)
            {
                beg = songs.Length;
            }
            for (int i = beg; i < songNumbers.Count; i++)
            {
                songNumbers[i].Visible = false;
                songNumbers[i].Enabled = false;
                songNames[i].Visible = false;
                songNames[i].Enabled = false;
                songDeletes[i].Visible = false;
                songDeletes[i].Enabled = false;
            }

            //Add more controls if necessary
            while (songNumbers.Count < beg)
            {
                AddNewSongControl();
            }

            //Set control values
            for (int i = 0; i < beg; i++)
            {
                songNumbers[i].Visible = true;
                songNumbers[i].Enabled = true;
                songNames[i].Visible = true;
                songNames[i].Enabled = true;
                songDeletes[i].Visible = true;
                songDeletes[i].Enabled = true;
                songNumbers[i].Value = songs[i].Number;
                songNames[i].Text = songs[i].Name;
            }
            panelPlaylist.ResumeLayout(true);
        }

        private void AddNewSongControl()
        {
            NumericUpDown lastNUD = songNumbers.Last();
            NumericUpDown nupToAdd = new NumericUpDown();
            Point location = lastNUD.Location;
            location.Y += dy;
            nupToAdd.Location = location;
            nupToAdd.Size = lastNUD.Size;
            nupToAdd.Maximum = lastNUD.Maximum;
            nupToAdd.Anchor = lastNUD.Anchor;
            nupToAdd.ValueChanged += new EventHandler(songUpDown_valueChanged);
            nupToAdd.Name = "numericUpDownSongNumber" + (songNumbers.Count + 1);
            songNumbers.Add(nupToAdd);
            panelPlaylist.Controls.Add(nupToAdd);

            TextBox lastBox = songNames.Last();
            TextBox tbToAdd = new TextBox();
            location = lastBox.Location;
            location.Y += dy;
            tbToAdd.Location = location;
            tbToAdd.Size = lastBox.Size;
            tbToAdd.Anchor = lastBox.Anchor;
            tbToAdd.TextChanged += new EventHandler(songTextBox_textChanged);
            tbToAdd.Name = "textBoxSongName" + (songNames.Count + 1);
            songNames.Add(tbToAdd);
            panelPlaylist.Controls.Add(tbToAdd);

            Button lastButt = songDeletes.Last();
            Button btToAdd = new Button();
            location = lastButt.Location;
            location.Y += dy;
            btToAdd.Location = location;
            btToAdd.Size = lastButt.Size;
            btToAdd.Text = lastButt.Text;
            btToAdd.Anchor = lastButt.Anchor;
            btToAdd.Click += new EventHandler(songButton_press);
            btToAdd.Name = "buttonDeleteSong" + (songDeletes.Count + 1);
            songDeletes.Add(btToAdd);
            panelPlaylist.Controls.Add(btToAdd);
        }

        private void SetRomInfo(IROM rom)
        {            
            textBoxCode.Text = rom.Code;
            numericUpDownSongList.Value = rom.SongListOffset;
            textBoxCreator.Text = rom.Creator;
            textBoxScreenShot.Text = rom.Screenshot;
            textBoxTagger.Text = rom.Tagger;

            IPlaylist[] playlists = rom.GetPlaylists();
            comboBoxPlaylist.Items.Clear();
            comboBoxPlaylist.Items.AddRange(playlists);
            currentPlaylist = playlists[0];
            comboBoxPlaylist.SelectedIndex = 0;
            //IPlaylist playlist = CurrentPlaylist;
            //SetSongInfo(playlist);
        }


        private void songButton_press(object sender, EventArgs e)
        {
            int index = this.songDeletes.IndexOf(sender as Button);
            CurrentPlaylist.DeleteSong(index);
            SetSongInfo(CurrentPlaylist);
            //Delete the ROM at index
        }

        private void songUpDown_valueChanged(object sender, EventArgs e)
        {
            NumericUpDown item = sender as NumericUpDown;
            int index = this.songNumbers.IndexOf(item);
            ISong songToEdit = CurrentPlaylist.GetSong(index);
            songToEdit.Number = (int)item.Value;
        }

        private void songTextBox_textChanged(object sender, EventArgs e)
        {
            TextBox item = sender as TextBox;
            int index = this.songNames.IndexOf(item);
            ISong songToEdit = CurrentPlaylist.GetSong(index);
            songToEdit.Name = item.Text;
        }


        private bool Exit()
        {
            bool exit = true;
            if (fileToEdit.FilePath != null && fileToEdit.HasBeenEdited)
            {
                switch (MessageBox.Show("Want to save your changes before exiting?", "Save before exiting?", MessageBoxButtons.YesNoCancel))
                {
                    case DialogResult.Cancel:
                        exit = false;
                        break;
                    case DialogResult.No:
                        break;
                    case DialogResult.Yes:
                        fileToEdit.Save();
                        break;
                    default:
                        break;
                }
            }
            return exit;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (!Exit())
            {
                e.Cancel = true;
            }
            base.OnClosing(e);
        }

        #region Tool strip

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fileToEdit.Save();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                fileToEdit.Load(Path.GetFullPath(openFileDialog1.FileName));
                UpdateGUI();
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.InitialDirectory = Path.GetDirectoryName(fileToEdit.FilePath);
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                fileToEdit.SaveAs(saveFileDialog1.FileName);
            }
        }

        #endregion
        
        private void comboBoxRom_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxRom.SelectedItem != null)
            {
                currentRom = comboBoxRom.SelectedItem as IROM;
                SetRomInfo(currentRom);
            }
        }

        private void comboBoxRom_TextChanged(object sender, EventArgs e)
        {
            CurrentRom.Name = ((ComboBox)sender).Text;
        }
        
        private void textBoxCode_TextChanged(object sender, EventArgs e)
        {
            CurrentRom.Code = ((TextBox)sender).Text;
        }

        private void numericUpDownSongList_ValueChanged(object sender, EventArgs e)
        {
            CurrentRom.SongListOffset = (int)((NumericUpDown)sender).Value;
        }

        private void textBoxCreator_TextChanged(object sender, EventArgs e)
        {
            CurrentRom.Creator = (((TextBox)sender).Text);
        }

        private void textBoxScreenShot_TextChanged(object sender, EventArgs e)
        {
            CurrentRom.Screenshot = ((TextBox)sender).Text;
        }

        private void textBoxTagger_TextChanged(object sender, EventArgs e)
        {
            CurrentRom.Tagger = ((TextBox)sender).Text;
        }

        private void comboBoxPlaylist_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxPlaylist.SelectedItem != null)
            {
                currentPlaylist = comboBoxPlaylist.SelectedItem as IPlaylist;
                SetSongInfo(currentPlaylist);
            }
        }

        private void comboBoxPlaylist_TextChanged(object sender, EventArgs e)
        {
            CurrentPlaylist.Name = ((ComboBox)sender).Text;
        }

        private void buttonAddNewSong_Click(object sender, EventArgs e)
        {
            CurrentPlaylist.AddSong();
            SetSongInfo(CurrentPlaylist);
        }

        private void buttonNewPlaylist_Click(object sender, EventArgs e)
        {
            IPlaylist playList = CurrentRom.AddNewPlaylist();
            comboBoxPlaylist.Items.Add(playList);
            comboBoxPlaylist.SelectedItem = playList;
        }

        private void buttonNewROM_Click(object sender, EventArgs e)
        {
            IROM newRom = fileToEdit.AddROM();
            comboBoxRom.Items.Add(newRom);
            comboBoxRom.SelectedItem = newRom;
        }

        private void buttonDeletePlaylist_Click(object sender, EventArgs e)
        {
            int selectedIndex = comboBoxPlaylist.SelectedIndex;
            comboBoxPlaylist.Items.RemoveAt(selectedIndex);
            CurrentRom.DeletePlaylist(selectedIndex);
        }

        private void buttonDeleteROM_Click(object sender, EventArgs e)
        {
            int selectedIndex = comboBoxRom.SelectedIndex;
            comboBoxRom.Items.RemoveAt(selectedIndex);
            fileToEdit.DeleteROM(selectedIndex);
        }
    }
}
