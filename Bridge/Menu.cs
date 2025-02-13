using System;
using System.Collections.Generic;

namespace Bridge
{
	public sealed class Menu
	{
		public readonly Caption Caption;
		private readonly List<ExecuteItem> _items = [];
		private readonly List<UpdateableItem> _updateableItems = [];
		private int _activeItemInActivePage = 0;
		private int _activePage = 0;
		private int _itemCount = 0;
		private int _switchItemCount = 0;

		private const int ItemsMaxCountPerPage = 15;

		public Menu(string caption)
		{
			Caption = new(caption);
		}

		public void AddItem<T>(T item) where T : ExecuteItem
		{
			int index = _items.Count % ItemsMaxCountPerPage;
			item.Position.X = item.Size.X / 2.0f;
			item.Position.Y = item.Size.Y * (index + 0.5f) + Caption.Size.Y;
			if ((_items.Count & 1) == 0)
			{
				item.BGColor.R = Color.Lime.R;
				item.BGColor.G = Color.Lime.G;
				item.BGColor.B = Color.Lime.B;
				item.BGColor.A = Color.Lime.A;
			}
			else
			{
				item.BGColor.R = Color.Green.R;
				item.BGColor.G = Color.Green.G;
				item.BGColor.B = Color.Green.B;
				item.BGColor.A = Color.Green.A;
			}

			_items.Add(item);
			_itemCount++;
			if(item is UpdateableItem u)
			{
				_updateableItems.Add(u);
				_switchItemCount++;
			}
		}

		public int ActiveItemIndex { get { return _activePage * ItemsMaxCountPerPage + _activeItemInActivePage; } }

		public void OnDraw()
		{
			Caption.OnDraw();
			int end = Math.Min(_items.Count, (_activePage + 1) * ItemsMaxCountPerPage);
			int index = 0;
			for (int i = _activePage * ItemsMaxCountPerPage; i < end; i++)
			{
				_items[i].OnDraw(_activeItemInActivePage == index);
				index++;
			}
		}

		public void OnSwitchItemUpdate()
		{
			for (int i = 0; i < _switchItemCount; i++)
			{
				_updateableItems[i].Update();
			}
		}

		public void OnInput(KeyCode key)
		{
			int itemsLeft = _itemCount % ItemsMaxCountPerPage;
			int pageCount = _itemCount / ItemsMaxCountPerPage + (itemsLeft != 0 ? 1 : 0);
			int lineCountLastPage = itemsLeft > 0 ? itemsLeft : ItemsMaxCountPerPage;

			switch (key)
			{
				case KeyCode.Return:
					_items[ActiveItemIndex].Execute();
					break;
				case KeyCode.Up:
					if (_activePage != pageCount - 1)
					{
						_activeItemInActivePage = (_activeItemInActivePage + ItemsMaxCountPerPage - 1) % ItemsMaxCountPerPage;
					}
					else
					{
						_activeItemInActivePage = (_activeItemInActivePage + lineCountLastPage - 1) % lineCountLastPage;
					}
					break;
				case KeyCode.Down:
					if (_activePage != pageCount - 1)
					{
						_activeItemInActivePage = (_activeItemInActivePage + 1) % ItemsMaxCountPerPage;
					}
					else
					{
						_activeItemInActivePage = (_activeItemInActivePage + 1) % lineCountLastPage;
					}
					break;
				case KeyCode.Left:
					_activePage = (_activePage + pageCount - 1) % pageCount;
					_activeItemInActivePage = 0;
					break;
				case KeyCode.Right:
					_activePage = (_activePage + 1) % pageCount;
					_activeItemInActivePage = 0;
					break;
				case KeyCode.Back:
					break;
			}
		}
	}
}
