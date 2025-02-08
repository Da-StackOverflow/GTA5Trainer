import "Base.h";
import Menu;
import Util;
import Function;
import InputSystem;
import ItemInfo;
import PedModelInfos;
import WeaponsInfos;
import VehicleInfos;
import Player;
import Teleport;
import Weapon;
import Vehicle;


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
		newMenu->AddItem(new Teleport(L"麦克家", -852.4f, 160.0f, 65.6f));
		newMenu->AddItem(new Teleport(L"富兰克林家", 7.9f, 548.1f, 175.5f));
		newMenu->AddItem(new Teleport(L"崔佛的拖车", 1985.7f, 3812.2f, 32.2f));
		newMenu->AddItem(new Teleport(L"机场入口", -1034.6f, -2733.6f, 13.8f));
		newMenu->AddItem(new Teleport(L"机场跑道", -1336.0f, -3044.0f, 13.9f));
		newMenu->AddItem(new Teleport(L"梅利威瑟的基地", 338.2f, -2715.9f, 38.5f));
		newMenu->AddItem(new Teleport(L"沉船", 760.4f, -2943.2f, 5.8f));
		newMenu->AddItem(new Teleport(L"脱衣舞俱乐部", 127.4f, -1307.7f, 29.2f));
		newMenu->AddItem(new Teleport(L"ELBURRO HEIGHTS", 1384.0f, -2057.1f, 52.0f));
		newMenu->AddItem(new Teleport(L"摩天轮", -1670.7f, -1125.0f, 13.0f));
		newMenu->AddItem(new Teleport(L"丘马什", -3192.6f, 1100.0f, 20.2f));
		newMenu->AddItem(new Teleport(L"风力发电场", 2354.0f, 1830.3f, 101.1f));
		newMenu->AddItem(new Teleport(L"军事基地", -2047.4f, 3132.1f, 32.8f));
		newMenu->AddItem(new Teleport(L"麦肯奇机场", 2121.7f, 4796.3f, 41.1f));
		newMenu->AddItem(new Teleport(L"沙漠机场", 1747.0f, 3273.7f, 41.1f));
		newMenu->AddItem(new Teleport(L"乞里耶德山", 425.4f, 5614.3f, 766.5f));
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
		newMenu->AddItem(new UnlimitedAbility(L"无限体力"));
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
		for (var i = 0; i < sizeof(VehicleInfos) / sizeof(ItemInfo); i++)
		{
			newMenu->AddItem(new SpawnCar(VehicleInfos[i]));
		}
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