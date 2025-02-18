using ScriptUI;
using static ScriptUI.Functions;

namespace Player
{
	internal sealed class SuperJump : UpdateableItem
	{
		public SuperJump(string caption) : base(caption)
		{
		}

		protected override void OnUpdate()
		{
			if (DOES_ENTITY_EXIST(PlayerPed))
			{
				SET_SUPER_JUMP_THIS_FRAME(PlayerID);
			}
		}
	}
}
