#pragma once

#define WIN32_LEAN_AND_MEAN
import <windows.h>;
import "Base.h";

#ifndef Import_h
#define Import_h

#pragma comment(lib, "ScriptHookV.lib")

#define IMPORT __declspec(dllimport)

/* textures */

// Create texture
//	texFileName	- texture file name, it's best to specify full texture path and use PNG textures
//	returns	internal texture id
//	Texture deletion is performed automatically when game reloads scripts
//	Can be called only in the same thread as natives

IMPORT int createTexture(const char* texFileName);

// Draw texture
//	id		-	texture id recieved from createTexture()
//	index	-	each texture can have up to 64 different instances on screen at one time
//	level	-	draw level, being used in global draw order, texture instance with least level draws first
//	time	-	how much time (ms) texture instance will stay on screen, the amount of time should be enough
//				for it to stay on screen until the next corresponding drawTexture() call
//	sizeX,Y	-	size in screen space, should be in the range from 0.0 to 1.0, e.g setting this to 0.2 means that
//				texture instance will take 20% of the screen space
//	centerX,Y -	center position in texture space, e.g. 0.5 means real texture center
//	posX,Y	-	position in screen space, [0.0, 0.0] - top left corner, [1.0, 1.0] - bottom right,
//				texture instance is positioned according to it's center
//	rotation -	should be in the range from 0.0 to 1.0
//	screenHeightScaleFactor - screen aspect ratio, used for texture size correction, you can get it using natives
//	r,g,b,a	-	color, should be in the range from 0.0 to 1.0
//
//	Texture instance draw parameters are updated each time script performs corresponding call to drawTexture()
//	You should always check your textures layout for 16:9, 16:10 and 4:3 screen aspects, for ex. in 1280x720,
//	1440x900 and 1024x768 screen resolutions, use windowed mode for this
//	Can be called only in the same thread as natives

IMPORT void drawTexture(int id, int index, int level, int time,
	float sizeX, float sizeY, float centerX, float centerY,
	float posX, float posY, float rotation, float screenHeightScaleFactor,
	float r, float g, float b, float a);

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
IMPORT void scriptUnregister(FunctionPtr); // deprecated

IMPORT void nativeInit(UINT64 hash);
IMPORT void nativePush64(UINT64 val);
IMPORT PUINT64 nativeCall();

// Returns pointer to global variable
// make sure that you check game version before accessing globals because
// ids may differ between patches
IMPORT UINT64* getGlobalPtr(int globalId);

/* world */

// Get entities from internal pools
// return value represents filled array elements count
// can be called only in the same thread as natives
IMPORT int worldGetAllVehicles(int* arr, int arrSize);
IMPORT int worldGetAllPeds(int* arr, int arrSize);
IMPORT int worldGetAllObjects(int* arr, int arrSize);
IMPORT int worldGetAllPickups(int* arr, int arrSize);

/* misc */

// Returns base object pointer using it's script handle
// make sure that you check game version before accessing object fields because
// offsets may differ between patches
IMPORT byte* getScriptHandleBaseAddress(int handle);

enum eGameVersion : int
{
	VER_1_0_335_2_STEAM,
	VER_1_0_335_2_NOSTEAM,

	VER_1_0_350_1_STEAM,
	VER_1_0_350_2_NOSTEAM,

	VER_1_0_372_2_STEAM,
	VER_1_0_372_2_NOSTEAM,

	VER_1_0_393_2_STEAM,
	VER_1_0_393_2_NOSTEAM,

	VER_1_0_393_4_STEAM,
	VER_1_0_393_4_NOSTEAM,

	VER_1_0_463_1_STEAM,
	VER_1_0_463_1_NOSTEAM,

	VER_1_0_505_2_STEAM,
	VER_1_0_505_2_NOSTEAM,

	VER_1_0_573_1_STEAM,
	VER_1_0_573_1_NOSTEAM,

	VER_1_0_617_1_STEAM,
	VER_1_0_617_1_NOSTEAM,

	VER_SIZE,
	VER_UNK = -1
};

IMPORT eGameVersion getGameVersion();

#endif
