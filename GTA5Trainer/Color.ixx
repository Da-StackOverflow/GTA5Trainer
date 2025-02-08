export module Color;

import "Base.h";

export struct Color
{
public:
	byte a;
	byte r;
	byte g;
	byte b;

	constexpr Color(uint colorNum) : a(255), r((byte)((colorNum >> 16) & 0xff)), g((byte)((colorNum >> 8) & 0xff)), b((byte)(colorNum & 0xff))
	{
	}

	constexpr Color(byte r, byte g, byte b) : a(255), r(r), g(g), b(b)
	{
	}

	constexpr Color() : a(255), r(255), g(255), b(255)
	{
	}
};

export const Color Red = Color(0xff4f6c);
export const Color Lime = Color(0x65ef63);
export const Color Green = Color(0x35b52a);
export const Color Blue = Color(0x4fadff);
export const Color Yellow = Color(0xffa14f);
export const Color White = Color(255, 255, 255);
export const Color Black = Color(0, 0, 0);
export const Color Cyan = Color(0, 255, 255);
export const Color Gold = Color(0xf38300ul);