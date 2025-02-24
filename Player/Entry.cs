using ScriptUI;
using static ScriptUI.Functions;
using static Player.PlayerResources;

namespace Player
{
	public class Entry : AScriptEntry
	{
		protected override void OnInit()
		{
			Log.Info("Player OnInit");
			_controller.MainMenu.AddItem(new SubMenu("玩家系统", GetOrCreatePlayerMenu));
		}

		private Menu GetOrCreatePlayerMenu()
		{
			if (!_controller.TryGetMenu("玩家系统", out Menu menu))
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
				_controller.Register(menu);
			}
			return menu;
		}

		private Menu GetOrCreatePlayerChangeSkinMenu()
		{
			if (!_controller.TryGetMenu("改变玩家模型", out Menu menu))
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
				_controller.Register(menu);
			}
			return menu;
		}

		private Menu GetOrCreatePlayerChangeToAnimalMenu()
		{
			if (!_controller.TryGetMenu("变成动物", out Menu menu))
			{
				menu = new Menu("变成动物");
				var length = Animals.Length;
				for (var i = 0; i < length; i++)
				{
					menu.AddItem(new ChangeSkin(Animals[i]));
				}
				_controller.Register(menu);
			}
			return menu;
		}

		private Menu GetOrCreatePlayerChangeToMaleMenu()
		{
			if (!_controller.TryGetMenu("变成男人", out Menu menu))
			{
				menu = new Menu("变成男人");
				var length = Male.Length;
				for (var i = 0; i < length; i++)
				{
					menu.AddItem(new ChangeSkin(Male[i]));
				}
				_controller.Register(menu);
			}
			return menu;
		}

		private Menu GetOrCreatePlayerChangeToFemalMenu()
		{
			if (!_controller.TryGetMenu("变成女人", out Menu menu))
			{
				menu = new Menu("变成女人");
				var length = Femal.Length;
				for (var i = 0; i < length; i++)
				{
					menu.AddItem(new ChangeSkin(Femal[i]));
				}
				_controller.Register(menu);
			}
			return menu;
		}

		private Menu GetOrCreatePlayerChangeToMedicMenu()
		{
			if (!_controller.TryGetMenu("变成医生", out Menu menu))
			{
				menu = new Menu("变成医生");
				var length = Medic.Length;
				for (var i = 0; i < length; i++)
				{
					menu.AddItem(new ChangeSkin(Medic[i]));
				}
				_controller.Register(menu);
			}
			return menu;
		}

		private Menu GetOrCreatePlayerChangeToCopMenu()
		{
			if (!_controller.TryGetMenu("变成警察", out Menu menu))
			{
				menu = new Menu("变成警察");
				var length = Cop.Length;
				for (var i = 0; i < length; i++)
				{
					menu.AddItem(new ChangeSkin(Cop[i]));
				}
				_controller.Register(menu);
			}
			return menu;
		}

		private Menu GetOrCreatePlayerChangeToArmyMenu()
		{
			if (!_controller.TryGetMenu("变成军人", out Menu menu))
			{
				menu = new Menu("变成军人");
				var length = Army.Length;
				for (var i = 0; i < length; i++)
				{
					menu.AddItem(new ChangeSkin(Army[i]));
				}
				_controller.Register(menu);
			}
			return menu;
		}

		private Menu GetOrCreatePlayerChangeToFiremanMenu()
		{
			if (!_controller.TryGetMenu("变成消防员", out Menu menu))
			{
				menu = new Menu("变成消防员");
				var length = Fireman.Length;
				for (var i = 0; i < length; i++)
				{
					menu.AddItem(new ChangeSkin(Fireman[i]));
				}
				_controller.Register(menu);
			}
			return menu;
		}

		private Menu GetOrCreatePlayerChangeToSwatMenu()
		{
			if (!_controller.TryGetMenu("变成国安局特工", out Menu menu))
			{
				menu = new Menu("变成国安局特工");
				var length = Swat.Length;
				for (var i = 0; i < length; i++)
				{
					menu.AddItem(new ChangeSkin(Swat[i]));
				}
				_controller.Register(menu);
			}
			return menu;
		}

		private Menu GetOrCreatePlayerChangeToPlayerMenu()
		{
			if (!_controller.TryGetMenu("变成主角", out Menu menu))
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
				_controller.Register(menu);
			}
			return menu;
		}

		private Menu GetOrCreatePlayerTeleportMenu()
		{
			if (!_controller.TryGetMenu("传送", out Menu menu))
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
				menu.AddItem(new SubMenu("50封信", GetOrCreatePlayerTeleport50LetterMenu));
				menu.AddItem(new SubMenu("50UFO碎片", GetOrCreatePlayerTeleport50UFOMenu));
				menu.AddItem(new SubMenu("50猴子马赛克", GetOrCreatePlayerTeleport50MonkeyMenu));
				menu.AddItem(new SubMenu("27迷幻仙人掌", GetOrCreatePlayerTeleport27CactusMenu));
				_controller.Register(menu);
			}
			return menu;
		}

		private Menu GetOrCreatePlayerTeleport50LetterMenu()
		{
			if (!_controller.TryGetMenu("50封信", out Menu menu))
			{
				menu = new Menu("50封信");
				menu.AddItem(new Teleport("1",  -916.934f,  -2529.692f,  23.321f));
				menu.AddItem(new Teleport("2",  1026.478f,  -3026.476f,  14.327f));
				menu.AddItem(new Teleport("3",  1509.027f,  -2121.209f,  76.565f));
				menu.AddItem(new Teleport("4",  749.152f,  -2297.491f,  20.653f));
				menu.AddItem(new Teleport("5",  -81.561f,  -2726.916f,  8.740f));
				menu.AddItem(new Teleport("6",  75.461f,  -1970.502f,  21.125f));
				menu.AddItem(new Teleport("7",  0.331f,  -1733.515f,  31.631f));
				menu.AddItem(new Teleport("8",  -1820.939f,  -1201.448f,  19.165f));
				menu.AddItem(new Teleport("9",  -1380.384f,  -1404.385f,  2.727f));
				menu.AddItem(new Teleport("10",  -1034.291f,  -1276.096f,  1.886f));
				menu.AddItem(new Teleport("11",  -119.076f,  -977.225f,  304.250f));
				menu.AddItem(new Teleport("12",  642.792f,  -1035.383f,  36.889f));
				menu.AddItem(new Teleport("13",  2864.949f,  -1372.740f,  2.285f));
				menu.AddItem(new Teleport("14",  1095.762f,  -210.704f,  55.948f));
				menu.AddItem(new Teleport("15",  1052.168f,  167.611f,  88.740f));
				menu.AddItem(new Teleport("16",  265.526f,  -199.523f,  61.788f));
				menu.AddItem(new Teleport("17",  86.291f,  -433.701f,  36.000f));
				menu.AddItem(new Teleport("18",  -347.346f,  54.967f,  54.897f));
				menu.AddItem(new Teleport("19",  -1239.139f,  -507.005f,  38.602f));
				menu.AddItem(new Teleport("20",  -3020.872f,  37.788f,  10.118f));
				menu.AddItem(new Teleport("21",  -2303.596f,  217.535f,  167.602f));
				menu.AddItem(new Teleport("22",  -1724.191f,  -196.159f,  58.223f));
				menu.AddItem(new Teleport("23",  -138.942f,  868.574f,  232.696f));
				menu.AddItem(new Teleport("24",  682.481f,  1204.835f,  345.327f));
				menu.AddItem(new Teleport("25",  3063.897f,  2211.834f,  3.644f));
				menu.AddItem(new Teleport("26", -1048.441f, -2734.343f, 13.855f));
				menu.AddItem(new Teleport("27", 3082.064f, 1648.014f, 3.430f));
				menu.AddItem(new Teleport("28", 929.510f, 2443.853f, 49.431f));
				menu.AddItem(new Teleport("29", 180.133f, 2263.947f, 92.910f));
				menu.AddItem(new Teleport("30", -432.000f, 1596.891f, 356.836f));
				menu.AddItem(new Teleport("31", -1549.105f, 1380.137f, 126.347f));
				menu.AddItem(new Teleport("32", -594.714f, 2092.140f, 131.521f));
				menu.AddItem(new Teleport("33", -2380.092f, 2656.910f, 2.477f));
				menu.AddItem(new Teleport("34", -861.462f, 2753.421f, 13.841f));
				menu.AddItem(new Teleport("35", -289.314f, 2848.690f, 54.349f));
				menu.AddItem(new Teleport("36", 265.832f, 2866.403f, 74.175f));
				menu.AddItem(new Teleport("37", 1294.168f, 3001.753f, 57.710f));
				menu.AddItem(new Teleport("38", 1568.298f, 3572.663f, 33.323f));
				menu.AddItem(new Teleport("39", 3436.620f, 5176.782f, 7.382f));
				menu.AddItem(new Teleport("40", 1877.325f, 5079.250f, 51.404f));
				menu.AddItem(new Teleport("41", 1439.839f, 6335.396f, 23.930f));
				menu.AddItem(new Teleport("42", 1469.272f, 6552.076f, 14.681f));
				menu.AddItem(new Teleport("43", 66.789f, 6663.047f, 31.787f));
				menu.AddItem(new Teleport("44", -403.302f, 6318.864f, 32.223f));
				menu.AddItem(new Teleport("45", -578.593f, 5470.081f, 60.088f));
				menu.AddItem(new Teleport("46", -1001.223f, 4850.986f, 274.606f));
				menu.AddItem(new Teleport("47", -1608.820f, 4274.008f, 103.952f));
				menu.AddItem(new Teleport("48", -2.194f, 4334.911f, 33.684f));
				menu.AddItem(new Teleport("49", 1337.210f, 4307.336f, 38.138f));
				menu.AddItem(new Teleport("50", 444.633f, 5571.729f, 781.190f));
				_controller.Register(menu);
			}
			return menu;
		}

		private Menu GetOrCreatePlayerTeleport50UFOMenu()
		{
			if (!_controller.TryGetMenu("50UFO碎片", out Menu menu))
			{
				menu = new Menu("50UFO碎片");
				menu.AddItem(new Teleport("1", 338.332f, -2761.779f, 43.702f));
				menu.AddItem(new Teleport("2", 634.114f, -3233.189f, -15.123f));
				menu.AddItem(new Teleport("3", 1590.641f, -2810.470f, 4.422f));
				menu.AddItem(new Teleport("4", 1133.690f, -2605.180f, 15.963f));
				menu.AddItem(new Teleport("5", 370.095f, -2116.917f, 17.185f));
				menu.AddItem(new Teleport("6", 1741.791f, -1618.868f, 112.687f));
				menu.AddItem(new Teleport("7", 287.590f, -1444.361f, 46.510f));
				menu.AddItem(new Teleport("8", 17.406f, -1213.046f, 29.381f));
				menu.AddItem(new Teleport("9", -1217.219f, -3495.788f, 14.067f));
				menu.AddItem(new Teleport("10", -900.503f, -1165.183f, 32.750f));
				menu.AddItem(new Teleport("11", 1231.866f, -1102.129f, 35.431f));
				menu.AddItem(new Teleport("12", 81.204f, 813.934f, 214.291f));
				menu.AddItem(new Teleport("13", -1907.653f, 1388.440f, 218.972f));
				menu.AddItem(new Teleport("14", 469.533f, -730.888f, 27.387f));
				menu.AddItem(new Teleport("15", 202.701f, -569.391f, 129.090f));
				menu.AddItem(new Teleport("16", 159.313f, -564.052f, 22.014f));
				menu.AddItem(new Teleport("17", -1182.398f, -525.183f, 40.746f));
				menu.AddItem(new Teleport("18", -227.975f, -236.526f, 50.140f));
				menu.AddItem(new Teleport("19", -407.877f, -151.913f, 64.555f));
				menu.AddItem(new Teleport("20", -1175.336f, -65.565f, 45.650f));
				menu.AddItem(new Teleport("21", 1684.332f, 40.518f, 154.029f));
				menu.AddItem(new Teleport("22", 1965.964f, 555.911f, 161.387f));
				menu.AddItem(new Teleport("23", 22.599f, 638.663f, 190.699f));
				menu.AddItem(new Teleport("24", 2901.438f, 796.311f, 4.306f));
				menu.AddItem(new Teleport("25", -1529.846f, 871.687f, 181.641f));
				menu.AddItem(new Teleport("26", -404.478f, 1100.972f, 332.534f));
				menu.AddItem(new Teleport("27", -2809.209f, 1449.781f, 100.928f));
				menu.AddItem(new Teleport("28", 3143.261f, 2185.279f, -4.648f));
				menu.AddItem(new Teleport("29", 815.816f, 1851.004f, 121.146f));
				menu.AddItem(new Teleport("30", -1944.299f, 1941.143f, 163.824f));
				menu.AddItem(new Teleport("31", -1434.087f, 2138.124f, 28.485f));
				menu.AddItem(new Teleport("32", 1367.621f, 2180.819f, 97.699f));
				menu.AddItem(new Teleport("33", 172.010f, 2220.121f, 90.786f));
				menu.AddItem(new Teleport("34", 889.562f, 2869.724f, 56.279f));
				menu.AddItem(new Teleport("35", 1963.538f, 2922.751f, 58.675f));
				menu.AddItem(new Teleport("36", -390.387f, 2963.150f, 19.303f));
				menu.AddItem(new Teleport("37", 71.446f, 3279.186f, 31.366f));
				menu.AddItem(new Teleport("38", 1924.256f, 3471.143f, 51.320f));
				menu.AddItem(new Teleport("39", -583.169f, 3580.533f, 267.288f));
				menu.AddItem(new Teleport("40", 2517.336f, 3789.192f, 54.696f));
				menu.AddItem(new Teleport("41", 1485.680f, 3855.822f, 23.853f));
				menu.AddItem(new Teleport("42", -528.898f, 4441.187f, 32.915f));
				menu.AddItem(new Teleport("43", 3820.771f, 4441.913f, 2.808f));
				menu.AddItem(new Teleport("44", -1943.037f, 4585.045f, 47.652f));
				menu.AddItem(new Teleport("45", 2438.055f, 4779.997f, 34.492f));
				menu.AddItem(new Teleport("46", -1441.422f, 5414.840f, 24.185f));
				menu.AddItem(new Teleport("47", 2196.500f, 5599.306f, 53.698f));
				menu.AddItem(new Teleport("48", -503.291f, 5665.727f, 49.884f));
				menu.AddItem(new Teleport("49", -380.976f, 6086.973f, 39.615f));
				menu.AddItem(new Teleport("50", 440.839f, 6459.701f, 28.743f));
				_controller.Register(menu);
			}
			return menu;
		}

		private Menu GetOrCreatePlayerTeleport50MonkeyMenu()
		{
			if (!_controller.TryGetMenu("50猴子马赛克", out Menu menu))
			{
				menu = new Menu("50猴子马赛克");
				menu.AddItem(new Teleport("1", 2544.579f, 394.264f, 108.617f));
				menu.AddItem(new Teleport("2", 1121.041f, -1274.365f, 20.711f));
				menu.AddItem(new Teleport("3", 1150.290f, -666.602f, 57.437f));
				menu.AddItem(new Teleport("4", 562.752f, -551.621f, 24.787f));
				menu.AddItem(new Teleport("5", 282.127f, -475.694f, 35.250f));
				menu.AddItem(new Teleport("6", 590.399f, 138.907f, 104.251f));
				menu.AddItem(new Teleport("7", 344.199f, 345.152f, 105.201f));
				menu.AddItem(new Teleport("8", 87.612f, 212.760f, 118.051f));
				menu.AddItem(new Teleport("9", -146.593f, 232.823f, 94.958f));
				menu.AddItem(new Teleport("10", -468.567f, -18.203f, 52.472f));
				menu.AddItem(new Teleport("11", -939.940f, 334.612f, 71.597f));
				menu.AddItem(new Teleport("12", -1038.093f, -159.355f, 38.148f));
				menu.AddItem(new Teleport("13", -1372.172f, -525.472f, 30.360f));
				menu.AddItem(new Teleport("14", -1708.268f, -263.162f, 51.963f));
				menu.AddItem(new Teleport("15", -1677.144f, 172.902f, 62.212f));
				menu.AddItem(new Teleport("16", -423.425f, 1090.882f, 332.533f));
				menu.AddItem(new Teleport("17", 2397.568f, 3141.287f, 48.159f));
				menu.AddItem(new Teleport("18", 3416.488f, 3775.770f, 33.845f));
				menu.AddItem(new Teleport("19", 1728.871f, 4780.575f, 41.889f));
				menu.AddItem(new Teleport("20", 1671.892f, 4975.986f, 42.313f));
				menu.AddItem(new Teleport("21", 161.932f, 6661.580f, 31.370f));
				menu.AddItem(new Teleport("22", 157.842f, -1169.140f, 29.331f));
				menu.AddItem(new Teleport("23", -250.166f, 6234.235f, 31.490f));
				menu.AddItem(new Teleport("24", -1984.348f, 4523.288f, 18.240f));
				menu.AddItem(new Teleport("25", 355.308f, 3395.087f, 36.403f));
				menu.AddItem(new Teleport("26", 893.949f, 3616.522f, 32.824f));
				menu.AddItem(new Teleport("27", 1512.979f, 3568.668f, 38.736f));
				menu.AddItem(new Teleport("28", 1912.320f, 3734.368f, 32.591f));
				menu.AddItem(new Teleport("29", 555.383f, 2804.899f, 42.263f));
				menu.AddItem(new Teleport("30", -1102.720f, 2726.518f, 18.800f));
				menu.AddItem(new Teleport("31", -1938.541f, -407.115f, 38.536f));
				menu.AddItem(new Teleport("32", -1827.645f, -1267.716f, 8.618f));
				menu.AddItem(new Teleport("33", -1379.388f, -901.921f, 12.474f));
				menu.AddItem(new Teleport("34", -1282.444f, -1615.288f, 4.093f));
				menu.AddItem(new Teleport("35", -1380.635f, -360.847f, 42.202f));
				menu.AddItem(new Teleport("36", -498.057f, -456.549f, 32.432f));
				menu.AddItem(new Teleport("37", -955.442f, -774.275f, 16.449f));
				menu.AddItem(new Teleport("38", -814.803f, -1256.932f, 5.611f));
				menu.AddItem(new Teleport("39", -464.798f, -1449.395f, 20.740f));
				menu.AddItem(new Teleport("40", -1169.210f, -2034.670f, 13.424f));
				menu.AddItem(new Teleport("41", -807.093f, -2750.236f, 13.942f));
				menu.AddItem(new Teleport("42", -344.645f, -2275.369f, 7.608f));
				menu.AddItem(new Teleport("43", 249.855f, -3313.322f, 5.790f));
				menu.AddItem(new Teleport("44", 1242.720f, -3318.158f, 6.029f));
				menu.AddItem(new Teleport("45", 1572.861f, -2130.246f, 77.561f));
				menu.AddItem(new Teleport("46", 756.839f, -1857.722f, 49.292f));
				menu.AddItem(new Teleport("47", -94.951f, -744.190f, 37.436f));
				menu.AddItem(new Teleport("48", 204.611f, -2029.215f, 18.275f));
				menu.AddItem(new Teleport("49", 473.342f, -1472.637f, 35.091f));
				menu.AddItem(new Teleport("50", 151.102f, -1186.161f, 31.320f));
				_controller.Register(menu);
			}
			return menu;
		}

		private Menu GetOrCreatePlayerTeleport27CactusMenu()
		{
			if (!_controller.TryGetMenu("27迷幻仙人掌", out Menu menu))
			{
				menu = new Menu("27迷幻仙人掌");
				menu.AddItem(new Teleport("1", 1328.583f, -607.034f, 74.508f));
				menu.AddItem(new Teleport("2", 1422.989f, -2615.264f, 47.779f));
				menu.AddItem(new Teleport("3", -563.629f, -2481.061f, -17.189f));
				menu.AddItem(new Teleport("4", -320.367f, -1652.291f, 31.849f));
				menu.AddItem(new Teleport("5", -1162.365f, -1998.343f, 13.247f));
				menu.AddItem(new Teleport("6", -1334.917f, -1066.365f, 12.591f));
				menu.AddItem(new Teleport("7", -1845.693f, -1257.988f, -23.040f));
				menu.AddItem(new Teleport("8", -94.765f, 321.992f, 142.835f));
				menu.AddItem(new Teleport("9", 437.636f, 782.727f, 193.241f));
				menu.AddItem(new Teleport("10", -116.108f, 1428.394f, 294.490f));
				menu.AddItem(new Teleport("11", -1038.720f, 881.173f, 162.099f));
				menu.AddItem(new Teleport("12", -1615.821f, 2072.709f, 78.071f));
				_controller.Register(menu);
			}
			return menu;
		}

		private Menu GetOrCreateAddCashMenu()
		{
			if (!_controller.TryGetMenu("增加金钱", out Menu menu))
			{
				menu = new Menu("增加金钱");
				menu.AddItem(new AddCash("给麦克增加一万", 0, 10000));
				menu.AddItem(new AddCash("给麦克增加一百万", 0, 1000000));
				menu.AddItem(new AddCash("给富兰克林增加一万", 1, 10000));
				menu.AddItem(new AddCash("给富兰克林增加一百万", 1, 1000000));
				menu.AddItem(new AddCash("给崔佛增加一万", 2, 10000));
				menu.AddItem(new AddCash("给崔佛增加一百万", 2, 1000000));
				_controller.Register(menu);
			}
			return menu;
		}

		private Menu GetOrCreateWantedMenu()
		{
			if (!_controller.TryGetMenu("通缉等级修改", out Menu menu))
			{
				menu = new Menu("通缉等级修改");
				menu.AddItem(new ClearWanted("清除通缉等级"));
				menu.AddItem(new ModifyWantedLevel("增加1颗星", 1));
				menu.AddItem(new ModifyWantedLevel("减少1颗星", -1));
				menu.AddItem(new NeverWanted("不再受通缉"));
				menu.AddItem(new PoliceIgnore("警察忽视玩家"));
				menu.AddItem(new EveryOneIgnorePlayer("所有人忽视玩家"));
				_controller.Register(menu);
			}
			return menu;
		}

		private Menu GetOrCreateSpawnPedMenu()
		{
			if (!_controller.TryGetMenu("生成NPC", out Menu menu))
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
				_controller.Register(menu);
			}
			return menu;
		}

		private Menu GetOrCreatePlayerSpawnAnimalMenu()
		{
			if (!_controller.TryGetMenu("生成动物", out Menu menu))
			{
				menu = new Menu("生成动物");
				var length = Animals.Length;
				for (var i = 0; i < length; i++)
				{
					menu.AddItem(new SpawnPed(Animals[i], PedType.ANIMAL));
				}
				_controller.Register(menu);
			}
			return menu;
		}

		private Menu GetOrCreatePlayerSpawnMaleMenu()
		{
			if (!_controller.TryGetMenu("生成男人", out Menu menu))
			{
				menu = new Menu("生成男人");
				var length = Male.Length;
				for (var i = 0; i < length; i++)
				{
					menu.AddItem(new SpawnPed(Male[i], PedType.CIVMALE));
				}
				_controller.Register(menu);
			}
			return menu;
		}

		private Menu GetOrCreatePlayerSpawnFemalMenu()
		{
			if (!_controller.TryGetMenu("生成女人", out Menu menu))
			{
				menu = new Menu("生成女人");
				var length = Femal.Length;
				for (var i = 0; i < length; i++)
				{
					menu.AddItem(new SpawnPed(Femal[i], PedType.CIVFEMALE));
				}
				_controller.Register(menu);
			}
			return menu;
		}

		private Menu GetOrCreatePlayerSpawnMedicMenu()
		{
			if (!_controller.TryGetMenu("生成医生", out Menu menu))
			{
				menu = new Menu("生成医生");
				var length = Medic.Length;
				for (var i = 0; i < length; i++)
				{
					menu.AddItem(new SpawnPed(Medic[i], PedType.MEDIC));
				}
				_controller.Register(menu);
			}
			return menu;
		}

		private Menu GetOrCreatePlayerSpawnCopMenu()
		{
			if (!_controller.TryGetMenu("生成警察", out Menu menu))
			{
				menu = new Menu("生成警察");
				var length = Cop.Length;
				for (var i = 0; i < length; i++)
				{
					menu.AddItem(new SpawnPed(Cop[i], PedType.COP));
				}
				_controller.Register(menu);
			}
			return menu;
		}

		private Menu GetOrCreatePlayerSpawnArmyMenu()
		{
			if (!_controller.TryGetMenu("生成军人", out Menu menu))
			{
				menu = new Menu("生成军人");
				var length = Army.Length;
				for (var i = 0; i < length; i++)
				{
					menu.AddItem(new SpawnPed(Army[i], PedType.ARMY));
				}
				_controller.Register(menu);
			}
			return menu;
		}

		private Menu GetOrCreatePlayerSpawnFiremanMenu()
		{
			if (!_controller.TryGetMenu("生成消防员", out Menu menu))
			{
				menu = new Menu("生成消防员");
				var length = Fireman.Length;
				for (var i = 0; i < length; i++)
				{
					menu.AddItem(new SpawnPed(Fireman[i], PedType.FIREMAN));
				}
				_controller.Register(menu);
			}
			return menu;
		}

		private Menu GetOrCreatePlayerSpawnSwatMenu()
		{
			if (!_controller.TryGetMenu("生成国安局特工", out Menu menu))
			{
				menu = new Menu("生成国安局特工");
				var length = Swat.Length;
				for (var i = 0; i < length; i++)
				{
					menu.AddItem(new SpawnPed(Swat[i], PedType.SWAT));
				}
				_controller.Register(menu);
			}
			return menu;
		}

		private Menu GetOrCreatePlayerSpawnPlayerMenu()
		{
			if (!_controller.TryGetMenu("生成主角", out Menu menu))
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
				_controller.Register(menu);
			}
			return menu;
		}

	}
}
