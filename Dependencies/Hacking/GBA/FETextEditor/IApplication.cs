using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FETextEditor
{
    public interface IApplication
    {
        void LoadText(int index);
        void LoadROM(string path);
        void SetText(string text, int index);
        void SaveROM();
        void SaveROM(string path);
        IUserInterface CurrentUI { get; set; }

        void DumbScript(string path);
        void InsertScript(string path);
    }
}
