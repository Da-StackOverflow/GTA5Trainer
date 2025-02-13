#pragma once

#pragma unmanaged

#include "Base.h"

#ifndef Import_h
#define Import_h

#pragma comment(lib, "ScriptHookV.lib")

#define IMPORT __declspec(dllimport)

// Register IDXGISwapChain::Present callback
// must be called on dll attach
IMPORT void presentCallbackRegister(PresentCallback cb);

// Unregister IDXGISwapChain::Present callback
// must be called on dll detach
IMPORT void presentCallbackUnregister(PresentCallback cb);

// Register keyboard handler
// must be called on dll attach
IMPORT void keyboardHandlerRegister(KeyboardHandler handler);

// Unregister keyboard handler
// must be called on dll detach
IMPORT void keyboardHandlerUnregister(KeyboardHandler handler);

/* scripts */

IMPORT void scriptWait(uint time);
IMPORT void scriptRegister(HMODULE module, FunctionPtr);
IMPORT void scriptRegisterAdditionalThread(HMODULE module, FunctionPtr);
IMPORT void scriptUnregister(HMODULE module);

#endif
