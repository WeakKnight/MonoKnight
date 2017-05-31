using System;
using System.Diagnostics;

namespace MonoKnight
{
	public static class Debug
	{
		public static void Assert(bool condition)
		{
			System.Diagnostics.Debug.Assert(condition);
		}

		public static void Log(string str) 
		{
			Console.WriteLine(str);	
		}
	}
}
