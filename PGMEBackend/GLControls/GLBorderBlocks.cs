using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace PGMEBackend.GLControls
{
    class GLBorderBlocks
    {
        Color rectColor;

        int width = 0;
        int height = 0;

        public MapEditorTools tool = MapEditorTools.None;

        public int mouseX = -1;
        public int mouseY = -1;
        public int endMouseX = -1;
        public int endMouseY = -1;

        public Color rectDefaultColor = Color.FromArgb(0, 255, 0);
        public Color rectPaintColor = Color.FromArgb(255, 0, 0);
        public Color rectSelectColor = Color.FromArgb(255, 255, 0);

        public GLBorderBlocks(int w, int h)
        {
            width = w;
            height = h;
            GL.ClearColor(Color.Transparent);
            SetupViewport();
            rectColor = rectDefaultColor;
        }

        public static implicit operator bool (GLBorderBlocks b)
        {
            return b != null;
        }
        
        private void SetupViewport()
        {
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(0, width, height, 0, -1, 1); // Top left corner pixel has coordinate (0, 0)
            GL.Viewport(0, 0, width, height); // Use all of the glControl painting area
        }

        public void Paint(int w, int h)
        {
            width = w;
            height = h;
            SetupViewport();

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();
            GL.Enable(EnableCap.Texture2D);
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);

            Render();

            GL.Disable(EnableCap.Texture2D);
            GL.Disable(EnableCap.Blend);
        }
        
        private void Render()
        {
            MapLayout layout = Program.currentLayout;
            if (layout != null)
            {
                width = layout.borderWidth * 16;
                height = layout.borderHeight * 16;
                
                layout.DrawBorder((Program.currentLayout.globalTileset != null) ? Program.currentLayout.globalTileset.tileSheets : null,
                            (Program.currentLayout.localTileset != null) ? Program.currentLayout.localTileset.tileSheets : null, 0, 0, 1);
                /*
                if(Config.settings.ShowGrid)
                {
                    Surface.SetColor(Color.Black);
                    for(int i = 0; i < layout.layoutWidth; i++)
                        Surface.DrawLine(new double[] { i * 16, 0, i * 16, height });
                    for (int i = 0; i < layout.layoutHeight; i++)
                        Surface.DrawLine(new double[] { 0, i * 16, width, i * 16 });
                }*/

                if (mouseX != -1 && mouseY != -1)
                {
                    int x = mouseX * 16;
                    int y = mouseY * 16;
                    int endX = endMouseX * 16;
                    int endY = endMouseY * 16;

                    if (endMouseX >= width / 16)
                        endX = ((width - 1) / 16) * 16;
                    if (endMouseY >= height / 16)
                        endY = ((height - 1) / 16) * 16;

                    int w = x - endX;
                    int h = y - endY;
                    
                    Surface.DrawOutlineRect(endX + (w < 0 ? 16 : 0), endY + (h < 0 ? 16 : 0), w + (w >= 0 ? 16 : -16), h + (h >= 0 ? 16 : -16), rectColor);
                }
            }
        }
        
        public void MouseMove(int x, int y)
        {
            int oldMouseX = mouseX;
            int oldMouseY = mouseY;

            mouseX = x / 16;
            mouseY = y / 16;

            if (mouseX >= width / 16)
                mouseX = (width - 1) / 16;
            if (mouseY >= height / 16)
                mouseY = (height - 1) / 16;

            if (mouseX < 0)
                mouseX = 0;
            if (mouseY < 0)
                mouseY = 0;

            if (tool == MapEditorTools.Pencil && (mouseX != oldMouseX || mouseY != oldMouseY))
            {
                PaintBlocksToBorder(Program.glBlockChooser.selectArray, mouseX, mouseY, Program.glBlockChooser.editorSelectWidth, Program.glBlockChooser.editorSelectHeight);
                //Paint();
            }

            if (tool != MapEditorTools.Eyedropper)
            {
                Program.glBlockChooser.editorSelectWidth = Math.Abs(Program.glBlockChooser.editorSelectWidth);
                Program.glBlockChooser.editorSelectHeight = Math.Abs(Program.glBlockChooser.editorSelectHeight);
                endMouseX = mouseX + Program.glBlockChooser.editorSelectWidth - 1;
                endMouseY = mouseY + Program.glBlockChooser.editorSelectHeight - 1;
            }
        }

        public void MouseLeave()
        {
            mouseX = -1;
            mouseY = -1;
            endMouseX = -1;
            endMouseY = -1;
        }

        [Obsolete("Now handled by undo code")]
        void Paint()
        {
            // insert painting code
            for (int j = mouseY; j <= mouseY + Program.glBlockChooser.editorSelectHeight; j++)
            {
                for (int i = mouseX; i <= mouseX + Program.glBlockChooser.editorSelectWidth; i++)
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

        short[] oldBorder;

        public void PaintBlocksToBorder(short[] blockArray, int x, int y, int w, int h)
        {
            bool found = false;
            for (int i = 0; i < h; i++)
            {
                for (int j = 0; j < w; j++)
                {
                    if ((x + j < Program.currentLayout.borderWidth) && (y + i < Program.currentLayout.borderHeight))
                    {
                        if (Program.currentLayout.border[(x + (y * Program.currentLayout.borderWidth)) + (i * Program.currentLayout.borderWidth) + j] != blockArray[(i * w) + j])
                            found = true;
                    }
                    if (found)
                        break;
                }
                if (found)
                    break;
            }
            if (found)
            {
                Console.WriteLine("Painting of " + w * h + " blocks...");
                for (int i = 0; i < h; i++)
                {
                    for (int j = 0; j < w; j++)
                    {
                        if ((x + j < Program.currentLayout.borderWidth) && (y + i < Program.currentLayout.borderHeight))
                        {
                            Console.WriteLine("Drawing at (" + (x + j) + ", " + (y + i) + "): " + blockArray[i * w + j]);
                            Program.currentLayout.border[(x + (y * Program.currentLayout.borderWidth)) + (i * Program.currentLayout.borderWidth) + j] = blockArray[(i * w) + j];
                        }
                    }
                }
            }
        }

        public void MouseDown(MapEditorTools Tool)
        {
            if (tool == MapEditorTools.None)
            {
                tool = Tool;
                if (tool == MapEditorTools.Pencil)
                {
                    rectColor = rectPaintColor;
                    oldBorder = new short[Program.currentLayout.borderWidth * Program.currentLayout.borderHeight];
                    Buffer.BlockCopy(Program.currentLayout.border, 0, oldBorder, 0, Program.currentLayout.border.Length);
                    PaintBlocksToBorder(Program.glBlockChooser.selectArray, mouseX, mouseY, Program.glBlockChooser.editorSelectWidth, Program.glBlockChooser.editorSelectHeight);
                    //Paint();
                }
                else if (tool == MapEditorTools.Eyedropper)
                {
                    Program.glBlockChooser.editorSelectWidth = 1;
                    Program.glBlockChooser.editorSelectHeight = 1;
                    endMouseX = mouseX;
                    endMouseY = mouseY;
                    rectColor = rectSelectColor;
                }
                else if (tool == MapEditorTools.Fill)
                {
                    if (Program.glBlockChooser.selectArray.Length == 1)
                    {
                        short originalBlock = Program.currentLayout.border[(mouseX + (mouseY * Program.currentLayout.borderWidth))];
                        short newBlock = Program.glBlockChooser.selectArray[0];
                        rectColor = rectPaintColor;
                        if (originalBlock != newBlock)
                        {
                            oldBorder = new short[Program.currentLayout.borderWidth * Program.currentLayout.borderHeight];
                            Buffer.BlockCopy(Program.currentLayout.border, 0, oldBorder, 0, Program.currentLayout.border.Length);
                            FillBlocks(mouseX, mouseY, originalBlock, newBlock);
                            StoreChangesToUndoBufferAndRedraw();
                        }
                    }
                }
                else if (tool == MapEditorTools.FillAll)
                {
                    if (Program.glBlockChooser.selectArray.Length == 1)
                    {
                        short originalBlock = Program.currentLayout.border[(mouseX + (mouseY * Program.currentLayout.layoutWidth))];
                        short newBlock = Program.glBlockChooser.selectArray[0];
                        if (originalBlock != newBlock)
                        {
                            rectColor = rectPaintColor;
                            oldBorder = new short[Program.currentLayout.borderWidth * Program.currentLayout.borderHeight];
                            Buffer.BlockCopy(Program.currentLayout.border, 0, oldBorder, 0, Program.currentLayout.border.Length);
                            for (int i = 0; i < Program.currentLayout.border.Length; i++)
                            {
                                if (Program.currentLayout.border[i] == originalBlock)
                                    Program.currentLayout.border[i] = newBlock;
                            }
                            StoreChangesToUndoBufferAndRedraw();
                        }
                    }
                }
                else
                    rectColor = rectDefaultColor;
            }
        }

        public void FillBlocks(int x, int y, short originalBlock, short newBlock)
        {
            if (x >= 0 && x < Program.currentLayout.borderWidth &&
                y >= 0 && y < Program.currentLayout.borderHeight &&
                Program.currentLayout.border[x + (y * Program.currentLayout.borderWidth)] == originalBlock)
            {
                Program.currentLayout.border[x + (y * Program.currentLayout.borderWidth)] = newBlock;
                FillBlocks(x, y + 1, originalBlock, newBlock);
                FillBlocks(x, y - 1, originalBlock, newBlock);
                FillBlocks(x - 1, y, originalBlock, newBlock);
                FillBlocks(x + 1, y, originalBlock, newBlock);
            }
        }

        public void StoreChangesToUndoBufferAndRedraw()
        {
            int x = int.MaxValue;
            int y = int.MaxValue;
            int w = -1;
            int h = -1;

            for (int i = 0; i < oldBorder.Length; i++)
            {
                if (oldBorder[i] != Program.currentLayout.border[i])
                {
                    if (i % Program.currentLayout.borderWidth < x)
                        x = i % Program.currentLayout.borderWidth;
                    if (i / Program.currentLayout.borderWidth < y)
                        y = i / Program.currentLayout.borderWidth;
                }
            }

            if (x < int.MaxValue && y < int.MaxValue)
            {
                for (int i = oldBorder.Length - 1; i >= 0; i--)
                {
                    if (oldBorder[i] != Program.currentLayout.border[i])
                    {
                        if (i % Program.currentLayout.borderWidth - x + 1 > w)
                            w = i % Program.currentLayout.borderWidth - x + 1;
                        if (i / Program.currentLayout.borderWidth - y + 1 > h)
                            h = i / Program.currentLayout.borderWidth - y + 1;
                    }
                }

                if (w > 0 && h > 0)
                {
                    short[] oldData = new short[w * h];
                    short[] newData = new short[w * h];
                    for (int k = 0; k < h; k++)
                    {
                        for (int l = 0; l < w; l++)
                        {
                            if ((x + l < Program.currentLayout.borderWidth) && (y + k < Program.currentLayout.borderHeight))
                            {
                                oldData[(k * w) + l] = oldBorder[(x + (y * Program.currentLayout.borderWidth)) + (k * Program.currentLayout.borderWidth) + l];
                                newData[(k * w) + l] = Program.currentLayout.border[(x + (y * Program.currentLayout.borderWidth)) + (k * Program.currentLayout.borderWidth) + l];
                            }
                        }
                    }
                    
                    UndoManager.Add(new Undo.PaintUndo(oldData, newData, x, y, w, h), false);
                }
            }
        }

        public void MouseUp(MapEditorTools Tool)
        {
            if (tool == Tool)
            {
                if (tool == MapEditorTools.Eyedropper)
                {
                    Program.glBlockChooser.editorSelectWidth = Math.Abs(mouseX - endMouseX) + 1;
                    Program.glBlockChooser.editorSelectHeight = Math.Abs(mouseY - endMouseY) + 1;

                    Program.glBlockChooser.selectArray = new short[Program.glBlockChooser.editorSelectWidth * Program.glBlockChooser.editorSelectHeight];

                    for (int i = 0; i < Program.glBlockChooser.editorSelectHeight; i++)
                        for (int j = 0; j < Program.glBlockChooser.editorSelectWidth; j++)
                            Program.glBlockChooser.selectArray[(i * Program.glBlockChooser.editorSelectWidth) + j] = Program.currentLayout.border[(((mouseX > endMouseX) ? endMouseX : mouseX) + (((mouseY > endMouseY) ? endMouseY : mouseY) * Program.currentLayout.borderWidth)) + (i * Program.currentLayout.borderWidth) + j];

                    /*
                    foreach (var item in selectArray)
                    {
                        Console.WriteLine(item.ToString("X4"));
                    }*/

                    if (Program.glBlockChooser.editorSelectWidth == 1 && Program.glBlockChooser.editorSelectHeight == 1)
                        Program.glBlockChooser.SelectBlock(Program.glBlockChooser.selectArray[0] & 0x3FF);

                    else if (Program.glBlockChooser.editorSelectWidth > 1 || Program.glBlockChooser.editorSelectHeight > 1)
                        Program.glBlockChooser.SelectBlock(-1);
                }
                else if (tool == MapEditorTools.Pencil)
                {
                    int x = int.MaxValue;
                    int y = int.MaxValue;
                    int w = -1;
                    int h = -1;

                    for (int i = 0; i < oldBorder.Length; i++)
                    {
                        if (oldBorder[i] != Program.currentLayout.border[i])
                        {
                            if (i % Program.currentLayout.borderWidth < x)
                                x = i % Program.currentLayout.borderWidth;
                            if (i / Program.currentLayout.borderWidth < y)
                                y = i / Program.currentLayout.borderWidth;
                        }
                    }

                    if (x < int.MaxValue && y < int.MaxValue)
                    {
                        for (int i = oldBorder.Length - 1; i >= 0; i--)
                        {
                            if (oldBorder[i] != Program.currentLayout.border[i])
                            {
                                if (i % Program.currentLayout.borderWidth - x + 1 > w)
                                    w = i % Program.currentLayout.borderWidth - x + 1;
                                if (i / Program.currentLayout.borderWidth - y + 1 > h)
                                    h = i / Program.currentLayout.borderWidth - y + 1;
                            }
                        }

                        if (w > 0 && h > 0)
                        {
                            short[] oldData = new short[w * h];
                            short[] newData = new short[w * h];
                            for (int k = 0; k < h; k++)
                            {
                                for (int l = 0; l < w; l++)
                                {
                                    if ((x + l < Program.currentLayout.borderWidth) && (y + k < Program.currentLayout.borderHeight))
                                    {
                                        oldData[(k * w) + l] = oldBorder[(x + (y * Program.currentLayout.borderWidth)) + (k * Program.currentLayout.borderWidth) + l];
                                        newData[(k * w) + l] = Program.currentLayout.border[(x + (y * Program.currentLayout.borderWidth)) + (k * Program.currentLayout.borderWidth) + l];
                                    }
                                }
                            }
                            UndoManager.Add(new Undo.PaintBorderUndo(oldData, newData, x, y, w, h), false);
                        }
                    }
                }

                tool = MapEditorTools.None;
                rectColor = rectDefaultColor;
            }
        }
    }
}
