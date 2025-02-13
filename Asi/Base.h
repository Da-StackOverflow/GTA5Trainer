#pragma once

#pragma unmanaged

#ifndef Base_h
#define Base_h

#define WIN32_LEAN_AND_MEAN
#include <Windows.h>
#include <chrono>
#include <fstream>


#define null nullptr
#define var auto

typedef unsigned char byte;
typedef signed char sbyte;
typedef unsigned short ushort;
typedef unsigned long uint;
typedef unsigned long long ulong;
typedef long long i64;

// uint key, ushort repeats, byte scanCode, int isExtended, int isWithAlt, int wasDownBefore, int isUpNow
typedef void(*KeyboardHandler)(uint, ushort, byte, int, int, int, int);
typedef void(*FunctionPtr)();

// IDXGISwapChain::Present callback
// Called right before the actual Present method call, render test calls don't trigger callbacks
// When the game uses DX10 it actually uses DX11 with DX10 feature level
// Remember that you can't call natives inside
// void OnPresent(IDXGISwapChain *swapChain);
typedef void(*PresentCallback)(void*);



static bool isDevMode = true;
static std::ofstream* _logger = null;
static char* _logBuffer = null;
static void InitLog()
{
	if (!isDevMode)
	{
		return;
	}
	_logBuffer = new char[1024]
		{
			0
		};
	_logger = new std::ofstream("GTA5TrainerBridgeLog.txt", std::ios::trunc);
}

static void Print(const char* log)
{
	if (!isDevMode)
	{
		return;
	}
	if (_logger->is_open())
	{
		*_logger << log << std::endl;
		_logger->flush();
	}
}

static void Print(const wchar_t* log)
{
	if (!isDevMode)
	{
		return;
	}
	if (_logger->is_open())
	{
		int size = WideCharToMultiByte(CP_UTF8, 0, log, -1, null, 0, null, null);
		WideCharToMultiByte(CP_UTF8, 0, log, -1, _logBuffer, size, null, null);
		*_logger << _logBuffer << std::endl;
		_logger->flush();
	}
}

static void CloseLog()
{
	if (!isDevMode)
	{
		return;
	}
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

static i64 GetTimeTicks()
{
	return std::chrono::duration_cast<std::chrono::milliseconds>(std::chrono::system_clock::now().time_since_epoch()).count();
}

#endif