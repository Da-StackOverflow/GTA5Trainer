using ScriptUI;
using static ScriptUI.Functions;

namespace Player
{
	internal sealed class GetTeleportCurrentCords : TriggerItem
	{
		public GetTeleportCurrentCords(string caption) : base(caption)
		{
		}

		protected override void OnExecute()
		{
			int e = PlayerPed;
			if (IS_PED_IN_ANY_VEHICLE(e, true))
			{
				e = GET_VEHICLE_PED_IS_USING(e);
			}
			Vector3 coords = GET_ENTITY_COORDS(e, true);
			SetTips(coords.ToString(), 10000);
			Log.Info($"GetTeleportCurrentCords {coords}");
		}
	}
}
