export module ItemInfo;

import "Base.h";

export struct ItemInfo
{
public:
	WString Caption;
	String Model;

	constexpr ItemInfo(WString caption, String model) : Model(model), Caption(caption)
	{
	}

	constexpr ItemInfo() : Model(""), Caption(L"")
	{
	}

};