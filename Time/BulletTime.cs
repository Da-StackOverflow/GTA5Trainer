using ScriptUI;
using static ScriptUI.Functions;

namespace Time
{
	internal sealed partial class BulletTime : UpdateableItem
	{
		public BulletTime(string title, float timeScale) : base(title)
		{
			_timeScale = timeScale;
		}

		private readonly float _timeScale;

		protected override void OnActive()
		{
			SET_TIME_SCALE(_timeScale);
			GlobalValue.SetFloatValue("Time.BulletTime", _timeScale);
		}

		protected override void OnInactive()
		{
			SET_TIME_SCALE(1.0f);
			GlobalValue.DeleteFloatValue("Time.BulletTime");
		}

		protected override void OnUpdate()
		{
			var scale = GlobalValue.GetFloatValue("Time.BulletTime", 1.0f);
			if(scale != _timeScale)
			{
				IsActive = false;
			}
		}
	}
}
