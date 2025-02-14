using Bridge;

public class Entrance : Entry
{
	public override void OnInit()
	{
		Log.Info("GTA5TrainerScript OnInit");
		MenuController.Instance.MainMenu.AddItem(new SubMenu("玩家系统", GetOrPlayerSystemMenu));
	}

	private Menu GetOrPlayerSystemMenu()
	{
		if (MenuController.Instance.TryGetMenu("玩家系统", out Menu menu))
		{
			menu = new Menu("玩家系统");
			MenuController.Instance.Register(menu);
		}
		return menu;
	}
}
