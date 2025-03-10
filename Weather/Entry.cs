﻿using ScriptUI;
using static Weather.WeatherResources;

namespace Weather
{
	public class Entry : AScriptEntry
	{
		protected override void OnInit()
		{
			Log.Info("Weather OnInit");
			_controller.MainMenu.AddItem(new SubMenu("天气系统", GetOrCreateWeatherMenu));
		}

		private Menu GetOrCreateChangeWeatherMenu()
		{
			if (!_controller.TryGetMenu("改变天气", out Menu menu))
			{
				menu = new Menu("改变天气");
				var length = WeatherInfos.Length;
				for (int i = 0; i < length; i++)
				{
					menu.AddItem(new ChangeWeather(WeatherInfos[i]));
				}
				_controller.Register(menu);
			}
			return menu;
		}

		private Menu GetOrCreateWeatherMenu()
		{
			if (!_controller.TryGetMenu("天气系统", out Menu menu))
			{
				menu = new Menu("天气系统");
				menu.AddItem(new SubMenu("改变天气", GetOrCreateChangeWeatherMenu));
				menu.AddItem(new StandChangedWeather("保持改变后的天气"));
				menu.AddItem(new SetWind("生成大风"));
				_controller.Register(menu);
				
			}
			return menu;
		}
	}
}