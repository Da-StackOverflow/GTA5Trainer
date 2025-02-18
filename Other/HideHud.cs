using ScriptUI;
using static ScriptUI.Functions;

namespace Other
{
	internal sealed class HideHud : UpdateableItem
	{
		public HideHud(string caption) : base(caption)
		{
		}

		protected override void OnUpdate()
		{
			HIDE_HUD_AND_RADAR_THIS_FRAME();
		}
	}
}
