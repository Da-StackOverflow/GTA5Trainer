export module Menu;

import Function;
import Color;
import Vector;
import Util;
import InputSystem;
import KeyCode;
import "Import.h";
import "Base.h";
import <vector>;
import <stack>;
import <unordered_map>;

class SubMenu;

export class MenuItem
{
public:
	constexpr MenuItem(const char* text, float width, float height, const Color& bgColor, const Color& textColor) noexcept : Width(width / 1920.0f), Height(height / 1080.0f), BgColor(bgColor), TextColor(textColor), Text(text), Scale(Vector2(0.0f, height / 1080.0f * 8.0f))
	{

	}

	inline int PlayerPed()
	{
		return PLAYER::PLAYER_PED_ID();
	}

	inline int PlayerID()
	{
		return PLAYER::PLAYER_ID();
	}

	Vector2 Position;
	Vector2 TextPosition;
	Vector2 Scale;

	float Width;
	float Height;

	Color BgColor;
	Color TextColor;
	const char* Text;

	void WaitAndDraw(i64 ms);

	void SetTips(const char* text, int ms = 2500);

	virtual void OnDraw(bool active = false) noexcept
	{
		DrawString(Text, TextPosition, Scale, TextColor);
		DrawBG(Position, Width, Height, BgColor);
	}
};

export class Caption : public MenuItem
{
public:
	constexpr Caption(const char* text) noexcept : MenuItem(text, 200, 60, Cyan, Blue)
	{
	}
};

export class ExcuteableItem : public MenuItem
{
public:
	Color BgActiveColor;
	Color TextActiveColor;

	constexpr ExcuteableItem(const char* text, float width, float height, const Color& bgColor, const Color& textColor, const Color& bgActiveColor, const Color& textActiveColor) noexcept : MenuItem(text, width, height, bgColor, textColor), BgActiveColor(bgActiveColor), TextActiveColor(textActiveColor)
	{
	}

	virtual void OnExecute() noexcept
	{
	}
};

export class TriggerItem : public ExcuteableItem
{
public:
	constexpr TriggerItem(const char* text) noexcept : ExcuteableItem(text, 200, 40, White, Blue, Green, Red)
	{

	}
};

export class SwitchItem : public ExcuteableItem
{
private:
	const char* ActiveString = "[已开启]";
	const char* InactiveString = "[已关闭]";

public:

	constexpr SwitchItem(const char* text) noexcept : ExcuteableItem(text, 200, 40, White, Blue, Green, Red)
	{

	}

	bool State;

	Color StateActiveColor;

	Color StateInactiveColor;

	Vector2 ActiveTextPosition;

	void OnDraw(bool active = false) noexcept override
	{
		ExcuteableItem::OnDraw(active);
		if (active)
		{

			if (State)
			{
				DrawString(ActiveString, ActiveTextPosition, Scale, TextActiveColor);
			}
			else
			{
				DrawString(InactiveString, ActiveTextPosition, Scale, TextActiveColor);
			}
		}
		else
		{
			if (State)
			{
				DrawString(ActiveString, ActiveTextPosition, Scale, StateActiveColor);
			}
			else
			{
				DrawString(InactiveString, ActiveTextPosition, Scale, StateInactiveColor);
			}
		}
	}

	void OnExecute() noexcept override
	{
		State = !State;
		if (State)
		{
			OnActive();
			OnUpdate();
		}
		else
		{
			OnInactive();
		}
	}

	void Update()
	{
		if (State)
		{
			OnUpdate();
		}
	}

protected:
	virtual void OnActive() noexcept
	{

	}

	virtual void OnInactive() noexcept
	{

	}

	virtual void OnUpdate() noexcept
	{

	}
};

export class Menu
{
private:
	std::vector<ExcuteableItem> _items;
	std::vector<SwitchItem> _switchItems;
	int _activeItemInActivePage;
	int _activePage;
	int _itemCount;
	int _switchItemCount;

	static const int ItemsMaxCountPerPage = 15;
public:
	const char* Text;

	constexpr Menu() noexcept : Text(""), Tittle(""), _items(std::vector<ExcuteableItem>()), _switchItems(std::vector<SwitchItem>()), _activeItemInActivePage(0), _activePage(0), _itemCount(0), _switchItemCount(0)
	{

	}

