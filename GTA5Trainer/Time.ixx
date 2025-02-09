export module Time;

import "Base.h";
import Function;
import Util;
import Menu;

export class TimeModify : public TriggerItem
{
private:
	int _hour;
public:
	constexpr TimeModify(WString caption, int hour) : TriggerItem(caption), _hour(hour)
	{

	}

protected:
	void OnExecute() override
	{
		int h = CLOCK::GET_CLOCK_HOURS();
		h += _hour;
		h += 24;
		h %= 24;
		int m = CLOCK::GET_CLOCK_MINUTES();
		CLOCK::SET_CLOCK_TIME(h, m, 0);
		SetTips(ToString(h, m));
	}
};

export class TimeSynced : public SwitchItem
{
public:
	constexpr TimeSynced(WString caption) : SwitchItem(caption)
	{

	}

protected:
	void OnUpdate() override
	{
		var ticks = GetTimeTicks();
		var stamp = ticks / 1000;
		CLOCK::SET_CLOCK_TIME(stamp / 3600 % 24, stamp / 60 % 60, stamp % 60);
	}
};

export class TimePause : public SwitchItem
{
public:
	constexpr TimePause(WString caption) : SwitchItem(caption)
	{

	}

protected:
	void OnActive() override
	{
		CLOCK::PAUSE_CLOCK(true);
	}

	void OnInactive() override
	{
		CLOCK::PAUSE_CLOCK(false);
	}
};