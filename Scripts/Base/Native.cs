using System.Text;
using System.Runtime.InteropServices;
using System;


internal unsafe static class Native
{
	#region DLLImport

	private const string Libiary = "ScriptHookV.dll";

	[DllImport(Libiary, EntryPoint = "?createTexture@@YAHPEBD@Z")]
	private static extern int createTexture(char* texFileName);

	[DllImport(Libiary, EntryPoint = "?drawTexture@@YAXHHHHMMMMMMMMMMMM@Z")]
	private static extern void drawTexture(int id, int index, int level, int time,
	float sizeX, float sizeY, float centerX, float centerY,
	float posX, float posY, float rotation, float screenHeightScaleFactor,
	float r, float g, float b, float a);

	[DllImport(Libiary, EntryPoint = "?scriptWait@@YAXK@Z")]
	private static extern void ThreadSleep(uint time);

	[DllImport(Libiary, EntryPoint = "?nativeInit@@YAX_K@Z")]
	private static extern void InitFunc(ulong hash);

	[DllImport(Libiary, EntryPoint = "?nativePush64@@YAX_K@Z")]
	private static extern void PushArg(ulong val);

	[DllImport(Libiary, EntryPoint = "?nativeCall@@YAPEA_KXZ")]
	private static extern ulong* Invoke();

	// Returns pointer to global variable
	// make sure that you check game version before accessing globals because
	// ids may differ between patches
	[DllImport(Libiary, EntryPoint = "?getGlobalPtr@@YAPEA_KH@Z")]
	private static extern ulong* GetGamePtr(int globalId);

	/* world */

	// Get entities from internal pools
	// return value represents filled array elements count
	// can be called only in the same thread as natives
	[DllImport(Libiary, EntryPoint = "?worldGetAllVehicles@@YAHPEAHH@Z")]
	private static extern int WorldGetAllVehicles(int* arr, int arrSize);

	[DllImport(Libiary, EntryPoint = "?worldGetAllPeds@@YAHPEAHH@Z")]
	private static extern int WorldGetAllPeds(int* arr, int arrSize);

	[DllImport(Libiary, EntryPoint = "?worldGetAllObjects@@YAHPEAHH@Z")]
	private static extern int WorldGetAllObjects(int* arr, int arrSize);

	[DllImport(Libiary, EntryPoint = "?worldGetAllPickups@@YAHPEAHH@Z")]
	private static extern int WorldGetAllPickups(int* arr, int arrSize);

	public enum GameVersion : int
	{
		VER_1_0_335_2_STEAM,
		VER_1_0_335_2_NOSTEAM,

		VER_1_0_350_1_STEAM,
		VER_1_0_350_2_NOSTEAM,

		VER_1_0_372_2_STEAM,
		VER_1_0_372_2_NOSTEAM,

		VER_1_0_393_2_STEAM,
		VER_1_0_393_2_NOSTEAM,

		VER_1_0_393_4_STEAM,
		VER_1_0_393_4_NOSTEAM,

		VER_1_0_463_1_STEAM,
		VER_1_0_463_1_NOSTEAM,

		VER_1_0_505_2_STEAM,
		VER_1_0_505_2_NOSTEAM,

		VER_1_0_573_1_STEAM,
		VER_1_0_573_1_NOSTEAM,

		VER_1_0_617_1_STEAM,
		VER_1_0_617_1_NOSTEAM,

		VER_SIZE,
		VER_UNK = -1
	};

	[DllImport(Libiary, EntryPoint = "?getGameVersion@@YA?AW4eGameVersion@@XZ")]
	private static extern GameVersion getGameVersion();

	private static ulong VarString(byte* ptr)
	{
		return (ulong)ptr;
	}

	private static void FreeBuffer(IntPtr ptr)
	{
		Marshal.FreeHGlobal(ptr);
	}

	private static IntPtr CreateBuffer(int size)
	{
		return Marshal.AllocHGlobal(size);
	}

	#endregion

	private delegate ulong* DInvoke();
	private delegate ulong* DGetGamePtr(int globalId);
	private delegate int DWorldGetAllVehicles(int* arr, int arrSize);
	private delegate int DWorldGetAllPeds(int* arr, int arrSize);
	private delegate int DWorldGetAllObjects(int* arr, int arrSize);
	private delegate int DWorldGetAllPickups(int* arr, int arrSize);
	private delegate ulong DVarString(byte* ptr);

	private static Action<uint> _ThreadSleep;
	private static Action<ulong> _InitFunc;
	private static Action<ulong> _PushArg;
	private static DInvoke _Invoke;
	private static DGetGamePtr _GetGamePtr;
	private static DWorldGetAllVehicles _WorldGetAllVehicles;
	private static DWorldGetAllPeds _WorldGetAllPeds;
	private static DWorldGetAllObjects _WorldGetAllObjects;
	private static DWorldGetAllPickups _WorldGetAllPickups;
	private static DVarString _VarString;