	constexpr Menu(const char* tittle) noexcept : Text(tittle), Tittle(tittle), _items(std::vector<ExcuteableItem>()), _switchItems(std::vector<SwitchItem>()), _activeItemInActivePage(0), _activePage(0), _itemCount(0), _switchItemCount(0)
	{

	}

	Caption Tittle;


	void AddItem(TriggerItem& item)
	{
		int index = _items.size() % ItemsMaxCountPerPage;
		item.Position = Vector2(item.Width / 2.0f, item.Height * index + 0.08f);
		item.TextPosition = Vector2(0.01f, item.Position.y - item.Height / 4.0f);
		item.BgColor = (_items.size() & 1) == 0 ? White : Yellow;
		_items.push_back(item);
		_itemCount++;
	}

	void AddItem(SubMenu& item);

	void AddItem(SwitchItem& item)
	{
		int index = _items.size() % ItemsMaxCountPerPage;
		item.Position = Vector2(item.Width / 2.0f, item.Height * index + 0.08f);
		item.TextPosition = Vector2(0.01f, item.Position.y - item.Height / 4.0f);
		item.BgColor = (_items.size() & 1) == 0 ? White : Yellow;
		item.ActiveTextPosition = Vector2(0.15f, item.TextPosition.y);
		_items.push_back(item);
		_switchItems.push_back(item);
		_itemCount++;
		_switchItemCount++;
	}

	int ActiveItemIndex() const
	{
		return _activePage * ItemsMaxCountPerPage + _activeItemInActivePage;
	}

	void OnDraw()
	{
		Tittle.OnDraw();
		var count = (int)_items.size();
		var maxEnd = (_activePage + 1) * ItemsMaxCountPerPage;
		int end = count > maxEnd ? maxEnd : count;
		int index = 0;
		for (int i = _activePage * ItemsMaxCountPerPage; i < end; i++)
		{
			_items[i].OnDraw(_activeItemInActivePage == index);
			index++;
		}
	}

	void OnSwitchItemUpdate()
	{
		for (int i = 0; i < _switchItemCount; i++)
		{
			_switchItems[i].Update();
		}
	}

	void OnInput(KeyCode key)
	{
		int itemsLeft = _itemCount % ItemsMaxCountPerPage;
		int pageCount = _itemCount / ItemsMaxCountPerPage + (itemsLeft != 0 ? 1 : 0);
		int lineCountLastPage = itemsLeft > 0 ? itemsLeft : ItemsMaxCountPerPage;

		switch (key)
		{
			case KeyCode::Return:
				_items[ActiveItemIndex()].OnExecute();
				break;
			case KeyCode::Up:
				if (_activePage != pageCount - 1)
				{
					_activeItemInActivePage = (_activeItemInActivePage + ItemsMaxCountPerPage - 1) % ItemsMaxCountPerPage;
				}
				else
				{
					_activeItemInActivePage = (_activeItemInActivePage + lineCountLastPage - 1) % lineCountLastPage;
				}
				break;
			case KeyCode::Down:
				if (_activePage != pageCount - 1)
				{
					_activeItemInActivePage = (_activeItemInActivePage + 1) % ItemsMaxCountPerPage;
				}
				else
				{
					_activeItemInActivePage = (_activeItemInActivePage + 1) % lineCountLastPage;
				}
				break;
			case KeyCode::Left:
				_activePage = (_activePage + pageCount - 1) % pageCount;
				_activeItemInActivePage = 0;
				break;
			case KeyCode::Right:
				_activePage = (_activePage + 1) % pageCount;
				_activeItemInActivePage = 0;
				break;
			case KeyCode::Back:
				break;
		}
	}
};


export class SubMenu : public ExcuteableItem
{
public:
	Menu menu;
	SubMenu(const char* text, Menu& m) : ExcuteableItem(text, 200, 40, White, Blue, Green, Red), menu(m)
	{
		
	}

