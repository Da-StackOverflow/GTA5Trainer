export module Util;

import "Base.h";
import Vector;
import Color;
import Function;
import "Import.h";
import <chrono>;
import <fstream>;
import <random>;
import <unordered_map>;
import <string>;

static std::random_device rd;
static std::mt19937 generator(rd());
static std::uniform_real_distribution<float> distribution(0, 1);

export float Random()
{
	return distribution(generator);
}

export float Random(float min, float max)
{
	return min + (max - min) * distribution(generator);
}

export int Random(int min, int max)
{
	return (int)(min + (max - min) * distribution(generator));
}

export i64 GetTimeTicks()
{
	return std::chrono::duration_cast<std::chrono::milliseconds>(std::chrono::system_clock::now().time_since_epoch()).count();
}

export void ThreadSleep(uint milliseconds)
{
	scriptWait(milliseconds);
}

static char* CreateBuffer(int size)
{
	if (size <= 1024)
	{
		return new char[1024];
	}
	return new char[size];
}

static std::unordered_map<WString, int> _stringMap;
static char* _buffer = null;
static int _bufferSize = 0;
static int _bufferOffset = 0;

export void FreeStoredStringBuffer()
{
	if (_buffer != null)
	{
		delete[] _buffer;
		_buffer = null;
	}
}

static String StoreString(WString str)
{
	if (_buffer == null)
	{
		_buffer = CreateBuffer(20480);
		_bufferSize = 20480;
		_bufferOffset = 0;
	}

	int size = WideCharToMultiByte(CP_UTF8, 0, str, -1, null, 0, null, null);
	if (_bufferOffset + size >= _bufferSize)
	{
		var newbuffer = CreateBuffer(_bufferSize * 2);
		memcpy(newbuffer, _buffer, _bufferOffset);
		FreeStoredStringBuffer();
		_buffer = newbuffer;
		_bufferSize *= 2;
	}
	var buffer = _buffer + _bufferOffset;
	WideCharToMultiByte(CP_UTF8, 0, str, -1, buffer, size, null, null);
	_stringMap[str] = _bufferOffset;
	_bufferOffset += size;
	return buffer;
}

export String GetStoredString(WString str)
{
	auto it = _stringMap.find(str);
	if (it == _stringMap.end())
	{
		return StoreString(str);
	}
	return _buffer + it->second;
}

wchar_t _tostringBuffer[1024]{ 0 };

export WString ToString(float x, float y, float z)
{
	swprintf_s(_tostringBuffer, L"(%.3f, %.3f, %.3f)", x, y, z);
	return _tostringBuffer;
}

export WString ToString(Vector3& vec)
{
	swprintf_s(_tostringBuffer, L"(%.3f, %.3f, %.3f)", vec.x, vec.y, vec.z);
	return _tostringBuffer;
}

std::ofstream* _logger = null;
char* _logBuffer = null;
export void InitLog()
{
	_logBuffer = new char[1024]{0};
	_logger = new std::ofstream("GTA5TrainerLog.txt", std::ios::trunc);
}

export void Log(String log)
{
	if (_logger->is_open())
	{
		*_logger << log << std::endl;
		_logger->flush();
	}
}

export void Log(String log, String log2)
{
	if (_logger->is_open())
	{
		*_logger << log << log2 << std::endl;
		_logger->flush();
	}
}

export void Log(WString log)
{
	if (_logger->is_open())
	{
		int size = WideCharToMultiByte(CP_UTF8, 0, log, -1, null, 0, null, null);
		WideCharToMultiByte(CP_UTF8, 0, log, -1, _logBuffer, size, null, null);
		*_logger << _logBuffer << std::endl;
		_logger->flush();
	}
}

export void Log(WString log, String log2)
{
	if (_logger->is_open())
	{
		int size = WideCharToMultiByte(CP_UTF8, 0, log, -1, null, 0, null, null);
		
		WideCharToMultiByte(CP_UTF8, 0, log, -1, _logBuffer, size, null, null);
		*_logger << _logBuffer;
		_logger->flush();
		*_logger << log2 << std::endl;
		_logger->flush();
	}
}

export void Log(int log)
{
	if (_logger->is_open())
	{
		*_logger << log << std::endl;
		_logger->flush();
	}
}

export void Log(float log)
{
	if (_logger->is_open())
	{
		*_logger << log << std::endl;
		_logger->flush();
	}
}

export void Log(i64 log)
{
	if (_logger->is_open())
	{
		*_logger << log << std::endl;
		_logger->flush();
	}
}

export void Log(double log)
{
	if (_logger->is_open())
	{
		*_logger << log << std::endl;
		_logger->flush();
	}
}

export void Log(ulong log)
{
	if (_logger->is_open())
	{
		*_logger << log << std::endl;
		_logger->flush();
	}
}

export void CloseLog()
{
	if (_logger != null)
	{
		_logger->flush();
		_logger->close();
		delete _logger;
		_logger = null;
	}
	if (_logBuffer != null)
	{
		delete[] _logBuffer;
		_logBuffer = null;
	}
}

export void PaintText(WString text, Vector2& position, float scale, const Color& color)
{
	HUD::SET_TEXT_FONT(0);
	HUD::SET_TEXT_SCALE(0.0, scale);
	HUD::SET_TEXT_COLOR(color.r, color.g, color.b, color.a);
	HUD::SET_TEXT_CENTRE(0);
	HUD::SET_TEXT_DROPSHADOW(0, 0, 0, 0, 0);
	HUD::SET_TEXT_EDGE(0, 0, 0, 0, 0);
	HUD::BEGIN_TEXT_COMMAND_DISPLAY_TEXT("STRING");
	HUD::ADD_TEXT_COMPONENT_SUBSTRING_PLAYER_NAME(GetStoredString(text));
	HUD::END_TEXT_COMMAND_DISPLAY_TEXT(position.x, position.y);
}

export void PaintText(WString text, float x, float y, float scale, const Color& color)
{
	HUD::SET_TEXT_FONT(0);
	HUD::SET_TEXT_SCALE(0.0, scale);
	HUD::SET_TEXT_COLOR(color.r, color.g, color.b, color.a);
	HUD::SET_TEXT_CENTRE(0);
	HUD::SET_TEXT_DROPSHADOW(0, 0, 0, 0, 0);
	HUD::SET_TEXT_EDGE(0, 0, 0, 0, 0);
	HUD::BEGIN_TEXT_COMMAND_DISPLAY_TEXT("STRING");
	HUD::ADD_TEXT_COMPONENT_SUBSTRING_PLAYER_NAME(GetStoredString(text));
	HUD::END_TEXT_COMMAND_DISPLAY_TEXT(x, y);
}

export void PaintBG(const Vector2& position, float width, float height, Color& color)
{
	GRAPHICS::DRAW_RECT(position.x, position.y, width, height, color.r, color.g, color.b, color.a);
}

export void PlaySounds()
{
	AUDIO::PLAY_SOUND_FRONTEND(-1, "NAV_UP_DOWN", "HUD_FRONTEND_DEFAULT_SOUNDSET", 0);
}