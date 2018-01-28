
using UnityEngine;

using Chocolate.Battle.Bettery.Model;
using Chocolate.Battle.Map.Model;
using Chocolate.Battle.Object.Model;

namespace Chocolate.Battle.Area.Model
{
	public class BattleAreaModel {

		private BattleMapModel map;
		public BattleMapModel Map{
			get{ Debug.Assert (map != null); return map; }
		}

		private BattleBetteryModel bettery;
		public BattleBetteryModel Bettery{
			get{ Debug.Assert (bettery != null); return bettery; }
		}

		protected BattleAreaModel(){
		}

		// public ------------------------

		public void GoToNextWave(){

			// 発射する
			bettery.Director.ReciveMessage ("Burning");
		}

		public bool IsNearWall( IBattleObjectModel model ){

			var areaCollision = map.View.GetCollision ();
			var modelCollsiion = model.View.GetCollision ();

			if (areaCollision.IsHamidashi (modelCollsiion)) {
				return true;
			}

			return false;
		}

		public static BattleAreaModel CreateArea( BattleBetteryModel bettery, BattleMapModel map ){

			BattleAreaModel area = new BattleAreaModel ();

			area.bettery = bettery;
			area.map = map;

			return area;
		}

		public Vector3 CalcRandomPointInArea(){

			var collision = map.View.GetCollision ();

			// 半径
			var rad = Random.Range (0, collision.Radius*0.9f);

			// 座標の取得
			var pos = collision.Position + new Vector3 (
				Mathf.Cos( Random.Range( 0, Mathf.PI * 2 ) ) * rad ,
				0,
				Mathf.Sin( Random.Range( 0, Mathf.PI * 2 ) ) * rad );

			return pos;
		}
	}
}