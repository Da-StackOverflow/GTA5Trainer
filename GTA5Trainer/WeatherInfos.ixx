export module WeatherInfos;

import ItemInfo;

export constexpr ItemInfo WeatherInfos[] = {
	ItemInfo(L"阳光明媚", "EXTRASUNNY"),
	ItemInfo(L"晴天", "CLEAR"),
	ItemInfo(L"多云", "CLOUDS"),
	ItemInfo(L"起雾", "SMOG"),
	ItemInfo(L"大雾", "FOGGY"),
	ItemInfo(L"阴天", "OVERCAST"),
	ItemInfo(L"下雨", "RAIN"),
	ItemInfo(L"打雷", "THUNDER"),
	ItemInfo(L"清楚的", "CLEARING"),
	ItemInfo(L"气候中性", "NEUTRAL"),
	ItemInfo(L"下雪", "SNOW"),
	ItemInfo(L"暴雪", "BLIZZARD"),
	ItemInfo(L"小雪", "SNOWLIGHT"),
	ItemInfo(L"XMAS(圣诞节)", "XMAS"), 
};