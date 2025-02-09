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
	constexpr MenuItem(WString text, float width, float height, const Color& bgColor, const Color& textColor, float scale) : Width(width / 1920.0f), Height(height / 1080.0f), BgColor(bgColor), TextColor(textColor), Text(text), Scale(scale), Position(Vector2(width / 1920.0f / 2.0f, height / 1080.0f / 2.0f)), TextPosition(Vector2(0.01f, height / 1080.0f / 8.0f))
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
	float Scale;

	float Width;
	float Height;

	Color BgColor;
	Color TextColor;
	WString Text;

	void WaitAndDraw(i64 ms);

	void SetTips(WString text, int ms = 3000);
	void SetTips(String text, int ms = 3000);

	virtual void OnDraw(bool active = false)
	{
		PaintText(Text, TextPosition, Scale, TextColor);
		PaintBG(Position, Width, Height, BgColor);
	}
};

export class Caption : public MenuItem
{
public:
	int CurrentPage;
	int MaxPage;
	char Buffer[32];
	constexpr Caption(WString text) : MenuItem(text, 400, 80, Blue, White, 0.4f), CurrentPage(1), MaxPage(1)
	{
	}

	void OnDraw(bool active = false) override
	{
		MenuItem::OnDraw(active);
		if (MaxPage > 1)
		{
			sprintf_s(Buffer, 32, "%02d/%02d", CurrentPage, MaxPage);
			PaintText(Buffer, TextPosition.x + 0.16f, TextPosition.y, Scale, TextColor);
		}
	}
};

export class ExcuteableItem : public MenuItem
{
public:
	Color BgActiveColor;
	Color TextActiveColor;

	constexpr ExcuteableItem(WString text, const Color& bgColor, const Color& textColor, const Color& bgActiveColor, const Color& textActiveColor) : MenuItem(text, 400, 40, bgColor, textColor, 0.3f), BgActiveColor(bgActiveColor), TextActiveColor(textActiveColor)
	{
	}

	virtual void OnExecute()
	{
	}

	void OnDraw(bool active = false) override
	{
		if (active)
		{
			PaintText(Text, TextPosition, Scale, TextActiveColor);
			PaintBG(Position, Width, Height, BgActiveColor);
		}
		else
		{
			PaintText(Text, TextPosition, Scale, TextColor);
			PaintBG(Position, Width, Height, BgColor);
		}
	}
};

export class TriggerItem : public ExcuteableItem
{
public:
	constexpr TriggerItem(WString text) : ExcuteableItem(text, White, White, Gold, White)
	{

	}
};

export class SwitchItem : public ExcuteableItem
{
private:
	WString ActiveString = L"[已开启]";
	WString InactiveString = L"[已关闭]";

public:

	constexpr SwitchItem(WString text) : ExcuteableItem(text, White, White, Gold, White), StateActiveColor(Green), StateInactiveColor(White)
	{

	}

	bool State;

	Color StateActiveColor;

	Color StateInactiveColor;

	Vector2 ActiveTextPosition;

	void OnDraw(bool active = false) override
	{
		ExcuteableItem::OnDraw(active);
		if (active)
		{

			if (State)
			{
				PaintText(ActiveString, ActiveTextPosition, Scale, TextActiveColor);
			}
			else
			{
				PaintText(InactiveString, ActiveTextPosition, Scale, TextActiveColor);
			}
		}
		else
		{
			if (State)
			{
				PaintText(ActiveString, ActiveTextPosition, Scale, StateActiveColor);
			}
			else
			{
				PaintText(InactiveString, ActiveTextPosition, Scale, StateInactiveColor);
			}
		}
	}

	void OnExecute() override
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
	virtual void OnActive()
	{

	}

	virtual void OnInactive()
	{

	}

	virtual void OnUpdate()
	{

	}
};

export class Menu
{
private:
	std::vector<ExcuteableItem*> _items;
	std::vector<SwitchItem*> _switchItems;
	int _activeItemInActivePage;
	int _activePage;
	int _itemCount;
	int _switchItemCount;

	static const int ItemsMaxCountPerPage = 15;
public:
	WString Text;

