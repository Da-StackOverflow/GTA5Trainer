#pragma once
#ifndef Base_h
#define Base_h

#define null nullptr
#define var auto
#define f64 __declspec(align(8)) float
#define Temp1(n1) template<typename n1>
#define Temp2(n1, n2) template<typename n1, typename n2>
#define Temp3(n1, n2, n3) template<typename n1, typename n2, typename n3>
#define Temp4(n1, n2, n3, n4) template<typename n1, typename n2, typename n3, typename n4>
#define Temp5(n1, n2, n3, n4, n5) template<typename n1, typename n2, typename n3, typename n4, typename n5>
#define Temp6(n1, n2, n3, n4, n5, n6) template<typename n1, typename n2, typename n3, typename n4, typename n5, typename n6>
#define Temp7(n1, n2, n3, n4, n5, n6, n7) template<typename n1, typename n2, typename n3, typename n4, typename n5, typename n6, typename n7>
#define Temp8(n1, n2, n3, n4, n5, n6, n7, n8) template<typename n1, typename n2, typename n3, typename n4, typename n5, typename n6, typename n7, typename n8>
#define Temp9(n1, n2, n3, n4, n5, n6, n7, n8, n9) template<typename n1, typename n2, typename n3, typename n4, typename n5, typename n6, typename n7, typename n8, typename n9>
#define Temp10(n1, n2, n3, n4, n5, n6, n7, n8, n9, n10) template<typename n1, typename n2, typename n3, typename n4, typename n5, typename n6, typename n7, typename n8, typename n9, typename n10>
#define Temp11(n1, n2, n3, n4, n5, n6, n7, n8, n9, n10, n11) template<typename n1, typename n2, typename n3, typename n4, typename n5, typename n6, typename n7, typename n8, typename n9, typename n10, typename n11>
#define Temp12(n1, n2, n3, n4, n5, n6, n7, n8, n9, n10, n11, n12) template<typename n1, typename n2, typename n3, typename n4, typename n5, typename n6, typename n7, typename n8, typename n9, typename n10, typename n11, typename n12>
#define Temp13(n1, n2, n3, n4, n5, n6, n7, n8, n9, n10, n11, n12, n13) template<typename n1, typename n2, typename n3, typename n4, typename n5, typename n6, typename n7, typename n8, typename n9, typename n10, typename n11, typename n12, typename n13>
#define Temp14(n1, n2, n3, n4, n5, n6, n7, n8, n9, n10, n11, n12, n13, n14) template<typename n1, typename n2, typename n3, typename n4, typename n5, typename n6, typename n7, typename n8, typename n9, typename n10, typename n11, typename n12, typename n13, typename n14>
#define Temp15(n1, n2, n3, n4, n5, n6, n7, n8, n9, n10, n11, n12, n13, n14, n15) template<typename n1, typename n2, typename n3, typename n4, typename n5, typename n6, typename n7, typename n8, typename n9, typename n10, typename n11, typename n12, typename n13, typename n14, typename n15>
#define Temp16(n1, n2, n3, n4, n5, n6, n7, n8, n9, n10, n11, n12, n13, n14, n15, n16) template<typename n1, typename n2, typename n3, typename n4, typename n5, typename n6, typename n7, typename n8, typename n9, typename n10, typename n11, typename n12, typename n13, typename n14, typename n15, typename n16>
#define Temp17(n1, n2, n3, n4, n5, n6, n7, n8, n9, n10, n11, n12, n13, n14, n15, n16, n17) template<typename n1, typename n2, typename n3, typename n4, typename n5, typename n6, typename n7, typename n8, typename n9, typename n10, typename n11, typename n12, typename n13, typename n14, typename n15, typename n16, typename n17>
#define Temp18(n1, n2, n3, n4, n5, n6, n7, n8, n9, n10, n11, n12, n13, n14, n15, n16, n17, n18) template<typename n1, typename n2, typename n3, typename n4, typename n5, typename n6, typename n7, typename n8, typename n9, typename n10, typename n11, typename n12, typename n13, typename n14, typename n15, typename n16, typename n17, typename n18>
#define Temp19(n1, n2, n3, n4, n5, n6, n7, n8, n9, n10, n11, n12, n13, n14, n15, n16, n17, n18, n19) template<typename n1, typename n2, typename n3, typename n4, typename n5, typename n6, typename n7, typename n8, typename n9, typename n10, typename n11, typename n12, typename n13, typename n14, typename n15, typename n16, typename n17, typename n18, typename n19>
#define Temp20(n1, n2, n3, n4, n5, n6, n7, n8, n9, n10, n11, n12, n13, n14, n15, n16, n17, n18, n19, n20) template<typename n1, typename n2, typename n3, typename n4, typename n5, typename n6, typename n7, typename n8, typename n9, typename n10, typename n11, typename n12, typename n13, typename n14, typename n15, typename n16, typename n17, typename n18, typename n19, typename n20>

typedef unsigned char byte;
typedef signed char sbyte;
typedef unsigned short ushort;
typedef unsigned long uint;
typedef unsigned long long ulong;
typedef long long i64;

typedef uint Void;
typedef uint Any;
typedef uint uint;
typedef uint Hash;
typedef int Entity;
typedef int Player;
typedef int FireId;
typedef int Ped;
typedef int Vehicle;
typedef int Cam;
typedef int CarGenerator;
typedef int Group;
typedef int Train;
typedef int Pickup;
typedef int Object;
typedef int Weapon;
typedef int Interior;
typedef int Blip;
typedef int Texture;
typedef int TextureDict;
typedef int CoverPoint;
typedef int Camera;
typedef int TaskSequence;
typedef int ColourIndex;
typedef int Sphere;
typedef int ScrHandle;
typedef int AnimScene;
typedef int Volume;
typedef int ItemSet;
typedef int Prompt;
typedef int PropSet;
typedef int PersChar;
typedef int PopZone;

// uint key, ushort repeats, byte scanCode, int isExtended, int isWithAlt, int wasDownBefore, int isUpNow
typedef void(*KeyboardHandler)(uint, ushort, byte, int, int, int, int);
typedef void(*FunctionPtr)();
// IDXGISwapChain::Present callback
// Called right before the actual Present method call, render test calls don't trigger callbacks
// When the game uses DX10 it actually uses DX11 with DX10 feature level
// Remember that you can't call natives inside
// void OnPresent(IDXGISwapChain *swapChain);
typedef void(*PresentCallback)(void*);

#endif