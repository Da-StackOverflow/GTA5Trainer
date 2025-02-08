export module Weapon;

import "Base.h";
import Function;
import Util;
import Menu;
import ItemInfo;
import WeaponsInfos;

export class GetAllWeapons : public TriggerItem
{
public:
	constexpr GetAllWeapons(WString caption) : TriggerItem(caption)
	{
	}

	void OnExecute() override
	{
		for (int i = 0; i < sizeof(WeaponsInfos) / sizeof(WeaponsInfos[0]); i++)
		{
			//WEAPON::GIVE_DELAYED_WEAPON_TO_PED(PlayerPed(), MISC::GET_HASH_KEY(WeaponsInfos[i].Model), 9999, 0);
			WEAPON::GIVE_WEAPON_TO_PED(PlayerPed(), MISC::GET_HASH_KEY(WeaponsInfos[i].Model), 9999, false, false);
		}
			
	}
};

export class GetWeapon : public TriggerItem
{
private:
	ItemInfo weaponInfo;
public:
	constexpr GetWeapon(const ItemInfo& weaponInfo) : TriggerItem(weaponInfo.Caption)
	{
	}

	void OnExecute() override
	{
		//WEAPON::GIVE_DELAYED_WEAPON_TO_PED(PlayerPed(), MISC::GET_HASH_KEY(weaponInfo.Model), 9999, 0);
		WEAPON::GIVE_WEAPON_TO_PED(PlayerPed(), MISC::GET_HASH_KEY(weaponInfo.Model), 9999, false, false);
	}
};

export class RemoveAllWeapon : public TriggerItem
{
public:
	constexpr RemoveAllWeapon(WString caption) : TriggerItem(caption)
	{
	}

	void OnExecute() override
	{
		WEAPON::REMOVE_ALL_PED_WEAPONS(PlayerPed(), false);
	}
};

export class DropCurrentWeapon : public TriggerItem
{
public:
	constexpr DropCurrentWeapon(WString caption) : TriggerItem(caption)
	{
	}

	void OnExecute() override
	{
		WEAPON::SET_PED_DROPS_WEAPON(PlayerPed());
	}
};