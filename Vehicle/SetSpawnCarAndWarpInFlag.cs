using ScriptUI;
using static ScriptUI.Functions;

namespace Vehicle
{
	internal sealed class SetSpawnCarAndWarpInFlag : SwitchItem
	{
		public SetSpawnCarAndWarpInFlag(string caption) : base(caption)
		{
		}

		protected override void OnActive()
		{
			GlobalValue.SetBoolValue("Vehicle.SetSpawnCarAndWarpInFlag.WrapInWhenCarSpawned", true);
		}

		protected override void OnInactive()
		{
			GlobalValue.SetBoolValue("Vehicle.SetSpawnCarAndWarpInFlag.WrapInWhenCarSpawned", false);
		}
	}
}
