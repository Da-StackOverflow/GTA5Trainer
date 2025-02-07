export module Vehicle;

import "Base.h";
import Function;
import Util;
import Menu;
import Vector;
import VehicleInfos;
import ItemInfo;


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
			Vector3 coords = ENTITY::GET_OFFSET_FROM_ENTITY_IN_WORLD_COORDS(PLAYER::PLAYER_PED_ID(), 0.0, 5.0, 0.0);
			Vehicle veh = VEHICLE::CREATE_VEHICLE(model, coords.x, coords.y, coords.z, 0.0f, true, true, true);
			VEHICLE::SET_VEHICLE_ON_GROUND_PROPERLY(veh, 5.0f);

			if (_WrapInWhenCarSpawned)
			{
				ENTITY::SET_ENTITY_HEADING(veh, ENTITY::GET_ENTITY_HEADING(PLAYER::PLAYER_PED_ID()));
				PED::SET_PED_INTO_VEHICLE(PLAYER::PLAYER_PED_ID(), veh, -1);
			}

			ThreadSleep(0);
			STREAMING::SET_MODEL_AS_NO_LONGER_NEEDED(model);
			ENTITY::SET_VEHICLE_AS_NO_LONGER_NEEDED(&veh);
		}
	}
};
