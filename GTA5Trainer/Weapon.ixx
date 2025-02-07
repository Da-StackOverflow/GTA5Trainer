export module Weapon;

import "Base.h";
import Function;
import Util;
import Menu;
import ItemInfo;
import WeaponsInfo;

export class GetAllWeapons : public TriggerItem
{
public:
	constexpr GetAllWeapons(WString caption) : TriggerItem(caption)
	{
	}

	void OnExecute() override
	{
		for (int i = 0; i < sizeof(WeaponsInfo) / sizeof(WeaponsInfo[0]); i++)
		{
			WEAPON::GIVE_DELAYED_WEAPON_TO_PED(PlayerPed(), MISC::GET_HASH_KEY(WeaponsInfo[i].Model), 9999, 0);
		}
			
	}
};

export class GetWeapon : public TriggerItem
{
private:
	ItemInfo weaponInfo;
public:
	constexpr GetWeapon(ItemInfo& weaponInfo) : TriggerItem(weaponInfo.Caption)
	{
	}

	void OnExecute() override
	{
		WEAPON::GIVE_DELAYED_WEAPON_TO_PED(PlayerPed(), MISC::GET_HASH_KEY(weaponInfo.Model), 9999, 0);
	}
};