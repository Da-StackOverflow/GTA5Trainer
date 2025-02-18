using System;
using System.Collections.Generic;

namespace ScriptUI
{
	public abstract class AMenu
	{
		public readonly Caption Caption;
		protected int _activeItemInActivePage = 0;
		protected int _activePage = 0;
		protected int _itemCount = 0;

		protected const int ItemsMaxCountPerPage = 15;

		protected AMenu(string caption)
		{
			Caption = new(caption);
		}

		internal virtual void OnDraw()
		{
			Caption.OnDraw();
		}

		public abstract void AddItem<T>(T item) where T : ExecuteItem;

		internal void OnInput(KeyCode key)
		{
			int itemsLeft = _itemCount % ItemsMaxCountPerPage;
			int pageCount = _itemCount / ItemsMaxCountPerPage + (itemsLeft != 0 ? 1 : 0);
			int lineCountLastPage = itemsLeft > 0 ? itemsLeft : ItemsMaxCountPerPage;

			switch (key)
			{
				case KeyCode.Return:
					ProcessExecute(_activePage * ItemsMaxCountPerPage + _activeItemInActivePage);
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
					Caption.CurrentPage = _activePage + 1;
					break;
				case KeyCode.Right:
					_activePage = (_activePage + 1) % pageCount;
					_activeItemInActivePage = 0;
					Caption.CurrentPage = _activePage + 1;
					break;
				case KeyCode.Back:
					break;
			}
		}

		protected abstract void ProcessExecute(int index);
	}

	public sealed class Menu : AMenu, IUpdate
	{
		private readonly List<ExecuteItem> _items = [];
		private readonly List<UpdateableItem> _updateableItems = [];
		private int _switchItemCount = 0;

		public Menu(string caption) : base(caption)
		{
			
		}

		public override void AddItem<T>(T item)
		{
			if (item is null)
			{
				Log.Error($"{typeof(Menu)} AddItem is null");
				return;
			}
			int index = _items.Count % ItemsMaxCountPerPage;
			item.SetPosition(item.Size.X / 2.0f, item.Size.Y * (index + 0.5f) + Caption.Size.Y);
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
			if (item is UpdateableItem u)
			{
				_updateableItems.Add(u);
				_switchItemCount++;
			}
			Caption.MaxPage = _itemCount / ItemsMaxCountPerPage + (_itemCount % ItemsMaxCountPerPage != 0 ? 1 : 0);
		}

		internal override void OnDraw()
		{
			base.OnDraw();
			int end = Math.Min(_items.Count, (_activePage + 1) * ItemsMaxCountPerPage);
			int index = 0;
			for (int i = _activePage * ItemsMaxCountPerPage; i < end; i++)
			{
				_items[i].OnDraw(_activeItemInActivePage == index);
				index++;
			}
		}

		protected override void ProcessExecute(int index)
		{
			_items[index].Execute();
		}

		public void Update()
		{
			for (int i = 0; i < _switchItemCount; i++)
			{
				_updateableItems[i].Update();
			}
		}
	}

	public class Menu<T> : AMenu where T : ExecuteItem
	{
		protected readonly List<T> _items = [];
		protected readonly List<T> _cache = [];

		protected readonly Func<T> _createItemFunc;
		protected readonly Action<int, T> _refreshItemFunc;


		public Menu(string caption, Func<T> createItemFunc, Action<int, T> refreshItemFunc) : base(caption)
		{
			_createItemFunc = createItemFunc;
			_refreshItemFunc = refreshItemFunc;
		}

		public void SetItemNums(int num)
		{
			_activeItemInActivePage = 0;
			_activePage = 0;

			if (num < _itemCount)
			{
				var numToCache = _itemCount - num;
				for (int i = 0; i < numToCache; i++)
				{
					_itemCount--;
					_cache.Add(_items[_itemCount]);
					_items.RemoveAt(_itemCount);

					Caption.MaxPage = _itemCount / ItemsMaxCountPerPage + (_itemCount % ItemsMaxCountPerPage != 0 ? 1 : 0);
				}
			}
			else if (num > _itemCount)
			{
				var numToCreate = num - _itemCount;
				for (int i = 0; i < numToCreate; i++)
				{
					if (_cache.Count == 0)
					{
						AddItem(_createItemFunc?.Invoke());
					}
					else
					{
						var item = _cache[_cache.Count - 1];
						_cache.RemoveAt(_cache.Count - 1);
						AddItem(item);
					}
				}
			}
			for (int i = 0; i < _itemCount; i++)
			{
				_refreshItemFunc(i, _items[i]);
			}
		}

		internal sealed override void OnDraw()
		{
			base.OnDraw();
			int end = Math.Min(_items.Count, (_activePage + 1) * ItemsMaxCountPerPage);
			int index = 0;
			for (int i = _activePage * ItemsMaxCountPerPage; i < end; i++)
			{
				_items[i].OnDraw(_activeItemInActivePage == index);
				index++;
			}
		}

		protected sealed override void ProcessExecute(int index)
		{
			_items[index].Execute();
		}

		public sealed override void AddItem<T1>(T1 item)
		{
			if (item is null)
			{
				Log.Error($"{typeof(Menu)} AddItem is null");
				return;
			}
			if (item is T t)
			{
				int index = _items.Count % ItemsMaxCountPerPage;
				item.SetPosition(item.Size.X / 2.0f, item.Size.Y * (index + 0.5f) + Caption.Size.Y);
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

				_items.Add(t);
				_itemCount++;
				Caption.MaxPage = _itemCount / ItemsMaxCountPerPage + (_itemCount % ItemsMaxCountPerPage != 0 ? 1 : 0);
			}
		}
	}

	public sealed class UpdateableItemMenu<T> : Menu<T>, IUpdate where T : UpdateableItem
	{
		public UpdateableItemMenu(string caption, Func<T> createItemFunc, Action<int, T> refreshItemFunc) : base(caption, createItemFunc, refreshItemFunc)
		{

		}

		public void Update()
		{
			for (int i = 0; i < _itemCount; i++)
			{
				_items[i].Update();
			}
		}
	}
}
