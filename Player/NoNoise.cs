using ScriptUI;
using static ScriptUI.Functions;

namespace Player
{
	internal sealed class NoNoise : UpdateableItem
	{
		private int _lastPlayerPed = -1;
		public NoNoise(string caption) : base(caption)
		{
		}

		protected override void OnActive()
		{
			if (DOES_ENTITY_EXIST(PlayerPed))
			{
				SET_PLAYER_NOISE_MULTIPLIER(PlayerID, 0.0f);
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
				SET_PLAYER_NOISE_MULTIPLIER(PlayerID, 0.0f);
				_lastPlayerPed = PlayerPed;
			}
		}

		protected override void OnInactive()
		{
			if (DOES_ENTITY_EXIST(PlayerPed))
			{
				SET_PLAYER_NOISE_MULTIPLIER(PlayerID, 1.0f);
			}
			_lastPlayerPed = -1;
		}
	}
}
