using System;

namespace Bridge
{
	public abstract class MenuItem
	{
		internal readonly string Text;
		internal Vector2 Position;
		internal readonly Vector2 Size;
		internal readonly Color TextColor = Color.White;
		internal Color BGColor;
		internal readonly float FontSize;
		internal float TextY;

		protected MenuItem(string title, int height, Color bgColor, int fontSize = 45)
		{
			Text = title;
			Size = new Vector2(400.0f / 1920.0f, height / 1080.0f);
			BGColor = bgColor;
			Position = new Vector2(Size.X / 2.0f, Size.Y / 2.0f);
			FontSize = fontSize / 100.0f;
			TextY = Position.Y - Size.Y / 3.0f;
		}

		protected MenuItem(string title, int height, int fontSize = 30)
		{
			Text = title;
			Size = new Vector2(400.0f / 1920.0f, height / 1080.0f);
			BGColor = Color.Green;
			Position = new Vector2();
			FontSize = fontSize / 100.0f;
		}

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

		protected static void PaintText(string text, float x, float y, float fontSize, int r, int g, int b, int a)
		{
			Functions.SET_TEXT_FONT(0);
			Functions.SET_TEXT_SCALE(0.0f, fontSize);
			Functions.SET_TEXT_COLOR(r, g, b, a);
			Functions.SET_TEXT_OUTLINE();
			Functions.BEGIN_TEXT_COMMAND_DISPLAY_TEXT("STRING");
			Functions.ADD_TEXT_COMPONENT_SUBSTRING_PLAYER_NAME(text);
			Functions.END_TEXT_COMMAND_DISPLAY_TEXT(x, y);
		}

		public override string ToString()
		{
			return Text;
		}
	}

	public sealed class Caption : MenuItem
	{
		internal int MaxPage;
		internal int CurrentPage;
		public Caption(string title) : base(title, 60, Color.Cyan)
		{
			MaxPage = 0;
			CurrentPage = 0;
		}

		internal override void OnDraw(bool isSelected = false)
		{
			base.OnDraw(isSelected);
			if (MaxPage > 1)
			{
				PaintText($"{CurrentPage:02d}/{MaxPage:02d}", 0.17f, TextY, FontSize, TextColor.R, TextColor.G, TextColor.B, TextColor.A);
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

		internal void Execute()
		{
			OnExecute();
			Functions.PLAY_SOUND_FRONTEND(-1, "NAV_UP_DOWN", "HUD_FRONTEND_DEFAULT_SOUNDSET", false);
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

	public sealed class SubMenu : ExecuteItem
	{
		private const string AdditionText = ">>";
		private readonly Func<Menu> _menuGetter;
		public SubMenu(string title, Func<Menu> menuGetter) : base(title)
		{
			_menuGetter = menuGetter;
		}

		protected sealed override void OnExecute()
		{
			MenuController.Instance.PushMenu(_menuGetter?.Invoke());
		}

		internal override void OnDraw(bool isSelected = false)
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

		protected abstract void OnActive();
		protected abstract void OnInactive();

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

		internal void Update()
		{
			if (IsActive)
			{
				OnUpdate();
			}
		}

		protected abstract void OnUpdate();

		protected override void OnActive()
		{

		}

		protected override void OnInactive()
		{

		}
	}
}
