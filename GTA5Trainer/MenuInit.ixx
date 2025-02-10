import "Base.h";
import Menu;
import Util;
import Function;
import InputSystem;
import ItemInfo;
import PedModelInfos;
import WeaponsInfos;
import VehicleInfos;
import WeatherInfos;
import Player;
import Teleport;
import Weapon;
import Vehicle;
import Misc;
import Time;
import Weather;


static Menu* GetOrCreatePlayerChangeSkinMenu()
{
	if (Controller->IsMenuExist(L"改变玩家模型"))
	{
		return Controller->GetMenu(L"改变玩家模型");
	}
	else
	{
		var newMenu = new Menu(L"改变玩家模型");
		Controller->Register(newMenu);
		newMenu->AddItem(new FallBackSkinWhenDead(L"自动换回默认模型"));
		for (var i = 0; i < sizeof(PedModelInfos) / sizeof(ItemInfo); i++)
		{
			newMenu->AddItem(new ChangeSkin(PedModelInfos[i]));
		}
		return newMenu;
	}
}

static Menu* GetOrCreatePlayerTeleportMenu()
{
	if (Controller->IsMenuExist(L"传送"))
	{
		return Controller->GetMenu(L"传送");
	}
	else
	{
		var newMenu = new Menu(L"传送");
		Controller->Register(newMenu);
		newMenu->AddItem(new TeleportMarker(L"传送到地图标记点"));
		newMenu->AddItem(new GetTeleportCurrentCords(L"显示当前玩家位置坐标"));
		newMenu->AddItem(new GetTeleportMarkerCords(L"显示地图标记点坐标"));
		newMenu->AddItem(new Teleport(L"武装国度", 16.652f, -1116.532f, 29.791f));
		newMenu->AddItem(new Teleport(L"改车店", 1182.520f, 2651.934f, 37.810f));
		newMenu->AddItem(new Teleport(L"乞里耶德山", 499.739f, 5589.924f, 794.821f));
		newMenu->AddItem(new Teleport(L"花园银行顶部",-79.241f, -821.336f, 326.175f));
		newMenu->AddItem(new Teleport(L"在建大楼顶部", -148.989f, -962.854f, 269.135f));
		newMenu->AddItem(new Teleport(L"塔吊顶部", -119.054f, -976.211f, 296.197f));
		return newMenu;
	}
}

static Menu* GetOrCreateAddCashMenu()
{
	if (Controller->IsMenuExist(L"增加金钱"))
	{
		return Controller->GetMenu(L"增加金钱");
	}
	else
	{
		var newMenu = new Menu(L"增加金钱");
		Controller->Register(newMenu);
		newMenu->AddItem(new AddCash(L"给麦克增加一万", 0, 10000));
		newMenu->AddItem(new AddCash(L"给麦克增加一百万", 0, 1000000));
		newMenu->AddItem(new AddCash(L"给富兰克林增加一万", 1, 10000));
		newMenu->AddItem(new AddCash(L"给富兰克林增加一百万", 1, 1000000));
		newMenu->AddItem(new AddCash(L"给崔佛增加一万", 2, 10000));
		newMenu->AddItem(new AddCash(L"给崔佛增加一百万", 2, 1000000));
		return newMenu;
	}
}

static Menu* GetOrCreateWantedMenu()
{
	if (Controller->IsMenuExist(L"通缉等级修改"))
	{
		return Controller->GetMenu(L"通缉等级修改");
	}
	else
	{
		var newMenu = new Menu(L"通缉等级修改");
		Controller->Register(newMenu);
		newMenu->AddItem(new ClearWanted(L"清除通缉等级"));
		newMenu->AddItem(new ModifyWantedLevel(L"增加1颗星", 1));
		newMenu->AddItem(new ModifyWantedLevel(L"减少1颗星", 1));
		newMenu->AddItem(new NeverWanted(L"不再受通缉"));
		newMenu->AddItem(new PoliceIgnore(L"警察忽视玩家"));
		return newMenu;
	}
}

