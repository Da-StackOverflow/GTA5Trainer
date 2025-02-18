using ScriptUI;
using static ScriptUI.Functions;

namespace Other
{
	internal sealed class MoonGravity : SwitchItem
	{
		public MoonGravity(string caption) : base(caption)
		{
		}

		protected override
		void OnActive()
		{
			SET_GRAVITY_LEVEL(2);
		}

		protected override void OnInactive()
		{
			SET_GRAVITY_LEVEL(0);
		}
	}
}
