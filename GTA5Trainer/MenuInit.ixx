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
	if (Controller->IsMenuExist(L"�ı����ģ��"))
	{
		return Controller->GetMenu(L"�ı����ģ��");
	}
	else
	{
		var newMenu = new Menu(L"�ı����ģ��");
		Controller->Register(newMenu);
		newMenu->AddItem(new FallBackSkinWhenDead(L"�Զ�����Ĭ��ģ��"));
		for (var i = 0; i < sizeof(PedModelInfos) / sizeof(ItemInfo); i++)
		{
			newMenu->AddItem(new ChangeSkin(PedModelInfos[i]));
		}
		return newMenu;
	}
}

static Menu* GetOrCreatePlayerTeleportMenu()
{
	if (Controller->IsMenuExist(L"����"))
	{
		return Controller->GetMenu(L"����");
	}
	else
	{
		var newMenu = new Menu(L"����");
		Controller->Register(newMenu);
		newMenu->AddItem(new TeleportMarker(L"���͵���ͼ��ǵ�"));
		newMenu->AddItem(new GetTeleportCurrentCords(L"��ʾ��ǰ���λ������"));
		newMenu->AddItem(new GetTeleportMarkerCords(L"��ʾ��ͼ��ǵ�����"));
		newMenu->AddItem(new Teleport(L"��װ����", 16.652f, -1116.532f, 29.791f));
		newMenu->AddItem(new Teleport(L"�ĳ���", 1182.520f, 2651.934f, 37.810f));
		newMenu->AddItem(new Teleport(L"����Ү��ɽ", 499.739f, 5589.924f, 794.821f));
		newMenu->AddItem(new Teleport(L"��԰���ж���",-79.241f, -821.336f, 326.175f));
		newMenu->AddItem(new Teleport(L"�ڽ���¥����", -148.989f, -962.854f, 269.135f));
		newMenu->AddItem(new Teleport(L"��������", -119.054f, -976.211f, 296.197f));
		return newMenu;
	}
}

static Menu* GetOrCreateAddCashMenu()
{
	if (Controller->IsMenuExist(L"���ӽ�Ǯ"))
	{
		return Controller->GetMenu(L"���ӽ�Ǯ");
	}
	else
	{
		var newMenu = new Menu(L"���ӽ�Ǯ");
		Controller->Register(newMenu);
		newMenu->AddItem(new AddCash(L"���������һ��", 0, 10000));
		newMenu->AddItem(new AddCash(L"���������һ����", 0, 1000000));
		newMenu->AddItem(new AddCash(L"��������������һ��", 1, 10000));
		newMenu->AddItem(new AddCash(L"��������������һ����", 1, 1000000));
		newMenu->AddItem(new AddCash(L"���޷�����һ��", 2, 10000));
		newMenu->AddItem(new AddCash(L"���޷�����һ����", 2, 1000000));
		return newMenu;
	}
}

static Menu* GetOrCreateWantedMenu()
{
	if (Controller->IsMenuExist(L"ͨ���ȼ��޸�"))
	{
		return Controller->GetMenu(L"ͨ���ȼ��޸�");
	}
	else
	{
		var newMenu = new Menu(L"ͨ���ȼ��޸�");
		Controller->Register(newMenu);
		newMenu->AddItem(new ClearWanted(L"���ͨ���ȼ�"));
		newMenu->AddItem(new ModifyWantedLevel(L"����1����", 1));
		newMenu->AddItem(new ModifyWantedLevel(L"����1����", 1));
		newMenu->AddItem(new NeverWanted(L"������ͨ��"));
		newMenu->AddItem(new PoliceIgnore(L"����������"));
		return newMenu;
	}
}

