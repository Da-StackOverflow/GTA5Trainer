using Bridge;

namespace Time
{
	public class Entry : Bridge.Entry
	{
		public override void OnInit()
		{
			Log.Info("Time OnInit");
			MenuController.Instance.MainMenu.AddItem(new SubMenu("时间系统", GetOrCreateTimeMenu));
		}

		static Menu GetOrCreateTimeMenu()
		{
			if (!MenuController.Instance.TryGetMenu("时间系统", out Menu menu))
			{
				menu = new Menu("时间系统");
				menu.AddItem(new TimeModify("前进一小时", 1));
				menu.AddItem(new TimeModify("后退一小时", -1));
				menu.AddItem(new TimePause("暂停时间"));
				menu.AddItem(new TimeSynced("真实时间"));
				MenuController.Instance.Register(menu);
			}
			return menu;
		}
	}
}
