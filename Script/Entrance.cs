public class Entrance
{
	public void OnInit()
	{
		Log.InitLog();
		Log.Info("Entrance.OnInit");
	}
	
	public void OnUpdate()
	{
		
	}

	public void OnDestroy()
	{
		Log.Info("Entrance.OnDestroy");
		Log.CloseLog();
	}

	public void OnInput(uint key, bool isUpNow)
	{
		if (isUpNow)
		{
			Input.OnKeyUp(key);
			return;
		}
		Input.OnKeyDown(key);
	}
}
