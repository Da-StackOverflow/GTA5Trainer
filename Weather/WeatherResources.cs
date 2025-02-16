using Bridge;

namespace Weather
{
	internal static class WeatherResources
	{
		public static readonly ItemInfo[] WeatherInfos = [
			new ("阳光明媚", "EXTRASUNNY"),
			new ("晴天", "CLEAR"),
			new ("多云", "CLOUDS"),
			new ("起雾", "SMOG"),
			new ("大雾", "FOGGY"),
			new ("阴天", "OVERCAST"),
			new ("下雨", "RAIN"),
			new ("打雷", "THUNDER"),
			new ("清楚的", "CLEARING"),
			new ("气候中性", "NEUTRAL"),
			new ("下雪", "SNOW"),
			new ("暴雪", "BLIZZARD"),
			new ("小雪", "SNOWLIGHT"),
			new ("XMAS(圣诞节)", "XMAS"),
		];
	}
}
