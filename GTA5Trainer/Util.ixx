export module Util;

import "Base.h";
import Vector;
import Color;
import Function;
import "Import.h";
import <chrono>;
import <fstream>;

export void DrawString(const char* text, Vector2& position, Vector2& scale, const Color& color)
{
	UIDEBUG::_BG_SET_TEXT_SCALE(scale.x, scale.y);
	UIDEBUG::_BG_SET_TEXT_COLOR(color.r, color.g, color.b, color.a);
	UIDEBUG::_BG_DISPLAY_TEXT(text, position.x, position.y);
}

export void DrawString(const char* text, float positionX, float positionY, float scaleX, float scaleY, const Color& color)
{
	UIDEBUG::_BG_SET_TEXT_SCALE(scaleX, scaleY);
	UIDEBUG::_BG_SET_TEXT_COLOR(color.r, color.g, color.b, color.a);
	UIDEBUG::_BG_DISPLAY_TEXT(text, positionX, positionY);
}

export void DrawBG(const Vector2& position, float width, float height, Color& color)
{
	GRAPHICS::DRAW_RECT(position.x, position.y, width, height, color.r, color.g, color.b, color.a, 1, 1);
}

const char* _VarStringFlag = "LITERAL_STRING";
export ulong VarString(char* text)
{
	nativeInit(0xFA925AC00EB830B9);
	nativePush64(10);
	nativePush64((ulong)_VarStringFlag);
	nativePush64((ulong)text);
	return *nativeCall();
}

export i64 GetTimeTicks()
{
	return std::chrono::duration_cast<std::chrono::milliseconds>(std::chrono::system_clock::now().time_since_epoch()).count();
}

export byte* CreateBuffer(int size)
{
	if (size <= 0)
	{
		return nullptr;
	}
	return (byte*)calloc(size, 1);
}

export void FreeBuffer(byte* ptr)
{
	free(ptr);
}

std::ofstream _logger;

export void InitLog()
{
	_logger = std::ofstream("RDR2Trainer.txt", std::ios::app);
}

export void Log(const char* log)
{
	if (_logger.is_open())
	{
		_logger << log << std::endl;
	}
}

export void CloseLog()
{
	_logger.flush();
	_logger.close();
}