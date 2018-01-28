
using Chocolate.Battle.Object.Model;
using Chocolate.Battle.System;

namespace Chocolate.Battle.Map.Model
{
	public class PlayerMapDirectorModel : MapDirectorModel {

		protected override BattleMapModel GetMapModel(){

			return BattleGlobal.Instance.PlayerAreaModel.Map;
		}

	}
}
