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
        public int selectWidth = 0;
        public int selectHeight = 0;

        public Color rectDefaultColor = Color.FromArgb(0, 255, 0);
        public Color rectPaintColor = Color.FromArgb(255, 0, 0);
        public Color rectSelectColor = Color.FromArgb(255, 255, 0);

        public GLBlockChooser(int w, int h)
        {
            width = w;
            height = h;
            GL.ClearColor(Color.Transparent);
            SetupViewport();
            rectColor = rectDefaultColor;
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

            if (buttons != MouseButtons.Right)
            {
                selectWidth = Math.Abs(selectWidth);
                selectHeight = Math.Abs(selectHeight);
                endMouseX = mouseX + selectWidth;
                endMouseY = mouseY + selectHeight;
            }

        }

        public void MouseLeave()
        {
            mouseX = -1;
            mouseY = -1;
            mouseX = -1;
            mouseY = -1;
        }

        public void MouseDown(MouseButtons button)
        {
            buttons = button;
            if (buttons == MouseButtons.Left)
                rectColor = rectPaintColor;
            else if (buttons == MouseButtons.Right)
            {
                selectWidth = 0;
                selectHeight = 0;
                endMouseX = mouseX;
                endMouseY = mouseY;
                rectColor = rectSelectColor;
            }
            else
                rectColor = rectDefaultColor;
        }

        public void MouseUp()
        {
            selectWidth = mouseX - endMouseX;
            selectHeight = mouseY - endMouseY;

            buttons = MouseButtons.None;
            rectColor = rectDefaultColor;
        }

    }
}
