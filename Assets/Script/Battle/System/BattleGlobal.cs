
using UnityEngine;

using Chocolate.Battle.Object;
using Chocolate.Battle.Map.Model;
using Chocolate.Battle.Area.Model;
using Chocolate.Battle.Character.Model;
using Chocolate.Battle.Zako;
using Chocolate.Battle.Collision;
using Chocolate.Battle.Champion.Model;
using Chocolate.Battle.System.Factory;
using Chocolate.Battle.Wave.Model;

namespace Chocolate.Battle.System
{
	public class BattleGlobal {

		// 定義 --------------------------------

		private static BattleGlobal instance = null;
		public static BattleGlobal Instance{
			get{
				if (instance == null) {
					instance = new BattleGlobal ();
				}
				return instance;
			}
		}

		// 敵のエリア
		private BattleAreaModel enemyAreaModel = null;
		public BattleAreaModel EnemyAreaModel{
			get{ Debug.Assert (enemyAreaModel != null); return enemyAreaModel; }
		}

		// 味方のエリア
		private BattleAreaModel playerAreaModel = null;
		public BattleAreaModel PlayerAreaModel{
			get{ Debug.Assert (playerAreaModel != null); return playerAreaModel; }
		}

		// 敵のの雑魚
		private BattleZakoGroup enemyZakoGroup = null;
		public BattleZakoGroup EnemyZakoGroup{
			get{ Debug.Assert (enemyZakoGroup != null); return enemyZakoGroup; }
		}
			
		// 味方の雑魚
		private BattleZakoGroup playerZakoGroup = null;
		public BattleZakoGroup PlayerZakoGroup{
			get{ Debug.Assert (playerZakoGroup != null); return playerZakoGroup; }
		}
			
		// オブジェクトの情報( そのうちキャラクタだけにしたい )
		private BalleObjectsUpdater objectsUpdater = null;
		public BalleObjectsUpdater ObjectsUpdater{
			get{ Debug.Assert (objectsUpdater != null); return objectsUpdater; }
		}

		// 当たり判定関連を行う
		private BattleCollisionMediater collisionMediater = null;
		public BattleCollisionMediater CollisionMediater{
			get{ Debug.Assert (collisionMediater != null); return collisionMediater;}
		}
			
		// ファクトリー
		private BattleFactory factory = null;
		public BattleFactory Factory{
			get{ Debug.Assert (factory != null); return factory;}
		}

		// チャンピオン
		private BattleChampionsModel champions = null;
		public BattleChampionsModel Champions{
			get{ Debug.Assert (champions != null); return champions; }
		}

		// ターゲット情報
		private BattleMovableTargetModel movableTarget = null;
		public BattleMovableTargetModel MovableTarget{
			get{ Debug.Assert (movableTarget != null); return movableTarget; }
		}

		// WAVE
		private BattleWaveModel waveModel = null;
		public BattleWaveModel WaveModel{
			get{ Debug.Assert (waveModel != null); return waveModel; }
		}

		// コンストラクタ --------------------------------

		public BattleGlobal(){
			objectsUpdater = new BalleObjectsUpdater();
			enemyZakoGroup = new BattleZakoGroup ();
			playerZakoGroup = new BattleZakoGroup ();
			collisionMediater = new BattleCollisionMediater ();
			factory = new BattleFactory ();
			champions = new BattleChampionsModel ();
			waveModel = new BattleWaveModel ();
		}


		// メソッド --------------------------------

		// ゲームの各エリアを設定するが。。ここでいいのか？
		public void Initialize( 
			BattleAreaModel player , 
			BattleAreaModel enemy ,
			BattleMovableTargetModel movableTarget
		){

			this.playerAreaModel = player;
			this.enemyAreaModel = enemy;
			this.movableTarget = movableTarget;
		}
	}
}
