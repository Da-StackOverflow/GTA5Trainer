try
{
	File.Copy("F:/CSharp/GTA5Trainer/GTA5Trainer/bin/GTA5Trainer.asi", "D:/SteamLibrary/steamapps/common/Grand Theft Auto V/GTA5Trainer.asi", true);
	Console.WriteLine("成功");
}
catch(Exception e)
{
	Console.WriteLine(e.Message);
	Console.WriteLine(e.StackTrace);
}