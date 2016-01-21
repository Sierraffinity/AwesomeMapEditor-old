using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using PGMEBackend;

namespace GraphicsTest
{
    public class Screen
    {
        public static int Width = 1;
        public static int Height = 1;
    }

    // Yes, this is lazy.  No, I don't care.
    public static class Storage
    {
        public static Dictionary<object, object> Objects = new Dictionary<object, object>();

        public static T Get<T>(object i)
        {
            if (Objects.ContainsKey(i))
                return (T)Objects[i];
            return default(T);
        }

        public static void Set(object i, object obj)
        {
            Objects[i] = obj;
        }
    }

    // Example for switching the texture attached to an FBO
    public class FBOSwitch : GameWindow
    {
        public void Initialize()
        {
            //var tex = Texture2D.Load("");
            //Storage.Set(0, );

            var fbo = new FrameBuffer(32, 32);
            Storage.Set("fbo", fbo);

            var source = Texture2D.Load("C:/Users/Ian/Pictures/ninjesus.jpg");
            Storage.Set("tex", source);
            //fbo.

            //Storage.Set(1, Texture2D.Load(""));
        }

        List<Texture2D> Textures = new List<Texture2D>();
        public void BuildTextures()
        {
            var fbo = Storage.Get<FrameBuffer>("fbo");
            var tex = Storage.Get<Texture2D>("tex");

            GL.ClearColor(1,1,0,0);
            for (int i = 0; i < 32; i++)
            {
                FrameBuffer.Active = fbo;
                GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
                GL.PushAttrib(AttribMask.ViewportBit);
                GL.Viewport(0, 0, fbo.Width, fbo.Height);
                GL.MatrixMode(MatrixMode.Projection);
                GL.LoadIdentity();
                GL.Ortho(0, fbo.Width, fbo.Height, 0, -10, 10);

                GL.MatrixMode(MatrixMode.Modelview);
                GL.LoadIdentity();
                GL.PushMatrix();
                Surface.SetColor(1, 1, 1, 1);
                Surface.SetTexture(tex);
                Surface.DrawRect(-i * 32, 0, 32*32, 32*32);
                GL.PopMatrix();
                GL.PopAttrib();
                FrameBuffer.Active = null;
                // ReleaseTexture() assigns a new color texture to the FBO and returns the current one
                // MAKE SURE TO DISPOSE OF THE OLD ONE WHEN YOU'RE DONE WITH IT
                Textures.Add(fbo.ReleaseTexture());
            }
        }

        bool HasBuilt = false;
        public void Render(double dt)
        {
            var fbo = Storage.Get<FrameBuffer>("fbo");
            var tex = Storage.Get<Texture2D>("tex");

            GL.Enable(EnableCap.Texture2D);

            GL.ClearColor(0, 0, 0, 0);
            //*
            if (!HasBuilt)
            {
                BuildTextures();
                HasBuilt = true;
            }
            //*/

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.Viewport(0, 0, Width, Height);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(0, Width, Height, 0, -10, 10);

            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();
            GL.PushMatrix();
            int i = 0;
            Surface.SetColor(1, 1, 1, 1);
            foreach (var v in Textures)
            {
                Console.WriteLine(v.ID);
                Surface.SetTexture(v);
                Surface.DrawRect(32 * i, 32, 64, 64);
                i++;
            }
            Surface.SetTexture(fbo.ColorTexture);
            Surface.DrawRect(32 * i, 32, 64, 64);
            GL.PopMatrix();
            var err = GL.GetError();
            if (err != ErrorCode.NoError)
                Console.WriteLine(err .ToString());
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            using (var form = new FBOSwitch())
            {
                form.Initialize();

                form.UpdateFrame += (s, e) =>
                {
                    Screen.Width = form.Width;
                    Screen.Height = form.Height;

                    if (OpenTK.Input.Keyboard.GetState()[OpenTK.Input.Key.AltLeft] && OpenTK.Input.Keyboard.GetState()[OpenTK.Input.Key.F4] || OpenTK.Input.Keyboard.GetState()[OpenTK.Input.Key.Escape])
                    {
                        form.Close();
                    }
                };
                form.RenderFrame += (s,e) => {
                    form.Render(e.Time);
                    form.SwapBuffers();
                };
                form.Run(60, 60);
            }
        }
    }
}
