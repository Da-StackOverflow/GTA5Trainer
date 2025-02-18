using ScriptUI;
using static ScriptUI.Functions;

namespace Weather
{
	internal sealed class SetWind : SwitchItem
	{
		public SetWind(string caption) : base(caption)
		{
		}

		protected override void OnActive()
		{
			SET_WIND(1.0f);
			SET_WIND_SPEED(11.99f);
			SET_WIND_DIRECTION(GET_ENTITY_HEADING(PlayerPed));
		}

		protected override void OnInactive()
		{
			SET_WIND(0.0f);
			SET_WIND_SPEED(0.0f);
		}
	}
}
