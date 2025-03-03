using ScriptUI;
using static ScriptUI.Functions;

namespace Player
{
	internal sealed class SpawnEnemy : TriggerItem
	{
		public SpawnEnemy(string caption) : base(caption)
		{

		}

		private readonly string _hashKey = "g_m_y_lost_01";

		protected unsafe override void OnExecute()
		{
			uint model = GET_HASH_KEY(_hashKey);
			if (IS_MODEL_IN_CDIMAGE(model) && IS_MODEL_VALID(model))
			{
				REQUEST_MODEL(model);
				while (!HAS_MODEL_LOADED(model))
				{
					Wait(0);
				}
				Vector3 coords = GET_OFFSET_FROM_ENTITY_IN_WORLD_COORDS(PlayerPed, 0.0f, 5.0f, 0.0f);

				int ped = CREATE_PED(PedType.GANG_BIKER_1, model, coords.X, coords.Y, coords.Z, 0.0f, true, false);

				SET_ENTITY_HEADING(ped, GET_ENTITY_HEADING(PlayerPed));
				SET_ENTITY_HEALTH(ped, 99999, PlayerPed, 0);
				SET_PED_AS_ENEMY(ped, true);

				Wait(0);
				SET_MODEL_AS_NO_LONGER_NEEDED(model);
				SET_PED_AS_NO_LONGER_NEEDED(&ped);
			}
		}
	}
}