static Menu* GetOrCreatePlayerMenu()
{
	if (Controller->IsMenuExist(L"���ϵͳ"))
	{
		return Controller->GetMenu(L"���ϵͳ");
	}
	else
	{
		var newMenu = new Menu(L"���ϵͳ");
		Controller->Register(newMenu);
		newMenu->AddItem(new SubMenu(L"����", GetOrCreatePlayerTeleportMenu()));
		newMenu->AddItem(new FixPlayer(L"�ָ�����"));
		newMenu->AddItem(new SubMenu(L"���ӽ�Ǯ", GetOrCreateAddCashMenu()));
		newMenu->AddItem(new SubMenu(L"ͨ���ȼ��޸�", GetOrCreateWantedMenu()));
		newMenu->AddItem(new PlayerInvincible(L"�޵�"));
		newMenu->AddItem(new UnlimitedAbility(L"������������"));
		newMenu->AddItem(new NoNoise(L"����"));
		newMenu->AddItem(new FastSwim(L"������Ӿ"));
		newMenu->AddItem(new FastRun(L"������"));
		newMenu->AddItem(new SuperJump(L"������"));
		newMenu->AddItem(new SubMenu(L"�ı����ģ��", GetOrCreatePlayerChangeSkinMenu()));
		newMenu->AddItem(new FallBackSkinWhenDead(L"������������Զ��ָ���Ĭ��Ƥ��"));
		newMenu->AddItem(new FallBackSkin(L"�ָ���Ĭ��Ƥ��"));
		return newMenu;
	}
}

