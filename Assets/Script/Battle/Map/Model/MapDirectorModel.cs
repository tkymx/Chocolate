
using UnityEngine;
using Chocolate.Battle.Object.Model;

namespace Chocolate.Battle.Map.Model
{
	public class MapDirectorModel : DirectorModel {

		protected virtual BattleMapModel GetMapModel(){

			Debug.Assert ( false , "まだ実装していません MapDirectorModel" );

			return null;
		}

		public override void UpdateByFrame(){

			var mapModel = GetMapModel ();

			if ( mapModel.Life.IsDead ()) {

				// 死んだら正体を消す
				mapModel.View.RootTransform.SetVisible ( false );
			}
		}
	}
}
