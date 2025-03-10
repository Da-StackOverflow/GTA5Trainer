﻿using System.Runtime.CompilerServices;

namespace ScriptUI
{
	internal struct KeyState
	{
		public uint Key;
		public bool IsDown;
		public KeyState(uint key)
		{
			Key = key;
			IsDown = false;
		}

		public override readonly string ToString()
		{
			return string.Format("KeyCode:{0}, IsDown:{1}", Key, IsDown);
		}

		public static bool operator ==(KeyState left, KeyState right)
		{
			return left.Key == right.Key;
		}

		public static bool operator !=(KeyState left, KeyState right)
		{
			return left.Key != right.Key;
		}

		public override readonly int GetHashCode()
		{
			return Key.GetHashCode();
		}

		public override readonly bool Equals(object obj)
		{
			if (obj is null)
			{
				return false;
			}
			if (obj is KeyState v)
			{
				return Key == v.Key;
			}
			return false;
		}
	}

	public static class Input
	{
		private const uint KeyCodeType = 255;

		private static readonly KeyState[] _keyStates = new KeyState[KeyCodeType];

		static Input()
		{
			for (uint i = 0; i < KeyCodeType; i++)
			{
				_keyStates[i] = new KeyState(i);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void OnKeyDown(uint key)
		{
			_keyStates[key].IsDown = true;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void OnKeyUp(uint key)
		{
			_keyStates[key].IsDown = false;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsKeyDown(KeyCode keyCode)
		{
			return _keyStates[(uint)keyCode].IsDown;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsAccept()
		{
			return IsKeyDown(KeyCode.Num5); // || IsKeyDown(KeyCode.Return);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsBack()
		{
			return IsKeyDown(KeyCode.Num0); // || IsKeyDown(KeyCode.Back);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsUp()
		{
			return IsKeyDown(KeyCode.Num8); // || IsKeyDown(KeyCode.Up);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsDown()
		{
			return IsKeyDown(KeyCode.Num2); // || IsKeyDown(KeyCode.Down);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsLeft()
		{
			return IsKeyDown(KeyCode.Num4); // || IsKeyDown(KeyCode.Left);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsRight()
		{
			return IsKeyDown(KeyCode.Num6); // || IsKeyDown(KeyCode.Right);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsShift()
		{
			return IsKeyDown(KeyCode.Shift);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsSpace()
		{
			return IsKeyDown(KeyCode.Space);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool MenuSwitchPressed()
		{
			return IsKeyDown(KeyCode.F4);
		}
	}
}