export module Vehicle;

import "Base.h";
import Function;
import Util;
import Menu;
import Vector;
import VehicleInfos;
import ItemInfo;
import InputSystem;
import KeyCode;


bool _WrapInWhenCarSpawned = false;

export class SetSpawnCarAndWarpInFlag : public SwitchItem
{
public:
	constexpr SetSpawnCarAndWarpInFlag(WString caption) : SwitchItem(caption)
	{
	}

protected:
	void OnActive() override
	{
		_WrapInWhenCarSpawned = true;
	}

	void OnInactive() override
	{
		_WrapInWhenCarSpawned = false;
	}
};

export class SpawnCar : public TriggerItem
{
public:
	constexpr SpawnCar(const ItemInfo& carInfo) : TriggerItem(carInfo.Caption), CarInfo(carInfo)
	{
	}

private:
	ItemInfo CarInfo;

protected:
	void OnExecute() override
	{
		Entity model = MISC::GET_HASH_KEY(CarInfo.Model);
		if (STREAMING::IS_MODEL_IN_CDIMAGE(model) && STREAMING::IS_MODEL_A_VEHICLE(model))
		{
			STREAMING::REQUEST_MODEL(model);
			while (!STREAMING::HAS_MODEL_LOADED(model))
			{
				ThreadSleep(0);
			}
			Vector3 coords = ENTITY::GET_OFFSET_FROM_ENTITY_IN_WORLD_COORDS(PlayerPed(), 0.0, 5.0, 0.0);
			Vehicle veh = VEHICLE::CREATE_VEHICLE(model, coords.x, coords.y, coords.z, 0.0f, true, true, true);
			VEHICLE::SET_VEHICLE_ON_GROUND_PROPERLY(veh, 5.0f);

			if (_WrapInWhenCarSpawned)
			{
				ENTITY::SET_ENTITY_HEADING(veh, ENTITY::GET_ENTITY_HEADING(PlayerPed()));
				PED::SET_PED_INTO_VEHICLE(PlayerPed(), veh, -1);
			}

			ThreadSleep(0);
			STREAMING::SET_MODEL_AS_NO_LONGER_NEEDED(model);
			ENTITY::SET_VEHICLE_AS_NO_LONGER_NEEDED(&veh);
		}
	}
};

export class RandomPaintCar : public TriggerItem
{
public:
	constexpr RandomPaintCar(WString caption) : TriggerItem(caption)
	{
	}

protected:
	void OnExecute() override
	{
		if (PED::IS_PED_IN_ANY_VEHICLE(PlayerPed(), false))
		{
			Vehicle veh = PED::GET_VEHICLE_PED_IS_USING(PlayerPed());
			VEHICLE::SET_VEHICLE_CUSTOM_PRIMARY_COLOUR(veh, Random(0, 255), Random(0, 255), Random(0, 255));
			if (VEHICLE::GET_IS_VEHICLE_PRIMARY_COLOUR_CUSTOM(veh))
			{
				VEHICLE::SET_VEHICLE_CUSTOM_SECONDARY_COLOUR(veh, Random(0, 255), Random(0, 255), Random(0, 255));
			}
			return;
		}
		SetTips(L"请先进入车辆");
	}
};

export class FixCar : public TriggerItem
{
public:
	constexpr FixCar(WString caption) : TriggerItem(caption)
	{
	}

protected:
	void OnExecute() override
	{
		if (PED::IS_PED_IN_ANY_VEHICLE(PlayerPed(), 0))
		{
			VEHICLE::SET_VEHICLE_FIXED(PED::GET_VEHICLE_PED_IS_USING(PlayerPed()));
			return;
		}
		SetTips(L"请先进入车辆");
	}
};

export class SafeBelt : public SwitchItem
{
public:
	constexpr SafeBelt(WString caption) : SwitchItem(caption)
	{

	}

protected:
	void OnActive() override
	{
		if (ENTITY::DOES_ENTITY_EXIST(PlayerPed()))
		{
			if (PED::GET_PED_CONFIG_FLAG(PlayerPed(), 32, true))
			{
				PED::SET_PED_CONFIG_FLAG(PlayerPed(), 32, false);
			}
		}
	}

	void OnInactive() override
	{
		if (ENTITY::DOES_ENTITY_EXIST(PlayerPed()))
		{
			PED::SET_PED_CONFIG_FLAG(PlayerPed(), 32, true);
		}
	}
};

