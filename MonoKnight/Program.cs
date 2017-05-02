using System;
using OpenTK;
using System.Runtime.InteropServices;

namespace MonoKnight
{
	class MainClass
	{
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        public static void Main(string[] args)
		{
            if (PlatformID.MacOSX != Environment.OSVersion.Platform)
            {
                const int SW_HIDE = 0;
                var handle = GetConsoleWindow();
                // Hide
                ShowWindow(handle, SW_HIDE);
            }

            new Window().Run(60);
		}
	}
}
