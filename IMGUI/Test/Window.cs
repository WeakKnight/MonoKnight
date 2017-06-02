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
		}

        protected void Render()
        {
            GL.ClearColor(0.0f, 0.0f, 0.0f, 0.0f);
            GL.Clear(ClearBufferMask.ColorBufferBit|ClearBufferMask.DepthBufferBit);
        }
	}
}
