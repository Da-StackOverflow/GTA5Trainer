internal readonly struct Color
{
	public readonly byte A;
	public readonly byte R;
	public readonly byte G;
	public readonly byte B;

	public static readonly Color Red = new(255, 0, 0, 255);
	public static readonly Color Green = new(0, 255, 0, 255);
	public static readonly Color Blue = new(0, 0, 255, 255);
	public static readonly Color Yellow = new(255, 255, 0, 255);
	public static readonly Color White = new(255, 255, 255, 255);
	public static readonly Color Black = new(0, 0, 0, 255);
	public static readonly Color Cyan = new(0, 255, 255, 255);

	public Color(int colorNum, bool includeAlpha = false) : this((uint)colorNum, includeAlpha)
	{
	}

	public Color(uint colorNum, bool includeAlpha = false)
	{
		B = (byte)(colorNum & 0xff);
		G = (byte)((colorNum >> 8) & 0xff);
		R = (byte)((colorNum >> 16) & 0xff);
		A = includeAlpha ? (byte)((colorNum >> 24) & 0xff) : (byte)255;
	}

	public Color(byte r, byte g, byte b, byte a = 255)
	{
		R = r;
		G = g;
		B = b;
		A = a;
	}

	public override readonly string ToString()
	{
		return string.Format("A:{0:x}, R:{1:x}, G:{2:x}, B:{3:x}", A, R, G, B);
	}

	public static bool operator ==(Color left, Color right)
	{
		return left.R == right.R && left.G == right.G && left.B == right.B && left.A == right.A;
	}

	public static bool operator !=(Color left, Color right)
	{
		return left.R != right.R || left.G != right.G || left.B != right.B || left.A != right.A;
	}
	public override bool Equals(object obj)
	{
		if (obj is null)
		{
			return false;
		}
		if (obj is Color vector)
		{
			return R == vector.R && G == vector.G && B == vector.B && A == vector.A;
		}
		return false;
	}

	public override int GetHashCode()
	{
		return R.GetHashCode() ^ G.GetHashCode() ^ B.GetHashCode() ^ A.GetHashCode();
	}
}
