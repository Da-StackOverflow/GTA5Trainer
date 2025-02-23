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
		private static readonly string ScriptRootPath = Path.GetFullPath("GTA5Trainer");

		private static void Info(string log)
		{
			File.AppendAllText("GTA5TrainerScript.txt", $"{DateTime.Now:yyyy-MM-dd HH:mm:ss}: {log}\n");
		}

		private static void Error(string log)
		{
			File.AppendAllText("GTA5TrainerBridgeError.txt", $"{DateTime.Now:yyyy-MM-dd HH:mm:ss}: {log}\n");
		}

		private readonly AppDomain _domain;

		private readonly List<Assembly> _assemblyList;
		private readonly List<AEntry> _entries;
		private AController _controller;

		public override string ToString()
		{
			return _domain.FriendlyName;
		}

		public ProxyObject()
		{
			_domain = AppDomain.CurrentDomain;
			_assemblyList = [];
			_entries = [];
		}

		private bool _disposed;
		public void Dispose()
		{
			if (_disposed)
			{
				return;
			}
			_controller?.Dispose();
			_controller = null;
			GC.SuppressFinalize(this);
			_disposed = true;
		}

		public static void Unload(ProxyObject domain)
		{
			try
			{
				domain.Dispose();
				Info($"domain {domain} Disposed");
				AppDomain.Unload(domain._domain);
				Info($"domain Success Unloaded");
			}
			catch (System.Runtime.Remoting.RemotingException)
			{
				try
				{
					if (domain is not null)
					{
						AppDomain.Unload(domain._domain);
					}
				}
				catch
				{

				}
			}
			catch (Exception e)
			{
				try
				{
					if (domain is not null)
					{
						AppDomain.Unload(domain._domain);
					}
				}
				catch
				{

				}
				Error(e.GetType().ToString());
				Error(e.Message);
				Error(e.StackTrace);
			}
		}

		public static ProxyObject Load()
		{
			var name = "GTA5Trainer_" + (ScriptRootPath.GetHashCode() ^ DateTime.Now.Ticks).ToString("X");
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
				obj = (ProxyObject)newDomain.CreateInstanceFromAndUnwrap(Path.Combine(ScriptRootPath, "GTA5TrainerBridge.dll"), typeof(ProxyObject).FullName);
			}
			catch (Exception e)
			{
				AppDomain.Unload(newDomain);
				Error(e.GetType().ToString());
				Error(e.Message);
				Error(e.StackTrace);
			}
			return obj;
		}


		public void Start(bool isReload)
		{
			Info("Starting Script");
			try
			{
				var uiFile = Path.Combine(ScriptRootPath, "GTA5TrainerScriptUI.dll");
				Info($"Start Load Script: {uiFile}");
				var uiAssembly = Assembly.LoadFrom(uiFile);
				_assemblyList.Add(uiAssembly);
				foreach (var type in uiAssembly.GetTypes())
				{
					if (type.IsSubclassOf(typeof(AController)) && !type.IsAbstract)
					{
						_controller = (AController)Activator.CreateInstance(type, true);
						_controller._isReload = isReload;
						break;
					}
				}
			}
			catch(Exception e)
			{
				Error(e.GetType().ToString());
				Error(e.Message);
				Error(e.StackTrace);
			}

			if (_controller is null)
			{
				Error("Can't find AController");
				return;
			}

			var files = Directory.GetFiles(ScriptRootPath, "*.TrainerScript", SearchOption.AllDirectories);
			Info($"Find Script File Count: {files.Length}");

			foreach (var file in files)
			{
				try
				{
					Info($"Start Load Script: {file}");
					var assembly = Assembly.LoadFrom(file);
					_assemblyList.Add(assembly);
					foreach (var type in assembly.GetTypes())
					{
						if (type.IsSubclassOf(typeof(AEntry)) && !type.IsAbstract)
						{
							var entry = (AEntry)Activator.CreateInstance(type);
							_entries.Add(entry);
						}
					}
					Info($"Load Script: {file} Finish");
				}
				catch (Exception e)
				{
					Error(e.GetType().ToString());
					Error(e.Message);
					Error(e.StackTrace);
				}
			}

			try
			{
				int count = _entries.Count;
				for (int i = 0; i < count; i++)
				{
					_entries[i].OnInit(_controller);
				}
			}
			catch (Exception e)
			{
				Error(e.GetType().ToString());
				Error(e.Message);
				Error(e.StackTrace);
			}
		}

		public void OnUpdate()
		{
			try
			{
				_controller?.Update();
			}
			catch (Exception e)
			{
				Error(e.GetType().ToString());
				Error(e.Message);
				Error(e.StackTrace);
			}
		}

		public void OnInput(uint key, bool isUp)
		{
			try
			{
				_controller?.OnInput(key, isUp);
			}
			catch (Exception e)
			{
				Error(e.GetType().ToString());
				Error(e.Message);
				Error(e.StackTrace);
			}
		}
	}
}
