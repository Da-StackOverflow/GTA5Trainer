export module Player;

import "Base.h";
import Function;
import Util;
import Menu;
import PedModelInfos;
import ItemInfo;

bool ChangedSkin = false;

export class ChangeSkin : public TriggerItem
{
public:
	constexpr ChangeSkin(const ItemInfo& skinInfo) : TriggerItem(skinInfo.Caption), SkinInfo(skinInfo)
	{
	}

private:
	ItemInfo SkinInfo;

protected:
	void OnExecute() override
	{
		uint model = MISC::GET_HASH_KEY(SkinInfo.Model);
		if (STREAMING::IS_MODEL_IN_CDIMAGE(model) && STREAMING::IS_MODEL_VALID(model))
		{
			STREAMING::REQUEST_MODEL(model);
			while (!STREAMING::HAS_MODEL_LOADED(model))
			{
				ThreadSleep(0);
			}
			PLAYER::SET_PLAYER_MODEL(PlayerID(), model);
			PED::SET_PED_DEFAULT_COMPONENT_VARIATION(PlayerPed());
			ThreadSleep(0);
			for (int i = 0; i < 12; i++)
			{
				for (int j = 0; j < 100; j++)
				{
					int drawable = Random(0, 100) % 10;
					int texture = Random(0, 100) % 10;
					if (PED::IS_PED_COMPONENT_VARIATION_VALID(PLAYER::PLAYER_PED_ID(), i, drawable, texture))
					{
						PED::SET_PED_COMPONENT_VARIATION(PLAYER::PLAYER_PED_ID(), i, drawable, texture, 0);
						break;
					}
				}
			}
			ThreadSleep(100);
			STREAMING::SET_MODEL_AS_NO_LONGER_NEEDED(model);
			ChangedSkin = true;
		}
	}
};

export class FallBackSkinWhenDead : public SwitchItem
{
public:
	constexpr FallBackSkinWhenDead(WString caption) : SwitchItem(caption)
	{

	}

protected:
	void OnUpdate() override
	{
		FallBackSkin();
	}

private:
	void FallBackSkin()
	{
		if (ChangedSkin)
		{
			if (!ENTITY::DOES_ENTITY_EXIST(PlayerPed()))
			{
				return;
			}

			Hash model = ENTITY::GET_ENTITY_MODEL(PlayerPed());
			if (ENTITY::IS_ENTITY_DEAD(PlayerPed(), false) || PLAYER::IS_PLAYER_BEING_ARRESTED(PlayerID(), true))
			{
				PED::SET_PED_DEFAULT_COMPONENT_VARIATION(PlayerPed());
				ChangedSkin = false;
			}
		}
	}
};

export class FallBackSkin : public TriggerItem
{
public:
	constexpr FallBackSkin(WString caption) : TriggerItem(caption)
	{

	}

protected:
	void OnExecute() override
	{
		PED::SET_PED_DEFAULT_COMPONENT_VARIATION(PlayerPed());
		ChangedSkin = false;
	}
};

export class FixPlayer : public TriggerItem
{
public:
	constexpr FixPlayer(WString caption) : TriggerItem(caption)
	{

	}

	void OnExecute() override
	{
		ENTITY::SET_ENTITY_HEALTH(PlayerPed(), ENTITY::GET_ENTITY_MAX_HEALTH(PlayerPed()), 0, 0);
		PED::ADD_ARMOUR_TO_PED(PlayerPed(), PLAYER::GET_PLAYER_MAX_ARMOUR(PlayerID()) - PED::GET_PED_ARMOUR(PlayerPed()));
		if (PED::IS_PED_IN_ANY_VEHICLE(PlayerPed(), 0))
		{
			Vehicle playerVeh = PED::GET_VEHICLE_PED_IS_USING(PlayerPed());
			if (ENTITY::DOES_ENTITY_EXIST(playerVeh) && !ENTITY::IS_ENTITY_DEAD(playerVeh, false))
			{
				VEHICLE::SET_VEHICLE_FIXED(playerVeh);
			}
		}
		SetTips(L"Íæ¼ÒÈ«²¿»Ö¸´");
	}
};

