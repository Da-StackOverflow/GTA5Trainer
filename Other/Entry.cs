using ScriptUI;

namespace Other
{
	public class Entry : AScriptEntry
	{
		protected override void OnInit()
		{
			Log.Info("Other OnInit");
			_controller.MainMenu.AddItem(new SubMenu("其他系统", GetOrCreateOtherMenu));
		}

		private Menu GetOrCreateOtherMenu()
		{
			if (!_controller.TryGetMenu("其他系统", out Menu menu))
			{
				menu = new Menu("其他系统");
				menu.AddItem(new BigMiniMap("小地图范围放大"));
				menu.AddItem(new MoonGravity("月球引力"));
				menu.AddItem(new RandomCops("随机警察"));
				menu.AddItem(new RandomTrains("随机火车"));
				menu.AddItem(new RandomBoats("随机船"));
				menu.AddItem(new RandomGarbageTrucks("随机垃圾车"));
				menu.AddItem(new NextRadioTrack("下一首车载音乐"));
				menu.AddItem(new HideHud("隐藏Hud"));
				menu.AddItem(new AchieveAllAchievements("达成所有成就"));
				_controller.Register(menu);
			}
			return menu;
		}
	}
}