using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace ScriptUI
{
	public static class Log
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Info(string log)
		{
			File.AppendAllText("GTA5TrainerScript.txt", $"{DateTime.Now:yyyy-MM-dd HH:mm:ss}: {log}\n");
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static void Error(string log)
		{
			File.AppendAllText("GTA5TrainerScript.txt", $"{DateTime.Now:yyyy-MM-dd HH:mm:ss}: Error:\n{log}\n");
		}
	}
}
