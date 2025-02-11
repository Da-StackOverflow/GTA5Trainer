using System;
using System.IO;

internal static class Log
{
	private static StreamWriter _logWriter = null;

	public static void InitLog()
	{
		try
		{
			_logWriter ??= new("GTA5TrainerScript.txt");
			_logWriter.AutoFlush = true;
		}
		catch(Exception e)
		{
			_logWriter?.Close();
			_logWriter?.Dispose();
			_logWriter = null;
			File.WriteAllText("GTA5TrainerScriptError.txt", $"{e.Message}\n{e.StackTrace}");
		}
	}

	public static void CloseLog()
	{
		_logWriter?.Flush();
		_logWriter?.Close();
		_logWriter?.Dispose();
		_logWriter = null;
	}

	public static void Info(string log)
	{
		_logWriter?.WriteLine(log);
	}
}
