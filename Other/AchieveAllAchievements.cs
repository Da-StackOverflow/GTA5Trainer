using ScriptUI;
using static ScriptUI.Functions;

namespace Other
{
	internal class AchieveAllAchievements : TriggerItem
	{
		public AchieveAllAchievements(string caption) : base(caption)
		{
		}

		protected override void OnExecute()
		{
			for (int i = 1; i <= 77; i++)
			{
				if (!HAS_ACHIEVEMENT_BEEN_PASSED(i))
				{
					GIVE_ACHIEVEMENT_TO_PLAYER(i);
				}
			}
		}
	}
}
