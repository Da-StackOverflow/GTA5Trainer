using Bridge;
using static Bridge.Functions;

namespace Vehicle
{
	internal sealed class SafeBelt : UpdateableItem
	{
		public SafeBelt(string caption) : base(caption)
		{
		}

		protected override void OnUpdate()
		{
			if (DOES_ENTITY_EXIST(PlayerPed))
			{
				SET_PED_CONFIG_FLAG(PlayerPed, PedConfigFlags.WillFlyThruWindscreen, false);
			}
		}

		protected override void OnInactive()
		{
			if (DOES_ENTITY_EXIST(PlayerPed))
			{
				SET_PED_CONFIG_FLAG(PlayerPed, PedConfigFlags.WillFlyThruWindscreen, true);
			}
		}
	}
}
