using System.Collections.Generic;

namespace Bridge
{
	public sealed class MenuController
	{
		private readonly Stack<AMenu> _menuStack = [];
		private readonly Dictionary<string, AMenu> _menuList = [];
		private string _statusText;
		private long _nextCanInputTime;
		private long _statusTextMaxTicks;

		private static readonly MenuController _instance = new();
		public static MenuController Instance { get => _instance; }

		private readonly Menu _mainMenu;
		public Menu MainMenu => _mainMenu;

		private MenuController()
		{
			_nextCanInputTime = 0;
			_statusTextMaxTicks = 0;
			_statusText = "";
			_mainMenu = new Menu("内置修改器 by Da");
		}

		internal void Update()
		{
			OnDraw();
			OnInput();
			OnExecuteHookFunction();
		}

		internal void PushMenu(AMenu menu)
		{
			if (menu is not null)
			{
				_menuStack.Push(menu);
			}
		}

		internal void PopMenu()
		{
			_menuStack.Pop();
		}

		public void Register(AMenu menu)
		{
			if (_menuList.ContainsKey(menu.Caption.Text))
			{
				Log.Error($"Menu {menu.Caption.Text} already exists!");
			}
			_menuList[menu.Caption.Text] = menu;
		}

		public bool TryGetMenu<T>(string caption, out T menu) where T : AMenu
		{
			var result = _menuList.TryGetValue(caption, out var m);
			if (result)
			{
				menu = m as T;
			}
			else {
				menu = null;
			}
			return result;
		}

		public AMenu GetShowingMenu()
		{
			return _menuStack.Count > 0 ? _menuStack.Peek() : null;
		}

		public void SetTips(string text, long ms = 3000)
		{
			_statusText = text;
			_statusTextMaxTicks = Time.Now + ms;
		}

		private void DrawTips()
		{
			if (Time.Now < _statusTextMaxTicks)
			{
				Functions.SET_TEXT_FONT(0);
				Functions.SET_TEXT_SCALE(0.0f, 0.5f);
				Functions.SET_TEXT_COLOR(0, 0, 0, 255);
				Functions.SET_TEXT_OUTLINE();
				Functions.BEGIN_TEXT_COMMAND_DISPLAY_TEXT("STRING");
				Functions.ADD_TEXT_COMPONENT_SUBSTRING_PLAYER_NAME(_statusText);
				Functions.END_TEXT_COMMAND_DISPLAY_TEXT(0.5f, 0.5f);
			}
		}

		private void InputWait(long ms)
		{
			if (ms <= 0)
			{
				return;
			}
			_nextCanInputTime = Time.Now + ms;
		}

		private bool InputIsOnWait()
		{
			return _nextCanInputTime > Time.Now;
		}

		private void OnDraw()
		{
			GetShowingMenu()?.OnDraw();
			DrawTips();
		}

		private void OnInput()
		{
			if (InputIsOnWait())
			{
				return;
			}

			if (Input.MenuSwitchPressed())
			{
				if (_menuStack.Count == 0)
				{
					PushMenu(_mainMenu);
				}
				else
				{
					_menuStack.Clear();
				}
				InputWait(300);
				return;
			}

			var menu = GetShowingMenu();
			if (menu is not null)
			{
				var waitTime = ExcuteInput(menu);
				InputWait(waitTime);
			}
		}

		private int ExcuteInput(AMenu menu)
		{
			if (Input.IsAccept())
			{
				menu.OnInput(KeyCode.Return);
				return 150;
			}
			if (Input.IsBack())
			{
				PopMenu();
				return 200;
			}
			if (Input.IsUp())
			{
				menu.OnInput(KeyCode.Up);
				return 150;
			}
			if (Input.IsDown())
			{
				menu.OnInput(KeyCode.Down);
				return 150;
			}
			if (Input.IsLeft())
			{
				menu.OnInput(KeyCode.Left);
				return 150;
			}
			if (Input.IsRight())
			{
				menu.OnInput(KeyCode.Right);
				return 150;
			}
			return 100;
		}

		private void OnExecuteHookFunction()
		{
			foreach (var menu in _menuList.Values)
			{
				menu.Update();
			}
		}

		public void WaitAndDraw(long ms)
		{
			long time = Time.Now + ms;
			bool waited = false;
			while (Time.Now < time || !waited)
			{
				Native.Sleep(1);
				waited = true;
				OnDraw();
			}
		}
	}
}
