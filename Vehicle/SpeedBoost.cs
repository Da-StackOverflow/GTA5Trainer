using ScriptUI;
using static ScriptUI.Functions;

namespace Vehicle
{
	internal sealed class SpeedBoost : UpdateableItem
	{
		public SpeedBoost(string caption) : base(caption)
		{
		}

		protected override void OnActive()
		{
			SetTips("小键盘9加速, 小键盘3减速", 10000);
		}

		protected override void OnUpdate()
		{
			if (DOES_ENTITY_EXIST(PlayerPed) && IS_PED_IN_ANY_VEHICLE(PlayerPed, true))
			{
				int veh = GET_VEHICLE_PED_IS_USING(PlayerPed);

				bool bUp = Input.IsKeyDown(KeyCode.Num9);
				bool bDown = Input.IsKeyDown(KeyCode.Num3);

				if (bUp || bDown)
				{
					float speed = GET_ENTITY_SPEED(veh);
					if (bUp)
					{
						if (speed < 4.8f)
						{
							speed = 4.8f;
						}
						speed += speed * 0.05f;
						SET_VEHICLE_FORWARD_SPEED(veh, speed);
						return;
					}
					if (IS_ENTITY_IN_AIR(veh) || speed > 5.0f)
					{
						SET_VEHICLE_FORWARD_SPEED(veh, 0.0f);
					}
				}
			}
		}
	}
}
