using ScriptUI;
using static ScriptUI.Functions;

namespace Player
{
	internal sealed class Teleport : TriggerItem
	{
		private Vector3 _coords;
		public Teleport(string caption, float x, float y, float z) : base(caption)
		{
			_coords = new Vector3(x, y, z);
		}

		protected override void OnExecute()
		{
			int e = PlayerPed;
			if (IS_PED_IN_ANY_VEHICLE(e, true))
			{
				e = GET_VEHICLE_PED_IS_USING(e);
			}

			SET_ENTITY_COORDS_NO_OFFSET(e, _coords.X, _coords.Y, _coords.Z, false, false, true);
			Wait(0);
			SetTips("成功传送");
		}
	}
}
