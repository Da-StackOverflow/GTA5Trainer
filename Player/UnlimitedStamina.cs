using Bridge;
using static Bridge.Functions;

namespace Player
{
	internal sealed class UnlimitedStamina : UpdateableItem
	{
		public UnlimitedStamina(string caption) : base(caption)
		{
		}

		protected override void OnUpdate()
		{
			RESTORE_PLAYER_STAMINA(PlayerID, 100.0f);
		}
	}
}
