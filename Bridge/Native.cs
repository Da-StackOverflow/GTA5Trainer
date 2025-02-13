using System.Text;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Reflection;
using System;

namespace Bridge
{
	internal unsafe static class Native
	{
		#region DLLImport

		private const string Libiary = "ScriptHookV.dll";

		[DllImport(Libiary, EntryPoint = "?createTexture@@YAHPEBD@Z")]
		private static extern int createTexture(byte* texFileName);

		// Draw texture
		//	id		-	texture id recieved from createTexture()
		//	index	-	each texture can have up to 64 different instances on screen at one time
		//	level	-	draw level, being used in global draw order, texture instance with least level draws first
		//	time	-	how much time (ms) texture instance will stay on screen, the amount of time should be enough
		//				for it to stay on screen until the next corresponding drawTexture() call
		//	sizeX,Y	-	size in screen space, should be in the range from 0.0 to 1.0, e.g setting this to 0.2 means that
		//				texture instance will take 20% of the screen space
		//	centerX,Y -	center position in texture space, e.g. 0.5 means real texture center
		//	posX,Y	-	position in screen space, [0.0, 0.0] - top left corner, [1.0, 1.0] - bottom right,
		//				texture instance is positioned according to it's center
		//	rotation -	should be in the range from 0.0 to 1.0
		//	screenHeightScaleFactor - screen aspect ratio, used for texture size correction, you can get it using natives
		//	r,g,b,a	-	color, should be in the range from 0.0 to 1.0
		//
		//	Texture instance draw parameters are updated each time script performs corresponding call to drawTexture()
		//	You should always check your textures layout for 16:9, 16:10 and 4:3 screen aspects, for ex. in 1280x720,
		//	1440x900 and 1024x768 screen resolutions, use windowed mode for this
		//	Can be called only in the same thread as natives

		[DllImport(Libiary, EntryPoint = "?drawTexture@@YAXHHHHMMMMMMMMMMMM@Z")]
		private static extern void drawTexture(int id, int index, int level, int time,
			float sizeX, float sizeY, float centerX, float centerY,
			float posX, float posY, float rotation, float screenHeightScaleFactor,
			float r, float g, float b, float a);

		/* scripts */

		[DllImport(Libiary, EntryPoint = "?scriptWait@@YAXK@Z")]
		private static extern void scriptWait(uint time);

		[DllImport(Libiary, EntryPoint = "?nativeInit@@YAX_K@Z")]
		private static extern void nativeInit(ulong hash);
		[DllImport(Libiary, EntryPoint = "?nativePush64@@YAX_K@Z")]
		private static extern void nativePush64(ulong val);
		[DllImport(Libiary, EntryPoint = "?nativeCall@@YAPEA_KXZ")]
		private static extern ulong* nativeCall();

		// Returns pointer to global variable
		// make sure that you check game version before accessing globals because
		// ids may differ between patches
		[DllImport(Libiary, EntryPoint = "?getGlobalPtr@@YAPEA_KH@Z")]
		private static extern ulong* getGlobalPtr(int globalId);

		/* world */

		// Get entities from internal pools
		// return value represents filled array elements count
		// can be called only in the same thread as natives
		[DllImport(Libiary, EntryPoint = "?worldGetAllVehicles@@YAHPEAHH@Z")]
		private static extern int worldGetAllVehicles(int* arr, int arrSize);
		[DllImport(Libiary, EntryPoint = "?worldGetAllPeds@@YAHPEAHH@Z")]
		private static extern int worldGetAllPeds(int* arr, int arrSize);
		[DllImport(Libiary, EntryPoint = "?worldGetAllObjects@@YAHPEAHH@Z")]
		private static extern int worldGetAllObjects(int* arr, int arrSize);
		[DllImport(Libiary, EntryPoint = "?worldGetAllPickups@@YAHPEAHH@Z")]
		private static extern int worldGetAllPickups(int* arr, int arrSize);


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

		#endregion