export class AddCash : public TriggerItem
{
private:
	int Cash;
	int Player;
public:
	constexpr AddCash(WString caption, int player, int cash) : TriggerItem(caption), Cash(cash), Player(player)
	{

	}

	void OnExecute() override
	{
		switch (Player)
		{
			case 0:
			{
				Hash hash = MISC::GET_HASH_KEY("SP0_TOTAL_CASH");
				SetCash(hash);
				break;
			}
			case 1:
			{
				Hash hash = MISC::GET_HASH_KEY("SP1_TOTAL_CASH");
				SetCash(hash);
				break;
			}
			case 2:
			{
				Hash hash = MISC::GET_HASH_KEY("SP2_TOTAL_CASH");
				SetCash(hash);
				break;
			}
		}

	}
private:
	inline void SetCash(Hash hash) const
	{
		int val;
		STATS::STAT_GET_INT(hash, &val, -1);
		val += Cash;
		STATS::STAT_SET_INT(hash, val, 1);
	}
};

export class ModifyWantedLevel : public TriggerItem
{
private:
	int StarCount;
public:
	constexpr ModifyWantedLevel(WString caption, int starCount) : TriggerItem(caption), StarCount(starCount)
	{

	}

	void OnExecute() override
	{
		if (ENTITY::DOES_ENTITY_EXIST(PlayerPed()) && PLAYER::GET_PLAYER_WANTED_LEVEL(PlayerID()) < 5)
		{
			var current = PLAYER::GET_PLAYER_WANTED_LEVEL(PlayerID());
			var wanted = current + StarCount;
			wanted = wanted < 0 ? 0 : wanted;
			wanted = wanted > 5 ? 5 : wanted;

			PLAYER::SET_PLAYER_WANTED_LEVEL(PlayerID(), wanted, false);
			PLAYER::SET_PLAYER_WANTED_LEVEL_NOW(PlayerID(), false);
		}
	}
};

export class ClearWanted : public TriggerItem
{
public:
	constexpr ClearWanted(WString caption) : TriggerItem(caption)
	{

	}

protected:
	void OnExecute() override
	{
		if (ENTITY::DOES_ENTITY_EXIST(PlayerPed()))
		{
			PLAYER::CLEAR_PLAYER_WANTED_LEVEL(PlayerID());
		}
	}
};

export class NeverWanted : public SwitchItem
{
public:
	constexpr NeverWanted(WString caption) : SwitchItem(caption)
	{

	}

protected:
	void OnUpdate() override
	{
		if (ENTITY::DOES_ENTITY_EXIST(PlayerPed()))
		{
			PLAYER::CLEAR_PLAYER_WANTED_LEVEL(PlayerID());
		}
	}
};

export class PlayerInvincible : public SwitchItem
{
public:
	constexpr PlayerInvincible(WString caption) : SwitchItem(caption)
	{

	}

protected:
	void OnActive() override
	{
		if (ENTITY::DOES_ENTITY_EXIST(PlayerPed()))
		{
			PLAYER::SET_PLAYER_INVINCIBLE(PlayerID(), true);
		}
	}

	//void OnUpdate() override
	//{
	//	if (ENTITY::DOES_ENTITY_EXIST(PlayerPed()))
	//	{
	//		PLAYER::SET_PLAYER_INVINCIBLE(PlayerID(), true);
	//	}
	//}

	void OnInactive() override
	{
		if (ENTITY::DOES_ENTITY_EXIST(PlayerPed()))
		{
			PLAYER::SET_PLAYER_INVINCIBLE(PlayerID(), false);
		}
	}
};

export class PoliceIgnore : public SwitchItem
{
public:
	constexpr PoliceIgnore(WString caption) : SwitchItem(caption)
	{

	}

protected:
	void OnActive() override
	{
		if (ENTITY::DOES_ENTITY_EXIST(PlayerPed()))
		{
			PLAYER::SET_POLICE_IGNORE_PLAYER(PlayerID(), true);
		}
	}

