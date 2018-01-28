using UnityEngine;
using System.Collections;

namespace Chocolate.Battle.System.Phase
{
	public class BattleInGamePhase : IBattlePhase {

		public void OnInit (){
		}
		public void OnUpdate(){

			// WAVEの実行
			BattleGlobal.Instance.WaveModel.UpdateByFrame ();

			// オブジェクト登録されているものを更新
			BattleGlobal.Instance.PlayerZakoGroup.UpdateByFrame ();
			BattleGlobal.Instance.EnemyZakoGroup.UpdateByFrame ();

			// 衝突判定
			BattleGlobal.Instance.CollisionMediater.Mediate();

			// オブジェクトの更新
			BattleGlobal.Instance.ObjectsUpdater.UpdateByFrame ();
		}
		public void OnFinish (){
		}
		public bool IsFinishPhase(){
			return false;
		}
		public BattlePhaseName NextPhase(){
			Debug.AssertFormat (false, "Not Implement");
			return BattlePhaseName.UnKnown;
		}
	}
}