	static Native()
	{
		_ThreadSleep += ThreadSleep;
		_InitFunc += InitFunc;
		_PushArg += PushArg;
		_Invoke += Invoke;
		_GetGamePtr += GetGamePtr;
		_WorldGetAllVehicles += WorldGetAllVehicles;
		_WorldGetAllPeds += WorldGetAllPeds;
		_WorldGetAllObjects += WorldGetAllObjects;
		_WorldGetAllPickups += WorldGetAllPickups;
		_VarString += VarString;
	}

	public static void Sleep(uint time)
	{
		_ThreadSleep(time);
	}

	public static long GetTickCount()
	{
		return DateTime.Now.Ticks;
	}

	private static readonly Dictionary<string, int> _stringPool = [];
	private static readonly Encoding _encoding = Encoding.UTF8;

	private static IntPtr _buffer = IntPtr.Zero;
	private static int _bufferSize;
	private static int _bufferPosition;

	public static void Release()
	{
		if (_bufferSize > 0)
		{
			FreeBuffer(_buffer);
			_buffer = IntPtr.Zero;
			_bufferSize = 0;
			_bufferPosition = 0;
		}
		_ThreadSleep -= ThreadSleep;
		_InitFunc -= InitFunc;
		_PushArg -= PushArg;
		_Invoke -= Invoke;
		_GetGamePtr -= GetGamePtr;
		_WorldGetAllVehicles -= WorldGetAllVehicles;
		_WorldGetAllPeds -= WorldGetAllPeds;
		_WorldGetAllObjects -= WorldGetAllObjects;
		_WorldGetAllPickups -= WorldGetAllPickups;
		_VarString -= VarString;
	}

	private static byte* GetBytePtr(int position)
	{
		if (_bufferSize <= 0)
		{
			return default;
		}
		return (byte*)_buffer.ToPointer() + position;
	}

	private const int MinBufferSize = 10240;

	private static int StoreBytes(byte[] bytes)
	{
		if (_bufferSize <= 0)
		{
			_buffer = CreateBuffer(MinBufferSize);
			_bufferSize = MinBufferSize;
			_bufferPosition = 0;
		}
		if (_bufferPosition + bytes.Length >= _bufferSize)
		{
			var newBufferSize = (_bufferPosition + bytes.Length) * 2;
			var newBuffer = CreateBuffer(newBufferSize);
			
			if(_buffer != IntPtr.Zero)
			{
				var p1 = (byte*)_buffer.ToPointer();
				var p2 = (byte*)newBuffer.ToPointer();
				for (int i = 0; i < _bufferPosition; i++)
				{
					*(p2 + i) = *(p1 + i);
				}
				FreeBuffer(_buffer);
			}

			_buffer = newBuffer;
			_bufferSize = newBufferSize;
		}

		var p = (byte*)_buffer.ToPointer();
		for (int i = 0; i < bytes.Length; i++)
		{
			*(p + _bufferPosition + i) = bytes[i];
		}
		var startIndex = _bufferPosition;
		_bufferPosition += bytes.Length;
		return startIndex;
	}

	public static byte* GetBytePtr(string s)
	{
		if (_stringPool.TryGetValue(s, out var offset))
		{
			return GetBytePtr(offset);
		}

		offset = StoreBytes(_encoding.GetBytes($"{s}\0"));
		if (offset < 0)
		{
			return default;
		}

		_stringPool.Add(s, offset);
		return GetBytePtr(offset);
	}

	public static ulong* GetGlobalPtr(int globalId)
	{
		return _GetGamePtr(globalId);
	}

	public static ulong V(void* ptr)
	{
		return (ulong)ptr;
	}

	public static ulong V(long* ptr)
	{
		return (ulong)ptr;
	}

	public static ulong V(int* ptr)
	{
		return (ulong)ptr;
	}

	public static ulong V(uint* ptr)
	{
		return (ulong)ptr;
	}

	public static ulong V(char* ptr)
	{
		return (ulong)ptr;
	}

	public static ulong V(byte* ptr)
	{
		return (ulong)ptr;
	}

	public static ulong V(float* ptr)
	{
		return (ulong)ptr;
	}

	public static ulong V(double* ptr)
	{
		return (ulong)ptr;
	}

	public static ulong V(short* ptr)
	{
		return (ulong)ptr;
	}

	public static ulong V(ushort* ptr)
	{
		return (ulong)ptr;
	}
	public static ulong V(Vector3* ptr)
	{
		return (ulong)ptr;
	}

	public static ulong V<T>(T value) where T : unmanaged
	{
		return *(ulong*)&value;
	}

	public static ulong V(string value, bool showAtScreen = false)
	{
		if (showAtScreen)
		{
			return _VarString(GetBytePtr(value));
		}
		return V(GetBytePtr(value));
	}

	public static void Invoke(ulong function, params ulong[] args)
	{
		_InitFunc(function);
		for (int i = 0; i < args.Length; i++)
		{
			_PushArg(args[i]);
		}
		_Invoke();
	}

	public static T* PInvoke<T>(ulong function, params ulong[] args) where T : unmanaged
	{
		_InitFunc(function);
		for (int i = 0; i < args.Length; i++)
		{
			_PushArg(args[i]);
		}
		return (T*)_Invoke();
	}

	public static T Invoke<T>(ulong function, params ulong[] args) where T : unmanaged
	{
		return *PInvoke<T>(function, args);
	}
}
