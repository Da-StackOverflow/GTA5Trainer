using ScriptUI;
using static ScriptUI.Functions;

namespace Other
{
	internal sealed class NextRadioTrack : TriggerItem
	{
		public NextRadioTrack(string caption) : base(caption)
		{
		}

		protected override void OnExecute()
		{
			if (DOES_ENTITY_EXIST(PlayerPed) && IS_PED_IN_ANY_VEHICLE(PlayerPed, false))
			{
				SKIP_RADIO_FORWARD();
			}
		}
	}
}
