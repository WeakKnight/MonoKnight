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

        public static void Main()
		{
			var entity = new Entity();
			var com = entity.AddComponent<Component>();
			Object.Destroy(com);
			if (System.PlatformID.Unix != System.Environment.OSVersion.Platform && System.PlatformID.MacOSX != System.Environment.OSVersion.Platform)
            {
                const int SW_HIDE = 0;
                var handle = GetConsoleWindow();
                ShowWindow(handle, SW_HIDE);
            }

            new Window().Run(60);
		}
	}
}
