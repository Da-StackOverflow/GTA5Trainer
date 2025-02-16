#pragma once

#pragma unmanaged

#ifndef Base_h
#define Base_h

#define WIN32_LEAN_AND_MEAN
#include <Windows.h>
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

#endif