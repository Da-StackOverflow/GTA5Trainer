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
		newMenu->AddItem(new Teleport(L"��˼�", -852.4f, 160.0f, 65.6f));
		newMenu->AddItem(new Teleport(L"�������ּ�", 7.9f, 548.1f, 175.5f));
		newMenu->AddItem(new Teleport(L"�޷���ϳ�", 1985.7f, 3812.2f, 32.2f));
		newMenu->AddItem(new Teleport(L"�������", -1034.6f, -2733.6f, 13.8f));
		newMenu->AddItem(new Teleport(L"�����ܵ�", -1336.0f, -3044.0f, 13.9f));
		newMenu->AddItem(new Teleport(L"÷����ɪ�Ļ���", 338.2f, -2715.9f, 38.5f));
		newMenu->AddItem(new Teleport(L"����", 760.4f, -2943.2f, 5.8f));
		newMenu->AddItem(new Teleport(L"��������ֲ�", 127.4f, -1307.7f, 29.2f));
		newMenu->AddItem(new Teleport(L"ELBURRO HEIGHTS", 1384.0f, -2057.1f, 52.0f));
		newMenu->AddItem(new Teleport(L"Ħ����", -1670.7f, -1125.0f, 13.0f));
		newMenu->AddItem(new Teleport(L"����ʲ", -3192.6f, 1100.0f, 20.2f));
		newMenu->AddItem(new Teleport(L"�������糡", 2354.0f, 1830.3f, 101.1f));
		newMenu->AddItem(new Teleport(L"���»���", -2047.4f, 3132.1f, 32.8f));
		newMenu->AddItem(new Teleport(L"��������", 2121.7f, 4796.3f, 41.1f));
		newMenu->AddItem(new Teleport(L"ɳĮ����", 1747.0f, 3273.7f, 41.1f));
		newMenu->AddItem(new Teleport(L"����Ү��ɽ", 425.4f, 5614.3f, 766.5f));
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
		newMenu->AddItem(new UnlimitedAbility(L"��������"));
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
	if (Controller->IsMenuExist(L"����ϵͳ"))
	{
		return Controller->GetMenu(L"����ϵͳ");
	}
	else
	{
		var newMenu = new Menu(L"����ϵͳ");
		newMenu->AddItem(new SubMenu(L"���ɳ���", GetOrCreateSpawnVehicleMenu()));
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