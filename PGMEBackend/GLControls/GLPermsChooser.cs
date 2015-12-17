using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace PGMEBackend.GLControls
{
    class GLPermsChooser
    {
        Color rectColor;

        int width = 0;
        int height = 0;

        public MapEditorTools tool = MapEditorTools.None;

        public int mouseX = -1;
        public int mouseY = -1;
        public int selectX = 0;
        public int selectY = 0;
        public int editorSelectWidth = 1;
        public int editorSelectHeight = 1;
        public byte[] selectArray = { 0 };

        public Color rectDefaultColor = Color.FromArgb(0, 255, 0);
        public Color rectPaintColor = Color.FromArgb(255, 0, 0);

        public GLPermsChooser(int w, int h)
        {
            width = w;
            height = h;
            GL.ClearColor(Color.Transparent);
            SetupViewport();
            rectColor = rectPaintColor;
        }

        public static implicit operator bool (GLPermsChooser b)
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
            for (int i = 0; i < 0x40; i++)
                Program.glMapEditor.movementPerms.Draw(i, (i % 4) * 16, (i / 4) * 16, 1);

            int x = selectX * 16;
            int y = selectY * 16;
            
            Surface.DrawOutlineRect(x , y, 16, 16, rectColor);

            if (tool == MapEditorTools.None)
            {
                Surface.DrawOutlineRect(mouseX * 16, mouseY * 16, 16, 16, rectDefaultColor);
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
                selectX = mouseX;
                selectY = mouseY;
            }
        }

        public void MouseLeave()
        {
            mouseX = -1;
            mouseY = -1;
        }

        public void MouseDown(MapEditorTools Tool)
        {
            if (tool == MapEditorTools.None)
            {
                tool = Tool;
                if (tool == MapEditorTools.Pencil)
                {
                    selectX = mouseX;
                    selectY = mouseY;
                    rectColor = rectPaintColor;
                }
                else
                    rectColor = rectDefaultColor;
            }
        }

        public void MouseUp(MapEditorTools Tool)
        {
            if (tool == Tool)
            {
                selectX = mouseX;
                selectY = mouseY;
                editorSelectWidth = 1;
                editorSelectHeight = 1;
                selectArray = new byte[] { (byte)(selectX + (selectY * 4)) };

                tool = MapEditorTools.None;
                rectColor = rectPaintColor;
            }
        }

        public void SelectPerm(int permNum)
        {
            if (permNum >= 0)
            {
                selectX = permNum % 4;
                selectY = permNum / 4;
                Program.mainGUI.ScrollPermChooserToPerm(permNum);
            }
            else
            {
                selectX = -1;
                selectY = -1;
            }

            Program.mainGUI.RefreshPermsChooserControl();
        }
    }
}
