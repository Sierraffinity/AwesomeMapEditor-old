// Surface Library
// A tiny wrapper for OpenTK.

// Copyright (C) 2015 shadowndacorner

// This file is part of Surface Library.
// Surface Library is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.

// Surface Library is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.

// You should have received a copy of the GNU General Public License
// along with Surface Library. If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace PGMEBackend
{
    public static class Surface
    {
        /*public static BitmapFont CreateFont(string name, string font, int size, params object[] options)
        {
            return BitmapFont.CreateFont(name, font, size, options);
        }*/

        public static Texture2D ActiveTexture;
        public static void SetColor(double r, double g, double b)
        {
            GL.Color3(r, g, b);
        }

        public static void SetColor(double r, double g, double b, double a)
        {
            GL.Color4(r, g, b, a);
        }

        public static void SetColor(Color col)
        {
            GL.Color4(col);
        }

        public static void SetTexture(Texture2D tex)
        {
            if (tex == null)
                GL.BindTexture(TextureTarget.Texture2D, 0);
            else
                if (ActiveTexture != tex)
                GL.BindTexture(TextureTarget.Texture2D, tex);
        }

        public static void SetTexture()
        {
            SetTexture(null);
        }
        
        public static void DrawOutlineRect(double x, double y, double w, double h, Color col)
        {
            SetColor(col);
            DrawOutlineRect(x, y, w, h);
        }

        public static void DrawOutlineRect(double x, double y, double w, double h)
        {
            if (w < 0)
            {
                w = -w;
                x -= w;
            }
            if (h < 0)
            {
                h = -h;
                y -= h;
            }

            x++;
            w--;
            h--;

            GL.PushMatrix();

            GL.Translate(x, y, 0);
            GL.Scale(w, h, 1);

            GL.Begin(PrimitiveType.LineStrip);
            GL.TexCoord2(0, 1);
            GL.Vertex2(0, 0);

            GL.TexCoord2(1, 1);
            GL.Vertex2(1, 0);

            GL.TexCoord2(1, 0);
            GL.Vertex2(1, 1);

            GL.TexCoord2(0, 0);
            GL.Vertex2(0, 1);

            GL.End();

            GL.Begin(PrimitiveType.Lines);

            GL.Vertex2(0, 1 + 1 / h);

            GL.Vertex2(0, 0);

            GL.End();

            GL.PopMatrix();
        }

        public static bool EnableQuadWireframe = false;
        public static void DrawQuad(double blx, double bly, double brx, double bry, double trx, double trY, double tlx, double tly)
        {
            GL.Begin(PrimitiveType.Quads);
            GL.TexCoord2(blx, bly);
            GL.Vertex2(0, 0);

            GL.TexCoord2(brx, bry);
            GL.Vertex2(1, 0);

            GL.TexCoord2(trx, trY);
            GL.Vertex2(1, 1);

            GL.TexCoord2(tlx, tly);
            GL.Vertex2(0, 1);
            GL.End();

            if (EnableQuadWireframe)
            {
                SetColor(Color.White);
                SetTexture();
                GL.Begin(PrimitiveType.LineLoop);
                GL.TexCoord2(blx, bly);
                GL.Vertex2(0, 0);

                GL.TexCoord2(brx, bry);
                GL.Vertex2(1, 0);

                GL.TexCoord2(trx, trY);
                GL.Vertex2(1, 1);

                GL.TexCoord2(tlx, tly);
                GL.Vertex2(0, 1);
                GL.End();

                GL.Begin(PrimitiveType.LineLoop);
                GL.TexCoord2(blx, bly);
                GL.Vertex2(0, 0);

                GL.TexCoord2(brx, bry);
                GL.Vertex2(1, 0);

                GL.TexCoord2(trx, trY);
                GL.Vertex2(1, 1);
                GL.End();
            }
        }

        public static void DrawRect(double x, double y, double w, double h, double blx, double bly, double brx, double bry, double trx, double trY, double tlx, double tly)
        {
            GL.PushMatrix();

            GL.Translate(x, y, 0);
            GL.Scale(w, h, 1);

            DrawQuad(blx, bly, brx, bry, trx, trY, tlx, tly);

            GL.PopMatrix();
        }

        public static void DrawRect(double x, double y, double w, double h)
        {
            DrawRect(x, y, w, h, 0, 0, 1, 0, 1, 1, 0, 1);
        }


        public static void DrawRect(double x, double y, double w, double h, Color col)
        {
            SetColor(col);
            DrawRect(x, y, w, h);
        }

        public static void DrawTexturedRectUV(Texture2D texture, double x, double y, double w, double h, double blx, double bly, double brx, double bry, double trx, double trY, double tlx, double tly, Color col)
        {
            SetColor(col);
            SetTexture(texture);
            DrawRect(x, y, w, h, blx, bly, brx, bry, trx, trY, tlx, tly);
            SetTexture();
        }

        public static void DrawTexturedRectUV(Texture2D texture, double x, double y, double w, double h, double blx, double bly, double brx, double bry, double trx, double trY, double tlx, double tly)
        {
            DrawTexturedRectUV(texture, x, y, w, h, blx, bly, brx, bry, trx, trY, tlx, tly, Color.White);
        }

        public static void DrawTexturedRectUVRotated(Texture2D texture, double x, double y, double w, double h, double blx, double bly, double brx, double bry, double trx, double trY, double tlx, double tly, Color col)
        {
            SetColor(col);
            SetTexture(texture);
            DrawRect(x, y, w, h, blx, bly, brx, bry, trx, trY, tlx, tly);
            SetTexture();
        }

        public static void DrawTexturedRectUVRotated(Texture2D texture, double x, double y, double w, double h, double blx, double bly, double brx, double bry, double trx, double trY, double tlx, double tly)
        {
            DrawTexturedRectUV(texture, x, y, w, h, blx, bly, brx, bry, trx, trY, tlx, tly, Color.White);
        }

        public static void DrawTexturedRect(Texture2D texture, double x, double y, double w, double h, Color col)
        {
            SetColor(col);
            SetTexture(texture);
            DrawRect(x, y, w, h, col);
            SetTexture();
        }

        public static void DrawTexturedRect(Texture2D texture, double x, double y, double w, double h)
        {
            DrawTexturedRect(texture, x, y, w, h, Color.White);
        }
        /*
        public static void DrawTexturedRectRotated(Texture2D texture, double x, double y, double w, double h, Quaternion rot, Color col, double blx, double bly, double brx, double bry, double trx, double trY, double tlx, double tly)
        {
            SetColor(col);
            SetTexture(texture);
            GL.PushMatrix();
            GL.Translate(x, y, 0);
            GL.Scale(w, h, 1);
            var euler = RPGEngine.Game.Components.Transform.ToEuler(rot);
            GL.Rotate(euler.X, Vector3.UnitX);
            GL.Rotate(euler.Y, Vector3.UnitY);
            GL.Rotate(euler.Z, Vector3.UnitZ);

            GL.Translate(-0.5f, -0.5f, 0);
            DrawQuad(blx, bly, brx, bry, trx, trY, tlx, tly);

            GL.PopMatrix();
            GL.PushMatrix();
        }
        */
        public static void DrawTexturedRectRotated(Texture2D texture, double x, double y, double w, double h, double rot, Color col, double blx, double bly, double brx, double bry, double trx, double trY, double tlx, double tly)
        {
            SetColor(col);
            SetTexture(texture);
            GL.PushMatrix();
            GL.Translate(x, y, 0);
            GL.Scale(w, h, 1);
            GL.Rotate(rot, Vector3d.UnitZ);

            GL.Translate(-0.5f, -0.5f, 0);
            DrawQuad(blx, bly, brx, bry, trx, trY, tlx, tly);

            GL.PopMatrix();
            /*GL.PushMatrix();

            
            SetColor(1, 0, 0, 1);
            GL.Translate(x, y, 0);
            GL.Scale(w / 2, h / 2, 1);
            GL.Rotate(rot, Vector3d.UnitZ);
 
            GL.Begin(PrimitiveType.Quads);
            GL.TexCoord2(0, 0);
            GL.Vertex2(-1, -1);
 
            GL.TexCoord2(1, 0);
            GL.Vertex2(1, -1);
 
            GL.TexCoord2(1, 1);
            GL.Vertex2(1, 1);
 
            GL.TexCoord2(0, 1);
            GL.Vertex2(-1, 1);
            GL.End();
 
            GL.PopMatrix();
            SetTexture();
            //*/
        }

        //(0, 0, 1, 0, 1, 1, 0, 1);
        public static void DrawTexturedRectRotated(Texture2D texture, double x, double y, double w, double h, double rot, Color col)
        {
            DrawTexturedRectRotated(texture, x, y, w, h, rot, col, 0, 0, 1, 0, 1, 1, 0, 1);
        }

        public static void DrawTexturedRectRotated(Texture2D texture, double x, double y, double w, double h, Color col)
        {
            DrawTexturedRectRotated(texture, x, y, w, h, 0, col);
        }

        public static void DrawTexturedRectRotated(Texture2D texture, double x, double y, double w, double h, double rot)
        {
            DrawTexturedRectRotated(texture, x, y, w, h, rot, Color.White);
        }

        public static void DrawTexturedRectRotated(Texture2D texture, double x, double y, double w, double h)
        {
            DrawTexturedRectRotated(texture, x, y, w, h, 0);
        }

        public static void DrawTexturedRectRotated(Texture2D texture, Vector2 position, Vector2 scale)
        {
            DrawTexturedRectRotated(texture, position.X, position.Y, scale.X, scale.Y);
        }

        //public static void DrawLine(double x, double y, double xt, double yt)
        //{
        //DrawLine(x, y, xt, yt);
        //}

        public static void DrawLine(params double[] pos)
        {
            if (pos.Length % 2 != 0)
                throw new ArgumentException("Incorrect number of arguments");
            GL.Begin(PrimitiveType.LineStrip);
            for (int i = 0; i < pos.Length; i += 2)
            {
                GL.Vertex2(pos[i], pos[i + 1]);
            }
            GL.End();
        }

        public static void DrawLine(params Vector2[] pos)
        {
            GL.Begin(PrimitiveType.LineStrip);
            for (int i = 0; i < pos.Length; i++)
                GL.Vertex2(pos[i]);
            GL.End();
        }
    }
}
