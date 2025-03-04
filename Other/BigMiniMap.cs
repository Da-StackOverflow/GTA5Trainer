using ScriptUI;
using static ScriptUI.Functions;

namespace Other
{
	internal sealed partial class BigMiniMap : SwitchItem
	{
		public BigMiniMap(string title) : base(title)
		{
		}

		protected override void OnActive()
		{
			SET_BIGMAP_ACTIVE(true, false);
		}

		protected override void OnInactive()
		{
			SET_BIGMAP_ACTIVE(false, false);
		}
	}
}
