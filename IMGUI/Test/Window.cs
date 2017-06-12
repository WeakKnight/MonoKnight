using System;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace IMGUI.Test
{
	public class Window : GameWindow
	{

		public Window() :
		base
		(
            800,
            600,
			GraphicsMode.Default,
			"IMGUI TEST",
			GameWindowFlags.Default,
			DisplayDevice.Default,
			4,
			0,
			GraphicsContextFlags.ForwardCompatible
		)
		{
		}

		protected override void OnLoad(EventArgs e)
		{
			CursorVisible = true;
            IMGUI.Init();
		}

		protected override void OnResize(EventArgs e)
		{
            Render();
			SwapBuffers();
		}

		protected override void OnRenderFrame(FrameEventArgs e)
		{
            Render();
			SwapBuffers();
		}

		protected override void OnUpdateFrame(FrameEventArgs e)
		{
            if (IMGUI.CheckUpdate())
            {
                IMGUI.ProcessInput(this.X, this.Y);
                //if (IMGUI.Button())
                {
                    
                }
                //IMGUI.AddRect(0.0f, 0.0f, 150.0f, 50.0f,Color4.Yellow,true);
                IMGUI.End();
            }
            Console.WriteLine("x is" + OpenTK.Input.Mouse.GetCursorState().X);
            Console.WriteLine("y is" + OpenTK.Input.Mouse.GetCursorState().Y);
		}

        protected void Render()
        {
            GL.Viewport(0,0,Width,Height);
            GL.Enable(EnableCap.DepthTest);
            GL.ClearColor(0.1f, 0.3f, 0.4f, 0.0f);
            GL.Clear(ClearBufferMask.ColorBufferBit|ClearBufferMask.DepthBufferBit);
            DrawCommandRenderer.DrawText(0.0f, 0.0f, "hahahahahahha");
            //DrawCommandRenderer.DrawRect(0.0f, 0.0f, 150.0f, 50.0f,Color4.Yellow);
            IMGUI.Render();
        }
	}
}
