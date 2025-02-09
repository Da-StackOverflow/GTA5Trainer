export module Weather;

import "Base.h";
import Function;
import Util;
import Menu;
import ItemInfo;

const char* _Weather = null;

export class ChangeWeather : public TriggerItem
{
private:
	ItemInfo _weather;
public:
	constexpr ChangeWeather(ItemInfo weather) : TriggerItem(weather.Caption), _weather(weather)
	{

	}

protected:
	void OnExecute() override
	{
		MISC::SET_WEATHER_TYPE_NOW_PERSIST(_weather.Model);
		MISC::CLEAR_WEATHER_TYPE_PERSIST();
		_Weather = _weather.Model;
	}
};

export class SetWind : public SwitchItem
{
public:
	constexpr SetWind(WString caption) : SwitchItem(caption)
	{

	}

protected:
	void OnActive() override
	{
		MISC::SET_WIND(1.0f);
		MISC::SET_WIND_SPEED(11.99f);
		MISC::SET_WIND_DIRECTION(ENTITY::GET_ENTITY_HEADING(PlayerPed()));
	}

	void OnInactive() override
	{
		MISC::SET_WIND(0.0f);
		MISC::SET_WIND_SPEED(0.0f);
	}
};

export class StandCurrentWeather : public SwitchItem
{
private:
	const char* _weather;
public:
	constexpr StandCurrentWeather(WString caption) : SwitchItem(caption), _weather(null)
	{

	}

protected:
	void OnUpdate() override
	{
		if (_weather != _Weather)
		{
			_weather = _Weather;
			MISC::SET_OVERRIDE_WEATHER(_weather);
		}
	}

	void OnInactive() override
	{
		
	}
};