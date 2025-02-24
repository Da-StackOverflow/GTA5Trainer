﻿using ScriptUI;
using static ScriptUI.Functions;

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
			for (int i = 1; i <= 46; i++)
			{
				SUPPRESS_CRIME_THIS_FRAME(PlayerID, i);
			}
		}
	}
}
