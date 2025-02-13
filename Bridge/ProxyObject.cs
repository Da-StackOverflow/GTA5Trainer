using System;
using System.IO;
using System.Security;
using System.Reflection;
using System.Collections.Generic;
using System.Security.Permissions;

namespace Bridge
{
	public sealed class ProxyObject : MarshalByRefObject, IDisposable
	{
		public static readonly string ScriptRootPath = Path.GetPathRoot(Path.GetFullPath("GTA5Trainer"));
		public static readonly string AsiPath = Path.GetFullPath("GTA5TrainerAsi.asi");
		public static readonly string BridgePath = Path.GetFullPath("GTA5Trainer/GTA5TrainerBridge.dll");

		public AppDomain Domain { get; private set; }

		private readonly List<Assembly> _assemblyList;
		private readonly List<Entry> _entries;

		private ProxyObject()
		{
			Domain = AppDomain.CurrentDomain;
			_assemblyList = [];
			_entries = [];
		}

		~ProxyObject()
		{
			Native.FreeBuffer();
		}

		public void Dispose()
		{
			Native.Release();
			GC.SuppressFinalize(this);
		}

		public static void Unload(ProxyObject domain)
		{
			try
			{
				domain.Dispose();
				AppDomain.Unload(domain.Domain);
			}
			catch (Exception e)
			{
				Log.Info(e.Message);
				Log.Info(e.StackTrace);
			}
		}

		public static ProxyObject Load()
		{
			var name = "GTA5Trainer_" + (ScriptRootPath.GetHashCode() ^ Time.Now).ToString("X");
			var setup = new AppDomainSetup
			{
				CachePath = Path.GetTempPath(),
				ApplicationBase = ScriptRootPath,
				ShadowCopyFiles = "true",
				ShadowCopyDirectories = ScriptRootPath
			};

			var newDomain = AppDomain.CreateDomain(name, null, setup, new PermissionSet(PermissionState.Unrestricted));
			newDomain.InitializeLifetimeService();

			AppDomain.CurrentDomain.InitializeLifetimeService();

			ProxyObject obj = null;
			try
			{
				obj = (ProxyObject)newDomain.CreateInstanceFromAndUnwrap(typeof(ProxyObject).Assembly.Location, typeof(ProxyObject).FullName);
			}
			catch (Exception ex)
			{
				AppDomain.Unload(newDomain);
				Log.Info(ex.Message);
				Log.Info(ex.StackTrace);
			}
			return obj;
		}


		public void Start()
		{
			var files = Directory.GetFiles(ScriptRootPath, "*.TrainerScript", SearchOption.AllDirectories);
			foreach (var file in files)
			{
				try
				{
					var assembly = Assembly.LoadFrom(file);
					_assemblyList.Add(assembly);
					foreach (var type in assembly.GetTypes())
					{
						if (type.IsSubclassOf(typeof(Entry)) && !type.IsAbstract)
						{
							var entry = (Entry)Activator.CreateInstance(type);
							_entries.Add(entry);
						}
					}
				}
				catch (Exception e)
				{
					Log.Info(e.Message);
					Log.Info(e.StackTrace);
				}
			}
			int count = _entries.Count;
			for (int i = 0; i < count; i++)
			{
				_entries[i].OnInit();
			}
		}

		public void OnUpdate()
		{
			int count = _entries.Count;
			for (int i = 0; i < count; i++)
			{
				_entries[i].OnUpdate();
			}
		}

		public void OnInput(uint key, bool isUp)
		{
			if (isUp)
			{
				Input.OnKeyUp(key);
				return;
			}
			Input.OnKeyDown(key);
		}
	}
}
