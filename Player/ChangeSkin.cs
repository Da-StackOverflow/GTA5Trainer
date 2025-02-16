using Bridge;
using static Bridge.Functions;

namespace Player
{
	internal sealed class ChangeSkin : TriggerItem
	{
		public ChangeSkin(ItemInfo skinInfo) : base(skinInfo.Name)
		{
			SkinInfo = skinInfo;
		}

		private readonly ItemInfo SkinInfo;

		protected override void OnExecute()
		{
			uint model = GET_HASH_KEY(SkinInfo.HashKey);
			if (IS_MODEL_IN_CDIMAGE(model) && IS_MODEL_VALID(model))
			{
				REQUEST_MODEL(model);
				while (!HAS_MODEL_LOADED(model))
				{
					Wait(0);
				}
				SET_PLAYER_MODEL(PlayerID, model);
				SET_PED_DEFAULT_COMPONENT_VARIATION(PlayerPed);
				Wait(0);
				for (int i = 0; i < 12; i++)
				{
					for (int j = 0; j < 100; j++)
					{
						int drawable = Random.Next(9);
						int texture = Random.Next(9);
						if (IS_PED_COMPONENT_VARIATION_VALID(PlayerPed, i, drawable, texture))
						{
							SET_PED_COMPONENT_VARIATION(PlayerPed, i, drawable, texture, 0);
							break;
						}
					}
				}
				Wait(100);
				SET_MODEL_AS_NO_LONGER_NEEDED(model);
				GlobalValue.SetBoolValue("Player.ChangeSkin.ChangedSkin", true);
			}
		}
	}
}
