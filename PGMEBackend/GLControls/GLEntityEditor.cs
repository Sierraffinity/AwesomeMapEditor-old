﻿using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace PGMEBackend.GLControls
{
    class GLEntityEditor
    {
        Color rectColor;

        int width = 0;
        int height = 0;

        public MapEditorTools tool = MapEditorTools.None;

        public int mouseX = -1;
        public int mouseY = -1;
        public int endMouseX = -1;
        public int endMouseY = -1;
        public int selectWidth = 1;
        public int selectHeight = 1;

        public Color rectDefaultColor = Color.FromArgb(0, 255, 0);
        public Color rectPaintColor = Color.FromArgb(255, 0, 0);
        public Color rectSelectColor = Color.FromArgb(255, 255, 0);

        public GLEntityEditor(int w, int h)
        {
            width = w;
            height = h;
            GL.ClearColor(Color.Transparent);
            SetupViewport();
            rectColor = rectDefaultColor;
        }

        public static implicit operator bool (GLEntityEditor b)
        {
            return b != null;
        }

        private void SetupViewport()
        {
            GL.Viewport(0, 0, width, height); // Use all of the glControl painting area
        }

        public void Paint(int w, int h)
        {
            GL.Enable(EnableCap.Texture2D);
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
            PreRender();

            width = w;
            height = h;
            SetupViewport();

            GL.ClearColor(Color.Transparent);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            var proj = OpenTK.Matrix4.CreateOrthographicOffCenter(0, width, height, 0, -1, 1);
            GL.LoadMatrix(ref proj);

            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();

            Render();

            GL.Disable(EnableCap.Texture2D);
            GL.Disable(EnableCap.Blend);
            /*
            var err = GL.GetError();
            if (err != ErrorCode.NoError)
                System.Windows.Forms.MessageBox.Show(err.ToString(), "OpenGL Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            */
        }

        private void PreRender()
        {
            var layout = Program.currentLayout;
            if (layout != null)
            {
                layout.RefreshChunks((Program.currentLayout.globalTileset != null) ? Program.currentLayout.globalTileset.tileSheets : null,
                            (Program.currentLayout.localTileset != null) ? Program.currentLayout.localTileset.tileSheets : null, 0, 0, 1);
            }
        }

        private void Render()
        {
            MapLayout layout = Program.currentLayout;
            if (layout != null)
            {
                layout.Draw((Program.currentLayout.globalTileset != null) ? Program.currentLayout.globalTileset.tileSheets : null,
                            (Program.currentLayout.localTileset != null) ? Program.currentLayout.localTileset.tileSheets : null, 0, 0, 1);
                Program.mainGUI.SetGLEntityEditorSize(layout.layoutWidth * 16, layout.layoutHeight * 16);

                width = layout.layoutWidth * 16;
                height = layout.layoutHeight * 16;

                if(Config.settings.ShowGrid)
                {
                    Surface.SetColor(Color.Black);
                    for(int i = 0; i < layout.layoutWidth; i++)
                        Surface.DrawLine(new double[] { i * 16, 0, i * 16, height });
                    for (int i = 0; i < layout.layoutHeight; i++)
                        Surface.DrawLine(new double[] { 0, i * 16, width, i * 16 });
                }

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
                PaintBlocksToMap(Program.glBlockChooser.selectArray, mouseX, mouseY, selectWidth, selectHeight);
                //Paint();
            }

            if (tool != MapEditorTools.Eyedropper)
            {
                selectWidth = Math.Abs(selectWidth);
                selectHeight = Math.Abs(selectHeight);
                endMouseX = mouseX + selectWidth - 1;
                endMouseY = mouseY + selectHeight - 1;
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
            for (int j = mouseY; j <= mouseY + selectHeight; j++)
            {
                for (int i = mouseX; i <= mouseX + selectWidth; i++)
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

        short[] oldLayout;

        public void PaintBlocksToMap(short[] blockArray, int x, int y, int w, int h)
        {
            bool found = false;
            for (int i = 0; i < h; i++)
            {
                for (int j = 0; j < w; j++)
                {
                    if ((x + j < Program.currentLayout.layoutWidth) && (y + i < Program.currentLayout.layoutHeight))
                    {
                        if (Program.currentLayout.layout[(x + (y * Program.currentLayout.layoutWidth)) + (i * Program.currentLayout.layoutWidth) + j] != blockArray[(i * w) + j])
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
                        if ((x + j < Program.currentLayout.layoutWidth) && (y + i < Program.currentLayout.layoutHeight))
                        {
                            Console.WriteLine("Drawing at (" + (x + j) + ", " + (y + i) + "): " + blockArray[i * w + j]);
                            Program.currentLayout.layout[(x + (y * Program.currentLayout.layoutWidth)) + (i * Program.currentLayout.layoutWidth) + j] = blockArray[(i * w) + j];
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

        public void MouseDown(MapEditorTools Tool)
        {
            if (tool == MapEditorTools.None)
            {
                tool = Tool;
                if (tool == MapEditorTools.Pencil)
                {
                    rectColor = rectPaintColor;
                    oldLayout = new short[Program.currentLayout.layoutWidth * Program.currentLayout.layoutHeight];
                    Buffer.BlockCopy(Program.currentLayout.layout, 0, oldLayout, 0, Program.currentLayout.layout.Length);
                    PaintBlocksToMap(Program.glBlockChooser.selectArray, mouseX, mouseY, selectWidth, selectHeight);
                    //Paint();
                }
                else if (tool == MapEditorTools.Eyedropper)
                {
                    selectWidth = 1;
                    selectHeight = 1;
                    endMouseX = mouseX;
                    endMouseY = mouseY;
                    rectColor = rectSelectColor;
                }
                else if (tool == MapEditorTools.Fill)
                {
                    if (Program.glBlockChooser.selectArray.Length == 1)
                    {
                        short originalBlock = (short)(Program.currentLayout.layout[(mouseX + (mouseY * Program.currentLayout.layoutWidth))] & 0x3FF);
                        short newBlock = (short)(Program.glBlockChooser.selectArray[0] & 0x3FF);
                        rectColor = rectPaintColor;
                        if (originalBlock != newBlock)
                        {
                            oldLayout = new short[Program.currentLayout.layoutWidth * Program.currentLayout.layoutHeight];
                            Buffer.BlockCopy(Program.currentLayout.layout, 0, oldLayout, 0, Program.currentLayout.layout.Length);
                            FillBlocks(mouseX, mouseY, originalBlock, newBlock);
                            StoreChangesToUndoBufferAndRedraw();
                        }
                    }
                }
                else if (tool == MapEditorTools.FillAll)
                {
                    if (Program.glBlockChooser.selectArray.Length == 1)
                    {
                        short originalBlock = (short)(Program.currentLayout.layout[(mouseX + (mouseY * Program.currentLayout.layoutWidth))] & 0x3FF);
                        short newBlock = (short)(Program.glBlockChooser.selectArray[0] & 0x3FF);
                        if (originalBlock != newBlock)
                        {
                            rectColor = rectPaintColor;
                            oldLayout = new short[Program.currentLayout.layoutWidth * Program.currentLayout.layoutHeight];
                            Buffer.BlockCopy(Program.currentLayout.layout, 0, oldLayout, 0, Program.currentLayout.layout.Length);
                            for (int i = 0; i < Program.currentLayout.layout.Length; i++)
                            {
                                if ((short)(Program.currentLayout.layout[i] & 0x3FF) == originalBlock)
                                    Program.currentLayout.layout[i] = (short)(newBlock + (Program.currentLayout.layout[i] & 0xFC00));
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
            if (x >= 0 && x < Program.currentLayout.layoutWidth &&
                y >= 0 && y < Program.currentLayout.layoutHeight &&
                (short)(Program.currentLayout.layout[x + (y * Program.currentLayout.layoutWidth)] & 0x3FF) == originalBlock)
            {
                Program.currentLayout.layout[x + (y * Program.currentLayout.layoutWidth)] = (short)(newBlock + (Program.currentLayout.layout[x + (y * Program.currentLayout.layoutWidth)] & 0xFC00));
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

            for (int i = 0; i < oldLayout.Length; i++)
            {
                if (oldLayout[i] != Program.currentLayout.layout[i])
                {
                    if (i % Program.currentLayout.layoutWidth < x)
                        x = i % Program.currentLayout.layoutWidth;
                    if (i / Program.currentLayout.layoutWidth < y)
                        y = i / Program.currentLayout.layoutWidth;
                }
            }

            if (x < int.MaxValue && y < int.MaxValue)
            {
                for (int i = oldLayout.Length - 1; i >= 0; i--)
                {
                    if (oldLayout[i] != Program.currentLayout.layout[i])
                    {
                        if (i % Program.currentLayout.layoutWidth - x + 1 > w)
                            w = i % Program.currentLayout.layoutWidth - x + 1;
                        if (i / Program.currentLayout.layoutWidth - y + 1 > h)
                            h = i / Program.currentLayout.layoutWidth - y + 1;
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
                            if ((x + l < Program.currentLayout.layoutWidth) && (y + k < Program.currentLayout.layoutHeight))
                            {
                                oldData[(k * w) + l] = oldLayout[(x + (y * Program.currentLayout.layoutWidth)) + (k * Program.currentLayout.layoutWidth) + l];
                                newData[(k * w) + l] = Program.currentLayout.layout[(x + (y * Program.currentLayout.layoutWidth)) + (k * Program.currentLayout.layoutWidth) + l];
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
                    selectWidth = Math.Abs(mouseX - endMouseX) + 1;
                    selectHeight = Math.Abs(mouseY - endMouseY) + 1;

                    Program.glBlockChooser.selectArray = new short[selectWidth * selectHeight];

                    for (int i = 0; i < selectHeight; i++)
                        for (int j = 0; j < selectWidth; j++)
                            Program.glBlockChooser.selectArray[(i * selectWidth) + j] = Program.currentLayout.layout[(((mouseX > endMouseX) ? endMouseX : mouseX) + (((mouseY > endMouseY) ? endMouseY : mouseY) * Program.currentLayout.layoutWidth)) + (i * Program.currentLayout.layoutWidth) + j];

                    /*
                    foreach (var item in selectArray)
                    {
                        Console.WriteLine(item.ToString("X4"));
                    }*/

                    if (selectWidth == 1 && selectHeight == 1)
                        Program.glBlockChooser.SelectBlock(Program.glBlockChooser.selectArray[0] & 0x3FF);

                    else if (selectWidth > 1 || selectHeight > 1)
                        Program.glBlockChooser.SelectBlock(-1);
                }
                else if (tool == MapEditorTools.Pencil)
                {
                    int x = int.MaxValue;
                    int y = int.MaxValue;
                    int w = -1;
                    int h = -1;

                    for (int i = 0; i < oldLayout.Length; i++)
                    {
                        if (oldLayout[i] != Program.currentLayout.layout[i])
                        {
                            if (i % Program.currentLayout.layoutWidth < x)
                                x = i % Program.currentLayout.layoutWidth;
                            if (i / Program.currentLayout.layoutWidth < y)
                                y = i / Program.currentLayout.layoutWidth;
                        }
                    }

                    if (x < int.MaxValue && y < int.MaxValue)
                    {
                        for (int i = oldLayout.Length - 1; i >= 0; i--)
                        {
                            if (oldLayout[i] != Program.currentLayout.layout[i])
                            {
                                if (i % Program.currentLayout.layoutWidth - x + 1 > w)
                                    w = i % Program.currentLayout.layoutWidth - x + 1;
                                if (i / Program.currentLayout.layoutWidth - y + 1 > h)
                                    h = i / Program.currentLayout.layoutWidth - y + 1;
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
                                    if ((x + l < Program.currentLayout.layoutWidth) && (y + k < Program.currentLayout.layoutHeight))
                                    {
                                        oldData[(k * w) + l] = oldLayout[(x + (y * Program.currentLayout.layoutWidth)) + (k * Program.currentLayout.layoutWidth) + l];
                                        newData[(k * w) + l] = Program.currentLayout.layout[(x + (y * Program.currentLayout.layoutWidth)) + (k * Program.currentLayout.layoutWidth) + l];
                                    }
                                }
                            }
                            UndoManager.Add(new Undo.PaintUndo(oldData, newData, x, y, w, h), false);
                        }
                    }
                }

                tool = MapEditorTools.None;
                rectColor = rectDefaultColor;
            }
        }
    }
}