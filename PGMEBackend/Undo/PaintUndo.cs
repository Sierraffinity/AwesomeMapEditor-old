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
        public PaintUndo(short[] OldLayout, short[] NewLayout)
        {
            oldLayout = OldLayout;
            newLayout = NewLayout;
        }

        public override void CallUndo()
        {
            if (firstStep)
                Program.isEdited = false;
            Console.WriteLine("Undoing painting of " + Program.currentLayout.layoutWidth * Program.currentLayout.layoutHeight + " blocks...");
            Buffer.BlockCopy(oldLayout, 0, Program.currentLayout.layout, 0, Program.currentLayout.layout.Length);

            for (int j = 0; j <= Program.currentLayout.layoutHeight; j++)
            {
                for (int i = 0; i <= Program.currentLayout.layoutWidth; i++)
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
            Console.WriteLine("Redoing painting of " + Program.currentLayout.layoutWidth * Program.currentLayout.layoutHeight + " blocks...");
            Buffer.BlockCopy(newLayout, 0, Program.currentLayout.layout, 0, Program.currentLayout.layout.Length);

            for (int j = 0; j <= Program.currentLayout.layoutHeight; j++)
            {
                for (int i = 0; i <= Program.currentLayout.layoutWidth; i++)
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
