#pragma once

#define uint unsigned long

typedef uint Void;
typedef uint Any;
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

#pragma pack(push, 1)
typedef struct
{
	float x;
	uint _paddingx;
	float y;
	uint _paddingy;
	float z;
	uint _paddingz;
} Vector3;
#pragma pack(pop)