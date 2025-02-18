using ScriptUI;
using static ScriptUI.Functions;

namespace Player
{
	internal sealed class EveryOneIgnorePlayer : UpdateableItem
	{
		private int _lastPlayerPed = -1;
		public EveryOneIgnorePlayer(string caption) : base(caption)
		{
		}

		protected override void OnActive()
		{
			if (DOES_ENTITY_EXIST(PlayerPed))
			{
				SET_EVERYONE_IGNORE_PLAYER(PlayerID, true);
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
				SET_EVERYONE_IGNORE_PLAYER(PlayerID, true);
				_lastPlayerPed = PlayerPed;
			}
		}

		protected override void OnInactive()
		{
			if (DOES_ENTITY_EXIST(PlayerPed))
			{
				SET_EVERYONE_IGNORE_PLAYER(PlayerID, false);
			}
			_lastPlayerPed = -1;
		}
	}
}
