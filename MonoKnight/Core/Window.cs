using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Graphics;
using OpenTK.Input;
using ProtoBuf;
using System.IO;
using System.Xml;

namespace MonoKnight
{
	public delegate void WindowDelegate();

	public class Window : GameWindow
	{
		
		public Window(WindowConfiguration config) :
		base
		(
			config.DefaultWidth, 
			config.DefaultHeight, 
			GraphicsMode.Default,
			config.DefaultTitle,  
			GameWindowFlags.Default,
			DisplayDevice.Default,
			4, 
			0,
			GraphicsContextFlags.ForwardCompatible
		)
		{
			W = Width;
			H = Height;
		}

		public WindowDelegate LoadDelegate;
		public WindowDelegate UpdateDelegate;
		public WindowDelegate ResizeDelegate;
		public WindowDelegate RenderDelegate;

		protected override void OnLoad(EventArgs e)
		{
            CursorVisible = true;
			if (LoadDelegate != null)
			{
				LoadDelegate();
			}
        }

		public static int W = 0;
		public static int H = 0;

		protected override void OnResize(EventArgs e)
		{
			W = Width;
			H = Height;
			if (RenderDelegate != null)
			{
				RenderDelegate();
			}
            SwapBuffers();
		}

		protected override void OnRenderFrame(FrameEventArgs e)
		{
			if (RenderDelegate != null)
			{
				RenderDelegate();
			}
            SwapBuffers();
		}

		protected override void OnUpdateFrame(FrameEventArgs e)
		{
			//TODO Time Manager
			Time.DeltaTime = (float)e.Time;
			if (UpdateDelegate != null)
			{
                UpdateDelegate();
			}
		}
	}
}