	constexpr Menu() : Text(L""), Title(L""), _activeItemInActivePage(0), _activePage(0), _itemCount(0), _switchItemCount(0)
	{
		
	}

	constexpr Menu(WString caption) : Text(caption), Title(caption), _activeItemInActivePage(0), _activePage(0), _itemCount(0), _switchItemCount(0)
	{
		
	}

	~Menu()
	{
		_switchItems.clear();
		for (var item : _items)
		{
			delete item;
		}
		_items.clear();
		Log(Text, " Release");
	}

	Caption Title;


	void AddItem(TriggerItem* item)
	{
		int index = _items.size() % ItemsMaxCountPerPage;
		item->Position = Vector2(item->Width / 2.0f, item->Height * index + Title.Height);
		item->TextPosition = Vector2(0.01f, item->Position.y - item->Height / 3.0f);
		item->BgColor = (_items.size() & 1) == 0 ? Green : Lime;
		_items.push_back(item);
		_itemCount++;
		Title.MaxPage = _itemCount / ItemsMaxCountPerPage + (_itemCount % ItemsMaxCountPerPage != 0 ? 1 : 0);
	}

	void AddItem(SubMenu* item);

	void AddItem(SwitchItem* item)
	{
		int index = _items.size() % ItemsMaxCountPerPage;
		item->Position = Vector2(item->Width / 2.0f, item->Height * index + Title.Height);
		item->TextPosition = Vector2(0.01f, item->Position.y - item->Height / 3.0f);
		item->BgColor = (_items.size() & 1) == 0 ? Green : Lime;
		item->ActiveTextPosition = Vector2(0.17f, item->TextPosition.y);
		_items.push_back(item);
		_switchItems.push_back(item);
		_itemCount++;
		_switchItemCount++;
		Title.MaxPage = _itemCount / ItemsMaxCountPerPage + (_itemCount % ItemsMaxCountPerPage != 0 ? 1 : 0);
	}

	int ActiveItemIndex() const
	{
		return _activePage * ItemsMaxCountPerPage + _activeItemInActivePage;
	}

	void OnDraw()
	{
		Title.OnDraw();
		var count = (int)_items.size();
		var maxEnd = (_activePage + 1) * ItemsMaxCountPerPage;
		int end = count > maxEnd ? maxEnd : count;
		int index = 0;
		for (int i = _activePage * ItemsMaxCountPerPage; i < end; i++)
		{
			_items[i]->OnDraw(_activeItemInActivePage == index);
			index++;
		}
	}

	void OnSwitchItemUpdate()
	{
		for (int i = 0; i < _switchItemCount; i++)
		{
			_switchItems[i]->Update();
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
				_items[ActiveItemIndex()]->OnExecute();
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
				Title.CurrentPage = _activePage + 1;
				break;
			case KeyCode::Right:
				_activePage = (_activePage + 1) % pageCount;
				_activeItemInActivePage = 0;
				Title.CurrentPage = _activePage + 1;
				break;
			case KeyCode::Back:
				break;
		}
	}
};


export class SubMenu : public ExcuteableItem
{
private:
	WString Caption2 = L">>";
public:
	Menu* menu;
	Vector2 Caption2Position;

	SubMenu(WString text, Menu* m) : ExcuteableItem(text, White, White, Gold, White), menu(m)
	{
		
	}
	void OnExecute() override;

	void OnDraw(bool active = false) override
	{
		ExcuteableItem::OnDraw(active);
		if (active)
		{
			PaintText(Caption2, Caption2Position, Scale, TextActiveColor);
		}
		else
		{
			PaintText(Caption2, Caption2Position, Scale, TextActiveColor);
		}
	}
};

export class MenuController
{
public:
	void Init();

	void Update();

	MenuController() : _nextCanInputTime(0), _statusTextMaxTicks(0), _statusText(L"")
	{
		Log("MenuController Init");
	}

	~MenuController()
	{
		while (!_menuStack.empty())
		{
			_menuStack.pop();
		}

		for (var& menu : _menuList)
		{
			delete menu.second;
		}
		_menuList.clear();
		Log("MenuController Release");
	}

	inline void PushMenu(Menu* menu)
	{
		_menuStack.push(menu);
	}

