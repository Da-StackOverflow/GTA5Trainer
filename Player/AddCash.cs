using Bridge;
using static Bridge.Functions;

namespace Player
{
	internal sealed class AddCash : TriggerItem
	{
		private readonly int _cash;
		private readonly int _player;
		public AddCash(string caption, int player, int cash) : base(caption)
		{
			_cash = cash;
			_player = player;
		}

		protected override void OnExecute()
		{
			switch (_player)
			{
				case 0:
				{
					SetCash("SP0_TOTAL_CASH");
					break;
				}
				case 1:
				{
					SetCash("SP1_TOTAL_CASH");
					break;
				}
				case 2:
				{
					SetCash("SP2_TOTAL_CASH");
					break;
				}
			}

		}
		private unsafe void SetCash(string hashKey)
		{
			uint hash = GET_HASH_KEY(hashKey);
			int val;
			STAT_GET_INT(hash, &val, -1);
			val += _cash;
			STAT_SET_INT(hash, val, true);
		}
	}
}
