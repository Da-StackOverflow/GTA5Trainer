namespace ScriptUI
{
	public struct Color
	{
		public byte A;
		public byte R;
		public byte G;
		public byte B;

		public static readonly Color Red = new(0xff4f6cu);
		public static readonly Color Lime = new(0x65ef63u);
		public static readonly Color Green = new(0x35b52au);
		public static readonly Color Blue = new(0x4fadffu);
		public static readonly Color Yellow = new(0xffe851u);
		public static readonly Color White = new(255, 255, 255);
		public static readonly Color Black = new(0, 0, 0);
		public static readonly Color Cyan = new(0, 255, 255);
		public static readonly Color Gold = new(0xf38300u);

		public Color(int colorNum) : this((uint)colorNum)
		{
		}

		public Color(uint colorNum)
		{
			B = (byte)(colorNum & 0xff);
			G = (byte)((colorNum >> 8) & 0xff);
			R = (byte)((colorNum >> 16) & 0xff);
			A = 255;
		}

		public Color(byte r, byte g, byte b, byte a = 255)
		{
			R = r;
			G = g;
			B = b;
			A = a;
		}

		public Color()
		{
			R = 255;
			G = 255;
			B = 255;
			A = 255;
		}

		public override readonly string ToString()
		{
			return string.Format("R:{1:x}, G:{2:x}, B:{3:x}, A:{0:x}", A, R, G, B);
		}

		public static bool operator ==(Color left, Color right)
		{
			return left.R == right.R && left.G == right.G && left.B == right.B && left.A == right.A;
		}

		public static bool operator !=(Color left, Color right)
		{
			return left.R != right.R || left.G != right.G || left.B != right.B || left.A != right.A;
		}

		public override readonly int GetHashCode()
		{
			return R.GetHashCode() ^ G.GetHashCode() ^ B.GetHashCode() ^ A.GetHashCode();
		}

		public override readonly bool Equals(object obj)
		{
			if (obj is null)
			{
				return false;
			}
			if (obj is Color c)
			{
				return R == c.R && G == c.G && B == c.B && A == c.A;
			}
			return false;
		}

	}
}