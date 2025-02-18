using Bridge;

namespace ScriptUI
{
	public abstract class AScriptEntry : AEntry
	{
		protected MenuController _controller;
		protected sealed override void OnInit(AController controller)
		{
			_controller = (MenuController)controller;
			OnInit();
		}

		protected abstract void OnInit();
	}
}
