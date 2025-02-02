#pragma unmanaged

#define WIN32_LEAN_AND_MEAN
#include <Windows.h>
#include <chrono>

#pragma comment(lib, "ScriptHookV.lib")

#define DllImport __declspec(dllimport)


typedef unsigned long uint;
typedef unsigned short ushort;
typedef unsigned long long ulong;
typedef unsigned char byte;
typedef long long i64;

typedef void(*KeyboardHandler)(uint key, ushort repeats, byte scanCode, int isExtended, int isWithAlt, int wasDownBefore, int isUpNow);
typedef void(*FuncPtr)();

DllImport void keyboardHandlerRegister(KeyboardHandler handler);

DllImport void keyboardHandlerUnregister(KeyboardHandler handler);

DllImport void scriptWait(uint time);

DllImport void scriptRegister(HMODULE module, FuncPtr action);
DllImport void scriptUnregister(HMODULE module);

static void RegisterKeyEvent(KeyboardHandler handler) {
	keyboardHandlerRegister(handler);
}

static void UnregisterKeyEvent(KeyboardHandler handler) {
	keyboardHandlerUnregister(handler);
}

static i64 GetTimeTicks() {
	return std::chrono::duration_cast<std::chrono::milliseconds>(std::chrono::system_clock::now().time_since_epoch()).count();
}

static void OnDllProcessAttach(HMODULE module, FuncPtr action) {
	scriptRegister(module, action);
}

static void OnDllProcessDetach(HMODULE module) {
	scriptUnregister(module);
}

#pragma managed

using namespace System;
using namespace System::IO;
using namespace System::Reflection;
using namespace System::Collections::Generic;
using namespace System::Runtime::Loader;

ref class ProxyObject : public MarshalByRefObject
{
private:
	AssemblyLoadContext^ _assemblyLoadContext = nullptr;
	Dictionary<String^, Type^>^ types = nullptr;
	Dictionary<String^, MethodInfo^>^ methods = nullptr;
	Dictionary<String^, Object^>^ objects = nullptr;

public:
	ProxyObject() {
		_assemblyLoadContext = gcnew AssemblyLoadContext("GTA5Trainer", true);
		types = gcnew Dictionary<String^, Type^>(4);
		methods = gcnew Dictionary<String^, MethodInfo^>(4);
		objects = gcnew Dictionary<String^, Object^>(4);
	}

	void Delete() {
		types->Clear();
		methods->Clear();
		objects->Clear();

		types = nullptr;
		methods = nullptr;
		objects = nullptr;

		if (_assemblyLoadContext != nullptr)
		{
			_assemblyLoadContext->Unload();
			_assemblyLoadContext = nullptr;
		}
	}

	void Load()
	{
		_assemblyLoadContext->LoadFromAssemblyPath(Path::GetFullPath("GTA5Trainer.dll"));
	}

	void Invoke(String^ fullClassName, String^ methodName)
	{
		Type^ tp = nullptr;
		MethodInfo^ method = nullptr;
		Object^ obj = nullptr;
		if (!types->TryGetValue(fullClassName, tp))
		{
			auto assemblies = _assemblyLoadContext->Assemblies;
			for each (Assembly ^ assembly in assemblies)
			{
				tp = assembly->GetType(fullClassName);
				if (tp != nullptr)
				{
					types->Add(fullClassName, tp);
					break;
				}
			}
		}
		if (tp == nullptr)
		{
			return;
		}
		if (!methods->TryGetValue(fullClassName + methodName, method))
		{
			method = tp->GetMethod(methodName);
			methods->Add(fullClassName + methodName, method);
		}

		if (!objects->TryGetValue(fullClassName + methodName, obj))
		{

			obj = Activator::CreateInstance(tp);
			objects->Add(fullClassName + methodName, obj);
		}
		method->Invoke(obj, nullptr);
	}

	void Invoke(String^ fullClassName, String^ methodName, array<Object^>^ args)
	{
		Type^ tp = nullptr;
		MethodInfo^ method = nullptr;
		Object^ obj = nullptr;
		if (!types->TryGetValue(fullClassName, tp))
		{
			auto assemblies = _assemblyLoadContext->Assemblies;
			for each (Assembly ^ assembly in assemblies)
			{
				tp = assembly->GetType(fullClassName);
				if (tp != nullptr)
				{
					types->Add(fullClassName, tp);
					break;
				}
			}
		}
		if (tp == nullptr)
		{
			return;
		}
		if (!methods->TryGetValue(fullClassName + methodName, method))
		{
			method = tp->GetMethod(methodName);
			methods->Add(fullClassName + methodName, method);
		}

		if (!objects->TryGetValue(fullClassName + methodName, obj))
		{
			obj = Activator::CreateInstance(tp);
			objects->Add(fullClassName + methodName, obj);
		}
		method->Invoke(obj, args);
	}
};

