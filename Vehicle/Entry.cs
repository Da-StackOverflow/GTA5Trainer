using ScriptUI;
using static Vehicle.VehicleResources;

namespace Vehicle
{
	public class Entry : AScriptEntry
	{
		protected override void OnInit()
		{
			Log.Info("Vehicle OnInit");
			_controller.MainMenu.AddItem(new SubMenu("车辆系统", GetOrCreateVehicleMenu));
		}

		private Menu GetOrCreateSpawnCarMenu()
		{
			if (!_controller.TryGetMenu("生成汽车", out Menu menu))
			{
				menu = new Menu("生成汽车");
				var length = Car.Length;
				for (var i = 0; i < length; i++)
				{
					menu.AddItem(new SpawnCar(Car[i]));
				}
				_controller.Register(menu);
				
			}
			return menu;
		}

		private Menu GetOrCreateSpawnHelicopterMenu()
		{
			if (!_controller.TryGetMenu("生成直升机", out Menu menu))
			{
				menu = new Menu("生成直升机");
				var length = Helicopter.Length;
				for (var i = 0; i < length; i++)
				{
					menu.AddItem(new SpawnCar(Helicopter[i]));
				}
				_controller.Register(menu);
			}
			return menu;
		}

		private Menu GetOrCreateSpawnBikeMenu()
		{
			if (!_controller.TryGetMenu("生成摩托车", out Menu menu))
			{
				menu = new Menu("生成摩托车");
				var length = Bike.Length;
				for (var i = 0; i < length; i++)
				{
					menu.AddItem(new SpawnCar(Bike[i]));
				}
				_controller.Register(menu);
			}
			return menu;
		}

		private Menu GetOrCreateSpawnPlaneMenu()
		{
			if (!_controller.TryGetMenu("生成飞机", out Menu menu))
			{
				menu = new Menu("生成飞机");
				var length = Plane.Length;
				for (var i = 0; i < length; i++)
				{
					menu.AddItem(new SpawnCar(Plane[i]));
				}
				_controller.Register(menu);
			}
			return menu;
		}

		private Menu GetOrCreateSpawnAmphibiousAutomobileMenu()
		{
			if (!_controller.TryGetMenu("生成水陆两栖车", out Menu menu))
			{
				menu = new Menu("生成水陆两栖车");
				var length = AmphibiousAutomobile.Length;
				for (var i = 0; i < length; i++)
				{
					menu.AddItem(new SpawnCar(AmphibiousAutomobile[i]));
				}
				_controller.Register(menu);
			}
			return menu;
		}

		private Menu GetOrCreateSpawnTrailerMenu()
		{
			if (!_controller.TryGetMenu("生成拖车", out Menu menu))
			{
				menu = new Menu("生成拖车");
				var length = Trailer.Length;
				for (var i = 0; i < length; i++)
				{
					menu.AddItem(new SpawnCar(Trailer[i]));
				}
				_controller.Register(menu);
			}
			return menu;
		}

		private Menu GetOrCreateSpawnSubmarineMenu()
		{
			if (!_controller.TryGetMenu("生成潜水艇", out Menu menu))
			{
				menu = new Menu("生成潜水艇");
				var length = Submarine.Length;
				for (var i = 0; i < length; i++)
				{
					menu.AddItem(new SpawnCar(Submarine[i]));
				}
				_controller.Register(menu);
			}
			return menu;
		}

		private Menu GetOrCreateSpawnQuadbikeMenu()
		{
			if (!_controller.TryGetMenu("生成四轮摩托车", out Menu menu))
			{
				menu = new Menu("生成四轮摩托车");
				var length = Quadbike.Length;
				for (var i = 0; i < length; i++)
				{
					menu.AddItem(new SpawnCar(Quadbike[i]));
				}
				_controller.Register(menu);
			}
			return menu;
		}

		private Menu GetOrCreateSpawnAmphibiousQuadbikeMenu()
		{
			if (!_controller.TryGetMenu("生成水陆四轮摩托车", out Menu menu))
			{
				menu = new Menu("生成水陆四轮摩托车");
				var length = AmphibiousQuadbike.Length;
				for (var i = 0; i < length; i++)
				{
					menu.AddItem(new SpawnCar(AmphibiousQuadbike[i]));
				}
				_controller.Register(menu);
			}
			return menu;
		}

		private Menu GetOrCreateSpawnBlimpMenu()
		{
			if (!_controller.TryGetMenu("生成飞艇", out Menu menu))
			{
				menu = new Menu("生成飞艇");
				var length = Blimp.Length;
				for (var i = 0; i < length; i++)
				{
					menu.AddItem(new SpawnCar(Blimp[i]));
				}
				_controller.Register(menu);
			}
			return menu;
		}

		private Menu GetOrCreateSpawnBicycleMenu()
		{
			if (!_controller.TryGetMenu("生成自行车", out Menu menu))
			{
				menu = new Menu("生成自行车");
				var length = Bicycle.Length;
				for (var i = 0; i < length; i++)
				{
					menu.AddItem(new SpawnCar(Bicycle[i]));
				}
				_controller.Register(menu);
			}
			return menu;
		}

		private Menu GetOrCreateSpawnTrainMenu()
		{
			if (!_controller.TryGetMenu("生成火车", out Menu menu))
			{
				menu = new Menu("生成火车");
				var length = Train.Length;
				for (var i = 0; i < length; i++)
				{
					menu.AddItem(new SpawnCar(Train[i]));
				}
				_controller.Register(menu);
			}
			return menu;
		}

		private Menu GetOrCreateSpawnBoatMenu()
		{
			if (!_controller.TryGetMenu("生成船", out Menu menu))
			{
				menu = new Menu("生成船");
				var length = Boat.Length;
				for (var i = 0; i < length; i++)
				{
					menu.AddItem(new SpawnCar(Boat[i]));
				}
				_controller.Register(menu);
			}
			return menu;
		}

		private Menu GetOrCreateSpawnSubmarinecarMenu()
		{
			if (!_controller.TryGetMenu("生成潜水车", out Menu menu))
			{
				menu = new Menu("生成潜水车");
				var length = Submarinecar.Length;
				for (var i = 0; i < length; i++)
				{
					menu.AddItem(new SpawnCar(Submarinecar[i]));
				}
				_controller.Register(menu);
			}
			return menu;
		}

		private Menu GetOrCreateSpawnVehicleMenu()
		{
			if (!_controller.TryGetMenu("生成车辆", out Menu menu))
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
				_controller.Register(menu);
			}
			return menu;
		}

		private Menu GetOrCreateVehicleMenu()
		{
			if (!_controller.TryGetMenu("车辆系统", out Menu menu))
			{
				menu = new Menu("车辆系统");
				menu.AddItem(new SubMenu("生成车辆", GetOrCreateSpawnVehicleMenu));
				menu.AddItem(new GetInCar("进入瞄准的汽车"));
				menu.AddItem(new RandomPaintCar("改变车辆颜色"));
				menu.AddItem(new FixCar("修理车辆"));
				menu.AddItem(new SafeBelt("安全带"));
				menu.AddItem(new InvincibleCar("车辆无敌"));
				menu.AddItem(new InvincibleWheel("防弹车轮"));
				menu.AddItem(new SetSpeed("车辆加速"));
				menu.AddItem(new VehicleRockets("车载火箭炮"));
				_controller.Register(menu);
			}
			return menu;
		}
	}
}