static Menu* GetOrCreateGetWeaponMenu()
{
	if (Controller->IsMenuExist(L"��ȡ����"))
	{
		return Controller->GetMenu(L"��ȡ����");
	}
	else
	{
		var newMenu = new Menu(L"��ȡ����");
		newMenu->AddItem(new GetAllWeapons(L"��ȡ��������"));
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
	if (Controller->IsMenuExist(L"����ϵͳ"))
	{
		return Controller->GetMenu(L"����ϵͳ");
	}
	else
	{
		var newMenu = new Menu(L"����ϵͳ");
		newMenu->AddItem(new SubMenu(L"��ȡ����", GetOrCreateGetWeaponMenu()));
		newMenu->AddItem(new GetAllWeapons(L"��ȡ��������"));
		newMenu->AddItem(new DropCurrentWeapon(L"�Ƴ���ǰ����"));
		newMenu->AddItem(new RemoveAllWeapon(L"�Ƴ���������"));
		newMenu->AddItem(new UnlimitedAmmo(L"�����ӵ�"));
		newMenu->AddItem(new FireAmmo(L"�����ӵ�"));
		newMenu->AddItem(new ExplosiveAmmo(L"��ը�ӵ�"));
		newMenu->AddItem(new ExplosiveMelee(L"��ը��ս����"));
		Controller->Register(newMenu);
		return newMenu;
	}
}

static Menu* GetOrCreateSpawnCarMenu()
{
	if (Controller->IsMenuExist(L"��������"))
	{
		return Controller->GetMenu(L"��������");
	}
	else
	{
		var newMenu = new Menu(L"��������");
		
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
	if (Controller->IsMenuExist(L"����ֱ����"))
	{
		return Controller->GetMenu(L"����ֱ����");
	}
	else
	{
		var newMenu = new Menu(L"����ֱ����");

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
	if (Controller->IsMenuExist(L"����Ħ�г�"))
	{
		return Controller->GetMenu(L"����Ħ�г�");
	}
	else
	{
		var newMenu = new Menu(L"����Ħ�г�");

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
	if (Controller->IsMenuExist(L"���ɷɻ�"))
	{
		return Controller->GetMenu(L"���ɷɻ�");
	}
	else
	{
		var newMenu = new Menu(L"���ɷɻ�");

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
	if (Controller->IsMenuExist(L"����ˮ½���ܳ�"))
	{
		return Controller->GetMenu(L"����ˮ½���ܳ�");
	}
	else
	{
		var newMenu = new Menu(L"����ˮ½���ܳ�");

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
	if (Controller->IsMenuExist(L"�����ϳ�"))
	{
		return Controller->GetMenu(L"�����ϳ�");
	}
	else
	{
		var newMenu = new Menu(L"�����ϳ�");

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
	if (Controller->IsMenuExist(L"����Ǳˮͧ"))
	{
		return Controller->GetMenu(L"����Ǳˮͧ");
	}
	else
	{
		var newMenu = new Menu(L"����Ǳˮͧ");

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
	if (Controller->IsMenuExist(L"��������Ħ�г�"))
	{
		return Controller->GetMenu(L"��������Ħ�г�");
	}
	else
	{
		var newMenu = new Menu(L"��������Ħ�г�");

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
	if (Controller->IsMenuExist(L"����ˮ½����Ħ�г�"))
	{
		return Controller->GetMenu(L"����ˮ½����Ħ�г�");
	}
	else
	{
		var newMenu = new Menu(L"����ˮ½����Ħ�г�");

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
	if (Controller->IsMenuExist(L"���ɷ�ͧ"))
	{
		return Controller->GetMenu(L"���ɷ�ͧ");
	}
	else
	{
		var newMenu = new Menu(L"���ɷ�ͧ");

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
	if (Controller->IsMenuExist(L"�������г�"))
	{
		return Controller->GetMenu(L"�������г�");
	}
	else
	{
		var newMenu = new Menu(L"�������г�");

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
	if (Controller->IsMenuExist(L"���ɻ�"))
	{
		return Controller->GetMenu(L"���ɻ�");
	}
	else
	{
		var newMenu = new Menu(L"���ɻ�");

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
	if (Controller->IsMenuExist(L"���ɴ�"))
	{
		return Controller->GetMenu(L"���ɴ�");
	}
	else
	{
		var newMenu = new Menu(L"���ɴ�");

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
	if (Controller->IsMenuExist(L"����Ǳˮ��"))
	{
		return Controller->GetMenu(L"����Ǳˮ��");
	}
	else
	{
		var newMenu = new Menu(L"����Ǳˮ��");

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
	if (Controller->IsMenuExist(L"���ɳ���"))
	{
		return Controller->GetMenu(L"���ɳ���");
	}
	else
	{
		var newMenu = new Menu(L"���ɳ���");
		newMenu->AddItem(new SetSpawnCarAndWarpInFlag(L"�������ɳ������������뵽����"));
		newMenu->AddItem(new SubMenu(L"����Ǳˮ��", GetOrCreateSpawnSubmarinecarMenu()));
		newMenu->AddItem(new SubMenu(L"��������", GetOrCreateSpawnCarMenu()));
		newMenu->AddItem(new SubMenu(L"����ֱ����", GetOrCreateSpawnHelicopterMenu()));
		newMenu->AddItem(new SubMenu(L"����Ħ�г�", GetOrCreateSpawnBikeMenu()));
		newMenu->AddItem(new SubMenu(L"���ɷɻ�", GetOrCreateSpawnPlaneMenu()));
		newMenu->AddItem(new SubMenu(L"���ɴ�", GetOrCreateSpawnBoatMenu()));
		newMenu->AddItem(new SubMenu(L"����ˮ½���ܳ�", GetOrCreateSpawnAmphibiousAutomobileMenu()));
		newMenu->AddItem(new SubMenu(L"�����ϳ�", GetOrCreateSpawnTrailerMenu()));
		newMenu->AddItem(new SubMenu(L"����Ǳˮͧ", GetOrCreateSpawnSubmarineMenu()));
		newMenu->AddItem(new SubMenu(L"��������Ħ�г�", GetOrCreateSpawnQuadbikeMenu()));
		newMenu->AddItem(new SubMenu(L"����ˮ½����Ħ�г�", GetOrCreateSpawnAmphibiousQuadbikeMenu()));
		newMenu->AddItem(new SubMenu(L"���ɷ�ͧ", GetOrCreateSpawnBlimpMenu()));
		newMenu->AddItem(new SubMenu(L"�������г�", GetOrCreateSpawnBicycleMenu()));
		newMenu->AddItem(new SubMenu(L"���ɻ�", GetOrCreateSpawnTrainMenu()));
		Controller->Register(newMenu);
		return newMenu;
	}
}

static Menu* GetOrCreateVehicleMenu()
{
	if (Controller->IsMenuExist(L"����ϵͳ"))
	{
		return Controller->GetMenu(L"����ϵͳ");
	}
	else
	{
		var newMenu = new Menu(L"����ϵͳ");
		newMenu->AddItem(new SubMenu(L"���ɳ���", GetOrCreateSpawnVehicleMenu()));
		newMenu->AddItem(new RandomPaintCar(L"�ı䳵����ɫ"));
		newMenu->AddItem(new FixCar(L"������"));
		newMenu->AddItem(new SafeBelt(L"��ȫ��"));
		newMenu->AddItem(new InvincibleCar(L"�����޵�"));
		newMenu->AddItem(new InvincibleWheel(L"��������"));
		newMenu->AddItem(new SpeedBoost(L"��������"));
		newMenu->AddItem(new VehicleRockets(L"���ػ����"));
		Controller->Register(newMenu);
		return newMenu;
	}
}

static Menu* GetOrCreateChangeWeatherMenu()
{
	if (Controller->IsMenuExist(L"�ı�����"))
	{
		return Controller->GetMenu(L"�ı�����");
	}
	else
	{
		var newMenu = new Menu(L"�ı�����");
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
	if (Controller->IsMenuExist(L"����ϵͳ"))
	{
		return Controller->GetMenu(L"����ϵͳ");
	}
	else
	{
		var newMenu = new Menu(L"����ϵͳ");
		newMenu->AddItem(new SubMenu(L"�ı�����", GetOrCreateChangeWeatherMenu()));
		newMenu->AddItem(new StandCurrentWeather(L"���ֵ�ǰ����"));
		newMenu->AddItem(new SetWind(L"���ɴ��"));
		Controller->Register(newMenu);
		return newMenu;
	}
}

static Menu* GetOrCreateTimeMenu()
{
	if (Controller->IsMenuExist(L"ʱ��ϵͳ"))
	{
		return Controller->GetMenu(L"ʱ��ϵͳ");
	}
	else
	{
		var newMenu = new Menu(L"ʱ��ϵͳ");
		newMenu->AddItem(new TimeModify(L"ǰ��һСʱ", 1));
		newMenu->AddItem(new TimeModify(L"����һСʱ", -1));
		newMenu->AddItem(new TimePause(L"��ͣʱ��"));
		newMenu->AddItem(new TimeSynced(L"��ʵʱ��"));
		Controller->Register(newMenu);
		return newMenu;
	}
}

static Menu* GetOrCreateMiscMenu()
{
	if (Controller->IsMenuExist(L"����ϵͳ"))
	{
		return Controller->GetMenu(L"����ϵͳ");
	}
	else
	{
		var newMenu = new Menu(L"����ϵͳ");
		newMenu->AddItem(new MoonGravity(L"��������"));
		newMenu->AddItem(new RandomCops(L"�������"));
		newMenu->AddItem(new RandomTrains(L"�����"));
		newMenu->AddItem(new RandomBoats(L"�����"));
		newMenu->AddItem(new RandomGarbageTrucks(L"���������"));
		newMenu->AddItem(new NextRadioTrack(L"��һ�׳�������"));
		newMenu->AddItem(new HideHud(L"����Hud"));
		Controller->Register(newMenu);
		return newMenu;
	}
}

static Menu* GetOrCreateMainMenu()
{
	if (Controller->IsMenuExist(L"�����޸���1.0 By Da"))
	{
		return Controller->GetMenu(L"�����޸���1.0 By Da");
	}
	else
	{
		var newMenu = new Menu(L"�����޸���1.0 By Da");
		newMenu->AddItem(new SubMenu(L"���ϵͳ", GetOrCreatePlayerMenu()));
		newMenu->AddItem(new SubMenu(L"����ϵͳ", GetOrCreateWeaponMenu()));
		newMenu->AddItem(new SubMenu(L"����ϵͳ", GetOrCreateVehicleMenu()));
		newMenu->AddItem(new SubMenu(L"����ϵͳ", GetOrCreateWeatherMenu()));
		newMenu->AddItem(new SubMenu(L"ʱ��ϵͳ", GetOrCreateTimeMenu()));
		newMenu->AddItem(new SubMenu(L"����ϵͳ", GetOrCreateMiscMenu()));
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