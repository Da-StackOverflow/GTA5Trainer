using Bridge;
using static Bridge.Functions;

namespace Player
{
	internal sealed class PlayerInvincible : UpdateableItem
	{
		private int _lastPlayerPed = -1;
		public PlayerInvincible(string caption) : base(caption)
		{
		}

		protected override void OnActive()
		{
			if (DOES_ENTITY_EXIST(PlayerPed))
			{
				SET_PLAYER_INVINCIBLE(PlayerID, true);
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
				SET_PLAYER_INVINCIBLE(PlayerID, true);
				_lastPlayerPed = PlayerPed;
			}
		}

		protected override void OnInactive()
		{
			if (DOES_ENTITY_EXIST(PlayerPed))
			{
				SET_PLAYER_INVINCIBLE(PlayerID, false);
			}
			_lastPlayerPed = -1;
		}
	}

}
