internal struct KeyStates
{
	public int key;
	public long time;

	public static bool operator ==(KeyStates left, KeyStates right)
	{
		return left.key == right.key;
	}

	public static bool operator !=(KeyStates left, KeyStates right)
	{
		return left.key != right.key;
	}

	public override readonly bool Equals(object obj)
	{
		if(obj is null)
		{
			return false;
		}
		if(obj is KeyStates state)
		{
			return key == state.key;
		}
		return false;
	}

	public override readonly int GetHashCode()
	{
		return key.GetHashCode();
	}
}

internal class InputSystem
{
	private const uint KeyCodeType = 255;
	private const uint MaxPeriodTime = 100;

	private static readonly KeyStates[] _keyStates = new KeyStates[KeyCodeType];

	static InputSystem()
	{
		for (int i = 0; i < KeyCodeType; i++)
		{
			_keyStates[i] = new KeyStates
			{
				time = 0,
				key = i
			};
		}
	}

	public static void ProcessInput(uint key, bool isUpNow)
	{
		if (!isUpNow)
		{
			_keyStates[key].time = Native.GetTickCount();
		}
	}

	public static bool IsKeyDown(KeyCode keyCode)
	{
		uint key = (uint)keyCode;
		bool result = Native.GetTickCount() - _keyStates[key].time < MaxPeriodTime;
		return result;
	}

	public static bool IsAccept()
	{
		return IsKeyDown(KeyCode.Num5) || IsKeyDown(KeyCode.Return);
	}

	public static bool IsBack()
	{
		return IsKeyDown(KeyCode.Num0) || IsKeyDown(KeyCode.Back);
	}

	public static bool IsUp()
	{
		return IsKeyDown(KeyCode.Num8) || IsKeyDown(KeyCode.Up);
	}

	public static bool IsDown()
	{
		return IsKeyDown(KeyCode.Num2) || IsKeyDown(KeyCode.Down);
	}

	public static bool IsLeft()
	{
		return IsKeyDown(KeyCode.Num4) || IsKeyDown(KeyCode.Left);
	}

	public static bool IsRight()
	{
		return IsKeyDown(KeyCode.Num6) || IsKeyDown(KeyCode.Right);
	}

	public static bool IsShift()
	{
		return IsKeyDown(KeyCode.Shift);
	}

	public static bool IsSpace()
	{
		return IsKeyDown(KeyCode.Space);
	}

	public static bool MenuSwitchPressed()
	{
		return IsKeyDown(KeyCode.F4);
	}
}
