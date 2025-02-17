using System;
using System.IO;

namespace Bridge
{
	public static class Log
	{
		public static void Info(string log)
		{
			File.AppendAllText("GTA5TrainerScript.txt", $"{DateTime.Now:yyyy-MM-dd HH:mm:ss}: {log}\n");
		}
		public static void Info(object log)
		{
			File.AppendAllText("GTA5TrainerScript.txt", $"{DateTime.Now:yyyy-MM-dd HH:mm:ss}: {log}\n");
		}

		internal static void Error(string log)
		{
			File.AppendAllText("GTA5TrainerBridgeError.txt", $"{DateTime.Now:yyyy-MM-dd HH:mm:ss}: {log}\n");
		}
		internal static void Error(object log)
		{
			File.AppendAllText("GTA5TrainerBridgeError.txt", $"{DateTime.Now:yyyy-MM-dd HH:mm:ss}: {log}\n");
		}
	}
}
