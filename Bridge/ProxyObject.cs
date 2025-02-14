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
		public static readonly string ScriptRootPath = Directory.GetParent("GTA5TrainerAsi.asi").FullName;
		public static readonly string AsiPath = Path.GetFullPath("GTA5TrainerAsi.asi");
		public static readonly string BridgePath = Path.GetFullPath("GTA5TrainerBridge.dll");

		public AppDomain Domain { get; private set; }

		private readonly List<Assembly> _assemblyList;
		private readonly List<Entry> _entries;

		public ProxyObject()
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
				Log.Error(e.Message);
				Log.Error(e.StackTrace);
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
				Log.Error(ex.Message);
				Log.Error(ex.StackTrace);
			}
			return obj;
		}


		public void Start()
		{
			Log.Info("Starting...");
			var files = Directory.GetFiles(ScriptRootPath, "*.TrainerScript", SearchOption.AllDirectories);
			Log.Info($"Find File Count:{files.Length}");
			foreach (var file in files)
			{
				
				try
				{
					Log.Info($"Start Load {file}");
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
					Log.Info($"Load {file} Finish");
				}
				catch (Exception e)
				{
					Log.Error(e.Message);
					Log.Error(e.StackTrace);
				}
			}
			try
			{
				int count = _entries.Count;
				for (int i = 0; i < count; i++)
				{
					_entries[i].OnInit();
				}
			}
			catch (Exception e)
			{
				Log.Error(e.Message);
				Log.Error(e.StackTrace);
			}
		}

		public void OnUpdate()
		{
			try
			{
				MenuController.Instance.Update();
			}
			catch (Exception e)
			{
				Log.Error(e.Message);
				Log.Error(e.StackTrace);
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