		private delegate void DThreadSleep(uint time);
		private delegate void DInitFunc(ulong hash);
		private delegate void DPushArg(ulong val);
		private delegate ulong* DInvoke();
		private delegate ulong* DGetGamePtr(int globalId);
		private delegate int DWorldGetAllVehicles(int* arr, int arrSize);
		private delegate int DWorldGetAllPeds(int* arr, int arrSize);
		private delegate int DWorldGetAllObjects(int* arr, int arrSize);
		private delegate int DWorldGetAllPickups(int* arr, int arrSize);

		private static Action<uint> _ThreadSleep;
		private static Action<ulong> _InitFunc;
		private static Action<ulong> _PushArg;
		private static DInvoke _Invoke;
		private static DGetGamePtr _GetGamePtr;
		private static DWorldGetAllVehicles _WorldGetAllVehicles;
		private static DWorldGetAllPeds _WorldGetAllPeds;
		private static DWorldGetAllObjects _WorldGetAllObjects;
		private static DWorldGetAllPickups _WorldGetAllPickups;

		static Native()
		{
			_ThreadSleep += scriptWait;
			_InitFunc += nativeInit;
			_PushArg += nativePush64;
			_Invoke += nativeCall;
			_GetGamePtr += getGlobalPtr;
			_WorldGetAllVehicles += worldGetAllVehicles;
			_WorldGetAllPeds += worldGetAllPeds;
			_WorldGetAllObjects += worldGetAllObjects;
			_WorldGetAllPickups += worldGetAllPickups;
		}

		public static void Sleep(uint time)
		{
			_ThreadSleep(time);
		}

		private static readonly Dictionary<string, int> _stringPool = [];
		private static readonly Encoding _encoding = Encoding.UTF8;

		private static IntPtr _buffer = IntPtr.Zero;
		private static int _bufferSize;
		private static int _bufferPosition;

		public static void Release()
		{
			FreeBuffer();
			_ThreadSleep -= scriptWait;
			_InitFunc -= nativeInit;
			_PushArg -= nativePush64;
			_Invoke -= nativeCall;
			_GetGamePtr -= getGlobalPtr;
			_WorldGetAllVehicles -= worldGetAllVehicles;
			_WorldGetAllPeds -= worldGetAllPeds;
			_WorldGetAllObjects -= worldGetAllObjects;
			_WorldGetAllPickups -= worldGetAllPickups;
		}

		private static byte* GetBytePtr(int position)
		{
			if (_bufferSize == 0)
			{
				return null;
			}
			return (byte*)_buffer.ToPointer() + position;
		}

		private static void CreateNewBuffer()
		{
			if(_buffer != IntPtr.Zero)
			{
				_bufferSize *= 2;
				var newBuffer = Marshal.AllocHGlobal(_bufferSize);
				var oldP = (byte*)_buffer.ToPointer();
				var newP = (byte*)newBuffer.ToPointer();
				for (int i = 0; i < _bufferPosition; i++)
				{
					*newP++ = *oldP++;
				}
				Marshal.FreeHGlobal(_buffer);
				_buffer = newBuffer;
				
			}
			else
			{
				_bufferSize = 20480;
				_buffer = Marshal.AllocHGlobal(_bufferSize);
				_bufferPosition = 0;
			}
		}

		internal static void FreeBuffer()
		{
			if(_buffer != IntPtr.Zero)
			{
				Marshal.FreeHGlobal(_buffer);
				_buffer = IntPtr.Zero;
				_bufferSize = 0;
				_bufferPosition = 0;
			}
		}

		private static int StoreBytes(byte[] bytes)
		{
			if (_buffer == IntPtr.Zero)
			{
				CreateNewBuffer();
			}
			if (_bufferPosition + bytes.Length >= _bufferSize)
			{
				CreateNewBuffer();
			}
			var p = (byte*)_buffer.ToPointer();
			for (int i = 0; i < bytes.Length; i++)
			{
				*(p + _bufferPosition + i) = bytes[i];
			}
			var result = _bufferPosition;
			_bufferPosition += bytes.Length;
			return result;
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

		public static ulong V<T>(T value) where T : unmanaged
		{
			return *(ulong*)&value;
		}

		public static ulong V(string value)
		{
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
}
