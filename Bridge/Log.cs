using System;
using System.IO;

namespace Bridge
{
	public static class Log
	{
		public static void Info(string log)
		{
			File.AppendAllText("Script.txt", $"{DateTime.Now:yyyy-MM-dd HH:mm:ss}: {log}");
		}
		public static void Info(object log)
		{
			File.AppendAllText("Script.txt", $"{DateTime.Now:yyyy-MM-dd HH:mm:ss}: {log}");
		}

		internal static void Error(string log)
		{
			File.AppendAllText("Bridge.txt", $"{DateTime.Now:yyyy-MM-dd HH:mm:ss}: {log}");
		}
		internal static void Error(object log)
		{
			File.AppendAllText("Bridge.txt", $"{DateTime.Now:yyyy-MM-dd HH:mm:ss}: {log}");
		}
	}
}