export class InvincibleCar : public SwitchItem
{
private:
	Vehicle _veh = -1;
public:
	constexpr InvincibleCar(WString caption) : SwitchItem(caption)
	{

	}

protected:
	void OnActive() override
	{
		if (ENTITY::DOES_ENTITY_EXIST(PlayerPed()) && PED::IS_PED_IN_ANY_VEHICLE(PlayerPed(), 0))
		{
			Vehicle veh = PED::GET_VEHICLE_PED_IS_USING(PlayerPed());
			ENTITY::SET_ENTITY_INVINCIBLE(veh, true, false);
			ENTITY::SET_ENTITY_PROOFS(veh, 1, 1, 1, 1, 1, 1, 1, 1);
			VEHICLE::SET_VEHICLE_TYRES_CAN_BURST(veh, 0);
			VEHICLE::SET_VEHICLE_WHEELS_CAN_BREAK(veh, 0);
			VEHICLE::SET_VEHICLE_CAN_BE_VISIBLY_DAMAGED(veh, 0);
			_veh = veh;
		}
	}

	void OnUpdate() override
	{
		if (ENTITY::DOES_ENTITY_EXIST(PlayerPed()) && PED::IS_PED_IN_ANY_VEHICLE(PlayerPed(), 0))
		{
			Vehicle veh = PED::GET_VEHICLE_PED_IS_USING(PlayerPed());
			if (veh == _veh)
			{
				return;
			}
			ENTITY::SET_ENTITY_INVINCIBLE(veh, true, false);
			ENTITY::SET_ENTITY_PROOFS(veh, 1, 1, 1, 1, 1, 1, 1, 1);
			VEHICLE::SET_VEHICLE_TYRES_CAN_BURST(veh, 0);
			VEHICLE::SET_VEHICLE_WHEELS_CAN_BREAK(veh, 0);
			VEHICLE::SET_VEHICLE_CAN_BE_VISIBLY_DAMAGED(veh, 0);
			_veh = veh;
		}
	}

	void OnInactive() override
	{
		if (ENTITY::DOES_ENTITY_EXIST(PlayerPed()) && PED::IS_PED_IN_ANY_VEHICLE(PlayerPed(), 0))
		{
			Vehicle veh = PED::GET_VEHICLE_PED_IS_USING(PlayerPed());
			ENTITY::SET_ENTITY_INVINCIBLE(veh, false, false);
			ENTITY::SET_ENTITY_PROOFS(veh, 0, 0, 0, 0, 0, 0, 0, 0);
			VEHICLE::SET_VEHICLE_TYRES_CAN_BURST(veh, 1);
			VEHICLE::SET_VEHICLE_WHEELS_CAN_BREAK(veh, 1);
			VEHICLE::SET_VEHICLE_CAN_BE_VISIBLY_DAMAGED(veh, 1);
		}
		_veh = -1;
	}
};

export class InvincibleWheel : public SwitchItem
{
private:
	Vehicle _veh = -1;
public:
	constexpr InvincibleWheel(WString caption) : SwitchItem(caption)
	{

	}

protected:
	void OnActive() override
	{
		if (ENTITY::DOES_ENTITY_EXIST(PlayerPed()) && PED::IS_PED_IN_ANY_VEHICLE(PlayerPed(), 0))
		{
			Vehicle veh = PED::GET_VEHICLE_PED_IS_USING(PlayerPed());
			VEHICLE::SET_VEHICLE_TYRES_CAN_BURST(veh, false);
			VEHICLE::SET_VEHICLE_WHEELS_CAN_BREAK(veh, false);
			VEHICLE::SET_VEHICLE_HAS_STRONG_AXLES(veh, true);
			_veh = veh;
		}
	}

	void OnUpdate() override
	{
		if (ENTITY::DOES_ENTITY_EXIST(PlayerPed()) && PED::IS_PED_IN_ANY_VEHICLE(PlayerPed(), 0))
		{
			Vehicle veh = PED::GET_VEHICLE_PED_IS_USING(PlayerPed());
			if (veh == _veh)
			{
				return;
			}
			VEHICLE::SET_VEHICLE_TYRES_CAN_BURST(veh, false);
			VEHICLE::SET_VEHICLE_WHEELS_CAN_BREAK(veh, false);
			VEHICLE::SET_VEHICLE_HAS_STRONG_AXLES(veh, true);
			_veh = veh;
		}
	}

	void OnInactive() override
	{
		if (ENTITY::DOES_ENTITY_EXIST(PlayerPed()) && PED::IS_PED_IN_ANY_VEHICLE(PlayerPed(), 0))
		{
			Vehicle veh = PED::GET_VEHICLE_PED_IS_USING(PlayerPed());
			VEHICLE::SET_VEHICLE_TYRES_CAN_BURST(veh, true);
			VEHICLE::SET_VEHICLE_WHEELS_CAN_BREAK(veh, true);
			VEHICLE::SET_VEHICLE_HAS_STRONG_AXLES(veh, false);
		}
		_veh = -1;
	}
};