	void OnDraw(bool active = false) noexcept override
	{
		if (active)
		{
			DrawString(Text, TextPosition, Scale, TextActiveColor);
			DrawBG(Position, Width, Height, BgActiveColor);
		}
		else
		{
			DrawString(Text, TextPosition, Scale, TextColor);
			DrawBG(Position, Width, Height, BgColor);
		}
	}
	void OnExecute() noexcept override;
};

export class MenuController
{
public:
	void Init();

	void Update()
	{
		OnDraw();
		OnInput();
		OnExecuteHookFunction();
	}
	MenuController() : _nextCanInputTime(0), _statusTextMaxTicks(0), _statusText("")
	{
	}

	void PushMenu(const Menu& menu)
	{
		_menuStack.push(menu);
	}

	void PopMenu()
	{
		_menuStack.pop();
	}

	void Register(const Menu& menu)
	{
		_menuList.insert_or_assign(menu.Text, menu);
	}

	Menu* GetMenu(const char* id)
	{
		return &_menuList[id];
	}

	Menu* GetActiveMenu()
	{
		return _menuStack.empty() ? &_menuStack.top() : null;
	}

	void SetTips(const char* text, i64 ms)
	{
		_statusText = text;
		_statusTextMaxTicks = GetTimeTicks() + ms;
	}
	int ExcuteInput(Menu& menu)
	{
		if (Input.IsAccept())
		{
			menu.OnInput(KeyCode::Return);
			return 150;
		}
		else if (Input.IsBack())
		{
			PopMenu();
			return 200;
		}
		else if (Input.IsUp())
		{
			menu.OnInput(KeyCode::Up);
			return 150;
		}
		else if (Input.IsDown())
		{
			menu.OnInput(KeyCode::Down);
			return 150;
		}
		else if (Input.IsLeft())
		{
			menu.OnInput(KeyCode::Left);
			return 150;
		}
		else if (Input.IsRight())
		{
			menu.OnInput(KeyCode::Right);
			return 150;
		}
		return 0;
	}
	void WaitAndDraw(i64 ms)
	{
		i64 time = GetTimeTicks() + ms;
		bool waited = false;
		while (GetTimeTicks() < time || !waited)
		{
			scriptWait(1);
			waited = true;
			OnDraw();
		}
	}
private:
	std::stack<Menu> _menuStack;
	std::unordered_map<const char*, Menu> _menuList;
	const char* _statusText;
	i64 _nextCanInputTime;
	i64 _statusTextMaxTicks;

	void DrawTips()
	{
		if (GetTimeTicks() < _statusTextMaxTicks)
		{
			DrawString(_statusText, 0.5f, 0.5f, 0.5f, 0.5f, White);
		}
	}

	void InputWait(i64 ms)
	{
		if (ms <= 0)
		{
			return;
		}
		_nextCanInputTime = GetTimeTicks() + ms;
	}

	bool InputIsOnWait() const
	{
		return _nextCanInputTime > GetTimeTicks();
	}

	void OnDraw()
	{
		var menu = GetActiveMenu();
		if (menu != null)
		{
			menu->OnDraw();
		}
		DrawTips();
	}

	void OnInput()
	{
		if (InputIsOnWait())
		{
			return;
		}
		var menu = GetActiveMenu();
		if (menu != null)
		{
			var waitTime = ExcuteInput(*menu);
			InputWait(waitTime);
		}
	}

	void OnExecuteHookFunction()
	{
		for (var& menuPair : _menuList)
		{
			menuPair.second.OnSwitchItemUpdate();
		}
	}
};

export MenuController Controller = MenuController();

void Menu::AddItem(SubMenu& item)
{
	int index = _items.size() % ItemsMaxCountPerPage;
	item.Position = Vector2(item.Width / 2.0f, item.Height * index + 0.08f);
	item.TextPosition = Vector2(0.01f, item.Position.y - item.Height / 4.0f);
	item.BgColor = (_items.size() & 1) == 0 ? White : Yellow;
	_items.push_back(item);
	_itemCount++;
}

void MenuItem::WaitAndDraw(i64 ms)
{
	Controller.WaitAndDraw(ms);
}

void MenuItem::SetTips(const char* text, int ms)
{
	Controller.SetTips(text, ms);
}

void SubMenu::OnExecute() noexcept
{
	Controller.PushMenu(menu);
}