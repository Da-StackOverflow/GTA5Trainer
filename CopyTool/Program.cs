using System;
using System.IO;

namespace CopyTool
{
	internal class Program
	{
		static void Main(string[] args)
		{
			string GameDirectory = "D:/SteamLibrary/steamapps/common/Grand Theft Auto V";

			string GameScriptDirectory = $"{GameDirectory}/GTA5Trainer";

			string ProjectBasePath = "../..";

			if (!Directory.Exists(GameScriptDirectory))
			{
				Directory.CreateDirectory(GameScriptDirectory);
			}

			bool gameRunning = false;

			if (!gameRunning)
			{
				try
				{
					File.Copy($"{ProjectBasePath}/Asi/bin/GTA5Trainer.asi", $"{GameDirectory}/GTA5Trainer.asi", true);
					Console.WriteLine("Copy GTA5Trainer.asi Success");
				}
				catch (Exception e)
				{
					Console.WriteLine(e.Message);
					Console.WriteLine(e.StackTrace);
				}


				try
				{
					File.Copy($"{ProjectBasePath}/Bridge/bin/GTA5TrainerBridge.dll", $"{GameDirectory}/GTA5TrainerBridge.dll", true);
					Console.WriteLine("Copy GTA5TrainerBridge.dll Success");
				}
				catch (Exception e)
				{
					Console.WriteLine(e.Message);
					Console.WriteLine(e.StackTrace);
				}
			}


			try
			{
				File.Copy($"{ProjectBasePath}/Bridge/bin/GTA5TrainerBridge.dll", $"{GameDirectory}/GTA5Trainer/GTA5TrainerBridge.dll", true);
				Console.WriteLine("Copy GTA5Trainer/GTA5TrainerBridge.dll Success");
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
				Console.WriteLine(e.StackTrace);
			}

			try
			{
				File.Copy($"{ProjectBasePath}/ScriptUI/bin/GTA5TrainerScriptUI.dll", $"{GameDirectory}/GTA5Trainer/GTA5TrainerScriptUI.dll", true);
				Console.WriteLine("Copy GTA5Trainer/GTA5TrainerScriptUI.dll Success");
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
				Console.WriteLine(e.StackTrace);
			}

			string[] scripts = [
				"Player",
				"Vehicle",
				"Weapon",
				"Time",
				"Weather",
				"Other",
			];

			try
			{
				for (int i = 0; i < scripts.Length; i++)
				{
					var script = scripts[i];
					File.Copy($"{ProjectBasePath}/{script}/bin/{script}.dll", $"{GameScriptDirectory}/Script{i}_{script}.TrainerScript", true);
					Console.WriteLine($"Copy GTA5Trainer/Script{i}_{script}.TrainerScript Success");
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
				Console.WriteLine(e.StackTrace);
			}
		}
	}
}
