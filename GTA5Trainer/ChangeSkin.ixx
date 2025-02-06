export module ChangeSkin;

import "Base.h";
import Function;
import Util;
import Menu;
import PedModels;
import ItemInfo;


bool ChangedSkin = false;

export class ChangeSkin : public TriggerItem
{
public:
	constexpr ChangeSkin(ItemInfo& skinInfo) : TriggerItem(skinInfo.Caption), SkinInfo(skinInfo)
	{
	}

private:
	ItemInfo SkinInfo;

protected:
	void OnExecute() override
	{
		PlaySounds();
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

export class AutoFallBackSkin : public SwitchItem
{
public:
	constexpr AutoFallBackSkin(String caption) : SwitchItem(caption){

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
				if (model != MISC::GET_HASH_KEY("player_zero") && model != MISC::GET_HASH_KEY("player_one") && model != MISC::GET_HASH_KEY("player_two"))
				{
					SetTips("角色变成正常模型");
					ThreadSleep(1000);

					model = MISC::GET_HASH_KEY("player_zero");
					STREAMING::REQUEST_MODEL(model);
					while (!STREAMING::HAS_MODEL_LOADED(model))
					{
						ThreadSleep(0);
					}
						
					PLAYER::SET_PLAYER_MODEL(PlayerID(), model);
					PED::SET_PED_DEFAULT_COMPONENT_VARIATION(PlayerPed());
					STREAMING::SET_MODEL_AS_NO_LONGER_NEEDED(model);
					while (ENTITY::IS_ENTITY_DEAD(PlayerPed(), false) || PLAYER::IS_PLAYER_BEING_ARRESTED(PlayerID(), true))
					{
						ThreadSleep(0);
					}
				}
				ChangedSkin = false;
				State = false;
			}
		}
	}
};