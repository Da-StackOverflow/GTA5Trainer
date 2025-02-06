import "Base.h";
import Menu;
import Util;
import Function;
import InputSystem;

static Menu& GetOrCreatePlayerMenu()
{
	if (Controller.IsMenuExist("玩家系统"))
	{
		return Controller.GetMenu("玩家系统");
	}
	else
	{
		var newMenu = Menu("玩家系统");
		Controller.Register(newMenu);
		return Controller.GetMenu("玩家系统");
	}
}

static Menu& GetOrCreateWeaponMenu()
{
	if (Controller.IsMenuExist("武器系统"))
	{
		return Controller.GetMenu("武器系统");
	}
	else
	{
		var newMenu = Menu("武器系统");
		Controller.Register(newMenu);
		return Controller.GetMenu("武器系统");
	}
}

static Menu& GetOrCreateVehicleMenu()
{
	if (Controller.IsMenuExist("车辆系统"))
	{
		return Controller.GetMenu("车辆系统");
	}
	else
	{
		var newMenu = Menu("车辆系统");
		Controller.Register(newMenu);
		return Controller.GetMenu("车辆系统");
	}
}

static Menu& GetOrCreateWeatherMenu()
{
	if (Controller.IsMenuExist("天气系统"))
	{
		return Controller.GetMenu("天气系统");
	}
	else
	{
		var newMenu = Menu("天气系统");
		Controller.Register(newMenu);
		return Controller.GetMenu("天气系统");
	}
}

static Menu& GetOrCreateTimeMenu()
{
	if (Controller.IsMenuExist("时间系统"))
	{
		return Controller.GetMenu("时间系统");
	}
	else
	{
		var newMenu = Menu("时间系统");
		Controller.Register(newMenu);
		return Controller.GetMenu("时间系统");
	}
}

static Menu& GetOrCreateMiscMenu()
{
	if (Controller.IsMenuExist("其他系统"))
	{
		return Controller.GetMenu("其他系统");
	}
	else
	{
		var newMenu = Menu("其他系统");
		Controller.Register(newMenu);
		return Controller.GetMenu("其他系统");
	}
}

static Menu& GetOrCreateMainMenu()
{
	if (Controller.IsMenuExist("内置修改器1.0 By Da"))
	{
		return Controller.GetMenu("内置修改器1.0 By Da");
	}
	else
	{
		var newMenu = Menu("内置修改器1.0 By Da");
		var playerSubMenu = SubMenu("玩家系统", GetOrCreatePlayerMenu());
		var weaponSubMenu = SubMenu("武器系统", GetOrCreateWeaponMenu());
		var vehicleSubMenu = SubMenu("车辆系统", GetOrCreateVehicleMenu());
		var weatherSubMenu = SubMenu("天气系统", GetOrCreateWeatherMenu());
		var timeSubMenu = SubMenu("时间系统", GetOrCreateTimeMenu());
		var miscSubMenu = SubMenu("其他系统", GetOrCreateMiscMenu());
		newMenu.AddItem(playerSubMenu);
		newMenu.AddItem(weaponSubMenu);
		newMenu.AddItem(vehicleSubMenu);
		newMenu.AddItem(weatherSubMenu);
		newMenu.AddItem(timeSubMenu);
		Controller.Register(newMenu);
		return Controller.GetMenu("内置修改器1.0 By Da");
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