static Menu* GetOrCreatePlayerMenu()
{
	if (Controller->IsMenuExist(L"玩家系统"))
	{
		return Controller->GetMenu(L"玩家系统");
	}
	else
	{
		var newMenu = new Menu(L"玩家系统");
		Controller->Register(newMenu);
		newMenu->AddItem(new SubMenu(L"传送", GetOrCreatePlayerTeleportMenu()));
		newMenu->AddItem(new FixPlayer(L"恢复生命"));
		newMenu->AddItem(new SubMenu(L"增加金钱", GetOrCreateAddCashMenu()));
		newMenu->AddItem(new SubMenu(L"通缉等级修改", GetOrCreateWantedMenu()));
		newMenu->AddItem(new PlayerInvincible(L"无敌"));
		newMenu->AddItem(new UnlimitedAbility(L"无限特殊能力"));
		newMenu->AddItem(new NoNoise(L"无声"));
		newMenu->AddItem(new FastSwim(L"快速游泳"));
		newMenu->AddItem(new FastRun(L"快速跑"));
		newMenu->AddItem(new SuperJump(L"超级跳"));
		newMenu->AddItem(new SubMenu(L"改变玩家模型", GetOrCreatePlayerChangeSkinMenu()));
		newMenu->AddItem(new FallBackSkinWhenDead(L"当玩家死亡后自动恢复成默认皮肤"));
		newMenu->AddItem(new FallBackSkin(L"恢复成默认皮肤"));
		return newMenu;
	}
}

static Menu* GetOrCreateGetWeaponMenu()
{
	if (Controller->IsMenuExist(L"获取武器"))
	{
		return Controller->GetMenu(L"获取武器");
	}
	else
	{
		var newMenu = new Menu(L"获取武器");
		newMenu->AddItem(new GetAllWeapons(L"获取所有武器"));
		for (var i = 0; i < sizeof(WeaponsInfos) / sizeof(ItemInfo); i++)
		{
			newMenu->AddItem(new GetWeapon(WeaponsInfos[i]));
		}
		Controller->Register(newMenu);
		return newMenu;
	}
}

static Menu* GetOrCreateWeaponMenu()
{
	if (Controller->IsMenuExist(L"武器系统"))
	{
		return Controller->GetMenu(L"武器系统");
	}
	else
	{
		var newMenu = new Menu(L"武器系统");
		newMenu->AddItem(new SubMenu(L"获取武器", GetOrCreateGetWeaponMenu()));
		newMenu->AddItem(new GetAllWeapons(L"获取所有武器"));
		newMenu->AddItem(new DropCurrentWeapon(L"移除当前武器"));
		newMenu->AddItem(new RemoveAllWeapon(L"移除所有武器"));
		newMenu->AddItem(new UnlimitedAmmo(L"无限子弹"));
		newMenu->AddItem(new FireAmmo(L"火焰子弹"));
		newMenu->AddItem(new ExplosiveAmmo(L"爆炸子弹"));
		newMenu->AddItem(new ExplosiveMelee(L"爆炸近战武器"));
		Controller->Register(newMenu);
		return newMenu;
	}
}

static Menu* GetOrCreateSpawnCarMenu()
{
	if (Controller->IsMenuExist(L"生成汽车"))
	{
		return Controller->GetMenu(L"生成汽车");
	}
	else
	{
		var newMenu = new Menu(L"生成汽车");
		
		for (var i = 0; i < sizeof(Car) / sizeof(ItemInfo); i++)
		{
			newMenu->AddItem(new SpawnCar(Car[i]));
		}
		Controller->Register(newMenu);
		return newMenu;
	}
}

static Menu* GetOrCreateSpawnHelicopterMenu()
{
	if (Controller->IsMenuExist(L"生成直升机"))
	{
		return Controller->GetMenu(L"生成直升机");
	}
	else
	{
		var newMenu = new Menu(L"生成直升机");

		for (var i = 0; i < sizeof(Helicopter) / sizeof(ItemInfo); i++)
		{
			newMenu->AddItem(new SpawnCar(Helicopter[i]));
		}
		Controller->Register(newMenu);
		return newMenu;
	}
}

static Menu* GetOrCreateSpawnBikeMenu()
{
	if (Controller->IsMenuExist(L"生成摩托车"))
	{
		return Controller->GetMenu(L"生成摩托车");
	}
	else
	{
		var newMenu = new Menu(L"生成摩托车");

		for (var i = 0; i < sizeof(Bike) / sizeof(ItemInfo); i++)
		{
			newMenu->AddItem(new SpawnCar(Bike[i]));
		}
		Controller->Register(newMenu);
		return newMenu;
	}
}

