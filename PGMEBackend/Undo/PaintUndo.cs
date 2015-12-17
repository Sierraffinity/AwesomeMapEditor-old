using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PGMEBackend.Undo
{
    public class PaintUndo : UndoStep
    {
        short[] oldLayout;
        short[] newLayout;
        int x = -1;
        int y = -1;
        int w = -1;
        int h = -1;

        public PaintUndo(short[] OldLayout, short[] NewLayout, int X, int Y, int W, int H)
        {
            x = X;
            y = Y;
            w = W;
            h = H;
            oldLayout = OldLayout;
            newLayout = NewLayout;
        }

        public override void CallUndo()
        {
            if (firstStep)
                Program.isEdited = false;
            Console.WriteLine("Undoing painting of " + newLayout.Length + " blocks...");
            for (int i = 0; i < h; i++)
            {
                for (int j = 0; j < w; j++)
                {
                    if ((x + j < Program.currentLayout.layoutWidth) && (y + i < Program.currentLayout.layoutHeight))
                    {
                        Console.WriteLine("Drawing at (" + (x + j) + ", " + (y + i) + "): " + oldLayout[i * w + j]);
                        Program.currentLayout.layout[(x + (y * Program.currentLayout.layoutWidth)) + (i * Program.currentLayout.layoutWidth) + j] = oldLayout[(i * w) + j];
                    }
                }
            }
            for (int j = y; j <= y + h; j++)
            {
                for (int i = x; i <= x + w; i++)
                {
                    foreach (var v in Program.currentLayout.drawTiles)
                    {
                        if (v.Redraw)
                            continue;
                        if (i < v.xpos + v.Width && i >= v.xpos && j >= v.ypos && j < v.ypos + v.Height)
                        {
                            v.Redraw = true;
                            Console.WriteLine("Redrawing " + v.buffer.FBOHandle);
                            continue;
                        }
                    }
                }
            }
        }

        public override void CallRedo()
        {
            if (firstStep)
                Program.isEdited = true;
            Console.WriteLine("Redoing painting of " + newLayout.Length + " blocks...");
            for (int i = 0; i < h; i++)
            {
                for (int j = 0; j < w; j++)
                {
                    if ((x + j < Program.currentLayout.layoutWidth) && (y + i < Program.currentLayout.layoutHeight))
                    {
                        Console.WriteLine("Drawing at (" + (x + j) + ", " + (y + i) + "): " + newLayout[i * w + j]);
                        Program.currentLayout.layout[(x + (y * Program.currentLayout.layoutWidth)) + (i * Program.currentLayout.layoutWidth) + j] = newLayout[(i * w) + j];
                    }
                }
            }

            for (int j = y; j <= y + h; j++)
            {
                for (int i = x; i <= x + w; i++)
                {
                    foreach (var v in Program.currentLayout.drawTiles)
                    {
                        if (v.Redraw)
                            continue;
                        if (i < v.xpos + v.Width && i >= v.xpos && j >= v.ypos && j < v.ypos + v.Height)
                        {
                            v.Redraw = true;
                            Console.WriteLine("Redrawing " + v.buffer.FBOHandle);
                            continue;
                        }
                    }
                }
            }
        }
    }
}
