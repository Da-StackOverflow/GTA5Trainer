using ScriptUI;
using static ScriptUI.Functions;

namespace Weapon
{
	public class Entry : AScriptEntry
	{
		protected override void OnInit()
		{
			Log.Info("Weapon OnInit");
			_controller.MainMenu.AddItem(new SubMenu("武器系统", GetOrCreateWeaponMenu));
		}

		private Menu GetOrCreateGetWeaponMenu()
		{
			if (!_controller.TryGetMenu("获取武器", out Menu menu))
			{
				menu = new Menu("获取武器");
				menu.AddItem(new GetAllWeapons("获取所有武器"));
				var length = WeaponResources.WeaponsInfos.Length;
				for (var i = 0; i < length; i++)
				{
					menu.AddItem(new GetWeapon(WeaponResources.WeaponsInfos[i]));
				}
				_controller.Register(menu);
			}
			return menu;
		}

		static UpdateWeapon CreateUpdateWeaponItem()
		{
			return new UpdateWeapon();
		}

		static unsafe void RefreshUpdateWeaponItem(int index, UpdateWeapon item)
		{
			uint weaponHash = 0;
			if (GET_CURRENT_PED_WEAPON(PLAYER_PED_ID(), &weaponHash, true))
			{
				foreach (var pair in WeaponResources.WeaponComponents)
				{
					if (GET_HASH_KEY(pair.Key) == weaponHash)
					{
						var info = pair.Value[index];
						item.ComponentInfo = info;
						item.Text = info.Name;
						return;
					}
				}
			}
		}

		private unsafe Menu<UpdateWeapon> GetOrCreateUpdateWeaponMenu()
		{
			if (!_controller.TryGetMenu("升级武器", out Menu<UpdateWeapon> menu))
			{
				menu = new Menu<UpdateWeapon>("升级武器", CreateUpdateWeaponItem, RefreshUpdateWeaponItem);
				_controller.Register(menu);
			}
			uint weaponHash = 0;
			bool find = false;
			if (GET_CURRENT_PED_WEAPON(PLAYER_PED_ID(), &weaponHash, true))
			{
				foreach (var pair in WeaponResources.WeaponComponents)
				{
					if (GET_HASH_KEY(pair.Key) == weaponHash)
					{
						menu.SetItemNums(pair.Value.Count);
						find = true;
						break;
					}
				}
				if (!find)
				{
					menu.SetItemNums(0);
				}
			}
			else
			{
				menu.SetItemNums(0);
			}
			return menu;
		}

		private Menu GetOrCreateWeaponMenu()
		{
			if (!_controller.TryGetMenu("武器系统", out Menu menu))
			{
				menu = new Menu("武器系统");
				menu.AddItem(new SubMenu("获取武器", GetOrCreateGetWeaponMenu));
				menu.AddItem(new SubMenu<Menu<UpdateWeapon>>("升级武器", GetOrCreateUpdateWeaponMenu));
				menu.AddItem(new GetAllWeapons("获取所有武器"));
				menu.AddItem(new DropCurrentWeapon("移除当前武器"));
				menu.AddItem(new RemoveAllWeapon("移除所有武器"));
				menu.AddItem(new UnlimitedAmmo("无限子弹"));
				menu.AddItem(new FireAmmo("火焰子弹"));
				menu.AddItem(new ExplosiveAmmo("爆炸子弹"));
				menu.AddItem(new ExplosiveMelee("爆炸近战武器"));
				_controller.Register(menu);
			}
			return menu;
		}
	}
}