static Menu* GetOrCreateSpawnPlaneMenu()
{
	if (Controller->IsMenuExist(L"生成飞机"))
	{
		return Controller->GetMenu(L"生成飞机");
	}
	else
	{
		var newMenu = new Menu(L"生成飞机");

		for (var i = 0; i < sizeof(Plane) / sizeof(ItemInfo); i++)
		{
			newMenu->AddItem(new SpawnCar(Plane[i]));
		}
		Controller->Register(newMenu);
		return newMenu;
	}
}

static Menu* GetOrCreateSpawnAmphibiousAutomobileMenu()
{
	if (Controller->IsMenuExist(L"生成水陆两栖车"))
	{
		return Controller->GetMenu(L"生成水陆两栖车");
	}
	else
	{
		var newMenu = new Menu(L"生成水陆两栖车");

		for (var i = 0; i < sizeof(AmphibiousAutomobile) / sizeof(ItemInfo); i++)
		{
			newMenu->AddItem(new SpawnCar(AmphibiousAutomobile[i]));
		}
		Controller->Register(newMenu);
		return newMenu;
	}
}

static Menu* GetOrCreateSpawnTrailerMenu()
{
	if (Controller->IsMenuExist(L"生成拖车"))
	{
		return Controller->GetMenu(L"生成拖车");
	}
	else
	{
		var newMenu = new Menu(L"生成拖车");

		for (var i = 0; i < sizeof(Trailer) / sizeof(ItemInfo); i++)
		{
			newMenu->AddItem(new SpawnCar(Trailer[i]));
		}
		Controller->Register(newMenu);
		return newMenu;
	}
}

static Menu* GetOrCreateSpawnSubmarineMenu()
{
	if (Controller->IsMenuExist(L"生成潜水艇"))
	{
		return Controller->GetMenu(L"生成潜水艇");
	}
	else
	{
		var newMenu = new Menu(L"生成潜水艇");

		for (var i = 0; i < sizeof(Submarine) / sizeof(ItemInfo); i++)
		{
			newMenu->AddItem(new SpawnCar(Submarine[i]));
		}
		Controller->Register(newMenu);
		return newMenu;
	}
}

static Menu* GetOrCreateSpawnQuadbikeMenu()
{
	if (Controller->IsMenuExist(L"生成四轮摩托车"))
	{
		return Controller->GetMenu(L"生成四轮摩托车");
	}
	else
	{
		var newMenu = new Menu(L"生成四轮摩托车");

		for (var i = 0; i < sizeof(Quadbike) / sizeof(ItemInfo); i++)
		{
			newMenu->AddItem(new SpawnCar(Quadbike[i]));
		}
		Controller->Register(newMenu);
		return newMenu;
	}
}

static Menu* GetOrCreateSpawnAmphibiousQuadbikeMenu()
{
	if (Controller->IsMenuExist(L"生成水陆四轮摩托车"))
	{
		return Controller->GetMenu(L"生成水陆四轮摩托车");
	}
	else
	{
		var newMenu = new Menu(L"生成水陆四轮摩托车");

		for (var i = 0; i < sizeof(AmphibiousQuadbike) / sizeof(ItemInfo); i++)
		{
			newMenu->AddItem(new SpawnCar(AmphibiousQuadbike[i]));
		}
		Controller->Register(newMenu);
		return newMenu;
	}
}

static Menu* GetOrCreateSpawnBlimpMenu()
{
	if (Controller->IsMenuExist(L"生成飞艇"))
	{
		return Controller->GetMenu(L"生成飞艇");
	}
	else
	{
		var newMenu = new Menu(L"生成飞艇");

		for (var i = 0; i < sizeof(Blimp) / sizeof(ItemInfo); i++)
		{
			newMenu->AddItem(new SpawnCar(Blimp[i]));
		}
		Controller->Register(newMenu);
		return newMenu;
	}
}

static Menu* GetOrCreateSpawnBicycleMenu()
{
	if (Controller->IsMenuExist(L"生成自行车"))
	{
		return Controller->GetMenu(L"生成自行车");
	}
	else
	{
		var newMenu = new Menu(L"生成自行车");

		for (var i = 0; i < sizeof(Bicycle) / sizeof(ItemInfo); i++)
		{
			newMenu->AddItem(new SpawnCar(Bicycle[i]));
		}
		Controller->Register(newMenu);
		return newMenu;
	}
}

