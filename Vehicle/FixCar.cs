using ScriptUI;
using static ScriptUI.Functions;

namespace Vehicle
{
	internal sealed class FixCar : TriggerItem
	{
		public FixCar(string caption) : base(caption)
		{
		}

		protected override void OnExecute()
		{
			if (IS_PED_IN_ANY_VEHICLE(PlayerPed, true))
			{
				SET_VEHICLE_FIXED(GET_VEHICLE_PED_IS_USING(PlayerPed));
				return;
			}
			SetTips("请先进入车辆");
		}
	}
}
