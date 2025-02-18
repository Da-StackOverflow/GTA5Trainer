using Bridge;
using static Bridge.Functions;
using static Player.PlayerResources;

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
				menu.AddItem(new RestoreAbility("恢复特殊能力"));
				menu.AddItem(new RestoreStamina("恢复体力"));
				menu.AddItem(new RestoreBreath("恢复氧气"));
				menu.AddItem(new DisableHurttRagdoll("取消受伤害的布娃娃效果"));
				menu.AddItem(new PlayerInvincible("无敌"));
				menu.AddItem(new UnlimitedAbility("无限特殊能力"));
				menu.AddItem(new UnlimitedStamina("无限体力"));
				menu.AddItem(new UnlimitedBreath("无限氧气"));
				menu.AddItem(new NoNoise("无声"));
				menu.AddItem(new SubMenu("增加金钱", GetOrCreateAddCashMenu));
				menu.AddItem(new SubMenu("生成NPC", GetOrCreateSpawnPedMenu));
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
				menu.AddItem(new SubMenu("变成动物", GetOrCreatePlayerChangeToAnimalMenu));
				menu.AddItem(new SubMenu("变成男人", GetOrCreatePlayerChangeToMaleMenu));
				menu.AddItem(new SubMenu("变成女人", GetOrCreatePlayerChangeToFemalMenu));
				menu.AddItem(new SubMenu("变成医生", GetOrCreatePlayerChangeToMedicMenu));
				menu.AddItem(new SubMenu("变成警察", GetOrCreatePlayerChangeToCopMenu));
				menu.AddItem(new SubMenu("变成军人", GetOrCreatePlayerChangeToArmyMenu));
				menu.AddItem(new SubMenu("变成消防员", GetOrCreatePlayerChangeToFiremanMenu));
				menu.AddItem(new SubMenu("变成国安局特工", GetOrCreatePlayerChangeToSwatMenu));
				menu.AddItem(new SubMenu("变成主角", GetOrCreatePlayerChangeToPlayerMenu));
				MenuController.Instance.Register(menu);
			}
			return menu;
		}

		private Menu GetOrCreatePlayerChangeToAnimalMenu()
		{
			if (!MenuController.Instance.TryGetMenu("变成动物", out Menu menu))
			{
				menu = new Menu("变成动物");
				var length = Animals.Length;
				for (var i = 0; i < length; i++)
				{
					menu.AddItem(new ChangeSkin(Animals[i]));
				}
				MenuController.Instance.Register(menu);
			}
			return menu;
		}

		private Menu GetOrCreatePlayerChangeToMaleMenu()
		{
			if (!MenuController.Instance.TryGetMenu("变成男人", out Menu menu))
			{
				menu = new Menu("变成男人");
				var length = Male.Length;
				for (var i = 0; i < length; i++)
				{
					menu.AddItem(new ChangeSkin(Male[i]));
				}
				MenuController.Instance.Register(menu);
			}
			return menu;
		}

		private Menu GetOrCreatePlayerChangeToFemalMenu()
		{
			if (!MenuController.Instance.TryGetMenu("变成女人", out Menu menu))
			{
				menu = new Menu("变成女人");
				var length = Femal.Length;
				for (var i = 0; i < length; i++)
				{
					menu.AddItem(new ChangeSkin(Femal[i]));
				}
				MenuController.Instance.Register(menu);
			}
			return menu;
		}

		private Menu GetOrCreatePlayerChangeToMedicMenu()
		{
			if (!MenuController.Instance.TryGetMenu("变成医生", out Menu menu))
			{
				menu = new Menu("变成医生");
				var length = Medic.Length;
				for (var i = 0; i < length; i++)
				{
					menu.AddItem(new ChangeSkin(Medic[i]));
				}
				MenuController.Instance.Register(menu);
			}
			return menu;
		}

		private Menu GetOrCreatePlayerChangeToCopMenu()
		{
			if (!MenuController.Instance.TryGetMenu("变成警察", out Menu menu))
			{
				menu = new Menu("变成警察");
				var length = Cop.Length;
				for (var i = 0; i < length; i++)
				{
					menu.AddItem(new ChangeSkin(Cop[i]));
				}
				MenuController.Instance.Register(menu);
			}
			return menu;
		}

		private Menu GetOrCreatePlayerChangeToArmyMenu()
		{
			if (!MenuController.Instance.TryGetMenu("变成军人", out Menu menu))
			{
				menu = new Menu("变成军人");
				var length = Army.Length;
				for (var i = 0; i < length; i++)
				{
					menu.AddItem(new ChangeSkin(Army[i]));
				}
				MenuController.Instance.Register(menu);
			}
			return menu;
		}

		private Menu GetOrCreatePlayerChangeToFiremanMenu()
		{
			if (!MenuController.Instance.TryGetMenu("变成消防员", out Menu menu))
			{
				menu = new Menu("变成消防员");
				var length = Fireman.Length;
				for (var i = 0; i < length; i++)
				{
					menu.AddItem(new ChangeSkin(Fireman[i]));
				}
				MenuController.Instance.Register(menu);
			}
			return menu;
		}

		private Menu GetOrCreatePlayerChangeToSwatMenu()
		{
			if (!MenuController.Instance.TryGetMenu("变成国安局特工", out Menu menu))
			{
				menu = new Menu("变成国安局特工");
				var length = Swat.Length;
				for (var i = 0; i < length; i++)
				{
					menu.AddItem(new ChangeSkin(Swat[i]));
				}
				MenuController.Instance.Register(menu);
			}
			return menu;
		}

		private Menu GetOrCreatePlayerChangeToPlayerMenu()
		{
			if (!MenuController.Instance.TryGetMenu("变成主角", out Menu menu))
			{
				menu = new Menu("变成主角");
				{
					var length = Player0.Length;
					for (var i = 0; i < length; i++)
					{
						menu.AddItem(new ChangeSkin(Player0[i]));
					}
				}
				{
					var length = Player1.Length;
					for (var i = 0; i < length; i++)
					{
						menu.AddItem(new ChangeSkin(Player1[i]));
					}
				}
				{
					var length = Player2.Length;
					for (var i = 0; i < length; i++)
					{
						menu.AddItem(new ChangeSkin(Player2[i]));
					}
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
				menu.AddItem(new ModifyWantedLevel("减少1颗星", -1));
				menu.AddItem(new NeverWanted("不再受通缉"));
				menu.AddItem(new PoliceIgnore("警察忽视玩家"));
				menu.AddItem(new EveryOneIgnorePlayer("所有人忽视玩家"));
				MenuController.Instance.Register(menu);
			}
			return menu;
		}

		private Menu GetOrCreateSpawnPedMenu()
		{
			if (!MenuController.Instance.TryGetMenu("生成NPC", out Menu menu))
			{
				menu = new Menu("生成NPC");
				menu.AddItem(new SubMenu("生成动物", GetOrCreatePlayerSpawnAnimalMenu));
				menu.AddItem(new SubMenu("生成男人", GetOrCreatePlayerSpawnMaleMenu));
				menu.AddItem(new SubMenu("生成女人", GetOrCreatePlayerSpawnFemalMenu));
				menu.AddItem(new SubMenu("生成医生", GetOrCreatePlayerSpawnMedicMenu));
				menu.AddItem(new SubMenu("生成警察", GetOrCreatePlayerSpawnCopMenu));
				menu.AddItem(new SubMenu("生成军人", GetOrCreatePlayerSpawnArmyMenu));
				menu.AddItem(new SubMenu("生成消防员", GetOrCreatePlayerSpawnFiremanMenu));
				menu.AddItem(new SubMenu("生成国安局特工", GetOrCreatePlayerSpawnSwatMenu));
				menu.AddItem(new SubMenu("生成主角", GetOrCreatePlayerSpawnPlayerMenu));
				MenuController.Instance.Register(menu);
			}
			return menu;
		}

		private Menu GetOrCreatePlayerSpawnAnimalMenu()
		{
			if (!MenuController.Instance.TryGetMenu("生成动物", out Menu menu))
			{
				menu = new Menu("生成动物");
				var length = Animals.Length;
				for (var i = 0; i < length; i++)
				{
					menu.AddItem(new SpawnPed(Animals[i], PedType.ANIMAL));
				}
				MenuController.Instance.Register(menu);
			}
			return menu;
		}

		private Menu GetOrCreatePlayerSpawnMaleMenu()
		{
			if (!MenuController.Instance.TryGetMenu("生成男人", out Menu menu))
			{
				menu = new Menu("生成男人");
				var length = Male.Length;
				for (var i = 0; i < length; i++)
				{
					menu.AddItem(new SpawnPed(Male[i], PedType.CIVMALE));
				}
				MenuController.Instance.Register(menu);
			}
			return menu;
		}

		private Menu GetOrCreatePlayerSpawnFemalMenu()
		{
			if (!MenuController.Instance.TryGetMenu("生成女人", out Menu menu))
			{
				menu = new Menu("生成女人");
				var length = Femal.Length;
				for (var i = 0; i < length; i++)
				{
					menu.AddItem(new SpawnPed(Femal[i], PedType.CIVFEMALE));
				}
				MenuController.Instance.Register(menu);
			}
			return menu;
		}

		private Menu GetOrCreatePlayerSpawnMedicMenu()
		{
			if (!MenuController.Instance.TryGetMenu("生成医生", out Menu menu))
			{
				menu = new Menu("生成医生");
				var length = Medic.Length;
				for (var i = 0; i < length; i++)
				{
					menu.AddItem(new SpawnPed(Medic[i], PedType.MEDIC));
				}
				MenuController.Instance.Register(menu);
			}
			return menu;
		}

		private Menu GetOrCreatePlayerSpawnCopMenu()
		{
			if (!MenuController.Instance.TryGetMenu("生成警察", out Menu menu))
			{
				menu = new Menu("生成警察");
				var length = Cop.Length;
				for (var i = 0; i < length; i++)
				{
					menu.AddItem(new SpawnPed(Cop[i], PedType.COP));
				}
				MenuController.Instance.Register(menu);
			}
			return menu;
		}

		private Menu GetOrCreatePlayerSpawnArmyMenu()
		{
			if (!MenuController.Instance.TryGetMenu("生成军人", out Menu menu))
			{
				menu = new Menu("生成军人");
				var length = Army.Length;
				for (var i = 0; i < length; i++)
				{
					menu.AddItem(new SpawnPed(Army[i], PedType.ARMY));
				}
				MenuController.Instance.Register(menu);
			}
			return menu;
		}

		private Menu GetOrCreatePlayerSpawnFiremanMenu()
		{
			if (!MenuController.Instance.TryGetMenu("生成消防员", out Menu menu))
			{
				menu = new Menu("生成消防员");
				var length = Fireman.Length;
				for (var i = 0; i < length; i++)
				{
					menu.AddItem(new SpawnPed(Fireman[i], PedType.FIREMAN));
				}
				MenuController.Instance.Register(menu);
			}
			return menu;
		}

		private Menu GetOrCreatePlayerSpawnSwatMenu()
		{
			if (!MenuController.Instance.TryGetMenu("生成国安局特工", out Menu menu))
			{
				menu = new Menu("生成国安局特工");
				var length = Swat.Length;
				for (var i = 0; i < length; i++)
				{
					menu.AddItem(new SpawnPed(Swat[i], PedType.SWAT));
				}
				MenuController.Instance.Register(menu);
			}
			return menu;
		}

		private Menu GetOrCreatePlayerSpawnPlayerMenu()
		{
			if (!MenuController.Instance.TryGetMenu("生成主角", out Menu menu))
			{
				menu = new Menu("生成主角");
				{
					var length = Player0.Length;
					for (var i = 0; i < length; i++)
					{
						menu.AddItem(new SpawnPed(Player0[i], PedType.PLAYER_0));
					}
				}
				{
					var length = Player1.Length;
					for (var i = 0; i < length; i++)
					{
						menu.AddItem(new SpawnPed(Player1[i], PedType.PLAYER_1));
					}
				}
				{
					var length = Player2.Length;
					for (var i = 0; i < length; i++)
					{
						menu.AddItem(new SpawnPed(Player2[i], PedType.PLAYER_2));
					}
				}
				MenuController.Instance.Register(menu);
			}
			return menu;
		}

	}
}
