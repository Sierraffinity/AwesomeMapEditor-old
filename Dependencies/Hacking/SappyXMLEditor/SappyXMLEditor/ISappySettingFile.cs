using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SappyXMLEditor
{
    public interface ISappySettingFile
    {
        bool HasBeenEdited
        {
            get;
        }
        string FilePath
        {
            get;
        }
        
        void Load(string path);
        void Save();
        void SaveAs(string path);

        IROM AddROM();
        IROM GetROM(string name);
        IROM GetROM(int index);
        void DeleteROM(string name);
        void DeleteROM(int index);

        IROM[] GetROMs();
    }

    public interface IROM
    {
        string Name
        {
            get;
            set;
        }
        string Code
        {
            get;
            set;
        }
        int SongListOffset
        {
            get;
            set;
        }
        string Creator
        {
            get;
            set;
        }
        string Screenshot
        {
            get;
            set;
        }
        string Tagger
        {
            get;
            set;
        }

        IPlaylist AddNewPlaylist();
        IPlaylist GetPlaylist(string name);
        IPlaylist GetPlaylist(int index);
        void DeletePlaylist(string name);
        void DeletePlaylist(int index);

        IPlaylist[] GetPlaylists();
    }

    public interface IPlaylist
    {
        string Name
        {
            get;
            set;
        }

        ISong AddSong();
        ISong GetSong(string name);
        ISong GetSong(int number);
        void DeleteSong(string name);
        void DeleteSong(int index);

        ISong[] GetSongs();
    }

    public interface ISong
    {
        int Number
        {
            get;
            set;
        }
        string Name
        {
            get;
            set;
        }
    }
}
