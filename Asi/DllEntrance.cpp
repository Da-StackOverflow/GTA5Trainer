// 定义 DLL 应用程序的入口点。
#define WIN32_LEAN_AND_MEAN // 从 Windows 头文件中排除极少使用的内容
#include <windows.h>
#include "main.h"
#include "script.h"
#include "keyboard.h"

int __stdcall DllMain(HMODULE hModule, unsigned long  callReason, void* lpReserved)
{
	switch (callReason)
	{
		case DLL_PROCESS_ATTACH:
			scriptRegister(hModule, ScriptMain);
			keyboardHandlerRegister(OnKeyboardMessage);
			break;
		case DLL_PROCESS_DETACH:
			scriptUnregister(hModule);
			keyboardHandlerUnregister(OnKeyboardMessage);
			break;
	}
	return TRUE;
}

