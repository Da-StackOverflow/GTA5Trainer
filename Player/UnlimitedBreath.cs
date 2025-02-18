using ScriptUI;
using static ScriptUI.Functions;

namespace Player
{
	internal sealed class UnlimitedBreath : UpdateableItem
	{
		public UnlimitedBreath(string caption) : base(caption)
		{
		}

		protected override void OnUpdate()
		{
			SET_PLAYER_UNDERWATER_BREATH_PERCENT_REMAINING(PlayerID, 100.0f);
		}
	}
}
