using Bridge;
using static Bridge.Functions;

namespace Player
{
	internal sealed class ModifyWantedLevel : TriggerItem
	{
		private readonly int _starCount;
		public ModifyWantedLevel(string caption, int starCount) : base(caption)
		{
			_starCount = starCount;
		}

		protected override void OnExecute()
		{
			if (DOES_ENTITY_EXIST(PlayerPed) && GET_PLAYER_WANTED_LEVEL(PlayerID) < 5)
			{
				var current = GET_PLAYER_WANTED_LEVEL(PlayerID);
				var wanted = current + _starCount;
				wanted = wanted < 0 ? 0 : wanted;
				wanted = wanted > 5 ? 5 : wanted;

				SET_PLAYER_WANTED_LEVEL(PlayerID, wanted, false);
				SET_PLAYER_WANTED_LEVEL_NOW(PlayerID, false);
			}
		}
	}
}
