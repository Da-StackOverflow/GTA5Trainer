using System;
using System.IO;

namespace ScriptUI
{
	public static class Log
	{
		public static void Info(string log)
		{
			File.AppendAllText("GTA5TrainerScript.txt", $"{DateTime.Now:yyyy-MM-dd HH:mm:ss}: {log}\n");
		}

		internal static void Error(string log)
		{
			File.AppendAllText("GTA5TrainerScript.txt", $"{DateTime.Now:yyyy-MM-dd HH:mm:ss}: Error:\n{log}\n");
		}
	}
}