export class SpeedBoost : public SwitchItem
{
public:
	constexpr SpeedBoost(WString caption) : SwitchItem(caption)
	{

	}

protected:
	void OnActive() override
	{
		SetTips(L"小键盘9加速, 小键盘3减速", 10000);
	}

	void OnUpdate() override
	{
		if (ENTITY::DOES_ENTITY_EXIST(PlayerPed()) && PED::IS_PED_IN_ANY_VEHICLE(PlayerPed(), 0))
		{
			Vehicle veh = PED::GET_VEHICLE_PED_IS_USING(PlayerPed());
			Entity model = ENTITY::GET_ENTITY_MODEL(veh);

			bool bUp = Input.IsKeyDown(KeyCode::Num9);
			bool bDown = Input.IsKeyDown(KeyCode::Num3);

			if (bUp || bDown)
			{
				float speed = ENTITY::GET_ENTITY_SPEED(veh);
				if (bUp)
				{
					if (speed < 3.0f) speed = 3.0f;
					speed += speed * 0.05f;
					VEHICLE::SET_VEHICLE_FORWARD_SPEED(veh, speed);
					return;
				}
				if (ENTITY::IS_ENTITY_IN_AIR(veh) || speed > 5.0)
				{
					VEHICLE::SET_VEHICLE_FORWARD_SPEED(veh, 0.0);
				}
			}
		}
	}
};

export class VehicleRockets : public SwitchItem
{
private:
	i64 _vehBoostLastTime = 0;
public:
	constexpr VehicleRockets(WString caption) : SwitchItem(caption)
	{
	}

	void OnActive() override
	{
		SetTips(L"X键发射火箭", 10000);
	}

	void OnUpdate() override
	{
		if (!ENTITY::DOES_ENTITY_EXIST(PlayerPed()))
		{
			return;
		}

		if (Input.IsKeyDown(KeyCode::X) && _vehBoostLastTime + 150 < GetTimeTicks() &&
			PLAYER::IS_PLAYER_CONTROL_ON(PlayerID()) && PED::IS_PED_IN_ANY_VEHICLE(PlayerPed(), 0))
		{
			Vehicle veh = PED::GET_VEHICLE_PED_IS_USING(PlayerPed());

			Vector3 v0, v1;
			MISC::GET_MODEL_DIMENSIONS(ENTITY::GET_ENTITY_MODEL(veh), &v0, &v1);

			Hash weaponAssetRocket = MISC::GET_HASH_KEY("WEAPON_VEHICLE_ROCKET");
			if (!WEAPON::HAS_WEAPON_ASSET_LOADED(weaponAssetRocket))
			{
				WEAPON::REQUEST_WEAPON_ASSET(weaponAssetRocket, 31, 0);
				while (!WEAPON::HAS_WEAPON_ASSET_LOADED(weaponAssetRocket))
				{
					ThreadSleep(0);
				}	
			}

			Vector3 coords0from = ENTITY::GET_OFFSET_FROM_ENTITY_IN_WORLD_COORDS(veh, -(v1.x + 0.25f), v1.y + 1.25f, 0.1f);
			Vector3 coords1from = ENTITY::GET_OFFSET_FROM_ENTITY_IN_WORLD_COORDS(veh, (v1.x + 0.25f), v1.y + 1.25f, 0.1f);
			Vector3 coords0to = ENTITY::GET_OFFSET_FROM_ENTITY_IN_WORLD_COORDS(veh, -v1.x, v1.y + 100.0f, 0.1f);
			Vector3 coords1to = ENTITY::GET_OFFSET_FROM_ENTITY_IN_WORLD_COORDS(veh, v1.x, v1.y + 100.0f, 0.1f);

			MISC::SHOOT_SINGLE_BULLET_BETWEEN_COORDS(coords0from.x, coords0from.y, coords0from.z,
				coords0to.x, coords0to.y, coords0to.z,
				250, 1, weaponAssetRocket, PlayerPed(), 1, 0, -1.0);
			MISC::SHOOT_SINGLE_BULLET_BETWEEN_COORDS(coords1from.x, coords1from.y, coords1from.z,
				coords1to.x, coords1to.y, coords1to.z,
				250, 1, weaponAssetRocket, PlayerPed(), 1, 0, -1.0);

			_vehBoostLastTime = GetTimeTicks();
		}
	}
};