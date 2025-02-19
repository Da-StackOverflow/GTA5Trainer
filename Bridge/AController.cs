using System;

namespace Bridge
{
	public abstract class AController : IDisposable
	{
		internal protected bool _isReload = false;
		internal protected abstract void Update();
		internal protected abstract void OnInput(uint key, bool isUpNow);

		public abstract void Dispose();
	}
}
