using System;

namespace Bridge
{
	public abstract class MenuItem
	{
		public string Text;
		public Vector2 Position;
		public Vector2 Size;
		public Color TextColor;
		public Color BGColor;
		private float Scale;

		protected MenuItem(string title, int height, Color bgColor, float scale = 0.3f)
		{
			Text = title;
			Size = new Vector2(300.0f / 1920.0f, height / 1080.0f);
			TextColor = Color.White;
			BGColor = bgColor;
			Position = new Vector2();
			Scale = scale;
		}

		protected MenuItem(string title, int height)
		{
			Text = title;
			Size = new Vector2(300.0f / 1920.0f, height / 1080.0f);
			TextColor = Color.White;
			BGColor = Color.Green;
			Position = new Vector2();
		}

		internal virtual void OnDraw(bool isSelected = false)
		{
			PaintText(Text, 0.01f, Position.Y, Scale, ref TextColor);
			Functions.DRAW_RECT(Position.X, Position.Y, Size.X, Size.Y, BGColor.R, BGColor.G, BGColor.B, BGColor.A);
		}

		protected static void PaintText(string text, float x, float y, float scale, ref Color color)
		{
			Functions.SET_TEXT_FONT(0);
			Functions.SET_TEXT_SCALE(0.0f, scale);
			Functions.SET_TEXT_COLOR(color.R, color.G, color.B, color.A);
			Functions.SET_TEXT_OUTLINE();
			Functions.BEGIN_TEXT_COMMAND_DISPLAY_TEXT("STRING");
			Functions.ADD_TEXT_COMPONENT_SUBSTRING_PLAYER_NAME(text);
			Functions.END_TEXT_COMMAND_DISPLAY_TEXT(x, y);
		}
	}

	public sealed class Caption : MenuItem
	{
		public Caption(string title) : base(title, 60, Color.Cyan, 0.5f)
		{
		}
	}

	public abstract class ExecuteItem : MenuItem
	{
		public Color OnSelectTextColor;
		public Color OnSelectBGColor;
		protected ExecuteItem(string title) : base(title, 40)
		{
			OnSelectTextColor = Color.Yellow;
			OnSelectBGColor = Color.Gold;
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
	}

	public sealed class SubMenu : ExecuteItem
	{
		private const string AdditionText = ">>";
		private Func<Menu> _menuGetter;
		public SubMenu(string title, Func<Menu> menuGetter) : base(title)
		{
			_menuGetter = menuGetter;
		}

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
	}

	public abstract class SwitchItem : ExecuteItem
	{
		protected bool IsActive;
		protected const string ActiveText = "[已激活]";
		protected const string InactiveText = "[未激活]";

		protected SwitchItem(string title) : base(title)
		{
			IsActive = false;
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
