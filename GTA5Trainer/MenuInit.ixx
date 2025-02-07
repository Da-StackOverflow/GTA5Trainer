import "Base.h";
import Menu;
import Util;
import Function;
import InputSystem;
import ChangeSkin;
import PedModels;
import ItemInfo;
import Teleport;
import Weapon;

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
		newMenu->AddItem(new AutoFallBackSkin(L"自动换回默认模型"));
		for (var i = 0; i < sizeof(PedModels) / sizeof(ItemInfo); i++)
		{
			newMenu->AddItem(new ChangeSkin(PedModels[i]));
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
		newMenu->AddItem(new GetTeleportMarkerCords(L"显示地图标记点信息"));
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
		newMenu->AddItem(new SubMenu(L"改变玩家模型", GetOrCreatePlayerChangeSkinMenu()));
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