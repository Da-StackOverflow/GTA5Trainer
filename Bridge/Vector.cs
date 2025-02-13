using System.Runtime.InteropServices;

namespace Bridge
{
	public struct Vector2
	{
		public float X;
		public float Y;

		public Vector2(float x, float y)
		{
			X = x;
			Y = y;
		}

		public override readonly string ToString()
		{
			return string.Format("X:{0}, Y:{1}", X, Y);
		}

		public static bool operator ==(Vector2 left, Vector2 right)
		{
			return left.X == right.X && left.Y == right.Y;
		}

		public static bool operator !=(Vector2 left, Vector2 right)
		{
			return left.X != right.X || left.Y != right.Y;
		}

		public override readonly int GetHashCode()
		{
			return X.GetHashCode() ^ Y.GetHashCode();
		}

		public override readonly bool Equals(object obj)
		{
			if (obj is null)
			{
				return false;
			}
			if (obj is Vector2 v)
			{
				return X == v.X && Y == v.Y;
			}
			return false;
		}
	}

	[StructLayout(LayoutKind.Explicit, Size = 24)]
	public unsafe struct Vector3
	{
		[FieldOffset(0)]
		public float X;

		[FieldOffset(8)]
		public float Y;

		[FieldOffset(16)]
		public float Z;

		public Vector3(float x, float y, float z)
		{
			X = x;
			Y = y;
			Z = z;
		}

		public Vector3(Vector3* p)
		{
			X = p->X;
			Y = p->Y;
			Z = p->Z;
		}

		public Vector3(ulong* addr)
		{
			var p = (Vector3*)addr;
			X = p->X;
			Y = p->Y;
			Z = p->Z;
		}

		public Vector3(long* addr)
		{
			var p = (Vector3*)addr;
			X = p->X;
			Y = p->Y;
			Z = p->Z;
		}

		public override readonly string ToString()
		{
			return string.Format("X:{0}, Y:{1}, Z:{2}", X, Y, Z);
		}

		public static bool operator ==(Vector3 left, Vector3 right)
		{
			return left.X == right.X && left.Y == right.Y && left.Z == right.Z;
		}

		public static bool operator !=(Vector3 left, Vector3 right)
		{
			return left.X != right.X || left.Y != right.Y || left.Z != right.Z;
		}

		public override readonly int GetHashCode()
		{
			return X.GetHashCode() ^ Y.GetHashCode() ^ Z.GetHashCode();
		}

		public override readonly bool Equals(object obj)
		{
			if (obj is null)
			{
				return false;
			}
			if (obj is Vector3 v)
			{
				return X == v.X && Y == v.Y && Z == v.Z;
			}
			return false;
		}
	}
}