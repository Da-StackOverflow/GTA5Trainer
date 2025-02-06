export module ItemInfo;

import "Base.h";

export struct ItemInfo
{
public:
	String Model;
	String Caption;

	constexpr ItemInfo(String caption, String model) : Model(model), Caption(caption)
	{
	}

	constexpr ItemInfo() : Model(""), Caption("")
	{
	}

};