	//void OnUpdate() override
	//{
	//	if (ENTITY::DOES_ENTITY_EXIST(PlayerPed()))
	//	{
	//		PLAYER::SET_POLICE_IGNORE_PLAYER(PlayerID(), true);
	//	}
	//}

	void OnInactive() override
	{
		if (ENTITY::DOES_ENTITY_EXIST(PlayerPed()))
		{
			PLAYER::SET_POLICE_IGNORE_PLAYER(PlayerID(), false);
		}
	}
};

export class UnlimitedAbility : public SwitchItem
{
public:
	constexpr UnlimitedAbility(WString caption) : SwitchItem(caption)
	{

	}

protected:
	void OnUpdate() override
	{
		if (ENTITY::DOES_ENTITY_EXIST(PlayerPed()))
		{
			PLAYER::SPECIAL_ABILITY_FILL_METER(PlayerID(), true, 0);
		}
	}
};

export class NoNoise : public SwitchItem
{
public:
	constexpr NoNoise(WString caption) : SwitchItem(caption)
	{

	}

protected:
	void OnActive() override
	{
		if (ENTITY::DOES_ENTITY_EXIST(PlayerPed()))
		{
			PLAYER::SET_PLAYER_NOISE_MULTIPLIER(PlayerID(), 0.0f);
		}
	}

	//void OnUpdate() override
	//{
	//	if (ENTITY::DOES_ENTITY_EXIST(PlayerPed()))
	//	{
	//		PLAYER::SET_PLAYER_NOISE_MULTIPLIER(PlayerID(), 0.0f);
	//	}
	//}

	void OnInactive() override
	{
		if (ENTITY::DOES_ENTITY_EXIST(PlayerPed()))
		{
			PLAYER::SET_PLAYER_NOISE_MULTIPLIER(PlayerID(), 1.0f);
		}
	}
};

export class FastSwim : public SwitchItem
{
public:
	constexpr FastSwim(WString caption) : SwitchItem(caption)
	{

	}

protected:
	void OnActive() override
	{
		if (ENTITY::DOES_ENTITY_EXIST(PlayerPed()))
		{
			PLAYER::SET_SWIM_MULTIPLIER_FOR_PLAYER(PlayerID(), 1.49f);
		}
	}

	//void OnUpdate() override
	//{
	//	if (ENTITY::DOES_ENTITY_EXIST(PlayerPed()))
	//	{
	//		PLAYER::SET_SWIM_MULTIPLIER_FOR_PLAYER(PlayerID(), 1.49f);
	//	}
	//}

	void OnInactive() override
	{
		if (ENTITY::DOES_ENTITY_EXIST(PlayerPed()))
		{
			PLAYER::SET_SWIM_MULTIPLIER_FOR_PLAYER(PlayerID(), 1.0f);
		}
	}
};

export class FastRun : public SwitchItem
{
public:
	constexpr FastRun(WString caption) : SwitchItem(caption)
	{

	}

protected:
	void OnActive() override
	{
		if (ENTITY::DOES_ENTITY_EXIST(PlayerPed()))
		{
			PLAYER::SET_RUN_SPRINT_MULTIPLIER_FOR_PLAYER(PlayerID(), 1.49f);
		}
	}

	//void OnUpdate() override
	//{
	//	if (ENTITY::DOES_ENTITY_EXIST(PlayerPed()))
	//	{
	//		PLAYER::SET_RUN_SPRINT_MULTIPLIER_FOR_PLAYER(PlayerID(), 1.49f);
	//	}
	//}

	void OnInactive() override
	{
		if (ENTITY::DOES_ENTITY_EXIST(PlayerPed()))
		{
			PLAYER::SET_RUN_SPRINT_MULTIPLIER_FOR_PLAYER(PlayerID(), 1.0f);
		}
	}
};

export class SuperJump : public SwitchItem
{
public:
	constexpr SuperJump(WString caption) : SwitchItem(caption)
	{

	}

protected:
	void OnUpdate() override
	{
		if (ENTITY::DOES_ENTITY_EXIST(PlayerPed()))
		{
			MISC::SET_SUPER_JUMP_THIS_FRAME(PlayerID());
		}
	}
};