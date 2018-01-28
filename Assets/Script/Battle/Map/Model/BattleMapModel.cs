
using Chocolate.Battle.Object.Model;
using Chocolate.Battle.System;

namespace Chocolate.Battle.Map.Model
{
	public class BattleMapModel : BattleObjectModel {

		protected BattleMapModel(
			BattleViewModel view,
			DirectorModel directer
		) : base(view,directer) {
		}

		public static BattleMapModel CreateMap( BattleViewModel view , DirectorModel director ){

			BattleMapModel mapModel = new BattleMapModel (view, director);
			mapModel.SetInternalRefference ();

			// オブジェクトを追加
			BattleGlobal.Instance.ObjectsUpdater.AddObject ( mapModel );

			return mapModel;
		}

	}
}