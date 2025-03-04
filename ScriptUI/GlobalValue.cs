using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace ScriptUI
{
	public static class GlobalValue
	{
		private readonly static Dictionary<string, bool> _boolValues = [];
		private readonly static Dictionary<string, int> _intValues = [];
		private readonly static Dictionary<string, float> _floatValues = [];
		private readonly static Dictionary<string, string> _stringValues = [];

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool GetBoolValue(string key, bool defaultValue = false)
		{
			if (_boolValues.TryGetValue(key, out var value))
			{
				return value;
			}
			return defaultValue;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int GetIntValue(string key, int defaultValue = 0)
		{
			if (_intValues.TryGetValue(key, out var value))
			{
				return value;
			}
			return defaultValue;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float GetFloatValue(string key, float defaultValue = 0.0f)
		{
			if (_floatValues.TryGetValue(key, out var value))
			{
				return value;
			}
			return defaultValue;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static string GetStringValue(string key, string defaultValue = "")
		{
			if (_stringValues.TryGetValue(key, out var value))
			{
				return value;
			}
			return defaultValue;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetBoolValue(string key, bool value)
		{
			_boolValues[key] = value;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetIntValue(string key, int value)
		{
			_intValues[key] = value;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetFloatValue(string key, float value)
		{
			_floatValues[key] = value;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetStringValue(string key, string value)
		{
			_stringValues[key] = value;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void DeleteBoolValue(string key)
		{
			_boolValues.Remove(key);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void DeleteIntValue(string key)
		{
			_intValues.Remove(key);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void DeleteFloatValue(string key)
		{
			_floatValues.Remove(key);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void DeleteStringValue(string key)
		{
			_stringValues.Remove(key);
		}
	}
}
