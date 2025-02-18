using ScriptUI;

namespace Time
{
	public class Entry : AScriptEntry
	{
		protected override void OnInit()
		{
			Log.Info("Time OnInit");
			_controller.MainMenu.AddItem(new SubMenu("时间系统", GetOrCreateTimeMenu));
		}

		private Menu GetOrCreateTimeMenu()
		{
			if (!_controller.TryGetMenu("时间系统", out Menu menu))
			{
				menu = new Menu("时间系统");
				menu.AddItem(new TimeModify("前进一小时", 1));
				menu.AddItem(new TimeModify("后退一小时", -1));
				menu.AddItem(new TimePause("暂停时间"));
				menu.AddItem(new TimeSynced("真实时间"));
				_controller.Register(menu);
			}
			return menu;
		}
	}
}
