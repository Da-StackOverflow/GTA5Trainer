

namespace Bridge
{
	public abstract class MenuItem
	{
		public string Title;
		public Vector2 Position;
		public Vector2 Size;
		public Color TextColor;
		public Color BGColor;

		protected MenuItem(string title, int height, Color bgColor)
		{
			Title = title;
			Size = new Vector2(300.0f / 1920.0f, height / 1080.0f);
			TextColor = Color.White;
			BGColor = bgColor;
			Position = new Vector2();
		}

		protected MenuItem(string title, int height)
		{
			Title = title;
			Size = new Vector2(300.0f / 1920.0f, height / 1080.0f);
			TextColor = Color.White;
			BGColor = Color.Green;
			Position = new Vector2();
		}

		internal virtual void OnDraw()
		{

		}
	}

	public sealed class Caption : MenuItem
	{
		public Caption(string title) : base(title, 60, Color.Cyan)
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
		}

		protected abstract void OnExecute();
	}

	public abstract class SubMenu : ExecuteItem
	{
		protected const string AdditionText = ">>";
		protected SubMenu(string title) : base(title)
		{
			
		}

		protected sealed override void OnExecute()
		{

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
