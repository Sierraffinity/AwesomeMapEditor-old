// Awesome Map Editor
// A map editor for GBA Pokémon games.

// Copyright (C) 2015 Diegoisawesome

// This file is part of Awesome Map Editor.
// Awesome Map Editor is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.

// Awesome Map Editor is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.

// You should have received a copy of the GNU General Public License
// along with Awesome Map Editor. If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PGMEBackend
{
    public interface UIInteractionLayer
    {
        void SetTitleText(string title);
        void SetLoadingStatus(string status);

        string ShowMessageBox(string body, string title);
        string ShowMessageBox(string body, string title, string buttons);
        string ShowMessageBox(string body, string title, string buttons, string icon);

        string ShowFileOpenDialog(string title, string filter, bool multiselect);

        void EnableControlsOnROMLoad();
        void EnableControlsOnMapLoad();

        void LoadMapNodes();
        void ClearMapNodes();

        void LoadMapDropdowns();
        void LoadMusicDropdowns();

        void QuitApplication(int code);

        void LoadMap(object map);

        void SetGLMapEditorSize(int w, int h);
        void SetGLBlockChooserSize(int w, int h);
        void SetGLBorderBlocksSize(int w, int h);
        void SetGLEntityEditorSize(int w, int h);

        void RefreshMapEditorControl();
        void RefreshBlockEditorControl();
        void RefreshPermsChooserControl();
        void RefreshBorderBlocksControl();
        void RefreshEntityEditorControl();
        void ScrollBlockChooserToBlock(int blockNum);
        void ScrollPermChooserToPerm(int permNum);

        int PermTransPreviewValue();

        void AddRecentFile(string fname);
    }
}
