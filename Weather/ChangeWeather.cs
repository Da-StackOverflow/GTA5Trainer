using ScriptUI;
using static ScriptUI.Functions;

namespace Weather
{
	internal sealed class ChangeWeather : TriggerItem
	{
		private readonly ItemInfo _weather;
		public ChangeWeather(ItemInfo weather) : base(weather.Name)
		{
			_weather = weather;
		}

		protected override void OnExecute()
		{
			SET_WEATHER_TYPE_NOW_PERSIST(_weather.HashKey);
			CLEAR_WEATHER_TYPE_PERSIST();
			GlobalValue.SetStringValue("Weather.ChangeWeather.CurrentWeather", _weather.HashKey);
		}
	}
}
