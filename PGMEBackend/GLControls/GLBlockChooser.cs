using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace PGMEBackend.GLControls
{
    class GLBlockChooser
    {
        Color rectColor;

        int width = 0;
        int height = 0;

        public MouseButtons buttons;

        public int mouseX = -1;
        public int mouseY = -1;
        public int endMouseX = -1;
        public int endMouseY = -1;
        public int selectX = 0;
        public int selectY = 0;
        public int selectWidth = 1;
        public int selectHeight = 1;

        public Color rectDefaultColor = Color.FromArgb(0, 255, 0);
        public Color rectPaintColor = Color.FromArgb(255, 0, 0);
        public Color rectSelectColor = Color.FromArgb(255, 255, 0);

        public GLBlockChooser(int w, int h)
        {
            width = w;
            height = h;
            GL.ClearColor(Color.Transparent);
            SetupViewport();
            rectColor = rectPaintColor;
        }

        public static implicit operator bool (GLBlockChooser b)
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
                MapTileset globalTileset = Program.currentLayout.globalTileset;
                MapTileset localTileset = Program.currentLayout.localTileset;
                height = (int)Math.Ceiling(Program.currentGame.MainTSBlocks / 8.0d) * 16;
                if (globalTileset != null && globalTileset.blockSet != null)
                    globalTileset.blockSet.Draw((globalTileset != null) ? globalTileset.tileSheets : null, (localTileset != null) ? localTileset.tileSheets : null, 0, 0, 1);

                if (localTileset != null && localTileset.blockSet != null)
                {
                    localTileset.blockSet.Draw((globalTileset != null) ? globalTileset.tileSheets : null, (localTileset != null) ? localTileset.tileSheets : null, 0, height, 1);
                    height += (int)Math.Ceiling(localTileset.blockSet.blocks.Length / 8.0d) * 16;
                }
                Program.mainGUI.SetGLBlockChooserSize(height);

                int x = selectX * 16;
                int y = selectY * 16;
                int w = selectWidth * 16;
                int h = selectHeight * 16;

                if (selectX + selectWidth > width / 16)
                    w = ((width - 1) / 16 - selectX) * 16;
                if (selectY + selectHeight > height / 16)
                    h = ((height - 1) / 16 - selectY) * 16;
                
                Surface.DrawOutlineRect(x + (w < 0 ? 16 : 0), y + (h < 0 ? 16 : 0), w + (w >= 0 ? 0 : -16), h + (h >= 0 ? 0 : -16), rectColor);

                if (buttons == MouseButtons.None)
                {
                    Surface.DrawOutlineRect(mouseX * 16, mouseY * 16, 16, 16, rectDefaultColor);
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

            if (buttons == MouseButtons.Left && (mouseX != oldMouseX || mouseY != oldMouseY))
            {
                selectX = mouseX;
                selectY = mouseY;
                endMouseX = mouseX;
                endMouseY = mouseY;
                selectWidth = 1;
                selectHeight = 1;
            }
            else if (buttons == MouseButtons.Right)
            {
                selectX = (mouseX > endMouseX) ? endMouseX : mouseX;
                selectY = (mouseY > endMouseY) ? endMouseY : mouseY;
                selectWidth = Math.Abs(mouseX - endMouseX) + 1;
                selectHeight = Math.Abs(mouseY - endMouseY) + 1;
            }
        }

        public void MouseLeave()
        {
            mouseX = -1;
            mouseY = -1;
            endMouseX = -1;
            endMouseY = -1;
        }

        public void MouseDown(MouseButtons button)
        {
            buttons = button;
            if (buttons == MouseButtons.Left || buttons == MouseButtons.Right)
            {
                selectX = mouseX;
                selectY = mouseY;
                selectWidth = 1;
                selectHeight = 1;
                endMouseX = mouseX;
                endMouseY = mouseY;
                if (buttons == MouseButtons.Left)
                    rectColor = rectPaintColor;
                else
                    rectColor = rectSelectColor;
            }
            else
                rectColor = rectDefaultColor;
        }

        public void MouseUp()
        {
            selectX = (mouseX > endMouseX) ? endMouseX : mouseX;
            selectY = (mouseY > endMouseY) ? endMouseY : mouseY;
            selectWidth = Math.Abs(mouseX - endMouseX) + 1;
            selectHeight = Math.Abs(mouseY - endMouseY) + 1;
            Program.glMapEditor.selectWidth = selectWidth;
            Program.glMapEditor.selectHeight = selectHeight;

            Program.glMapEditor.selectArray = new short[selectWidth * selectHeight];

            for (int i = 0; i < selectHeight; i++)
                for (int j = 0; j < selectWidth; j++)
                    Program.glMapEditor.selectArray[(i * selectWidth) + j] = (short)((selectX + (selectY * 8)) + (i * 8) + j);
            /*
            foreach (var item in Program.glMapEditor.selectArray)
            {
                Console.WriteLine(item.ToString("X4"));
            }*/

            buttons = MouseButtons.None;
            rectColor = rectPaintColor;
        }

        public void SelectBlock(int blockNum)
        {
            Program.glBlockChooser.selectWidth = 1;
            Program.glBlockChooser.selectHeight = 1;
            if (blockNum >= 0)
            {
                Program.glBlockChooser.selectX = blockNum % 8;
                Program.glBlockChooser.selectY = blockNum / 8;
                Program.mainGUI.ScrollBlockChooserToBlock(blockNum);
            }
            else
            {
                Program.glBlockChooser.selectX = -1;
                Program.glBlockChooser.selectY = -1;
            }

            Program.mainGUI.RefreshBlockEditorControl();
        }
    }
}
