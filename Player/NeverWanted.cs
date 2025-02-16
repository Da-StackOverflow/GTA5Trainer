using Bridge;
using static Bridge.Functions;

namespace Player
{
	internal sealed class NeverWanted : UpdateableItem
	{
		public NeverWanted(string caption) : base(caption)
		{
		}

		protected override void OnUpdate()
		{
			if (DOES_ENTITY_EXIST(PlayerPed))
			{
				CLEAR_PLAYER_WANTED_LEVEL(PlayerID);
			}
		}
	}
}
