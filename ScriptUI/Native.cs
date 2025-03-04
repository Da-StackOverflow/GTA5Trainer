using System;
using System.Text;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace ScriptUI
{
	internal unsafe static class Native
	{
		#region DLLImport
		private static class ScriptHookV
		{
			private const string Libiary = "ScriptHookV.dll";

			[DllImport(Libiary, EntryPoint = "?createTexture@@YAHPEBD@Z")]
			internal static extern int createTexture(byte* texFileName);

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
			internal static extern void drawTexture(int id, int index, int level, int time, float sizeX, float sizeY, float centerX, float centerY, float posX, float posY, float rotation, float screenHeightScaleFactor, float r, float g, float b, float a);

			[DllImport(Libiary, EntryPoint = "?scriptWait@@YAXK@Z")]
			internal static extern void scriptWait(uint time);

			[DllImport(Libiary, EntryPoint = "?nativeInit@@YAX_K@Z")]
			internal static extern void nativeInit(ulong hash);
			[DllImport(Libiary, EntryPoint = "?nativePush64@@YAX_K@Z")]
			internal static extern void nativePush64(ulong val);
			[DllImport(Libiary, EntryPoint = "?nativeCall@@YAPEA_KXZ")]
			internal static extern ulong* nativeCall();

			[DllImport(Libiary, EntryPoint = "?getGlobalPtr@@YAPEA_KH@Z")]
			internal static extern ulong* getGlobalPtr(int globalId);

			[DllImport(Libiary, EntryPoint = "?worldGetAllVehicles@@YAHPEAHH@Z")]
			internal static extern int worldGetAllVehicles(int* arr, int arrSize);
			[DllImport(Libiary, EntryPoint = "?worldGetAllPeds@@YAHPEAHH@Z")]
			internal static extern int worldGetAllPeds(int* arr, int arrSize);
			[DllImport(Libiary, EntryPoint = "?worldGetAllObjects@@YAHPEAHH@Z")]
			internal static extern int worldGetAllObjects(int* arr, int arrSize);
			[DllImport(Libiary, EntryPoint = "?worldGetAllPickups@@YAHPEAHH@Z")]
			internal static extern int worldGetAllPickups(int* arr, int arrSize);

			[DllImport(Libiary, EntryPoint = "?getGameVersion@@YA?AW4eGameVersion@@XZ")]
			internal static extern GameVersion getGameVersion();
		}

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

		#endregion

		private static readonly delegate*<uint, void> _scriptWait = &ScriptHookV.scriptWait;
		private static readonly delegate*<ulong, void> _initFunc = &ScriptHookV.nativeInit;
		private static readonly delegate*<ulong, void> _pushArg = &ScriptHookV.nativePush64;
		private static readonly delegate*<ulong*> _invoke = &ScriptHookV.nativeCall;
		private static readonly delegate*<byte*, int> _createTexture = &ScriptHookV.createTexture;
		private static readonly delegate*<int, int, int, int, float, float, float, float, float, float, float, float, float, float, float, float, void> _drawTexture = &ScriptHookV.drawTexture;
		private static readonly delegate*<int, ulong*> _getGlobalPtr = &ScriptHookV.getGlobalPtr;
		private static readonly delegate*<int*, int, int> _worldGetAllVehicles = &ScriptHookV.worldGetAllVehicles;
		private static readonly delegate*<int*, int, int> _worldGetAllPeds = &ScriptHookV.worldGetAllPeds;
		private static readonly delegate*<int*, int, int> _worldGetAllObjects = &ScriptHookV.worldGetAllObjects;
		private static readonly delegate*<int*, int, int> _worldGetAllPickups = &ScriptHookV.worldGetAllPickups;

		private static readonly Dictionary<string, int> _stringPool = [];
		private static readonly Encoding _encoding = Encoding.UTF8;

		private static IntPtr _buffer = IntPtr.Zero;
		private static int _bufferSize;
		private static int _bufferPosition;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Release()
		{
			FreeBuffer();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static byte* GetBytePtr(int position)
		{
			if (_bufferSize == 0)
			{
				return null;
			}
			return (byte*)_buffer.ToPointer() + position;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void CreateNewBuffer()
		{
			if (_buffer != IntPtr.Zero)
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

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void FreeBuffer()
		{
			if (_buffer != IntPtr.Zero)
			{
				Marshal.FreeHGlobal(_buffer);
				_buffer = IntPtr.Zero;
				_bufferSize = 0;
				_bufferPosition = 0;
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
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

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
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

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Sleep(uint time)
		{
			_scriptWait(time);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ulong* GetGlobalPtr(int globalId)
		{
			return _getGlobalPtr(globalId);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int CreateTexture(byte* texFileName)
		{
			return _createTexture(texFileName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void DrawTexture(int id, int index, int level, int time, float sizeX, float sizeY, float centerX, float centerY, float posX, float posY, float rotation, float screenHeightScaleFactor, float r, float g, float b, float a)
		{
			_drawTexture(id, index, level, time, sizeX, sizeY, centerX, centerY, posX, posY, rotation, screenHeightScaleFactor, r, g, b, a);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static int WorldGetAllVehicles(int* arr, int arrSize)
		{
			return _worldGetAllVehicles(arr, arrSize);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static int WorldGetAllPeds(int* arr, int arrSize)
		{
			return _worldGetAllPeds(arr, arrSize);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static int WorldGetAllObjects(int* arr, int arrSize)
		{
			return _worldGetAllObjects(arr, arrSize);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static int WorldGetAllPickups(int* arr, int arrSize)
		{
			return _worldGetAllPickups(arr, arrSize);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ulong V(void* ptr)
		{
			return (ulong)ptr;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ulong V<T>(T value) where T : unmanaged
		{
			return *(ulong*)&value;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ulong V(string value)
		{
			return V(GetBytePtr(value));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Invoke(ulong function, params ulong[] args)
		{
			_initFunc(function);
			for (int i = 0; i < args.Length; i++)
			{
				_pushArg(args[i]);
			}
			_invoke();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static T* PInvoke<T>(ulong function, params ulong[] args) where T : unmanaged
		{
			_initFunc(function);
			for (int i = 0; i < args.Length; i++)
			{
				_pushArg(args[i]);
			}
			return (T*)_invoke();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static T Invoke<T>(ulong function, params ulong[] args) where T : unmanaged
		{
			return *PInvoke<T>(function, args);
		}
	}
}
