using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PGMEBackend.Undo
{
    public class PaintBorderUndo : UndoStep
    {
        short[] oldBorder;
        short[] newBorder;
        int x = -1;
        int y = -1;
        int w = -1;
        int h = -1;

        public PaintBorderUndo(short[] OldBorder, short[] NewBorder, int X, int Y, int W, int H)
        {
            x = X;
            y = Y;
            w = W;
            h = H;
            oldBorder = OldBorder;
            newBorder = NewBorder;
        }

        public override void CallUndo()
        {
            if (firstStep)
                Program.isEdited = false;
            Console.WriteLine("Undoing painting of " + newBorder.Length + " blocks...");
            for (int i = 0; i < h; i++)
            {
                for (int j = 0; j < w; j++)
                {
                    if ((x + j < Program.currentLayout.borderWidth) && (y + i < Program.currentLayout.borderHeight))
                    {
                        Console.WriteLine("Drawing at (" + (x + j) + ", " + (y + i) + "): " + oldBorder[i * w + j]);
                        Program.currentLayout.border[(x + (y * Program.currentLayout.borderWidth)) + (i * Program.currentLayout.borderWidth) + j] = oldBorder[(i * w) + j];
                    }
                }
            }
            Program.mainGUI.RefreshBorderBlocksControl();
        }

        public override void CallRedo()
        {
            if (firstStep)
                Program.isEdited = true;
            Console.WriteLine("Redoing painting of " + newBorder.Length + " blocks...");
            for (int i = 0; i < h; i++)
            {
                for (int j = 0; j < w; j++)
                {
                    if ((x + j < Program.currentLayout.borderWidth) && (y + i < Program.currentLayout.borderHeight))
                    {
                        Console.WriteLine("Drawing at (" + (x + j) + ", " + (y + i) + "): " + newBorder[i * w + j]);
                        Program.currentLayout.border[(x + (y * Program.currentLayout.borderWidth)) + (i * Program.currentLayout.borderWidth) + j] = newBorder[(i * w) + j];
                    }
                }
            }
            Program.mainGUI.RefreshBorderBlocksControl();
        }
    }
}
