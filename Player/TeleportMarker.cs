﻿using ScriptUI;
using static ScriptUI.Functions;

namespace Player
{
	internal sealed class TeleportMarker : TriggerItem
	{
		public TeleportMarker(string caption) : base(caption)
		{
		}

		protected unsafe override void OnExecute()
		{
			int e = PlayerPed;
			if (IS_PED_IN_ANY_VEHICLE(e, true))
			{
				e = GET_VEHICLE_PED_IS_USING(e);
			}

			Vector3 coords = new();

			bool blipFound = false;

			int blipIterator = GET_WAYPOINT_BLIP_ENUM_ID();
			for (int i = GET_FIRST_BLIP_INFO_ID(blipIterator); DOES_BLIP_EXIST(i); i = GET_NEXT_BLIP_INFO_ID(blipIterator))
			{
				if (GET_BLIP_INFO_ID_TYPE(i) == 4)
				{
					coords = GET_BLIP_INFO_ID_COORD(i);
					blipFound = true;
					break;
				}
			}
			if (!blipFound)
			{
				SetTips("请先在地图上设置标记点");
				return;
			}

			bool groundFound = false;
			var length = PlayerResources.GroundCheckHeight.Length;
			for (int i = 0; i < length; i++)
			{
				SET_ENTITY_COORDS_NO_OFFSET(e, coords.X, coords.Y, PlayerResources.GroundCheckHeight[i], false, false, true);
				Wait(100);
				if (GET_GROUND_Z_FOR_3D_COORD(coords.X, coords.Y, PlayerResources.GroundCheckHeight[i], &coords.Z, false, false))
				{
					groundFound = true;
					coords.Z += 1.0f;
					break;
				}
			}

			if (!groundFound)
			{
				coords.Z = 1000.0f;
				GIVE_WEAPON_TO_PED(PlayerPed, 0xFBAB5776, 1, false, false);
			}

			SET_ENTITY_COORDS_NO_OFFSET(e, coords.X, coords.Y, coords.Z, false, false, true);
			Wait(0);
			SetTips("成功传送");
		}
	}
}