static Menu* GetOrCreateSpawnTrainMenu()
{
	if (Controller->IsMenuExist(L"生成火车"))
	{
		return Controller->GetMenu(L"生成火车");
	}
	else
	{
		var newMenu = new Menu(L"生成火车");

		for (var i = 0; i < sizeof(RailwayTrain) / sizeof(ItemInfo); i++)
		{
			newMenu->AddItem(new SpawnCar(RailwayTrain[i]));
		}
		Controller->Register(newMenu);
		return newMenu;
	}
}

static Menu* GetOrCreateSpawnBoatMenu()
{
	if (Controller->IsMenuExist(L"生成船"))
	{
		return Controller->GetMenu(L"生成船");
	}
	else
	{
		var newMenu = new Menu(L"生成船");

		for (var i = 0; i < sizeof(Boat) / sizeof(ItemInfo); i++)
		{
			newMenu->AddItem(new SpawnCar(Boat[i]));
		}
		Controller->Register(newMenu);
		return newMenu;
	}
}

static Menu* GetOrCreateSpawnSubmarinecarMenu()
{
	if (Controller->IsMenuExist(L"生成潜水车"))
	{
		return Controller->GetMenu(L"生成潜水车");
	}
	else
	{
		var newMenu = new Menu(L"生成潜水车");

		for (var i = 0; i < sizeof(Submarinecar) / sizeof(ItemInfo); i++)
		{
			newMenu->AddItem(new SpawnCar(Submarinecar[i]));
		}
		Controller->Register(newMenu);
		return newMenu;
	}
}

static Menu* GetOrCreateSpawnVehicleMenu()
{
	if (Controller->IsMenuExist(L"生成车辆"))
	{
		return Controller->GetMenu(L"生成车辆");
	}
	else
	{
		var newMenu = new Menu(L"生成车辆");
		newMenu->AddItem(new SetSpawnCarAndWarpInFlag(L"设置生成车辆后立即进入到车里"));
		newMenu->AddItem(new SubMenu(L"生成潜水车", GetOrCreateSpawnSubmarinecarMenu()));
		newMenu->AddItem(new SubMenu(L"生成汽车", GetOrCreateSpawnCarMenu()));
		newMenu->AddItem(new SubMenu(L"生成直升机", GetOrCreateSpawnHelicopterMenu()));
		newMenu->AddItem(new SubMenu(L"生成摩托车", GetOrCreateSpawnBikeMenu()));
		newMenu->AddItem(new SubMenu(L"生成飞机", GetOrCreateSpawnPlaneMenu()));
		newMenu->AddItem(new SubMenu(L"生成船", GetOrCreateSpawnBoatMenu()));
		newMenu->AddItem(new SubMenu(L"生成水陆两栖车", GetOrCreateSpawnAmphibiousAutomobileMenu()));
		newMenu->AddItem(new SubMenu(L"生成拖车", GetOrCreateSpawnTrailerMenu()));
		newMenu->AddItem(new SubMenu(L"生成潜水艇", GetOrCreateSpawnSubmarineMenu()));
		newMenu->AddItem(new SubMenu(L"生成四轮摩托车", GetOrCreateSpawnQuadbikeMenu()));
		newMenu->AddItem(new SubMenu(L"生成水陆四轮摩托车", GetOrCreateSpawnAmphibiousQuadbikeMenu()));
		newMenu->AddItem(new SubMenu(L"生成飞艇", GetOrCreateSpawnBlimpMenu()));
		newMenu->AddItem(new SubMenu(L"生成自行车", GetOrCreateSpawnBicycleMenu()));
		newMenu->AddItem(new SubMenu(L"生成火车", GetOrCreateSpawnTrainMenu()));
		Controller->Register(newMenu);
		return newMenu;
	}
}

