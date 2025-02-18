using ScriptUI;
using static ScriptUI.Functions;

namespace Weather
{
	internal sealed class StandChangedWeather : UpdateableItem
	{
		private string _preWeather;
		public StandChangedWeather(string caption) : base(caption)
		{
			_preWeather = "";
		}

		protected override void OnUpdate()
		{
			var nowWeather = GlobalValue.GetStringValue("Weather.Weather.CurrentWeather");
			if (_preWeather != nowWeather)
			{
				_preWeather = nowWeather;
				SET_OVERRIDE_WEATHER(_preWeather);
			}
		}

		protected override void OnInactive()
		{
			GlobalValue.DeleteStringValue("Weather.Weather.CurrentWeather");
			_preWeather = "";
		}
	}
}
