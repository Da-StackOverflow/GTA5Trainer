//定义 DLL 应用程序的入口点。
#pragma unmanaged
#include "Import.h"
#include <stdexcept>

public enum ScriptState
{
	Loaded,
	Unloaded
};

ScriptState State = ScriptState::Unloaded;

#pragma managed

#ifdef GetTempPath
#undef GetTempPath
#endif

using namespace Bridge;
using namespace System;
using namespace System::IO;

static void Info(String^ log)
{
	File::AppendAllText("Script/GTA5TrainerAsiError.txt", log + "\n");
}

public ref class Engine
{
public:
	static i64 _operateTime = 0;
	static ProxyObject^ ProxyObj;

	static void Unload()
	{
		i64 time = GetTimeTicks();
		if (time - _operateTime > 3000)
		{
			State = ScriptState::Unloaded;
			_operateTime = GetTimeTicks();
		}
	}

	static void OnTick()
	{
		if (State == ScriptState::Loaded && ProxyObj != null)
		{
			ProxyObj->OnUpdate();
		}
	}

	static void OnInput(uint key, bool isUpNow)
	{
		switch (State)
		{
			case ScriptState::Loaded:
			{
				if (key == VK_F8)
				{
					Unload();
				}
				else if (0 < key && key < 256)
				{
					if (ProxyObj != null)
					{
						ProxyObj->OnInput(key, isUpNow);
					}
				}
				break;
			}
		}
	}
};

static void ForceCLRInit()
{

}

static void InitBridge()
{
	try
	{
		if (Engine::ProxyObj != null)
		{
			ProxyObject::Unload(Engine::ProxyObj);
		}

		Engine::ProxyObj = ProxyObject::Load();
		if (Engine::ProxyObj == null)
		{
			Info("ProxyObject::Load() failed");
			return;
		}

		Engine::ProxyObj->Start();
	}
	catch (Exception^ e)
	{
		Info(e->Message);
		Info(e->StackTrace);
	}
}

static void UpdateScript()
{
	Engine::OnTick();
}

static void ScriptOnInput(uint key, int isUpNow)
{
	Engine::OnInput(key, isUpNow);
}

#pragma unmanaged

static PVOID _preGameFiber = null;

static void Run()
{
	try
	{
		_preGameFiber = GetCurrentFiber();
		State = ScriptState::Loaded;
		while (true)
		{
			InitBridge();
			while (State == ScriptState::Loaded)
			{
				const PVOID currentFiber = GetCurrentFiber();
				if (currentFiber != _preGameFiber)
				{
					_preGameFiber = currentFiber;
					State = ScriptState::Unloaded;
					break;
				}
				UpdateScript();
				scriptWait(0);
			}
		}
	}
	catch (const std::exception& e)
	{
		Print("error");
		Print(e.what());
	}
}

static void OnInput(uint key, ushort repeats, byte scanCode, int isExtended, int isWithAlt, int wasDownBefore, int isUpNow)
{
	ScriptOnInput(key, isUpNow);
}

#pragma unmanaged

static const uint Dll_INIT = 1;
static const uint Dll_Release = 0;

bool __stdcall DllMain(HMODULE hModule, uint reason, void* lpReserved)
{
	switch (reason)
	{
		case Dll_INIT:
		{
			InitLog();
			DisableThreadLibraryCalls(hModule);
			if (!GetModuleHandle(TEXT("clr.dll")))
			{
				ForceCLRInit();
			}
			scriptRegister(hModule, Run);
			keyboardHandlerRegister(OnInput);
			break;
		}
		case Dll_Release:
		{
			keyboardHandlerUnregister(OnInput);
			scriptUnregister(hModule);
			CloseLog();
			break;
		}
	}
	return true;
}

#ifndef GetTempPath
#define GetTempPath GetTempPathW
#endif

