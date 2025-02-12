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

using namespace System;
using namespace System::IO;
using namespace System::Security;
using namespace System::Reflection;
using namespace System::Collections::Generic;
using namespace System::Security::Permissions;

static void Info(String^ log)
{
	File::AppendAllText("Script/GTA5TrainerAsiError.txt", log + "\n");
}

public ref class ProxyObject : public MarshalByRefObject, IDisposable
{
public:
	static ProxyObject^ Instance;
	static String^ RootPath = Path::GetPathRoot(Path::GetFullPath("GTA5TrainerAsi.asi"));
	static String^ ScriptRootPath = Path::GetPathRoot(Path::GetFullPath("Script"));
	static String^ AsiPath = Path::GetFullPath("GTA5TrainerAsi.asi");
	static String^ ScriptPath = Path::GetFullPath("Script/GTA5TrainerScript.dll");
	AppDomain^ Domain;
	Assembly^ _Assembly;
	MethodInfo^ _InitMethod;
	MethodInfo^ _UpdateMethod;
	MethodInfo^ _InputMethod;
	MethodInfo^ _DestroyMethod;
	Object^ _ScriptObject;
	array<Object^>^ _InputArgs;
	byte* _nativeMemory;

	ProxyObject()
	{
		Instance = this;
		Domain = AppDomain::CurrentDomain;
		_nativeMemory = nullptr;
	}

	~ProxyObject()
	{
		Release();
	}

	!ProxyObject()
	{
		
	}


	static void Unload(ProxyObject^ domain)
	{
		domain->~ProxyObject();

		try
		{
			AppDomain::Unload(domain->Domain);
		}
		catch (Exception^ ex)
		{
			Info("Failed to unload script domain: " + ex->ToString());
		}
	}

	static ProxyObject^ Load()
	{
		// Create application and script domain for all the scripts to reside in
		var name = "ScriptDomain_" + (AsiPath->GetHashCode() ^ Environment::TickCount).ToString("X");
		var setup = gcnew AppDomainSetup();
		setup->CachePath = Path::GetTempPath();
		setup->ApplicationBase = ScriptRootPath;
		setup->ShadowCopyFiles = "true";
		setup->ShadowCopyDirectories = ScriptRootPath;

		var newDomain = AppDomain::CreateDomain(name, null, setup, gcnew PermissionSet(PermissionState::Unrestricted));
		newDomain->InitializeLifetimeService();

		ProxyObject^ obj = null;
		try
		{
			obj = static_cast<ProxyObject^>(newDomain->CreateInstanceFromAndUnwrap(AsiPath, "ProxyObject"));
			AppDomain::Unload(newDomain);
		}
		catch (Exception^ ex)
		{
			AppDomain::Unload(newDomain);
			Info(ex->Message);
			Info(ex->StackTrace);
		}
		return obj;
	}

	void Release()
	{
		if (_DestroyMethod != null)
		{
			_DestroyMethod->Invoke(_ScriptObject, null);
		}
		_InitMethod = null;
		_UpdateMethod = null;
		_InputMethod = null;
		_ScriptObject = null;
		_InputArgs = null;
		_Assembly = null;
	}

	void Start()
	{
		try
		{
			_Assembly = Assembly::LoadFile(ScriptPath);
			var type = _Assembly->GetType("Entrance");
			_InitMethod = type->GetMethod("OnInit");
			_UpdateMethod = type->GetMethod("OnUpdate");
			_InputMethod = type->GetMethod("OnInput");
			_DestroyMethod = type->GetMethod("OnDestroy");
			_ScriptObject = Activator::CreateInstance(type);
			_InputArgs = gcnew array<Object^>(2);
			_InitMethod->Invoke(_ScriptObject, null);
		}
		catch (Exception^ ex)
		{
			Info(ex->Message);
			Info(ex->StackTrace);
		}
	}

	void OnUpdate()
	{
		try
		{
			_UpdateMethod->Invoke(_ScriptObject, null);
		}
		catch (Exception^ ex)
		{
			Info(ex->Message);
			Info(ex->StackTrace);
		}

	}

	void OnInput(uint key, bool isUp)
	{
		try
		{
			_InputArgs[0] = key;
			_InputArgs[1] = isUp;
			_InputMethod->Invoke(_ScriptObject, _InputArgs);
		}
		catch (Exception^ ex)
		{
			Info(ex->Message);
			Info(ex->StackTrace);
		}
	}
};

public ref class Bridge
{
public:
	static long long _operateTime = 0;
	static ProxyObject^ ProxyObj = ProxyObject::Instance;

	static void Unload()
	{
		long long time = GetTimeTicks();
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
		if (Bridge::ProxyObj != null)
		{
			ProxyObject::Unload(Bridge::ProxyObj);
		}

		Bridge::ProxyObj = ProxyObject::Load();
		if (Bridge::ProxyObj == null)
		{
			Info("ProxyObject::Load() failed");
			return;
		}

		Bridge::ProxyObj->Start();
	}
	catch (Exception^ e)
	{
		Info(e->Message);
		Info(e->StackTrace);
	}
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
		Log("error");
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

