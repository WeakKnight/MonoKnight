using System;
using OpenTK;
using OpenTK.Graphics;

namespace MonoKnight
{
	public class DesktopWindow:GameWindow
	{
		public DesktopWindow(Application app)
		:
		base
		(
			800, // initial width
			600, // initial height
			GraphicsMode.Default,
			"MonoKnight",  // initial title
			GameWindowFlags.Default,
			DisplayDevice.Default,
			4, // OpenGL major version
			0, // OpenGL minor version
			GraphicsContextFlags.ForwardCompatible
		)
		{
			attachedApp = app;
		}

		protected override void OnLoad(EventArgs e)
		{
			attachedApp.OnLoad();
		}

		protected override void OnResize(EventArgs e)
		{
			W = Width;
			H = Height;
			attachedApp.OnRender();
		}

		protected override void OnRenderFrame(FrameEventArgs e)
		{
			attachedApp.OnRender();
		}

		public int W = 0;
		public int H = 0;

		private Application attachedApp = null;
	}
}
