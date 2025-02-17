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
			RESET_PLAYER_STAMINA(PlayerID);
		}
	}
}
