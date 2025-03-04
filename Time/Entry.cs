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
				menu.AddItem(new SubMenu("子弹时间", GetOrCreateBulletTimeMenu));
				menu.AddItem(new TimeModify("前进一小时", 1));
				menu.AddItem(new TimeModify("后退一小时", -1));
				menu.AddItem(new TimePause("暂停时间"));
				menu.AddItem(new TimeSynced("真实时间"));
				_controller.Register(menu);
			}
			return menu;
		}

		private Menu GetOrCreateBulletTimeMenu()
		{
			if (!_controller.TryGetMenu("子弹时间", out Menu menu))
			{
				menu = new Menu("子弹时间");
				menu.AddItem(new BulletTime("放慢5倍", 0.2f));
				menu.AddItem(new BulletTime("放慢2倍", 0.5f));
				menu.AddItem(new BulletTime("放慢3倍", 0.3333f));
				menu.AddItem(new BulletTime("放慢4倍", 0.25f));
				menu.AddItem(new BulletTime("放慢10倍", 0.1f));
				_controller.Register(menu);
			}
			return menu;
		}
	}
}
