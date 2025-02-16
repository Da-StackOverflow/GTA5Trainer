using Bridge;
using static Vehicle.VehicleResources;

namespace Vehicle
{
	public class Entry : Bridge.Entry
	{
		public override void OnInit()
		{
			Log.Info("Vehicle OnInit");
			MenuController.Instance.MainMenu.AddItem(new SubMenu("车辆系统", GetOrCreateVehicleMenu));
		}

		static Menu GetOrCreateSpawnCarMenu()
		{
			if (!MenuController.Instance.TryGetMenu("生成汽车", out Menu menu))
			{
				menu = new Menu("生成汽车");
				var length = Car.Length;
				for (var i = 0; i < length; i++)
				{
					menu.AddItem(new SpawnCar(Car[i]));
				}
				MenuController.Instance.Register(menu);
				
			}
			return menu;
		}

		static Menu GetOrCreateSpawnHelicopterMenu()
		{
			if (!MenuController.Instance.TryGetMenu("生成直升机", out Menu menu))
			{
				menu = new Menu("生成直升机");
				var length = Helicopter.Length;
				for (var i = 0; i < length; i++)
				{
					menu.AddItem(new SpawnCar(Helicopter[i]));
				}
				MenuController.Instance.Register(menu);
			}
			return menu;
		}

		static Menu GetOrCreateSpawnBikeMenu()
		{
			if (!MenuController.Instance.TryGetMenu("生成摩托车", out Menu menu))
			{
				menu = new Menu("生成摩托车");
				var length = Bike.Length;
				for (var i = 0; i < length; i++)
				{
					menu.AddItem(new SpawnCar(Bike[i]));
				}
				MenuController.Instance.Register(menu);
			}
			return menu;
		}

		static Menu GetOrCreateSpawnPlaneMenu()
		{
			if (!MenuController.Instance.TryGetMenu("生成飞机", out Menu menu))
			{
				menu = new Menu("生成飞机");
				var length = Plane.Length;
				for (var i = 0; i < length; i++)
				{
					menu.AddItem(new SpawnCar(Plane[i]));
				}
				MenuController.Instance.Register(menu);
			}
			return menu;
		}

		static Menu GetOrCreateSpawnAmphibiousAutomobileMenu()
		{
			if (!MenuController.Instance.TryGetMenu("生成水陆两栖车", out Menu menu))
			{
				menu = new Menu("生成水陆两栖车");
				var length = AmphibiousAutomobile.Length;
				for (var i = 0; i < length; i++)
				{
					menu.AddItem(new SpawnCar(AmphibiousAutomobile[i]));
				}
				MenuController.Instance.Register(menu);
			}
			return menu;
		}

		static Menu GetOrCreateSpawnTrailerMenu()
		{
			if (!MenuController.Instance.TryGetMenu("生成拖车", out Menu menu))
			{
				menu = new Menu("生成拖车");
				var length = Trailer.Length;
				for (var i = 0; i < length; i++)
				{
					menu.AddItem(new SpawnCar(Trailer[i]));
				}
				MenuController.Instance.Register(menu);
			}
			return menu;
		}

		static Menu GetOrCreateSpawnSubmarineMenu()
		{
			if (!MenuController.Instance.TryGetMenu("生成潜水艇", out Menu menu))
			{
				menu = new Menu("生成潜水艇");
				var length = Submarine.Length;
				for (var i = 0; i < length; i++)
				{
					menu.AddItem(new SpawnCar(Submarine[i]));
				}
				MenuController.Instance.Register(menu);
			}
			return menu;
		}

		static Menu GetOrCreateSpawnQuadbikeMenu()
		{
			if (!MenuController.Instance.TryGetMenu("生成四轮摩托车", out Menu menu))
			{
				menu = new Menu("生成四轮摩托车");
				var length = Quadbike.Length;
				for (var i = 0; i < length; i++)
				{
					menu.AddItem(new SpawnCar(Quadbike[i]));
				}
				MenuController.Instance.Register(menu);
			}
			return menu;
		}

		static Menu GetOrCreateSpawnAmphibiousQuadbikeMenu()
		{
			if (!MenuController.Instance.TryGetMenu("生成水陆四轮摩托车", out Menu menu))
			{
				menu = new Menu("生成水陆四轮摩托车");
				var length = AmphibiousQuadbike.Length;
				for (var i = 0; i < length; i++)
				{
					menu.AddItem(new SpawnCar(AmphibiousQuadbike[i]));
				}
				MenuController.Instance.Register(menu);
			}
			return menu;
		}

		static Menu GetOrCreateSpawnBlimpMenu()
		{
			if (!MenuController.Instance.TryGetMenu("生成飞艇", out Menu menu))
			{
				menu = new Menu("生成飞艇");
				var length = Blimp.Length;
				for (var i = 0; i < length; i++)
				{
					menu.AddItem(new SpawnCar(Blimp[i]));
				}
				MenuController.Instance.Register(menu);
			}
			return menu;
		}

