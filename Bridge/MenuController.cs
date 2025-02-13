

using System;
using System.Collections.Generic;

namespace Bridge
{
	public sealed class MenuController
	{
		private readonly Stack<Menu> _menuStack = [];
		private readonly Dictionary<string, Menu> _menuList = [];
		private string _statusText;
		private long _nextCanInputTime;
		private long _statusTextMaxTicks;

		private static readonly MenuController _instance = new();
		public static MenuController Instance { get => _instance; }

		private MenuController()
		{
			_nextCanInputTime = 0;
			_statusTextMaxTicks = 0;
			_statusText = "";
		}

		public void Update()
		{
			OnDraw();
			OnInput();
			OnExecuteHookFunction();
		}

		public void PushMenu(Menu menu)
		{
			if (menu is not null)
			{
				_menuStack.Push(menu);
			}
		}

		public void PopMenu()
		{
			_menuStack.Pop();
		}

		public void Register(Menu menu)
		{
			if (_menuList.ContainsKey(menu.Caption.Text))
			{
				Log.Error($"Menu {menu.Caption.Text} already exists!");
			}
			_menuList[menu.Caption.Text] = menu;
		}

		public Menu GetMenu(string caption)
		{
			if (_menuList.TryGetValue(caption, out var menu))
			{
				return menu;
			}
			return null;
		}

		public Menu GetActiveMenu()
		{
			return _menuStack.Count > 0 ? _menuStack.Peek() : null;
		}

		public void SetTips(string text, long ms)
		{
			_statusText = text;
			_statusTextMaxTicks = Time.Now + ms;
		}

		private void DrawTips()
		{
			if (Time.Now < _statusTextMaxTicks)
			{
				//DrawText(_statusText, 0.5f, 0.5f, 0.5f, 0.5f, Color.White);
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
			GetActiveMenu()?.OnDraw();
			DrawTips();
		}

		private void OnInput()
		{
			if (InputIsOnWait())
			{
				return;
			}
			var menu = GetActiveMenu();
			if (menu is not null)
			{
				var waitTime = ExcuteInput(menu);
				InputWait(waitTime);
			}
		}

		private int ExcuteInput(Menu menu)
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
				menu.OnSwitchItemUpdate();
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
