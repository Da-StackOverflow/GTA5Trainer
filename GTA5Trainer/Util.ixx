export module Util;

import "Base.h";
import Vector;
import Color;
import Function;
import "Import.h";
import <chrono>;
import <fstream>;
import <random>;

std::random_device rd;
std::mt19937 generator(rd());
std::uniform_real_distribution<float> distribution(0, 1);

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

export void PaintText(String text, Vector2& position, float scale, const Color& color)
{
	HUD::SET_TEXT_FONT(0);
	HUD::SET_TEXT_SCALE(0.0, scale);
	HUD::SET_TEXT_COLOR(color.r, color.g, color.b, color.a);
	HUD::SET_TEXT_CENTRE(0);
	HUD::SET_TEXT_DROPSHADOW(0, 0, 0, 0, 0);
	HUD::SET_TEXT_EDGE(0, 0, 0, 0, 0);
	HUD::BEGIN_TEXT_COMMAND_DISPLAY_TEXT("STRING");
	HUD::ADD_TEXT_COMPONENT_SUBSTRING_PLAYER_NAME(text);
	HUD::END_TEXT_COMMAND_DISPLAY_TEXT(position.x, position.y);
}

export void PaintText(String text, float x, float y, float scale, const Color& color)
{
	HUD::SET_TEXT_FONT(0);
	HUD::SET_TEXT_SCALE(0.0, scale);
	HUD::SET_TEXT_COLOR(color.r, color.g, color.b, color.a);
	HUD::SET_TEXT_CENTRE(0);
	HUD::SET_TEXT_DROPSHADOW(0, 0, 0, 0, 0);
	HUD::SET_TEXT_EDGE(0, 0, 0, 0, 0);
	HUD::BEGIN_TEXT_COMMAND_DISPLAY_TEXT("STRING");
	HUD::ADD_TEXT_COMPONENT_SUBSTRING_PLAYER_NAME(text);
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

export i64 GetTimeTicks()
{
	return std::chrono::duration_cast<std::chrono::milliseconds>(std::chrono::system_clock::now().time_since_epoch()).count();
}

export void ThreadSleep(uint milliseconds)
{
	scriptWait(milliseconds);
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

export void Log(String log)
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