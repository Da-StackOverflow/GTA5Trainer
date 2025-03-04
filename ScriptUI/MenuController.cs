using Bridge;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace ScriptUI
{
	public sealed class MenuController : AController
	{
		private readonly Stack<AMenu> _menuStack = [];
		private readonly Dictionary<string, AMenu> _menuList = [];
		private readonly List<IUpdate> _updateList = [];
		private long _nextCanInputTime;
		private long _statusTextMaxTicks;
		private readonly byte[] _statusTextBytes = new byte[256];

		private static MenuController _instance;
		internal static MenuController Instance { get => _instance; }

		private readonly Menu _mainMenu;
		public Menu MainMenu => _mainMenu;

		private MenuController()
		{
			_instance = this;
			_nextCanInputTime = 0;
			_statusTextMaxTicks = 0;
			_mainMenu = new Menu("内置修改器 by Da");
		}

		~MenuController()
		{
			OnRelease(false);
		}

		public override void Dispose()
		{
			OnRelease(true);
		}

		private bool _isDisposed = false;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void OnRelease(bool isDisposeCall)
		{
			if (_isDisposed)
			{
				return;
			}
			Native.Release();
			if (isDisposeCall)
			{
				GC.SuppressFinalize(this);
			}
			_isDisposed = true;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal void PushMenu(AMenu menu)
		{
			if (menu is not null)
			{
				_menuStack.Push(menu);
				menu.SetDirty();
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal void PopMenu()
		{
			_menuStack.Pop();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Register(AMenu menu)
		{
			if (_menuList.ContainsKey(menu.Caption.Text))
			{
				Log.Error($"Menu {menu.Caption.Text} already exists!");
			}
			_menuList[menu.Caption.Text] = menu;
			if(menu is IUpdate update)
			{
				_updateList.Add(update);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
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

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public AMenu GetShowingMenu()
		{
			return _menuStack.Count > 0 ? _menuStack.Peek() : null;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void SetTips(string text, long ms = 3000)
		{
			var count = System.Text.Encoding.UTF8.GetBytes(text, 0, text.Length, _statusTextBytes, 0);
			if(count < _statusTextBytes.Length)
			{
				_statusTextBytes[count] = 0;
			}
			else
			{
				_statusTextBytes[_statusTextBytes.Length - 1] = 0;
			}
			_statusTextMaxTicks = Time.Now + ms;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private unsafe void DrawTips()
		{
			if (Time.Now < _statusTextMaxTicks)
			{
				Functions.SET_TEXT_FONT(0);
				Functions.SET_TEXT_SCALE(0.0f, 0.5f);
				Functions.SET_TEXT_COLOR(255, 255, 255, 255);
				Functions.SET_TEXT_OUTLINE();
				Functions.BEGIN_TEXT_COMMAND_DISPLAY_TEXT("STRING");
				Functions.SET_TEXT_CENTRE(false);
				Functions.SET_TEXT_JUSTIFICATION(0);
				fixed (byte* ptr = _statusTextBytes)
				{
					Functions.ADD_TEXT_COMPONENT_SUBSTRING_PLAYER_NAME(ptr);
				}
				Functions.END_TEXT_COMMAND_DISPLAY_TEXT(0.5f, 0.5f);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetInputWaitTime(long ms)
		{
			if (ms <= 0)
			{
				return;
			}
			_nextCanInputTime = Time.Now + ms;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private bool IsInputIsOnWait()
		{
			return _nextCanInputTime > Time.Now;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void ProcessUI()
		{
			GetShowingMenu()?.OnDraw();
			DrawTips();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void ProcessInput()
		{
			if (IsInputIsOnWait())
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
				SetInputWaitTime(300);
				return;
			}

			var menu = GetShowingMenu();
			if (menu is not null)
			{
				var waitTime = ExcuteInput(menu);
				SetInputWaitTime(waitTime);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private int ExcuteInput(AMenu menu)
		{
			if (Input.IsAccept())
			{
				menu.OnInput(KeyCode.Return);
				return 100;
			}
			if (Input.IsBack())
			{
				PopMenu();
				GetShowingMenu()?.SetDirty();
				return 100;
			}
			if (Input.IsUp())
			{
				menu.OnInput(KeyCode.Up);
				return 100;
			}
			if (Input.IsDown())
			{
				menu.OnInput(KeyCode.Down);
				return 100;
			}
			if (Input.IsLeft())
			{
				menu.OnInput(KeyCode.Left);
				return 100;
			}
			if (Input.IsRight())
			{
				menu.OnInput(KeyCode.Right);
				return 100;
			}
			return 50;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void ProcessScript()
		{
			var length = _updateList.Count;
			for (int i = 0; i < length; i++)
			{
				_updateList[i].Update();
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected override void Update()
		{
			if (_isReload)
			{
				SetTips("脚本重载成功!");
				_isReload = false;
			}
			ProcessUI();
			ProcessInput();
			ProcessScript();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected override void OnInput(uint key, bool isUpNow)
		{
			if (isUpNow)
			{
				Input.OnKeyUp(key);
			}
			else
			{
				Input.OnKeyDown(key);
			}
		}
	}
}
