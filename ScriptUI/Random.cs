using System.Runtime.CompilerServices;

namespace ScriptUI
{
	public static class Random
	{
		private static readonly ulong[] state = new ulong[4];

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void ResetSeed(ulong seed)
		{
			ulong sm64 = seed;
			for (int i = 0; i < 4; i++)
			{
				state[i] = SplitMix64(ref sm64);
			}
		}

		static Random()
		{
			ResetSeed((ulong)System.DateTime.Now.Ticks);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static ulong SplitMix64(ref ulong x)
		{
			ulong z = x += 0x9E3779B97F4A7C15;
			z = (z ^ (z >> 30)) * 0xBF58476D1CE4E5B9;
			z = (z ^ (z >> 27)) * 0x94D049BB133111EB;
			return z ^ (z >> 31);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static ulong RotateLeft(ulong value, int offset) => (value << offset) | (value >> (64 - offset));

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static ulong BigMul(ulong a, ulong b, out ulong low)
		{
			low = a * b;
			return ((a >> 32) * (b >> 32)) + ((a >> 32) * (uint)b) + ((b >> 32) * (uint)a);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ulong NextUInt64()
		{
			ulong result = RotateLeft(state[1] * 5, 7) * 9;
			ulong t = state[1] << 17;

			state[2] ^= state[0];
			state[3] ^= state[1];
			state[1] ^= state[2];
			state[0] ^= state[3];

			state[2] ^= t;
			state[3] = RotateLeft(state[3], 45);

			return result;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ulong NextUInt64(ulong maxValue)
		{
			ulong randomProduct = BigMul(maxValue, NextUInt64(), out ulong lowPart);

			if (lowPart < maxValue)
			{
				ulong remainder = (0ul - maxValue) % maxValue;

				while (lowPart < remainder)
				{
					randomProduct = BigMul(maxValue, NextUInt64(), out lowPart);
				}
			}

			return randomProduct;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ulong NextUInt64(ulong minValue, ulong maxValue) => NextUInt64(maxValue - minValue) + minValue;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint NextUInt32() => (uint)(NextUInt64() >> 32);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint NextUInt32(uint maxValue)
		{
			ulong randomProduct = (ulong)maxValue * NextUInt32();
			uint lowPart = (uint)randomProduct;

			if (lowPart < maxValue)
			{
				uint remainder = (0u - maxValue) % maxValue;

				while (lowPart < remainder)
				{
					randomProduct = (ulong)maxValue * NextUInt32();
					lowPart = (uint)randomProduct;
				}
			}

			return (uint)(randomProduct >> 32);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint NextUInt32(uint minValue, uint maxValue) => (NextUInt32(maxValue - minValue) + minValue);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int Next() => (int)(NextUInt64() >> 33);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int Next(int maxValue) => (int)NextUInt32((uint)maxValue);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int Next(int minValue, int maxValue) => (int)NextUInt32((uint)(maxValue - minValue)) + minValue;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static long NextInt64() => (long)(NextUInt64() >> 1);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static long NextInt64(long maxValue) => (long)NextUInt64((ulong)maxValue);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static long NextInt64(long minValue, long maxValue) => (long)NextUInt64((ulong)(maxValue - minValue)) + minValue;

		/// <summary>
		/// 64 位整数有 64 位二进制精度，但double只能精确表示 53 位。
		/// </summary>
		/// <returns>[0, 1]</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double NextDouble() => (NextUInt64() >> 11) * (1.0 / (1ul << 53 - 1));

		/// <summary>
		/// 64 位整数有 64 位二进制精度，但double只能精确表示 53 位。
		/// </summary>
		/// <returns>[0, 1)</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double NextDouble2() => (NextUInt64() >> 11) * (1.0 / (1ul << 53));

		/// <summary>
		/// 64 位整数有 64 位二进制精度，但double只能精确表示 24 位。
		/// </summary>
		/// <returns>[0, 1]</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float NextFloat() => (NextUInt64() >> 40) * (1.0f / (1u << 24 - 1));

		/// <summary>
		/// 64 位整数有 64 位二进制精度，但double只能精确表示 24 位。
		/// </summary>
		/// <returns>[0, 1)</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float NextFloat2() => (NextUInt64() >> 40) * (1.0f / (1u << 24));
	}
}
