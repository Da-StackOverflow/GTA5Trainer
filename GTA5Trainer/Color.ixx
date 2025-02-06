export module Color;

import "Base.h";

export struct Color
{
public:
	byte a;
	byte r;
	byte g;
	byte b;

	constexpr Color(int colorNum, bool includeAlpha = false) noexcept : Color((uint)colorNum, includeAlpha)
	{
	}

	constexpr Color(uint colorNum, bool includeAlpha = false) noexcept
	{
		a = (byte)(colorNum & 0xff);
		r = (byte)((colorNum >> 8) & 0xff);
		g = (byte)((colorNum >> 16) & 0xff);
		b = includeAlpha ? (byte)((colorNum >> 24) & 0xff) : (byte)255;
	}

	constexpr Color(byte r, byte g, byte b, byte a = 255) noexcept : a(a), r(r), g(g), b(b)
	{
	}

	constexpr Color() noexcept : a(0), r(0), g(0), b(255)
	{
	}
};

export const Color Red = Color(255, 0, 0);
export const Color Green = Color(0, 255, 0);
export const Color Blue = Color(0, 0, 255);
export const Color Yellow = Color(255, 255, 0);
export const Color White = Color(255, 255, 255);
export const Color Black = Color(0, 0, 0);
export const Color Cyan = Color(0, 255, 255);