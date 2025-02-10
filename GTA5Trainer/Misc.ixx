export module Misc;

import "Base.h";
import Function;
import Util;
import Menu;

export class MoonGravity : public SwitchItem
{
public:
	constexpr MoonGravity(WString caption) : SwitchItem(caption)
	{

	}

protected:
	void OnActive() override
	{
		MISC::SET_GRAVITY_LEVEL(2);
	}

	void OnInactive() override
	{
		MISC::SET_GRAVITY_LEVEL(0);
	}
};

export class RandomCops : public SwitchItem
{
public:
	constexpr RandomCops(WString caption) : SwitchItem(caption)
	{

	}

protected:
	void OnActive() override
	{
		if (PED::CAN_CREATE_RANDOM_COPS())
		{
			PED::SET_CREATE_RANDOM_COPS(true);
		}
	}

	void OnInactive() override
	{
		PED::SET_CREATE_RANDOM_COPS(false);
	}
};

export class RandomTrains : public SwitchItem
{
public:
	constexpr RandomTrains(WString caption) : SwitchItem(caption)
	{

	}

protected:
	void OnActive() override
	{
		VEHICLE::SET_RANDOM_TRAINS(true);
	}

	void OnInactive() override
	{
		VEHICLE::SET_RANDOM_TRAINS(false);
	}
};

export class RandomBoats : public SwitchItem
{
public:
	constexpr RandomBoats(WString caption) : SwitchItem(caption)
	{

	}

protected:
	void OnActive() override
	{
		VEHICLE::SET_RANDOM_BOATS(true);
	}

	void OnInactive() override
	{
		VEHICLE::SET_RANDOM_BOATS(false);
	}
};

export class RandomGarbageTrucks : public SwitchItem
{
public:
	constexpr RandomGarbageTrucks(WString caption) : SwitchItem(caption)
	{

	}

protected:
	void OnActive() override
	{
		VEHICLE::SET_GARBAGE_TRUCKS(true);
	}

	void OnInactive() override
	{
		VEHICLE::SET_GARBAGE_TRUCKS(false);
	}
};

export class NextRadioTrack : public TriggerItem
{
public:
	constexpr NextRadioTrack(WString caption) : TriggerItem(caption)
	{

	}

protected:
	void OnExecute() override
	{
		if (ENTITY::DOES_ENTITY_EXIST(PlayerPed()) && PED::IS_PED_IN_ANY_VEHICLE(PlayerPed(), 0))
		{
			AUDIO::SKIP_RADIO_FORWARD();
		}
	}
};

export class HideHud : public SwitchItem
{
public:
	constexpr HideHud(WString caption) : SwitchItem(caption)
	{

	}

protected:
	void OnUpdate() override
	{
		HUD::HIDE_HUD_AND_RADAR_THIS_FRAME();
	}
};

export class AchieveAllAchievements : public TriggerItem
{
public:
	constexpr AchieveAllAchievements(WString caption) : TriggerItem(caption)
	{

	}

protected:
	void OnExecute() override
	{
		for (int i = 1; i <= 77; i++)
		{
			if (!PLAYER::HAS_ACHIEVEMENT_BEEN_PASSED(i))
			{
				PLAYER::GIVE_ACHIEVEMENT_TO_PLAYER(i);
			}
		}
	}
};