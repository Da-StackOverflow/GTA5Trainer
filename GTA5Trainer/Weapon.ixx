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
			WEAPON::GIVE_WEAPON_TO_PED(PlayerPed(), MISC::GET_HASH_KEY(WeaponsInfos[i].Model), 9999, false, true);
		}
			
	}
};

export class GetWeapon : public TriggerItem
{
private:
	ItemInfo weaponInfo;
public:
	constexpr GetWeapon(const ItemInfo& weaponInfo) : TriggerItem(weaponInfo.Caption), weaponInfo(weaponInfo)
	{
	}

	void OnExecute() override
	{
		WEAPON::GIVE_WEAPON_TO_PED(PlayerPed(), MISC::GET_HASH_KEY(weaponInfo.Model), 9999, false, true);
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
		Hash weaponHash = 0;
		if (WEAPON::GET_CURRENT_PED_WEAPON(PlayerPed(), &weaponHash, true))
		{
			WEAPON::REMOVE_WEAPON_FROM_PED(PlayerPed(), weaponHash);
		}
	}
};

export class NoReload : public SwitchItem
{
public:
	constexpr NoReload(WString caption) : SwitchItem(caption)
	{
	}

	void OnUpdate() override
	{
		Hash cur;
		if (WEAPON::GET_CURRENT_PED_WEAPON(PlayerPed(), &cur, true))
		{
			if (WEAPON::IS_WEAPON_VALID(cur))
			{
				int maxAmmo;
				if (WEAPON::GET_MAX_AMMO(PlayerPed(), cur, &maxAmmo))
				{
					WEAPON::SET_PED_AMMO(PlayerPed(), cur, maxAmmo, true);

					maxAmmo = WEAPON::GET_MAX_AMMO_IN_CLIP(PlayerPed(), cur, 1);
					if (maxAmmo > 0)
					{
						WEAPON::SET_AMMO_IN_CLIP(PlayerPed(), cur, maxAmmo);
					}
				}
			}
		}
	}
};

export class FireAmmo : public SwitchItem
{
public:
	constexpr FireAmmo(WString caption) : SwitchItem(caption)
	{
	}

	void OnUpdate() override
	{
		if (ENTITY::DOES_ENTITY_EXIST(PlayerPed()))
		{
			MISC::SET_FIRE_AMMO_THIS_FRAME(PlayerID());
		}
	}
};

export class ExplosiveAmmo : public SwitchItem
{
public:
	constexpr ExplosiveAmmo(WString caption) : SwitchItem(caption)
	{
	}

	void OnUpdate() override
	{
		if (ENTITY::DOES_ENTITY_EXIST(PlayerPed()))
		{
			MISC::SET_EXPLOSIVE_AMMO_THIS_FRAME(PlayerID());
		}
	}
};

export class ExplosiveMelee : public SwitchItem
{
public:
	constexpr ExplosiveMelee(WString caption) : SwitchItem(caption)
	{
	}

	void OnUpdate() override
	{
		if (ENTITY::DOES_ENTITY_EXIST(PlayerPed()))
		{
			MISC::SET_EXPLOSIVE_MELEE_THIS_FRAME(PlayerID());
		}
	}
};