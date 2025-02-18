using System;

namespace ScriptUI
{
	public static class Random
	{
		private const int N = 624;
		private const int M = 397;
		private const uint MATRIX_A = 0x9908b0df; // logical right shift
		private const uint UPPER_MASK = 0x80000000; // most significant w-bit
		private const uint LOWER_MASK = 0x7fffffff; // least significant w-bit

		private static readonly uint[] mt = new uint[N]; // the array for the state vector
		private static int mti = N + 1; // mti==N+1 means mt[N] is not initialized

		static Random()
		{
			Initialize((uint)Environment.TickCount);
		}

		public static void Initialize(uint seed)
		{
			mt[0] = seed & 0xffffffff; // for >32 bit machines
			for (int i = 1; i < N; i++)
			{
				mt[i] = (1812433299U * (mt[i - 1] ^ (mt[i - 1] >> 30)) + (uint)i);
				mt[i] &= 0xffffffff; // for >32 bit machines
			}
			mti = N;
		}

		private static void GenerateNumbers()
		{
			for (int i = 0; i < N - M; i++)
			{
				uint y = (mt[i] & UPPER_MASK) | (mt[i + 1] & LOWER_MASK);
				mt[i] = mt[i + M] ^ (y >> 1) ^ (y % 2 == 0 ? 0 : MATRIX_A);
			}
			for (int i = N - M; i < N - 1; i++)
			{
				uint y = (mt[i] & UPPER_MASK) | (mt[i + 1] & LOWER_MASK);
				mt[i] = mt[i + (M - N)] ^ (y >> 1) ^ (y % 2 == 0 ? 0 : MATRIX_A);
			}
			uint y1 = (mt[N - 1] & UPPER_MASK) | (mt[0] & LOWER_MASK);
			mt[N - 1] = mt[M - 1] ^ (y1 >> 1) ^ (y1 % 2 == 0 ? 0 : MATRIX_A);
			mti = 0;
		}

		/// <summary>
		/// [0, uint.MaxValue]
		/// </summary>
		/// <returns></returns>
		private static uint Generate()
		{
			if (mti >= N) // generate N numbers at one time
			{
				if (mti == N + 1) // if init_genrand() has not been called,
					Initialize(5489U); // a default initial seed is used

				GenerateNumbers();
			}

			uint y = mt[mti++];
			// Tempering
			y ^= (y >> 11);
			y ^= (y << 7) & 0x9d2c5680;
			y ^= (y << 15) & 0xefc60000;
			y ^= (y >> 18);
			return y;
		}

		public static int Next()
		{
			return (int)Generate();
		}

		public static int Next(int minValue, int maxValue)
		{
			if (minValue > maxValue)
			{
				(minValue, maxValue) = (maxValue, minValue);
			}
			return minValue + (int)(Generate() % (maxValue - minValue + 1));
		}

		public static int Next(uint maxValue)
		{
			return (int)(Generate() % (maxValue + 1));
		}

		public static double NextDouble()
		{
			return Generate() / (double)uint.MaxValue;
		}
	}
}
