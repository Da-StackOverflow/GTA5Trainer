//定义 DLL 应用程序的入口点。
#pragma unmanaged
#include "Import.h"
#include <stdexcept>
#include <fstream>

public enum ScriptState
{
	Loaded,
	Reloading,
};

ScriptState State = ScriptState::Loaded;

bool IsReload = false;

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
		State = ScriptState::Reloading;
		Print("Start Reload Script");
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

		Engine::ProxyObj->Start(IsReload);
	}
	catch (Exception^ e)
	{
		Info(e->GetType()->ToString());
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
	catch (System::Runtime::Remoting::RemotingException^)
	{
		State = ScriptState::Reloading;
		Print("Game was Stop Long Time, Bridge was auto disposed by GC, Then try to Restart Bridge");
	}
	catch (Exception^ e)
	{
		Info(e->GetType()->ToString());
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
	catch (System::Runtime::Remoting::RemotingException^)
	{
		// 在 UpdateScript() 处理该异常
	}
	catch (Exception^ e)
	{
		Info(e->GetType()->ToString());
		Info(e->Message);
		Info(e->StackTrace);
	}
}

#pragma unmanaged

static void ClearPreLog()
{
	DeleteFile(L"GTA5TrainerAsiError.txt");
	DeleteFile(L"GTA5TrainerBridgeError.txt");
	DeleteFile(L"GTA5TrainerScript.txt");
}

static PVOID _preGameFiber = null;

static void Run()
{
	_preGameFiber = GetCurrentFiber();
	while (true)
	{
		switch (State)
		{
			case Loaded:
			{
				Print("Asi Start InitBridge");
				InitBridge();
				Print("Asi InitBridge Finished");
				while (State == ScriptState::Loaded)
				{
					const PVOID currentFiber = GetCurrentFiber();
					if (currentFiber != _preGameFiber)
					{
						_preGameFiber = currentFiber;
						State = ScriptState::Reloading;
						Print("Asi Fiber Changed");
						break;
					}
					UpdateScript();
					scriptWait(0);
				}
				break;
			}
			case Reloading:
			{
				State = ScriptState::Loaded;
				IsReload = true;
				break;
			}
			default:
			{
				scriptWait(0);
				break;
			}
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
			ClearPreLog();
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

