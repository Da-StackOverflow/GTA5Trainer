import "Base.h";
import "Import.h";
import Util;
import InputSystem;
import Menu;

static void Init()
{
	InitLog();
	Log("Init Finish");
}

static void Run()
{
	Log("Run");
	Controller.Init();
	Log("Controller.Init Finish");
	while (true)
	{
		Controller.Update();
		ThreadSleep(0);
	}
}

static void Release()
{
	Log("Release");
	CloseLog();
}

static void OnInput(uint key, ushort repeats, byte scanCode, int isExtended, int isWithAlt, int wasDownBefore, int isUpNow)
{
	if (isUpNow == 1)
	{
		Input.OnKeyUp(key);
		return;
	}
	Input.OnKeyDown(key);
}

const uint Dll_INIT = 1;
const uint Dll_Release = 0;

export int __stdcall DllMain(HMODULE hModule, uint reason, void* lpReserved)
{
	switch (reason)
	{
		case Dll_INIT:
			Init();
			scriptRegister(hModule, Run);
			keyboardHandlerRegister(OnInput);
		case Dll_Release:
			keyboardHandlerUnregister(OnInput);
			scriptUnregister(hModule);
			Release();
			break;
	}
	return 1;
}

