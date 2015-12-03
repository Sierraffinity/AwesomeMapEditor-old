using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace PGMEBackend.GLControls
{
    class GLMapEditor
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

        public GLMapEditor(int w, int h)
        {
            width = w;
            height = h;
            GL.ClearColor(Color.Transparent);
            SetupViewport();
            rectColor = rectDefaultColor;
        }

        public static implicit operator bool (GLMapEditor b)
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
                Program.mainGUI.SetGLMapEditorSize(layout.layoutWidth * 16, layout.layoutHeight * 16);

                width = layout.layoutWidth * 16;
                height = layout.layoutHeight * 16;
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

            if (buttons == MouseButtons.Left)
            {
                Paint();
            }

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
                        if (i <= v.xpos + v.Width && i >= v.xpos && j >= v.ypos && j <= v.ypos + v.Height)
                        {
                            v.Redraw = true;
                            Console.WriteLine("Redrawing " + v.buffer.FBOHandle);
                            continue;
                        }
                    }
                }
            }
        }

        public void MouseDown(MouseButtons button)
        {
            buttons = button;
            if (buttons == MouseButtons.Left)
            {
                rectColor = rectPaintColor;
                Paint();
            }
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