		static Menu GetOrCreateSpawnBicycleMenu()
		{
			if (!MenuController.Instance.TryGetMenu("生成自行车", out Menu menu))
			{
				menu = new Menu("生成自行车");
				var length = Bicycle.Length;
				for (var i = 0; i < length; i++)
				{
					menu.AddItem(new SpawnCar(Bicycle[i]));
				}
				MenuController.Instance.Register(menu);
			}
			return menu;
		}

		static Menu GetOrCreateSpawnTrainMenu()
		{
			if (!MenuController.Instance.TryGetMenu("生成火车", out Menu menu))
			{
				menu = new Menu("生成火车");
				var length = Train.Length;
				for (var i = 0; i < length; i++)
				{
					menu.AddItem(new SpawnCar(Train[i]));
				}
				MenuController.Instance.Register(menu);
			}
			return menu;
		}

		static Menu GetOrCreateSpawnBoatMenu()
		{
			if (!MenuController.Instance.TryGetMenu("生成船", out Menu menu))
			{
				menu = new Menu("生成船");
				var length = Boat.Length;
				for (var i = 0; i < length; i++)
				{
					menu.AddItem(new SpawnCar(Boat[i]));
				}
				MenuController.Instance.Register(menu);
			}
			return menu;
		}

		static Menu GetOrCreateSpawnSubmarinecarMenu()
		{
			if (!MenuController.Instance.TryGetMenu("生成潜水车", out Menu menu))
			{
				menu = new Menu("生成潜水车");
				var length = Submarinecar.Length;
				for (var i = 0; i < length; i++)
				{
					menu.AddItem(new SpawnCar(Submarinecar[i]));
				}
				MenuController.Instance.Register(menu);
			}
			return menu;
		}

		static Menu GetOrCreateSpawnVehicleMenu()
		{
			if (!MenuController.Instance.TryGetMenu("生成车辆", out Menu menu))
			{
				menu = new Menu("生成车辆");
				menu.AddItem(new SetSpawnCarAndWarpInFlag("设置生成车辆后立即进入到车里"));
				menu.AddItem(new SubMenu("生成潜水车", GetOrCreateSpawnSubmarinecarMenu));
				menu.AddItem(new SubMenu("生成汽车", GetOrCreateSpawnCarMenu));
				menu.AddItem(new SubMenu("生成直升机", GetOrCreateSpawnHelicopterMenu));
				menu.AddItem(new SubMenu("生成摩托车", GetOrCreateSpawnBikeMenu));
				menu.AddItem(new SubMenu("生成飞机", GetOrCreateSpawnPlaneMenu));
				menu.AddItem(new SubMenu("生成船", GetOrCreateSpawnBoatMenu));
				menu.AddItem(new SubMenu("生成水陆两栖车", GetOrCreateSpawnAmphibiousAutomobileMenu));
				menu.AddItem(new SubMenu("生成拖车", GetOrCreateSpawnTrailerMenu));
				menu.AddItem(new SubMenu("生成潜水艇", GetOrCreateSpawnSubmarineMenu));
				menu.AddItem(new SubMenu("生成四轮摩托车", GetOrCreateSpawnQuadbikeMenu));
				menu.AddItem(new SubMenu("生成水陆四轮摩托车", GetOrCreateSpawnAmphibiousQuadbikeMenu));
				menu.AddItem(new SubMenu("生成飞艇", GetOrCreateSpawnBlimpMenu));
				menu.AddItem(new SubMenu("生成自行车", GetOrCreateSpawnBicycleMenu));
				menu.AddItem(new SubMenu("生成火车", GetOrCreateSpawnTrainMenu));
				MenuController.Instance.Register(menu);
			}
			return menu;
		}

		static Menu GetOrCreateVehicleMenu()
		{
			if (!MenuController.Instance.TryGetMenu("车辆系统", out Menu menu))
			{
				menu = new Menu("车辆系统");
				menu.AddItem(new SubMenu("生成车辆", GetOrCreateSpawnVehicleMenu));
				menu.AddItem(new RandomPaintCar("改变车辆颜色"));
				menu.AddItem(new FixCar("修理车辆"));
				menu.AddItem(new SafeBelt("安全带"));
				menu.AddItem(new InvincibleCar("车辆无敌"));
				menu.AddItem(new InvincibleWheel("防弹车轮"));
				menu.AddItem(new SpeedBoost("车辆加速"));
				menu.AddItem(new VehicleRockets("车载火箭炮"));
				MenuController.Instance.Register(menu);
			}
			return menu;
		}
	}
}