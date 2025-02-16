using System;
using System.IO;

namespace Bridge
{
	public static class Log
	{
		static Log()
		{
			if (File.Exists("GTA5TrainerScript.txt"))
			{
				File.Delete("GTA5TrainerScript.txt");
			}

			if (File.Exists("GTA5TrainerBridgeError.txt"))
			{
				File.Delete("GTA5TrainerBridgeError.txt");
			}
		}

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
