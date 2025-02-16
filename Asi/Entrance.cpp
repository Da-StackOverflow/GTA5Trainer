//定义 DLL 应用程序的入口点。
#pragma unmanaged
#include "Import.h"
#include <stdexcept>
#include <fstream>

public enum ScriptState
{
	Loaded,
	Unloaded
};

ScriptState State = ScriptState::Loaded;

static void Print(const char* log)
{
	var logger = std::ofstream("GTA5TrainerScript.txt", std::ios::app);
	if (logger.is_open())
	{
		logger << log << std::endl;
		logger.flush();
		logger.close();
	}
}

#pragma managed

using namespace Bridge;
using namespace System;
using namespace System::IO;

static void Info(String^ log)
{
	File::AppendAllText("GTA5TrainerAsiError.txt", log + "\n");
}

public ref class Engine
{
public:
	static ProxyObject^ ProxyObj;

	static void ChangeLoadState()
	{
		if (State == ScriptState::Unloaded)
		{
			State = ScriptState::Loaded;
			Print("State is ScriptState::Loaded");
		}
		else
		{
			State = ScriptState::Unloaded;
			Print("State is ScriptState::Unloaded");
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
		if (key == VK_F8)
		{
			if (isUpNow)
			{
				ChangeLoadState();
			}
		}
		else if (0 < key && key < 256)
		{
			if (ProxyObj != null)
			{
				ProxyObj->OnInput(key, isUpNow);
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
	try
	{
		Engine::OnTick();
	}
	catch (Exception^ e)
	{
		Info(e->Message);
		Info(e->StackTrace);
	}
}

static void ScriptOnInput(uint key, int isUpNow)
{
	try
	{
		Engine::OnInput(key, isUpNow);
	}
	catch (Exception^ e)
	{
		Info(e->Message);
		Info(e->StackTrace);
	}
}

#pragma unmanaged

static PVOID _preGameFiber = null;

static void Run()
{
	_preGameFiber = GetCurrentFiber();
	while (true)
	{
		if (State == ScriptState::Loaded)
		{
			Print("InitBridge");
			InitBridge();
			while (State == ScriptState::Loaded)
			{
				const PVOID currentFiber = GetCurrentFiber();
				if (currentFiber != _preGameFiber)
				{
					_preGameFiber = currentFiber;
					State = ScriptState::Loaded;
					Print("Asi Fiber Changed");
					break;
				}
				UpdateScript();
				scriptWait(0);
			}
		}
		else
		{
			scriptWait(0);
		}
	}
}

static void OnInput(uint key, ushort repeats, byte scanCode, int isExtended, int isWithAlt, int wasDownBefore, int isUpNow)
{
	ScriptOnInput(key, isUpNow);
}

static const uint Dll_INIT = 1;
static const uint Dll_Release = 0;

bool __stdcall DllMain(HMODULE hModule, uint reason, void* lpReserved)
{
	switch (reason)
	{
		case Dll_INIT:
		{
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
			break;
		}
	}
	return true;
}

