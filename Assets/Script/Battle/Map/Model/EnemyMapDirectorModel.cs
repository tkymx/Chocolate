
using Chocolate.Battle.Object.Model;
using Chocolate.Battle.System;

namespace Chocolate.Battle.Map.Model
{
	public class EnemyMapDirectorModel : MapDirectorModel {

		protected override BattleMapModel GetMapModel(){

			return BattleGlobal.Instance.EnemyAreaModel.Map;
		}

	}
}
