using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace PGMEBackend
{
    public class FrameBuffer : IDisposable
    {
        private static FrameBuffer _active;
        public static FrameBuffer Active {
            get {
                return _active;
            }
            set {
                if (_active == value)
                    return;

                if (_active != null)
                    _active.Unset();

                if (value != null)
                    value.SetActive();
                _active = value;
            }
        }
        public int FBOHandle;
        private int colorTexID;
        public int ColorTextureID
        {
            get { return colorTexID; }
            set
            {
                colorTexID = value;
                ColorTexture.ID = colorTexID;
            }
        }
        private Texture2D _colTex;
        public Texture2D ColorTexture {
            get {
                if (_colTex == null)
                {
                    _colTex = new Texture2D();
                    _colTex.ID = ColorTextureID;
                    _colTex.Width = Width;
                    _colTex.Height = Height;
                }
                return _colTex;
            }
        }

        public void SetTexture(Texture2D tex)
        {
            tex.Width = Width;
            tex.Height = Height;
            _colTex = null;

            GL.Ext.BindFramebuffer(FramebufferTarget.FramebufferExt, FBOHandle);
            GL.Ext.FramebufferTexture2D(FramebufferTarget.FramebufferExt, FramebufferAttachment.ColorAttachment0Ext, TextureTarget.Texture2D, tex, 0);
            GL.Ext.BindFramebuffer(FramebufferTarget.FramebufferExt, 0);
            //*/
            _colTex = null;
            ColorTexture.Equals(null);
        }

        public Texture2D ReleaseTexture()
        {
            var old = ColorTexture;
            //*
            int _colTexID;

            GL.GenTextures(1, out _colTexID);

            if (_colTexID == 0)
                throw new Exception("ERROR");

            GL.BindTexture(TextureTarget.Texture2D, _colTexID);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Clamp);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Clamp);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba8, Width, Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, IntPtr.Zero);

            ColorTextureID = _colTexID;

            var err = GL.GetError();
            if (err != ErrorCode.NoError)
            {
                System.Windows.Forms.MessageBox.Show(err.ToString());
            }

            GL.Ext.BindFramebuffer(FramebufferTarget.FramebufferExt, FBOHandle);
            GL.Ext.FramebufferTexture2D(FramebufferTarget.FramebufferExt, FramebufferAttachment.ColorAttachment0Ext, TextureTarget.Texture2D, _colTexID, 0);
            GL.Ext.BindFramebuffer(FramebufferTarget.FramebufferExt, 0);
            //*/
            _colTex = null;
            ColorTexture.Equals(null);
            return old;
        }

        private int _width = 2;
        private int _height = 2;
        public int Width {
            get {
                return _width;
            }
            set {
                _width = value;
                ColorTexture.Width = value;
            }
        }

        public int Height
        {
            get
            {
                return _height;
            }
            set
            {
                _height = value;
                ColorTexture.Height = value;
            }
        }

        public FrameBuffer(int width, int height)
        {
            Width = width;
            Height = height;

            //Console.WriteLine(Width + ", " + Height);

            int FboHandle;
            int _colTex;

            GL.GenTextures(1, out _colTex);

            if (_colTex == 0)
                throw new Exception("ERROR");

            GL.BindTexture(TextureTarget.Texture2D, _colTex);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Clamp);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Clamp);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba8, Width, Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, IntPtr.Zero);

            // Create a FBO and attach the textures
            GL.Ext.GenFramebuffers(1, out FboHandle);
            GL.Ext.BindFramebuffer(FramebufferTarget.FramebufferExt, FboHandle);
            GL.Ext.FramebufferTexture2D(FramebufferTarget.FramebufferExt, FramebufferAttachment.ColorAttachment0Ext, TextureTarget.Texture2D, _colTex, 0);

            ColorTextureID = _colTex;
            FBOHandle = FboHandle;

            var st = GL.CheckFramebufferStatus(FramebufferTarget.FramebufferExt);
            if (st != FramebufferErrorCode.FramebufferCompleteExt)
            {
                System.Windows.Forms.MessageBox.Show(st.ToString());
            }

            var err = GL.GetError();
            if (err != ErrorCode.NoError)
            {
                System.Windows.Forms.MessageBox.Show(err.ToString());
            }

            GL.Ext.BindFramebuffer(FramebufferTarget.FramebufferExt, 0);
        }

        private void SetActive()
        {
            if (Active == this)
                return;
            GL.Ext.BindFramebuffer(FramebufferTarget.FramebufferExt, FBOHandle);
            GL.PushAttrib(AttribMask.ViewportBit);
            GL.Viewport(0, 0, Width, Height);

            GL.ClearColor(1, 0, 0, 1);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            
            GL.PushMatrix();

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            var proj = Matrix4.CreateOrthographicOffCenter(0, Width, 0, Height, -1, 1);
            GL.LoadMatrix(ref proj);
            
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();
        }

        private void Unset()
        {
            if (Active == this)
            {
                //GL.Disable(EnableCap.Texture2D);
                //GL.Disable(EnableCap.Blend);
                GL.PopMatrix();
                GL.PopAttrib();
                GL.Ext.BindFramebuffer(FramebufferTarget.FramebufferExt, 0);
                GL.ClearColor(0, 0, 0, 0);
            }
        }

        public void Dispose()
        {
            if (colorTexID != 0)
            {
                GL.DeleteTexture(colorTexID);
                colorTexID = 0;
            }

            if (FBOHandle != 0)
            {
                GL.DeleteFramebuffer(FBOHandle);
                FBOHandle = 0;
            }
        }
    }

    public class TemporaryFramebuffer : FrameBuffer {
        private FrameBuffer oldActive;
        public TemporaryFramebuffer(int width, int height)
            :base(width, height)
        {
            oldActive = Active;
            Active = this;
        }

        public Texture2D Commit()
        {
            var tex = ColorTexture;
            if (FBOHandle != 0)
            {
                GL.DeleteFramebuffer(FBOHandle);
                FBOHandle = 0;
            }
            Active = oldActive;
            return tex;
        }

        new public void Dispose()
        {
            if (FBOHandle != 0)
            {
                GL.DeleteFramebuffer(FBOHandle);
                FBOHandle = 0;
            }
        }
    }
}
