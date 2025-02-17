using Bridge;

namespace Player
{
	public class Entry : Bridge.Entry
	{
		public override void OnInit()
		{
			Log.Info("Player OnInit");
			MenuController.Instance.MainMenu.AddItem(new SubMenu("玩家系统", GetOrCreatePlayerMenu));
		}

		private Menu GetOrCreatePlayerMenu()
		{
			if (!MenuController.Instance.TryGetMenu("玩家系统", out Menu menu))
			{
				menu = new Menu("玩家系统");
				menu.AddItem(new SubMenu("传送", GetOrCreatePlayerTeleportMenu));
				menu.AddItem(new SubMenu("通缉等级修改", GetOrCreateWantedMenu));
				menu.AddItem(new FastSwim("快速游泳"));
				menu.AddItem(new FastRun("快速跑"));
				menu.AddItem(new SuperJump("超级跳"));
				menu.AddItem(new FixPlayer("恢复生命"));
				menu.AddItem(new RestoreStamina("恢复体力"));
				menu.AddItem(new RestoreBreath("恢复氧气"));
				menu.AddItem(new DisableVehicleImpactRagdoll("取消车辆碰撞的布娃娃"));
				menu.AddItem(new PlayerInvincible("无敌"));
				menu.AddItem(new UnlimitedAbility("无限特殊能力"));
				menu.AddItem(new UnlimitedStamina("无限体力"));
				menu.AddItem(new UnlimitedBreath("无限氧气"));
				menu.AddItem(new NoNoise("无声"));
				menu.AddItem(new SubMenu("增加金钱", GetOrCreateAddCashMenu));
				menu.AddItem(new SubMenu("改变玩家模型", GetOrCreatePlayerChangeSkinMenu));
				menu.AddItem(new FallBackSkinWhenDead("当玩家死亡后自动恢复成默认皮肤"));
				menu.AddItem(new FallBackSkin("恢复成默认皮肤"));
				MenuController.Instance.Register(menu);
			}
			return menu;
		}

		private Menu GetOrCreatePlayerChangeSkinMenu()
		{
			if (!MenuController.Instance.TryGetMenu("改变玩家模型", out Menu menu))
			{
				menu = new Menu("改变玩家模型");
				menu.AddItem(new FallBackSkinWhenDead("自动换回默认模型"));
				var length = PlayerResources.PedModelInfos.Length;
				for (var i = 0; i < length; i++)
				{
					menu.AddItem(new ChangeSkin(PlayerResources.PedModelInfos[i]));
				}
				MenuController.Instance.Register(menu);
			}
			return menu;
		}

		private Menu GetOrCreatePlayerTeleportMenu()
		{
			if (!MenuController.Instance.TryGetMenu("传送", out Menu menu))
			{
				menu = new Menu("传送");
				menu.AddItem(new TeleportMarker("传送到地图标记点"));
				menu.AddItem(new GetTeleportCurrentCords("显示当前玩家位置坐标"));
				menu.AddItem(new GetTeleportMarkerCords("显示地图标记点坐标"));
				menu.AddItem(new Teleport("武装国度", 16.652f, -1116.532f, 29.791f));
				menu.AddItem(new Teleport("改车店", 1182.520f, 2651.934f, 37.810f));
				menu.AddItem(new Teleport("乞里耶德山", 499.739f, 5589.924f, 794.821f));
				menu.AddItem(new Teleport("花园银行顶部", -79.241f, -821.336f, 326.175f));
				menu.AddItem(new Teleport("在建大楼顶部", -148.989f, -962.854f, 269.135f));
				menu.AddItem(new Teleport("塔吊顶部", -119.054f, -976.211f, 296.197f));
				MenuController.Instance.Register(menu);
			}
			return menu;
		}

		private Menu GetOrCreateAddCashMenu()
		{
			if (!MenuController.Instance.TryGetMenu("增加金钱", out Menu menu))
			{
				menu = new Menu("增加金钱");
				menu.AddItem(new AddCash("给麦克增加一万", 0, 10000));
				menu.AddItem(new AddCash("给麦克增加一百万", 0, 1000000));
				menu.AddItem(new AddCash("给富兰克林增加一万", 1, 10000));
				menu.AddItem(new AddCash("给富兰克林增加一百万", 1, 1000000));
				menu.AddItem(new AddCash("给崔佛增加一万", 2, 10000));
				menu.AddItem(new AddCash("给崔佛增加一百万", 2, 1000000));
				MenuController.Instance.Register(menu);
			}
			return menu;
		}

		private Menu GetOrCreateWantedMenu()
		{
			if (!MenuController.Instance.TryGetMenu("通缉等级修改", out Menu menu))
			{
				menu = new Menu("通缉等级修改");
				menu.AddItem(new ClearWanted("清除通缉等级"));
				menu.AddItem(new ModifyWantedLevel("增加1颗星", 1));
				menu.AddItem(new ModifyWantedLevel("减少1颗星", 1));
				menu.AddItem(new NeverWanted("不再受通缉"));
				menu.AddItem(new PoliceIgnore("警察忽视玩家"));
				MenuController.Instance.Register(menu);
			}
			return menu;
		}
	}
}