static Menu* GetOrCreateVehicleMenu()
{
	if (Controller->IsMenuExist(L"车辆系统"))
	{
		return Controller->GetMenu(L"车辆系统");
	}
	else
	{
		var newMenu = new Menu(L"车辆系统");
		newMenu->AddItem(new SubMenu(L"生成车辆", GetOrCreateSpawnVehicleMenu()));
		newMenu->AddItem(new RandomPaintCar(L"改变车辆颜色"));
		newMenu->AddItem(new FixCar(L"修理车辆"));
		newMenu->AddItem(new SafeBelt(L"安全带"));
		newMenu->AddItem(new InvincibleCar(L"车辆无敌"));
		newMenu->AddItem(new InvincibleWheel(L"防弹车轮"));
		newMenu->AddItem(new SpeedBoost(L"车辆加速"));
		newMenu->AddItem(new VehicleRockets(L"车载火箭炮"));
		Controller->Register(newMenu);
		return newMenu;
	}
}

static Menu* GetOrCreateChangeWeatherMenu()
{
	if (Controller->IsMenuExist(L"改变天气"))
	{
		return Controller->GetMenu(L"改变天气");
	}
	else
	{
		var newMenu = new Menu(L"改变天气");
		for (int i = 0; i < sizeof(WeatherInfos) / sizeof(ItemInfo); i++)
		{
			newMenu->AddItem(new ChangeWeather(WeatherInfos[i]));
		}
		Controller->Register(newMenu);
		return newMenu;
	}
}

static Menu* GetOrCreateWeatherMenu()
{
	if (Controller->IsMenuExist(L"天气系统"))
	{
		return Controller->GetMenu(L"天气系统");
	}
	else
	{
		var newMenu = new Menu(L"天气系统");
		newMenu->AddItem(new SubMenu(L"改变天气", GetOrCreateChangeWeatherMenu()));
		newMenu->AddItem(new StandCurrentWeather(L"保持当前天气"));
		newMenu->AddItem(new SetWind(L"生成大风"));
		Controller->Register(newMenu);
		return newMenu;
	}
}

static Menu* GetOrCreateTimeMenu()
{
	if (Controller->IsMenuExist(L"时间系统"))
	{
		return Controller->GetMenu(L"时间系统");
	}
	else
	{
		var newMenu = new Menu(L"时间系统");
		newMenu->AddItem(new TimeModify(L"前进一小时", 1));
		newMenu->AddItem(new TimeModify(L"后退一小时", -1));
		newMenu->AddItem(new TimePause(L"暂停时间"));
		newMenu->AddItem(new TimeSynced(L"真实时间"));
		Controller->Register(newMenu);
		return newMenu;
	}
}

static Menu* GetOrCreateMiscMenu()
{
	if (Controller->IsMenuExist(L"其他系统"))
	{
		return Controller->GetMenu(L"其他系统");
	}
	else
	{
		var newMenu = new Menu(L"其他系统");
		newMenu->AddItem(new MoonGravity(L"月球引力"));
		newMenu->AddItem(new RandomCops(L"随机警察"));
		newMenu->AddItem(new RandomTrains(L"随机火车"));
		newMenu->AddItem(new RandomBoats(L"随机船"));
		newMenu->AddItem(new RandomGarbageTrucks(L"随机垃圾车"));
		newMenu->AddItem(new NextRadioTrack(L"下一首车载音乐"));
		newMenu->AddItem(new HideHud(L"隐藏Hud"));
		Controller->Register(newMenu);
		return newMenu;
	}
}

static Menu* GetOrCreateMainMenu()
{
	if (Controller->IsMenuExist(L"内置修改器1.0 By Da"))
	{
		return Controller->GetMenu(L"内置修改器1.0 By Da");
	}
	else
	{
		var newMenu = new Menu(L"内置修改器1.0 By Da");
		newMenu->AddItem(new SubMenu(L"玩家系统", GetOrCreatePlayerMenu()));
		newMenu->AddItem(new SubMenu(L"武器系统", GetOrCreateWeaponMenu()));
		newMenu->AddItem(new SubMenu(L"车辆系统", GetOrCreateVehicleMenu()));
		newMenu->AddItem(new SubMenu(L"天气系统", GetOrCreateWeatherMenu()));
		newMenu->AddItem(new SubMenu(L"时间系统", GetOrCreateTimeMenu()));
		newMenu->AddItem(new SubMenu(L"其他系统", GetOrCreateMiscMenu()));
		Controller->Register(newMenu);
		return newMenu;
	}
}

void MenuController::Init()
{
	GetOrCreateMainMenu();
}

void MenuController::Update()
{
	if (!HaveActiveMenu() && Input.MenuSwitchPressed())
	{
		PushMenu(GetOrCreateMainMenu());
	}
	OnUpdate();
}