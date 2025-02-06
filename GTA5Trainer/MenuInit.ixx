import "Base.h";
import Menu;
import Util;
import Function;
import InputSystem;

static Menu& GetOrCreatePlayerMenu()
{
	if (Controller.IsMenuExist("���ϵͳ"))
	{
		return Controller.GetMenu("���ϵͳ");
	}
	else
	{
		var newMenu = Menu("���ϵͳ");
		Controller.Register(newMenu);
		return Controller.GetMenu("���ϵͳ");
	}
}

static Menu& GetOrCreateWeaponMenu()
{
	if (Controller.IsMenuExist("����ϵͳ"))
	{
		return Controller.GetMenu("����ϵͳ");
	}
	else
	{
		var newMenu = Menu("����ϵͳ");
		Controller.Register(newMenu);
		return Controller.GetMenu("����ϵͳ");
	}
}

static Menu& GetOrCreateVehicleMenu()
{
	if (Controller.IsMenuExist("����ϵͳ"))
	{
		return Controller.GetMenu("����ϵͳ");
	}
	else
	{
		var newMenu = Menu("����ϵͳ");
		Controller.Register(newMenu);
		return Controller.GetMenu("����ϵͳ");
	}
}

static Menu& GetOrCreateWeatherMenu()
{
	if (Controller.IsMenuExist("����ϵͳ"))
	{
		return Controller.GetMenu("����ϵͳ");
	}
	else
	{
		var newMenu = Menu("����ϵͳ");
		Controller.Register(newMenu);
		return Controller.GetMenu("����ϵͳ");
	}
}

static Menu& GetOrCreateTimeMenu()
{
	if (Controller.IsMenuExist("ʱ��ϵͳ"))
	{
		return Controller.GetMenu("ʱ��ϵͳ");
	}
	else
	{
		var newMenu = Menu("ʱ��ϵͳ");
		Controller.Register(newMenu);
		return Controller.GetMenu("ʱ��ϵͳ");
	}
}

static Menu& GetOrCreateMiscMenu()
{
	if (Controller.IsMenuExist("����ϵͳ"))
	{
		return Controller.GetMenu("����ϵͳ");
	}
	else
	{
		var newMenu = Menu("����ϵͳ");
		Controller.Register(newMenu);
		return Controller.GetMenu("����ϵͳ");
	}
}

static Menu& GetOrCreateMainMenu()
{
	if (Controller.IsMenuExist("�����޸���1.0 By Da"))
	{
		return Controller.GetMenu("�����޸���1.0 By Da");
	}
	else
	{
		var newMenu = Menu("�����޸���1.0 By Da");
		var playerSubMenu = SubMenu("���ϵͳ", GetOrCreatePlayerMenu());
		var weaponSubMenu = SubMenu("����ϵͳ", GetOrCreateWeaponMenu());
		var vehicleSubMenu = SubMenu("����ϵͳ", GetOrCreateVehicleMenu());
		var weatherSubMenu = SubMenu("����ϵͳ", GetOrCreateWeatherMenu());
		var timeSubMenu = SubMenu("ʱ��ϵͳ", GetOrCreateTimeMenu());
		var miscSubMenu = SubMenu("����ϵͳ", GetOrCreateMiscMenu());
		newMenu.AddItem(playerSubMenu);
		newMenu.AddItem(weaponSubMenu);
		newMenu.AddItem(vehicleSubMenu);
		newMenu.AddItem(weatherSubMenu);
		newMenu.AddItem(timeSubMenu);
		Controller.Register(newMenu);
		return Controller.GetMenu("�����޸���1.0 By Da");
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