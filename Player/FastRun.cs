using ScriptUI;
using static ScriptUI.Functions;

namespace Player
{
	internal sealed class FastRun : UpdateableItem
	{
		private int _lastPlayerPed = -1;
		public FastRun(string caption) : base(caption)
		{
		}

		protected override void OnActive()
		{
			if (DOES_ENTITY_EXIST(PlayerPed))
			{
				SET_RUN_SPRINT_MULTIPLIER_FOR_PLAYER(PlayerID, 1.49f);
				_lastPlayerPed = PlayerPed;
			}
		}

		protected override void OnUpdate()
		{
			if (_lastPlayerPed == PlayerPed)
			{
				return;
			}
			if (DOES_ENTITY_EXIST(PlayerPed))
			{
				SET_RUN_SPRINT_MULTIPLIER_FOR_PLAYER(PlayerID, 1.49f);
				_lastPlayerPed = PlayerPed;
			}
		}

		protected override void OnInactive()
		{
			if (DOES_ENTITY_EXIST(PlayerPed))
			{
				SET_RUN_SPRINT_MULTIPLIER_FOR_PLAYER(PlayerID, 1.0f);
			}
			_lastPlayerPed = -1;
		}
	}
}