enum ScriptState
{
	Loading,
	Loaded,
	Unloading,
	Unloaded
};

ref class Bridge
{
private:
	static const uint ReloadKey = 0x77; //F8
	static const i64 OperateTime = 3000;
	static ProxyObject^ _obj = nullptr;
	static i64 _operateTime = 0;
	static ScriptState _scriptState = ScriptState::Unloaded;
public:

	static void StartUnloadScripts()
	{
		i64 time = GetTimeTicks();
		if (_scriptState == ScriptState::Loaded && time - _operateTime > OperateTime)
		{
			_operateTime = time;
			_scriptState = ScriptState::Unloading;
		}
	}

	static void Unload()
	{
		if (_scriptState != ScriptState::Unloaded)
		{
			_operateTime = GetTimeTicks();
			_scriptState = ScriptState::Unloaded;

			if (_obj != nullptr)
			{
				_obj->Invoke("Entry", "OnDestroy");
				_obj->Delete();
				_obj = nullptr;
			}
			GC::Collect();
			GC::WaitForPendingFinalizers();
		}
	}

	static void StartLoadScripts()
	{
		i64 time = GetTimeTicks();
		if (_scriptState == ScriptState::Unloaded && time - _operateTime > OperateTime)
		{
			_operateTime = time;
			_scriptState = ScriptState::Loading;
		}
	}

	static void Load()
	{
		if (_scriptState == ScriptState::Loading)
		{
			_obj = gcnew ProxyObject();
			_obj->Load();
			_obj->Invoke("Entry", "OnStart");
			_operateTime = GetTimeTicks();
			_scriptState = ScriptState::Loaded;
		}
	}


	static void Update()
	{
		switch (_scriptState)
		{
			case ScriptState::Loaded:
				_obj->Invoke("Entry", "OnUpdate");
				break;
			case ScriptState::Loading:
				Load();
				break;
			case ScriptState::Unloading:
				Unload();
				break;
		}
	}

	static void Input(uint key, int isUpNow)
	{
		switch (_scriptState)
		{
			case ScriptState::Loaded:
			{
				if (key == ReloadKey)
				{
					StartUnloadScripts();
				}
				else
				{
					array<Object^>^ args = { key, isUpNow == 1 };
					_obj->Invoke("Entry", "OnInput", args);
				}
				break;
			}
			case ScriptState::Unloaded:
			{
				if (key == ReloadKey)
				{
					StartLoadScripts();
				}
				break;
			}
		}
	}
};

static void Release() {
	Bridge::Unload();
}

static void ScriptMain() {
	Bridge::StartLoadScripts();
	while (true)
	{
		Bridge::Update();
		scriptWait(0);
	}
}

static void ScriptKeyboardMessage(uint key, ushort repeats, byte scanCode, int isExtended, int isWithAlt, int wasDownBefore, int isUpNow) {
	Bridge::Input(key, isUpNow);
}

#pragma unmanaged

// 定义 DLL 应用程序的入口点。
int __stdcall DllMain(HMODULE hModule, uint option, void* lpReserved)
{
	switch (option)
	{
		case 1:
			OnDllProcessAttach(hModule, ScriptMain);
			RegisterKeyEvent(ScriptKeyboardMessage);
			break;
		case 0:
			Release();
			OnDllProcessDetach(hModule);
			UnregisterKeyEvent(ScriptKeyboardMessage);
			break;
	}
	return 1;
}

