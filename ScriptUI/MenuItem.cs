using System;
using System.Runtime.CompilerServices;

namespace ScriptUI
{
	public abstract class MenuItem
	{
		public string Text;
		internal Vector2 Position;
		internal readonly Vector2 Size;
		internal readonly Color TextColor = Color.White;
		internal Color BGColor;
		internal readonly float FontSize;
		internal float TextY;

		protected MenuItem(string title, int height, Color bgColor)
		{
			Text = title;
			Size = new Vector2(400.0f / 1920.0f, height / 1080.0f);
			BGColor = bgColor;
			Position = new Vector2(Size.X / 2.0f, Size.Y / 2.0f);
			FontSize = height / 150.0f;
			TextY = Position.Y - Size.Y / 3.0f;
		}

		protected MenuItem(string title, int height)
		{
			Text = title;
			Size = new Vector2(400.0f / 1920.0f, height / 1080.0f);
			BGColor = Color.Green;
			Position = new Vector2();
			FontSize = height / 150.0f;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]

		internal void SetPosition(float x, float y)
		{
			Position.X = x;
			Position.Y = y;
			TextY = Position.Y - Size.Y / 3.0f;
		}

		internal virtual void OnDraw(bool isSelected = false)
		{
			PaintText(Text, 0.01f, TextY, FontSize, TextColor.R, TextColor.G, TextColor.B, TextColor.A);
			Functions.DRAW_RECT(Position.X, Position.Y, Size.X, Size.Y, BGColor.R, BGColor.G, BGColor.B, BGColor.A);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static void PaintText(string text, float x, float y, float fontSize, int r, int g, int b, int a)
		{
			Functions.SET_TEXT_FONT(0);
			Functions.SET_TEXT_SCALE(0.0f, fontSize);
			Functions.SET_TEXT_COLOR(r, g, b, a);
			Functions.SET_TEXT_OUTLINE();
			Functions.BEGIN_TEXT_COMMAND_DISPLAY_TEXT("STRING");
			Functions.ADD_TEXT_COMPONENT_SUBSTRING_PLAYER_NAME(text);
			Functions.END_TEXT_COMMAND_DISPLAY_TEXT(x, y);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal unsafe static void PaintText(byte* text, float x, float y, float fontSize, int r, int g, int b, int a)
		{
			Functions.SET_TEXT_FONT(0);
			Functions.SET_TEXT_SCALE(0.0f, fontSize);
			Functions.SET_TEXT_COLOR(r, g, b, a);
			Functions.SET_TEXT_OUTLINE();
			Functions.BEGIN_TEXT_COMMAND_DISPLAY_TEXT("STRING");
			Functions.ADD_TEXT_COMPONENT_SUBSTRING_PLAYER_NAME(text);
			Functions.END_TEXT_COMMAND_DISPLAY_TEXT(x, y);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected static void Wait(uint ms)
		{
			Native.Sleep(ms);
		}

		protected static int PlayerID => Functions.PLAYER_ID();

		protected static int PlayerPed => Functions.PLAYER_PED_ID();

		public override string ToString()
		{
			return Text;
		}
	}

	public sealed class Caption : MenuItem
	{
		private readonly static byte[] _stringBytes = new byte[16];

		private bool _modified = true;

		private int _maxPage = 0;
		internal int MaxPage
		{
			get => _maxPage;
			set
			{
				_maxPage = value;
				_modified = true;
			}
		}
		private int _currentPage = 1;
		internal int CurrentPage
		{
			get => _currentPage;
			set
			{
				_currentPage = value;
				_modified = true;
			}
		}
		
		public Caption(string title) : base(title, 60, Color.Cyan)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]

		internal unsafe override void OnDraw(bool isSelected = false)
		{
			base.OnDraw(isSelected);
			if (_maxPage > 1)
			{
				if (_modified)
				{
					var str = $"{_currentPage:D2}/{_maxPage:D2}\0";
					var count = System.Text.Encoding.UTF8.GetBytes(str, 0, str.Length, _stringBytes, 0);
					if(count >= 16)
					{
						_stringBytes[15] = 0;
					}
					else
					{
						_stringBytes[count] = 0;
					}
					_modified = false;
				}
				fixed (byte* ptr = _stringBytes)
				{
					PaintText(ptr, 0.17f, TextY, FontSize, TextColor.R, TextColor.G, TextColor.B, TextColor.A);
				}
			}
		}
	}

	public abstract class ExecuteItem : MenuItem
	{
		internal readonly Color OnSelectTextColor = Color.Yellow;
		internal readonly Color OnSelectBGColor = Color.Gold;
		protected ExecuteItem(string title) : base(title, 40)
		{

		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal void Execute()
		{
			OnExecute();
			//Functions.PLAY_SOUND_FRONTEND(-1, "NAV_UP_DOWN", "HUD_FRONTEND_DEFAULT_SOUNDSET", false);
		}

		protected void SetTips(string tips, long ms = 3000)
		{
			MenuController.Instance.SetTips(tips, ms);
		}

		protected abstract void OnExecute();

		internal override void OnDraw(bool isSelected = false)
		{
			if (isSelected)
			{
				PaintText(Text, 0.01f, TextY, FontSize, OnSelectTextColor.R, OnSelectTextColor.G, OnSelectTextColor.B, OnSelectTextColor.A);
				Functions.DRAW_RECT(Position.X, Position.Y, Size.X, Size.Y, OnSelectBGColor.R, OnSelectBGColor.G, OnSelectBGColor.B, OnSelectBGColor.A);
			}
			else
			{
				base.OnDraw(isSelected);
			}
		}
	}

	public abstract class ASubMenu : ExecuteItem
	{
		private const string AdditionText = ">>";
		public ASubMenu(string title) : base(title)
		{
			
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal sealed override void OnDraw(bool isSelected = false)
		{
			base.OnDraw(isSelected);
			if (isSelected)
			{
				PaintText(AdditionText, 0.19f, TextY, FontSize, OnSelectTextColor.R, OnSelectTextColor.G, OnSelectTextColor.B, OnSelectTextColor.A);
			}
			else
			{
				PaintText(AdditionText, 0.19f, TextY, FontSize, TextColor.R, TextColor.G, TextColor.B, TextColor.A);
			}
		}
	}

	public sealed class SubMenu : ASubMenu
	{
		private readonly Func<Menu> _menuGetter;
		public SubMenu(string title, Func<Menu> menuGetter) : base(title)
		{
			_menuGetter = menuGetter;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected sealed override void OnExecute()
		{
			MenuController.Instance.PushMenu(_menuGetter?.Invoke());
		}
	}

	public sealed class SubMenu<T> : ASubMenu where T : AMenu
	{
		private readonly Func<T> _menuGetter;
		public SubMenu(string title, Func<T> menuGetter) : base(title)
		{
			_menuGetter = menuGetter;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected sealed override void OnExecute()
		{
			MenuController.Instance.PushMenu(_menuGetter?.Invoke());
		}
	}

	public abstract class TriggerItem : ExecuteItem
	{
		protected TriggerItem(string title) : base(title)
		{
		}

		internal sealed override void OnDraw(bool isSelected = false)
		{
			base.OnDraw(isSelected);
		}
	}

	public abstract class SwitchItem : ExecuteItem
	{
		protected bool IsActive = false;
		private const string ActiveText = "[已激活]";
		private const string InactiveText = "[未激活]";
		private readonly Color ActiveTextColor = Color.Lime;

		protected SwitchItem(string title) : base(title)
		{
			
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected sealed override void OnExecute()
		{
			IsActive = !IsActive;
			if (IsActive)
			{
				OnActive();
			}
			else
			{
				OnInactive();
			}
		}
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected abstract void OnActive();

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected abstract void OnInactive();

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal sealed override void OnDraw(bool isSelected = false)
		{
			base.OnDraw(isSelected);
			if (isSelected)
			{

				if (IsActive)
				{
					PaintText(ActiveText, 0.17f, TextY, FontSize, OnSelectTextColor.R, OnSelectTextColor.G, OnSelectTextColor.B, OnSelectTextColor.A);
				}
				else
				{
					PaintText(InactiveText, 0.17f, TextY, FontSize, OnSelectTextColor.R, OnSelectTextColor.G, OnSelectTextColor.B, OnSelectTextColor.A);
				}
			}
			else
			{
				if (IsActive)
				{
					PaintText(ActiveText, 0.17f, TextY, FontSize, ActiveTextColor.R, ActiveTextColor.G, ActiveTextColor.B, ActiveTextColor.A);
				}
				else
				{
					PaintText(InactiveText, 0.17f, TextY, FontSize, TextColor.R, TextColor.G, TextColor.B, TextColor.A);
				}
			}
		}
	}

	public abstract class UpdateableItem : SwitchItem
	{
		protected UpdateableItem(string title) : base(title)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal void Update()
		{
			if (IsActive)
			{
				OnUpdate();
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected abstract void OnUpdate();

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected override void OnActive()
		{

		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected override void OnInactive()
		{

		}
	}
}
