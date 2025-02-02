public class Entry
{
	private static StreamWriter _logWriter;

	public static void Log(string message)
	{
		_logWriter.WriteLine($"{message}");
	}

	public static void OnStart()
	{
		_logWriter = new("GTA5.log")
		{
			AutoFlush = true
		};
		Log("加载脚本");
	}

	public static void OnUpdate()
	{

	}

	public static void OnDestroy()
	{
		Native.Release();
		Log("卸载脚本");
		_logWriter.Flush();
		_logWriter.Close();
		_logWriter = null;
	}

	public static void OnInput(uint key, bool isUpNow)
	{
		InputSystem.ProcessInput(key, isUpNow);
	}
}