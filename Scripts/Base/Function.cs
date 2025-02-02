using static Native;

using Player = int;
using Ped = int;
using Void = uint;
using Any = uint;
using Hash = uint;
using Entity = int;
using FireId = int;
using Vehicle = int;
using Cam = int;
using CarGenerator = int;
using Group = int;
using Train = int;
using Pickup = int;
using Object = int;
using Weapon = int;
using Interior = int;
using Blip = int;
using Texture = int;
using TextureDict = int;
using CoverPoint = int;
using Camera = int;
using TaskSequence = int;
using ColourIndex = int;
using Sphere = int;
using ScrHandle = int;
using System.Collections.Generic;
using System.Linq;

internal unsafe static class Function
{
	public static Player PLAYER_ID() { return Invoke<Player>(0x4F8644AF03D0E0D6); }
	public static Ped PLAYER_PED_ID() { return Invoke<Ped>(0xD80958FC74E988A6); }

	public static void DrawBackground(ref Vector2 position, float width, float height, ref Color color)
	{
		DRAW_RECT(position.X, position.Y, width, height, color.R, color.G, color.B, color.A);
	}

	public static void DRAW_RECT(float posX, float posY, float width, float height, int R, int G, int B, int A) { Invoke(0x3A618A217E5154F0, V(posX), V(posY), V(width), V(height), V(R), V(G), V(B), V(A)); }

	public static void Draw_Menu_Line(string caption, float lineWidth, float lineHeight, float lineTop, float lineLeft, float textLeft, bool active, bool title, bool rescaleText = true)
	{
		var text_col = Color.White;
		var rect_col = new Color(70, 95, 95, 255);
		float text_scale = 0.35f;
		int font = 0;

		if (active)
		{
			text_col = Color.Black;
			rect_col = new Color(218, 242, 216, 255);

			if (rescaleText)
			{
				text_scale = 0.40f;
			}	
		}

		if (title)
		{
			rect_col = Color.Black;

			if (rescaleText)
			{
				text_scale = 0.50f;
			}
			font = 1;
		}

		int screen_w, screen_h;
		GET_SCREEN_RESOLUTION(&screen_w, &screen_h);

		textLeft += lineLeft;

		float lineWidthScaled = lineWidth / screen_w; // line width
		float lineTopScaled = lineTop / screen_h; // line top offset
		float textLeftScaled = textLeft / screen_w; // text left offset
		float lineHeightScaled = lineHeight / screen_h; // line height

		float lineLeftScaled = lineLeft / screen_w;

		// this is how it's done in original scripts

		// text upper part
		SET_TEXT_FONT(font);
		SET_TEXT_SCALE(0.0f, text_scale);
		SET_TEXT_COLOR(text_col.R, text_col.G, text_col.B, text_col.A);
		SET_TEXT_CENTRE(false);
		SET_TEXT_DROPSHADOW(0, 0, 0, 0, 0);
		SET_TEXT_EDGE(0, 0, 0, 0, 0);
		_SET_TEXT_ENTRY("STRING");
		_ADD_TEXT_COMPONENT_STRING(caption);
		_DRAW_TEXT(textLeftScaled, (((lineTopScaled + 0.00278f) + lineHeightScaled) - 0.005f));

		// text lower part
		SET_TEXT_FONT(font);
		SET_TEXT_SCALE(0.0f, text_scale);
		SET_TEXT_COLOR(text_col.R, text_col.G, text_col.B, text_col.A);
		SET_TEXT_CENTRE(false);
		SET_TEXT_DROPSHADOW(0, 0, 0, 0, 0);
		SET_TEXT_EDGE(0, 0, 0, 0, 0);
		_SET_TEXT_GXT_ENTRY("STRING");
		_ADD_TEXT_COMPONENT_STRING(caption);
		int num25 = UI::_0x9040DFB09BE75706(textLeftScaled, lineTopScaled + 0.00278f + lineHeightScaled - 0.005f));

		// rect
		draw_rect(,rect_col.R, rect_col.G, rect_col.B, rect_col.A);
	}
	static void GET_SCREEN_RESOLUTION(int* x, int* y) { Invoke(0x888D57E407E63624, V(x), V(y)); }
	static void SET_TEXT_FONT(int fontType) { Invoke(0x66E0276CC5F6B9DA, V(fontType)); }
	static void SET_TEXT_SCALE(float p0, float size) { Invoke(0x07C837F9A01C34C9, V(p0), V(size)); }
	static void SET_TEXT_COLOR(int red, int green, int blue, int alpha) { Invoke(0xBE6B23FFA53FB442, V(red), V(green), V(blue), V(alpha)); }
	static void SET_TEXT_CENTRE(bool align) { Invoke(0xC02F4DBFB51D988B, V(align)); }
	static void SET_TEXT_DROPSHADOW(int distance, int r, int g, int b, int a) { Invoke(0x465C84BC39F1C351, V(distance), V(r), V(g), V(b), V(a)); }
	static void SET_TEXT_EDGE(Hash p0, int r, int g, int b, int a) { Invoke(0x441603240D202FA6, V(p0), V(r), V(g), V(b), V(a)); }
	static void _SET_TEXT_ENTRY(string text) { Invoke(0x25FBB336DF1804CB, V(text)); }
	static void _ADD_TEXT_COMPONENT_STRING(string text) { Invoke(0x6C188BE134E074AA, V(text)); }
	static void _DRAW_TEXT(float x, float y) { Invoke(0xCD015E5BB0D96A57, V(x), V(y)); }
	static void _SET_TEXT_GXT_ENTRY(string entry) { Invoke(0x521FB041D93DD0E4, V(entry)); }
}