	inline void PopMenu()
	{
		_menuStack.pop();
	}

	inline void Register(Menu* menu)
	{
		_menuList.insert_or_assign(menu->Text, menu);
	}

	inline bool IsMenuExist(WString id)
	{
		return _menuList.find(id) != _menuList.end();
	}

	Menu* GetMenu(WString id)
	{
		return _menuList[id];
	}

	inline Menu* GetActiveMenu()
	{
		return _menuStack.top();
	}

	inline bool HaveActiveMenu()
	{
		return !_menuStack.empty();
	}

	void SetTips(WString text, i64 ms = 3000)
	{
		_statusText = text;
		_statusTextMaxTicks = GetTimeTicks() + ms;
	}

	void SetTips(String text, i64 ms = 3000)
	{
		_statusText2 = text;
		_statusTextMaxTicks2 = GetTimeTicks() + ms;
	}

	int ExcuteInput(Menu* menu)
	{
		if (Input.IsAccept())
		{
			menu->OnInput(KeyCode::Return);
			return 150;
		}
		else if (Input.IsBack())
		{
			PopMenu();
			return 200;
		}
		else if (Input.IsUp())
		{
			menu->OnInput(KeyCode::Up);
			return 150;
		}
		else if (Input.IsDown())
		{
			menu->OnInput(KeyCode::Down);
			return 150;
		}
		else if (Input.IsLeft())
		{
			menu->OnInput(KeyCode::Left);
			return 150;
		}
		else if (Input.IsRight())
		{
			menu->OnInput(KeyCode::Right);
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
			ThreadSleep(1);
			waited = true;
			OnDraw();
		}
	}
private:
	std::stack<Menu*> _menuStack;
	std::unordered_map<WString, Menu*> _menuList;
	WString _statusText;
	String _statusText2;
	i64 _nextCanInputTime;
	i64 _statusTextMaxTicks;
	i64 _statusTextMaxTicks2;

	void OnUpdate()
	{
		OnDraw();
		OnInput();
		OnExecuteHookFunction();
	}

	void DrawTips()
	{
		if (GetTimeTicks() < _statusTextMaxTicks)
		{
			PaintText(_statusText, 0.5f, 0.5f, 0.5f, White);
		}
	}

	void DrawTips2()
	{
		if (GetTimeTicks() < _statusTextMaxTicks2)
		{
			PaintText(_statusText2, 0.5f, 0.5f, 0.5f, White);
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
		if (HaveActiveMenu())
		{
			GetActiveMenu()->OnDraw();
		}
		DrawTips();
		DrawTips2();
	}

	void OnInput()
	{
		if (InputIsOnWait())
		{
			return;
		}
		if (HaveActiveMenu())
		{
			var waitTime = ExcuteInput(GetActiveMenu());
			InputWait(waitTime);
		}
	}

	void OnExecuteHookFunction()
	{
		for (var& menuPair : _menuList)
		{
			menuPair.second->OnSwitchItemUpdate();
		}
	}
};

export MenuController* Controller = nullptr;

void Menu::AddItem(SubMenu* item)
{
	int index = _items.size() % ItemsMaxCountPerPage;
	item->Position = Vector2(item->Width / 2.0f, item->Height * index + Title.Height);
	item->TextPosition = Vector2(0.01f, item->Position.y - item->Height / 3.0f);
	item->BgColor = (_items.size() & 1) == 0 ? Green : Lime;
	item->Caption2Position = Vector2(0.19f, item->TextPosition.y);
	_items.push_back(item);
	_itemCount++;
	Title.MaxPage = _itemCount / ItemsMaxCountPerPage + (_itemCount % ItemsMaxCountPerPage != 0 ? 1 : 0);
}

void MenuItem::WaitAndDraw(i64 ms)
{
	Controller->WaitAndDraw(ms);
}

void MenuItem::SetTips(WString text, int ms)
{
	Controller->SetTips(text, ms);
}

void MenuItem::SetTips(String text, int ms)
{
	Controller->SetTips(text, ms);
}

void SubMenu::OnExecute()
{
	Controller->PushMenu(menu);
}