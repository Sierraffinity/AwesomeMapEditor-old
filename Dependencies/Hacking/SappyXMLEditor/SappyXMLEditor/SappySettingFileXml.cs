using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using Nintenlord.Event_assembler;
using System.Xml;

namespace SappyXMLEditor
{
    public partial class SappyXmlSettingFile : ISappySettingFile
    {
        sappy sappyToEdit;
        XmlSerializer serializer;
        XmlWriterSettings writerSettings;
        XmlReaderSettings readerSettings;
        string filePath;
        
        public SappyXmlSettingFile()
        {
            serializer = new XmlSerializer(typeof(sappy));

            writerSettings = new XmlWriterSettings();
            writerSettings.OmitXmlDeclaration = true;
            writerSettings.Indent = true;
            writerSettings.IndentChars = "\t";
            writerSettings.CloseOutput = true;

            readerSettings = new XmlReaderSettings();
            readerSettings.CloseInput = true;
        }

        private XmlWriter GetWriter(string path)
        {
            XmlWriter writer = XmlWriter.Create(path, writerSettings);
            return writer;
        }

        private XmlReader GetReader(string path)
        {
            XmlReader reader = XmlReader.Create(path, readerSettings);
            return reader;
        }

        #region ISappySettingFile Members

        public bool HasBeenEdited
        {
            get 
            {
                foreach (rom item in this.sappyToEdit.rom)
                {
                    if (item.edited)
                    {
                        return true;
                    }
                    foreach (playlist list in item.playlist)
                    {
                        if (list.edited)
                        {
                            return true;
                        }
                        if (list.song != null)
                        {
                            foreach (song songItem in list.song)
                            {
                                if (songItem.edited)
                                {
                                    return true;
                                }
                            }
                        }
                    }
                }
                return false;
            }
            protected set
            {
                foreach (rom item in this.sappyToEdit.rom)
                {
                    item.edited = value;                    
                    foreach (playlist list in item.playlist)
                    {
                        list.edited = value;
                        if (list.song != null)
                        {
                            foreach (song songItem in list.song)
                            {
                                songItem.edited = value;
                            }
                        }
                    }
                }
            }
        }

        public string FilePath
        {
            get { return filePath; }
        }

        public void Load(string path)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException();

            XmlReader reader = GetReader(path);            
            this.sappyToEdit = serializer.Deserialize(reader) as sappy;
            reader.Close();
            this.filePath = path;
        }

        public void Save()
        {
            XmlWriter writer = GetWriter(this.filePath);
            serializer.Serialize(writer, sappyToEdit);
            writer.Close();
            this.HasBeenEdited = false;
        }

        public void SaveAs(string path)
        {
            XmlWriter writer = GetWriter(path);
            serializer.Serialize(writer, sappyToEdit);
            writer.Close();
            this.filePath = path;
            this.HasBeenEdited = false;
        }

        public IROM AddROM()
        {
            rom[] array = this.sappyToEdit.rom;
            Array.Resize<rom>(ref array, array.Length + 1);
            rom newRom = new rom();
            array[this.sappyToEdit.rom.Length - 1] = newRom;
            this.sappyToEdit.rom = array;
            newRom.edited = true;
            return newRom;
        }

        public IROM GetROM(string name)
        {
            rom toGet;
            if (this.sappyToEdit.rom.TryGetMathingID(name, out toGet))
            {
                return toGet;
            }
            else return null;
        }

        public IROM GetROM(int index)
        {
            return this.sappyToEdit.rom[index];
        }

        public IROM[] GetROMs()
        {
            return this.sappyToEdit.rom;
        }

        public void DeleteROM(string name)
        {            
            throw new NotImplementedException();
        }

        public void DeleteROM(int index)
        {
            rom[] roms = this.sappyToEdit.rom;
            if (index != roms.Length - 1)
            {
                rom[] temp = new rom[roms.Length - index - 1];
                Array.Copy(roms, index + 1, temp, 0, temp.Length);
                temp.CopyTo(roms, index);
            }
            Array.Resize<rom>(ref roms, roms.Length - 1);
            this.sappyToEdit.rom = roms;
        }

