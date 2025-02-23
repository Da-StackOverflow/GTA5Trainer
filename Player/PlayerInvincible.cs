using ScriptUI;
using static ScriptUI.Functions;

namespace Player
{
	internal sealed class PlayerInvincible : UpdateableItem
	{
		public PlayerInvincible(string caption) : base(caption)
		{
		}

		protected override void OnActive()
		{
			if (DOES_ENTITY_EXIST(PlayerPed))
			{
				SET_PLAYER_INVINCIBLE(PlayerID, true);
			}
		}

		protected override void OnUpdate()
		{
			if (DOES_ENTITY_EXIST(PlayerPed))
			{
				SET_PLAYER_INVINCIBLE(PlayerID, true);
			}
		}

		protected override void OnInactive()
		{
			if (DOES_ENTITY_EXIST(PlayerPed))
			{
				SET_PLAYER_INVINCIBLE(PlayerID, false);
			}
		}
	}

}
