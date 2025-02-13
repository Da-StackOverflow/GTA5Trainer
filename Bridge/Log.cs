using System;
using System.IO;

namespace Bridge
{
	public static class Log
	{
		public static void Info(string log)
		{
			File.AppendAllText("log.txt", $"{DateTime.Now:yyyy-MM-dd HH:mm:ss}: {log}");
		}
		public static void Info(object log)
		{
			File.AppendAllText("log.txt", $"{DateTime.Now:yyyy-MM-dd HH:mm:ss}: {log}");
		}
	}
}
