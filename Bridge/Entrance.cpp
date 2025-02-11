//定义 DLL 应用程序的入口点。
#pragma unmanaged
#include "Import.h"
#include <stdexcept>
static bool _gameReloaded = false;

#pragma managed

using namespace System;
using namespace System::IO;
using namespace System::Reflection;
using namespace System::Collections::Generic;

public ref class ProxyObject : public MarshalByRefObject, IDisposable
{
public:
	AppDomain^ AppDomain()
	{
		return AppDomain::CurrentDomain;
	}

	static ProxyObject CurrentDomain;
	Assembly^ _Assembly = null;
	MethodInfo^ _InitMethod = null;
	MethodInfo^ _UpdateMethod = null;
	MethodInfo^ _DestroyMethod = null;
	MethodInfo^ _InputMethod = null;
	Object^ _ScriptObject = null;
	array<Object^>^ _InputArgs = null;

	ProxyObject()
	{
		_Assembly = Assembly::LoadFile(Path::GetFullPath("GTA5Trainer.dll"));
		var type = _Assembly->GetType("Entrance");
		_InitMethod = type->GetMethod("OnInit");
		_UpdateMethod = type->GetMethod("OnUpdate");
		_DestroyMethod = type->GetMethod("OnDestroy");
		_InputMethod = type->GetMethod("OnInput");
		_ScriptObject = Activator::CreateInstance(type);
		_InputArgs = gcnew array<Object^>(2);
	}

	~ProxyObject()
	{
		GC::SuppressFinalize(this);
	}

	void Release()
	{
		_InitMethod = null;
		_UpdateMethod = null;
		_DestroyMethod = null;
		_InputMethod = null;
		_ScriptObject = null;
		_InputArgs = null;
		_Assembly = null;
	}

	void OnInit()
	{
		_InitMethod->Invoke(_ScriptObject, null);
	}

	void OnUpdate()
	{
		_UpdateMethod->Invoke(_ScriptObject, null);
	}

	void OnDestroy()
	{
		_DestroyMethod->Invoke(_ScriptObject, null);
	}

	void OnInput(uint key, bool isUp)
	{
		_InputArgs[0] = key;
		_InputArgs[1] = isUp;
		_InputMethod->Invoke(_ScriptObject, _InputArgs);
	}
};

public enum ScriptState
{
	Loading,
	Loaded,
	Unloading,
	Unloaded
};

public ref class Bridge
{
private:
	static long long _operateTime = 0;
	static ScriptState _scriptState = ScriptState::Unloaded;
	static AppDomain^ _domain;
public:

	static AppDomain^ CurrentDomain()
	{
		return AppDomain::CurrentDomain;
	}

	static void Load()
	{
		long long time = GetTimeTicks();
		if (_scriptState == ScriptState::Unloaded && time - _operateTime > 3000)
		{
			_operateTime = time;
			_scriptState = ScriptState::Loading;
			Log("Start Load Script");
		}
	}

	static void OnLoad()
	{
		if (_obj == null)
		{
			_obj = static_cast<ProxyObject^>(AppDomain::CurrentDomain->CreateInstanceFromAndUnwrap(DomainName, "ProxyObject"));
			_obj->OnInit();
			_operateTime = GetTimeTicks();
			_scriptState = ScriptState::Loaded;
			Log("Load Script Finish");
		}
	}

	static void Unload()
	{
		long long time = GetTimeTicks();
		if (_scriptState == ScriptState::Loaded && time - _operateTime > 3000)
		{
			_operateTime = time;
			_scriptState = ScriptState::Unloading;
			Log("Start Unload Script");
		}
	}

	static void OnUnload()
	{
		if (_obj != null)
		{
			_obj->OnDestroy();
			_obj->Release();
			_obj = nullptr;
			Log("Unload Script Finish");
		}
		_scriptState = ScriptState::Unloaded;
		_operateTime = GetTimeTicks();
	}

	static void OnTick()
	{
		switch (_scriptState)
		{
			case ScriptState::Loaded:
			{
				_obj->OnUpdate();
				break;
			}
			case ScriptState::Loading:
			{
				OnLoad();
				break;
			}
			case ScriptState::Unloading:
			{
				OnUnload();
				break;
			}
		}
	}

	static void OnInput(uint key, bool isUpNow)
	{
		switch (_scriptState)
		{
			case ScriptState::Loaded:
			{
				if (key == VK_F8)
				{
					Unload();
				}
				else if (0 < key && key < 256)
				{
					_obj->OnInput(key, isUpNow);
				}
				break;
			}
			case ScriptState::Unloaded:
			{
				if (key == VK_F8)
				{
					Load();
				}
				break;
			}
		}
	}

	static void Release(Object^ sender, EventArgs^ e)
	{
		OnUnload();
		DomainName = null;
		GC::Collect();
	}
};

static void ForceCLRInit()
{

}

static void InitBridge()
{
	Bridge::Load();
	AppDomain::CurrentDomain->ProcessExit += gcnew EventHandler(Bridge::Release);
}

static void UpdateScript()
{
	Bridge::OnTick();
}

static void ScriptOnInput(uint key, int isUpNow)
{
	Bridge::OnInput(key, isUpNow);
}

#pragma unmanaged

static PVOID _preGameFiber = null;

static void Run()
{
	try
	{
		_preGameFiber = GetCurrentFiber();

		while (true)
		{
			_gameReloaded = false;

			InitBridge();

			while (!_gameReloaded)
			{
				const PVOID currentFiber = GetCurrentFiber();
				if (currentFiber != _preGameFiber)
				{
					_preGameFiber = currentFiber;
					_gameReloaded = true;
					break;
				}

				UpdateScript();
				scriptWait(0);
			}
		}
	}
	catch (const std::exception& e)
	{
		Log(e.what());
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
			DisableThreadLibraryCalls(hModule);
			if (!GetModuleHandle(TEXT("clr.dll")))
			{
				ForceCLRInit();
			}
			InitLog();
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

