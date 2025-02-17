using Bridge;
using static Bridge.Functions;

namespace Vehicle
{
	internal sealed class SafeBelt : UpdateableItem
	{
		private int _prePed = -1;
		public SafeBelt(string caption) : base(caption)
		{
		}

		protected override
		void OnActive()
		{
			if (DOES_ENTITY_EXIST(PlayerPed))
			{
				if (GET_PED_CONFIG_FLAG(PlayerPed, PedConfigFlags.WillFlyThruWindscreen, true))
				{
					SET_PED_CONFIG_FLAG(PlayerPed, PedConfigFlags.WillFlyThruWindscreen, false);
				}
				_prePed = PlayerPed;
			}
		}

		protected override void OnUpdate()
		{
			if(_prePed == PlayerPed)
			{
				return;
			}

			if (DOES_ENTITY_EXIST(PlayerPed))
			{
				if (GET_PED_CONFIG_FLAG(PlayerPed, PedConfigFlags.WillFlyThruWindscreen, true))
				{
					SET_PED_CONFIG_FLAG(PlayerPed, PedConfigFlags.WillFlyThruWindscreen, false);
				}
				_prePed = PlayerPed;
			}
		}

		protected override void OnInactive()
		{
			if (DOES_ENTITY_EXIST(PlayerPed))
			{
				SET_PED_CONFIG_FLAG(PlayerPed, PedConfigFlags.WillFlyThruWindscreen, true);
			}
			_prePed = -1;
		}
	}
}
