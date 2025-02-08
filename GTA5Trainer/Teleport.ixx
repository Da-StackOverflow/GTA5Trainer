export module Teleport;

import "Base.h";
import Function;
import Util;
import Menu;
import Vector;

export constexpr const float GroundCheckHeight[] = {
	0.0f, 50.0f, 100.0f,150.0f, 200.0f, 250.0f, 300.0f, 350.0f, 400.0f, 450.0f, 500.0f, 550.0f,
	600.0f, 650.0f, 700.0f, 750.0f, 800.0f, 850.0f, 900.0f, 950.0f, 1000.0f,
	1050.0f, 1100.0f, 1150.0f, 1200.0f, 1250.0f, 1300.0f, 1350.0f, 1400.0f, 1450.0f,
	1500.0f, 1550.0f, 1600.0f, 1650.0f, 1700.0f, 1750.0f, 1800.0f, 1850.0f, 1900.0f,
	1950.0f, 2000.0f, 2050.0f, 2100.0f,
	-300.0f, -250.0f, -200.0f, -150.0f, -100.0f, -50.0f,
	-750.0f, -700.0f, -650.0f, -600.0f, -550.0f, -500.0f, -450.0f, -400.0f, -350.0f,
	-1200.0f, -1150.0f, -1100.0f, -1050.0f, -1000.0f, -950.0f, -900.0f, -850.0f, -800.0f,
	-1650.0f, -1600.0f, -1550.0f, -1500.0f, -1450.0f, -1400.0f, -1350.0f, -1300.0f, -1250.0f,
	-2100.0f, -2050.0f, -2000.0f, -1950.0f, -1900.0f, -1850.0f, -1800.0f, -1750.0f, -1700.0f,
};

export class TeleportMarker : public TriggerItem
{
public:
	constexpr TeleportMarker(WString caption) : TriggerItem(caption)
	{

	}

	void OnExecute() override
	{
		Entity e = PlayerPed();
		if (PED::IS_PED_IN_ANY_VEHICLE(e, 0))
		{
			e = PED::GET_VEHICLE_PED_IS_USING(e);
		}

		Vector3 coords;

		bool blipFound = false;

		int blipIterator = HUD::GET_WAYPOINT_BLIP_ENUM_ID();
		for (Blip i = HUD::GET_FIRST_BLIP_INFO_ID(blipIterator); HUD::DOES_BLIP_EXIST(i) != 0; i = HUD::GET_NEXT_BLIP_INFO_ID(blipIterator))
		{
			if (HUD::GET_BLIP_INFO_ID_TYPE(i) == 4)
			{
				coords = HUD::GET_BLIP_INFO_ID_COORD(i);
				blipFound = true;
				break;
			}
		}
		if (!blipFound)
		{
			SetTips(L"请先在地图上设置标记点");
			return;
		}

		bool groundFound = false;
		for (int i = 0; i < sizeof(GroundCheckHeight) / sizeof(float); i++)
		{
			ENTITY::SET_ENTITY_COORDS_NO_OFFSET(e, coords.x, coords.y, GroundCheckHeight[i], 0, 0, 1);
			ThreadSleep(100);
			if (MISC::GET_GROUND_Z_FOR_3D_COORD(coords.x, coords.y, GroundCheckHeight[i], &coords.z, false, false))
			{
				groundFound = true;
				coords.z += 3.0;
				break;
			}
		}

		if (!groundFound)
		{
			coords.z = 1000.0;
			WEAPON::GIVE_DELAYED_WEAPON_TO_PED(PlayerPed(), 0xFBAB5776, 1, 0);
		}

		ENTITY::SET_ENTITY_COORDS_NO_OFFSET(e, coords.x, coords.y, coords.z, 0, 0, 1);
		ThreadSleep(0);
		SetTips(L"成功传送");
	}
};

export class Teleport : public TriggerItem
{
private:
	Vector3 coords;

public:
	constexpr Teleport(WString caption, float x, float y, float z) : TriggerItem(caption), coords(x, y, z)
	{

	}

	void OnExecute() override
	{
		Entity e = PlayerPed();
		if (PED::IS_PED_IN_ANY_VEHICLE(e, 0))
		{
			e = PED::GET_VEHICLE_PED_IS_USING(e);
		}

		ENTITY::SET_ENTITY_COORDS_NO_OFFSET(e, coords.x, coords.y, coords.z, false, false, true);
		ThreadSleep(0);
		SetTips(L"成功传送");
	}
};

export class GetTeleportMarkerCords : public TriggerItem
{
public:
	constexpr GetTeleportMarkerCords(WString caption) : TriggerItem(caption)
	{

	}

	void OnExecute() override
	{
		Entity e = PlayerPed();
		if (PED::IS_PED_IN_ANY_VEHICLE(e, 0))
		{
			e = PED::GET_VEHICLE_PED_IS_USING(e);
		}
		
		Vector3 coords;

		bool blipFound = false;

		int blipIterator = HUD::GET_WAYPOINT_BLIP_ENUM_ID();
		for (Blip i = HUD::GET_FIRST_BLIP_INFO_ID(blipIterator); HUD::DOES_BLIP_EXIST(i) != 0; i = HUD::GET_NEXT_BLIP_INFO_ID(blipIterator))
		{
			if (HUD::GET_BLIP_INFO_ID_TYPE(i) == 4)
			{
				coords = HUD::GET_BLIP_INFO_ID_COORD(i);
				blipFound = true;
				break;
			}
		}
		if (!blipFound)
		{
			SetTips(L"请先在地图上设置标记点");
			return;
		}

		Vector3 preCoords = ENTITY::GET_ENTITY_COORDS(e, true);

		bool groundFound = false;
		for (int i = 0; i < sizeof(GroundCheckHeight) / sizeof(float); i++)
		{
			ENTITY::SET_ENTITY_COORDS_NO_OFFSET(e, coords.x, coords.y, GroundCheckHeight[i], 0, 0, 1);
			ThreadSleep(100);
			if (MISC::GET_GROUND_Z_FOR_3D_COORD(coords.x, coords.y, GroundCheckHeight[i], &coords.z, false, false))
			{
				groundFound = true;
				break;
			}
		}

		if (!groundFound)
		{
			coords.z = 1000.0f;
		}
		SetTips(ToString(coords));
		ENTITY::SET_ENTITY_COORDS_NO_OFFSET(e, preCoords.x, preCoords.y, preCoords.z, false, false, true);
	}
};

export class GetTeleportCurrentCords : public TriggerItem
{
public:
	constexpr GetTeleportCurrentCords(WString caption) : TriggerItem(caption)
	{

	}

	void OnExecute() override
	{
		Entity e = PlayerPed();
		if (PED::IS_PED_IN_ANY_VEHICLE(e, 0))
		{
			e = PED::GET_VEHICLE_PED_IS_USING(e);
		}
		Vector3 coords = ENTITY::GET_ENTITY_COORDS(e, true);
		SetTips(ToString(coords));
	}
};