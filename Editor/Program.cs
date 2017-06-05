﻿using System;
using System.Runtime.InteropServices;

namespace Editor
{
    class MainClass
    {
		[DllImport("kernel32.dll")]
		static extern IntPtr GetConsoleWindow();

		[DllImport("user32.dll")]
		static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

		public static void Main()
		{
			if (System.PlatformID.Unix != System.Environment.OSVersion.Platform && System.PlatformID.MacOSX != System.Environment.OSVersion.Platform)
			{
				const int SW_HIDE = 0;
				var handle = GetConsoleWindow();
				ShowWindow(handle, SW_HIDE);
			}

			var window = new MonoKnight.Window(new MonoKnight.WindowConfiguration());
			var game = new MonoKnight.Game(new MonoKnight.GameConfiguration());
			window.LoadDelegate = game.Start;
			window.RenderDelegate = game.Render;
			window.UpdateDelegate = game.Update;
			window.Run(60);
		}
    }
}