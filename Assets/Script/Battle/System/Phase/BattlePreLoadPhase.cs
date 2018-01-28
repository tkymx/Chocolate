
using UnityEngine;

namespace Chocolate.Battle.System.Phase
{
	// 画面遷移時のバトル情報が入ってい予定
	// TODO : これから内容を充実化させる
	public class BattlePreLoadParameter{

		// 雑魚のプレファブ
		public GameObject zakoPrefab;

		// 雑魚のプレファブ
		public GameObject zakoBigPrefab;

		// 雑魚のプレファブ
		public GameObject zakoEnemyPrefab;

		// でかい雑魚のプレファブ
		public GameObject zakoBigEnemyPrefab;

		// 雑魚のプレファブ
		public GameObject playerChampionPrefab;

		// 雑魚のプレファブ
		public GameObject enemyChampionPrefab;

		// 砲台のプレファブ
		public GameObject betteryPrefab;

		// エリアのプレファブ
		public GameObject mapPrefab;

		// 城のプレファブ
		public GameObject castlePrefab;

		// ターゲットのプレファブ
		public GameObject targetPrefab;
	}

	public class BattlePreLoadPhase : IBattlePhase {

		private BattlePreLoadParameter preLoadParameter;

		public BattlePreLoadPhase( BattlePreLoadParameter preLoadParameter ){
			this.preLoadParameter = preLoadParameter;
		}
			
		public void OnInit (){

			// ファクトリーへパラメータを設定
			BattleGlobal.Instance.Factory.SetParameter ( preLoadParameter );

			// エリアの作成
			var playerArea = BattleGlobal.Instance.Factory.CreatePlayerArea ();
			var enemyArea = BattleGlobal.Instance.Factory.CreateEnemyArea ();

			// 味方のチャンピオンの生成
			var playerChampion = BattleGlobal.Instance.Factory.CreatePlayerChampion();
			playerChampion.View.RootTransform.SetPosition ( new Vector3( 0,0.5f,-2 ) );

			// 敵のチャンピオンの生成
			var enemyChampion = BattleGlobal.Instance.Factory.CreateEnemyChampion();
			enemyChampion.View.RootTransform.SetPosition ( new Vector3( 0,0.5f,2 ) );

			// ターゲットの作成
			var target = BattleGlobal.Instance.Factory.CreateMovableTarget();

			// BattleGlobalの初期化
			BattleGlobal.Instance.Initialize (
				playerArea,
				enemyArea,
				target
			);
		}
		public void OnUpdate(){
		}
		public void OnFinish (){
		}
		public bool IsFinishPhase(){
			return true;
		}
		public BattlePhaseName NextPhase(){
			return BattlePhaseName.InGame;
		}
			
	}
}