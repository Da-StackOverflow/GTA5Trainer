

using System.Text;
using System.Text.Json;

var path = Path.GetFullPath("CopyFile.exe").Split("bin")[0];
/*
try
{
	var result = JsonSerializer.Deserialize<List<Car>>(File.ReadAllText($"{path}vehicles.json"), JsonSerializerOptions.Default);
	if (result is not null)
	{
		Dictionary<string, StringBuilder> types = [];
		foreach (var car in result)
		{
			if (!types.TryGetValue(car.Type ?? "", out var sb))
			{
				sb = new StringBuilder($"export constexpr const ItemInfo {(car.Type ?? "").ToLower()}[] = {{\n");
				types.Add(car.Type ?? "", sb);
			}
			sb.AppendLine($"    ItemInfo(L\"{car.DisplayName?.SimplifiedChinese}\", \"{car.Name?.ToUpper()}\"),");
		}

		foreach (var type in types)
		{
			Console.WriteLine(type.Value.ToString());
			Console.WriteLine("};");
			Console.WriteLine();
		}
	}
}
catch (Exception e)
{
	Console.WriteLine(e.Message);
	Console.WriteLine(e.StackTrace);
}
*/
/*
try
{
	var result = JsonSerializer.Deserialize<List<Weapon>>(File.ReadAllText($"{path}weapons.json"), JsonSerializerOptions.Default);
	if (result is not null)
	{
		Console.WriteLine($"export constexpr const ItemInfo WeaponsInfos[] = {{");
		foreach (var weapon in result)
		{
			Console.WriteLine($"    ItemInfo(L\"{weapon.TranslatedLabel?.SimplifiedChinese}\", \"{weapon.Name?.ToUpper()}\"),");
		}
		Console.WriteLine($"}};");
	}
}
catch (Exception e)
{
	Console.WriteLine(e.Message);
	Console.WriteLine(e.StackTrace);
}
*/


try
{
	File.Copy("F:/CSharp/GTA5Trainer/GTA5Trainer/bin/GTA5Trainer.asi", "D:/SteamLibrary/steamapps/common/Grand Theft Auto V/GTA5Trainer.asi", true);
	Console.WriteLine("成功");
}
catch (Exception e)
{
	Console.WriteLine(e.Message);
	Console.WriteLine(e.StackTrace);
}
