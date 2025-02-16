using Bridge;
using static Weather.WeatherResources;

namespace Weather
{
	public class Entry : Bridge.Entry
	{
		public override void OnInit()
		{
			Log.Info("Weather OnInit");
			MenuController.Instance.MainMenu.AddItem(new SubMenu("天气系统", GetOrCreateWeatherMenu));
		}

		static Menu GetOrCreateChangeWeatherMenu()
		{
			if (!MenuController.Instance.TryGetMenu("改变天气", out Menu menu))
			{
				menu = new Menu("改变天气");
				var length = WeatherInfos.Length;
				for (int i = 0; i < length; i++)
				{
					menu.AddItem(new ChangeWeather(WeatherInfos[i]));
				}
				MenuController.Instance.Register(menu);
			}
			return menu;
		}

		static Menu GetOrCreateWeatherMenu()
		{
			if (!MenuController.Instance.TryGetMenu("天气系统", out Menu menu))
			{
				menu = new Menu("天气系统");
				menu.AddItem(new SubMenu("改变天气", GetOrCreateChangeWeatherMenu));
				menu.AddItem(new StandChangedWeather("保持改变后的天气"));
				menu.AddItem(new SetWind("生成大风"));
				MenuController.Instance.Register(menu);
				
			}
			return menu;
		}
	}
}