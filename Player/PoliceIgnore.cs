using Bridge;
using static Bridge.Functions;

namespace Player
{
	internal sealed class PoliceIgnore : UpdateableItem
	{
		private int _lastPlayerPed = -1;
		public PoliceIgnore(string caption) : base(caption)
		{
		}

		protected override void OnActive()
		{
			if (DOES_ENTITY_EXIST(PlayerPed))
			{
				SET_POLICE_IGNORE_PLAYER(PlayerID, true);
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
				SET_POLICE_IGNORE_PLAYER(PlayerID, true);
				_lastPlayerPed = PlayerPed;
			}
		}

		protected override void OnInactive()
		{
			if (DOES_ENTITY_EXIST(PlayerPed))
			{
				SET_POLICE_IGNORE_PLAYER(PlayerID, false);
			}
			_lastPlayerPed = -1;
		}
	}
}