        #endregion
    }

    public partial class rom : IROM, IIdentifiable<string>
    {
        [XmlIgnore()]
        public bool edited = false;

        #region IROM Members
        [XmlIgnore()]
        public string Name
        {
            get { return this.nameField; }
            set 
            { 
                if (!value.Equals(this.nameField)) 
                    this.edited= true;
                this.nameField = value; 
            }
        }

        [XmlIgnore()]
        public string Code
        {
            get { return this.codeField; }
            set
            {
                if (!value.Equals(this.codeField))
                    this.edited = true;
                this.codeField = value;
            }
        }

        [XmlIgnore()]
        public int SongListOffset
        {
            get { return this.songtableField.GetValue(); }
            set
            {
                string text = (value).ToHexString("0x");
                if (!this.songtableField.Equals(text))
                    this.edited = true;
                this.songtableField = text;
            }
        }

        [XmlIgnore()]
        public string Creator
        {
            get { return this.creatorField; }
            set
            {
                if (!value.Equals(this.creatorField))
                    this.edited = true;
                if (string.IsNullOrEmpty(value))
                    this.creatorField = null;
                else this.creatorField = value;
            }
        }

        [XmlIgnore()]
        public string Screenshot
        {
            get { return this.screenshotField; }
            set
            {
                if (!value.Equals(this.screenshotField))
                    this.edited = true;
                if (string.IsNullOrEmpty(value))
                    this.screenshotField = null;
                else this.screenshotField = value;
            }
        }

        [XmlIgnore()]
        public string Tagger
        {
            get { return this.taggerField; }
            set
            {
                if (!value.Equals(this.taggerField))
                    this.edited = true;
                if (string.IsNullOrEmpty(value))
                    this.taggerField = null;
                else this.taggerField = value;
            }
        }

        public IPlaylist AddNewPlaylist()
        {
            edited = true;
            Array.Resize<playlist>(ref this.playlistField, this.playlistField.Length + 1);
            playlist newPlayList = new playlist();
            this.playlistField[this.playlistField.Length - 1] = newPlayList;
            return newPlayList;
        }

        public IPlaylist GetPlaylist(string name)
        {
            playlist toGet;
            if (playlist.TryGetMathingID(name, out toGet))
                return (playlist)toGet;
            else return null;
        }

        public IPlaylist GetPlaylist(int index)
        {
            return playlist[index];
        }

        public IPlaylist[] GetPlaylists()
        {
            return playlist;
        }

        public void DeletePlaylist(string name)
        {
            throw new NotImplementedException();
        }

        public void DeletePlaylist(int index)
        {
            playlist[] playlists = this.playlistField;
            if (index != playlists.Length - 1)
            {
                playlist[] temp = new playlist[playlists.Length - index - 1];
                Array.Copy(playlists, index + 1, temp, 0, temp.Length);
                temp.CopyTo(playlists, index);
            }
            Array.Resize<playlist>(ref playlists, playlists.Length - 1);
            this.playlistField = playlists;
        }

        #endregion

        #region IIdentifier<string> Members

        [XmlIgnore()]
        public string Identifier
        {
            get { return nameField; }
        }

        #endregion

        public override string ToString()
        {
            return this.nameField;
        }
    }

    public partial class playlist : IPlaylist, IIdentifiable<string>
    {
        [XmlIgnore()]
        public bool edited;

        #region IPlaylist Members

        [XmlIgnore()]
        public string Name
        {
            get { return this.nameField; }
            set
            {
                if (!this.nameField.Equals(value))
                    this.edited = true;
                this.nameField = value;
            }
        }

        public ISong AddSong()
        {
            edited = true;
            Array.Resize<song>(ref this.songField, this.songField.Length + 1);
            song newSong = new song();
            this.songField[this.songField.Length - 1] = newSong;
            return newSong;
        }

        public ISong GetSong(string name)
        {
            song toGet;
            if (songField.TryGetMathingID(name, out toGet))
            {
                return toGet;
            }
            else return null;
        }

        public ISong GetSong(int number)
        {
            return songField[number];
        }

        public ISong[] GetSongs()
        {
            return songField;
        }

        public void DeleteSong(string name)
        {
            throw new NotImplementedException();
        }

        public void DeleteSong(int index)
        {
            song[] songs = this.songField;
            if (index != songs.Length - 1)
            {
                song[] temp = new song[songs.Length - index - 1];
                Array.Copy(songs, index + 1, temp, 0, temp.Length);
                temp.CopyTo(songs, index);
            }
            Array.Resize<song>(ref songs, songs.Length - 1);
            this.songField = songs;
        }

        #endregion

        #region IIdentifier<string> Members

        [XmlIgnore()]
        public string Identifier
        {
            get { return this.nameField; }
        }

        #endregion

        public override string ToString()
        {
            return this.nameField;
        }

    }

    public partial class song : ISong, IIdentifiable<string>
    {
        public song()
        {
            this.textField = new string[1];
            this.textField[0] = "";
            this.trackField = (1).ToString();
            this.edited = true;
        }

        [XmlIgnore()]
        public bool edited;

        #region ISong Members

        [XmlIgnore()]
        public int Number
        {
            get { return trackField.GetValue(); }
            set
            {
                string text = value.ToString();
                if (!text.Equals(trackField))
                {
                    edited = true;
                }
                trackField = text;
            }
        }

        [XmlIgnore()]
        public string Name
        {
            get { return textField[0]; }
            set
            {
                if (!textField[0].Equals(value))
                {
                    edited = true;
                }
                textField[0] = value;
            }
        }

        #endregion

        #region IIdentifier<string> Members

        [XmlIgnore()]
        public string Identifier
        {
            get { return textField[0]; }
        }

        #endregion
    }
}