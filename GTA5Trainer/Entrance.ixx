import "Base.h";
import "Import.h";
import Util;
import InputSystem;
import Menu;
import <stdexcept>;

static void Init()
{
	InitLog();
	Controller = new MenuController();
	Log("Init Finish");
}

static void Run()
{
	try
	{
		Log("Start Run");
		while (true)
		{
			Controller->Update();
			ThreadSleep(0);
		}
	}
	catch(const std::exception& e){
		Log(e.what());
	}

}

static void Release()
{
	Log("Start Release");
	delete Controller;
	Controller = null;
	FreeStoredStringBuffer();
	Log("Release Finish");
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
		{
			Init();
			scriptRegister(hModule, Run);
			keyboardHandlerRegister(OnInput);
			break;
		}
		case Dll_Release:
		{
			keyboardHandlerUnregister(OnInput);
			scriptUnregister(hModule);
			Release();
			break;
		}
	}
	return 1;
}

