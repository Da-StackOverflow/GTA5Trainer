using Bridge;

namespace Other
{
	public class Entry : Bridge.Entry
	{
		public override void OnInit()
		{
			Log.Info("Other OnInit");
			MenuController.Instance.MainMenu.AddItem(new SubMenu("其他系统", GetOrCreateOtherMenu));
		}

		static Menu GetOrCreateOtherMenu()
		{
			if (!MenuController.Instance.TryGetMenu("其他系统", out Menu menu))
			{
				menu = new Menu("其他系统");
				menu.AddItem(new MoonGravity("月球引力"));
				menu.AddItem(new RandomCops("随机警察"));
				menu.AddItem(new RandomTrains("随机火车"));
				menu.AddItem(new RandomBoats("随机船"));
				menu.AddItem(new RandomGarbageTrucks("随机垃圾车"));
				menu.AddItem(new NextRadioTrack("下一首车载音乐"));
				menu.AddItem(new HideHud("隐藏Hud"));
				menu.AddItem(new AchieveAllAchievements("达成所有成就"));
				MenuController.Instance.Register(menu);
			}
			return menu;
		}
	}
}