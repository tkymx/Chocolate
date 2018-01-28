using UnityEngine;
using System.Collections;
using Chocolate.Battle.Object.Model;
using Chocolate.Battle.Zako.State;
using Chocolate.Battle.Collision.Model;
using Chocolate.Battle.Collision;
using Chocolate.Battle.Area.Model;
using Chocolate.Battle.System;

namespace Chocolate.Battle.Zako.Model
{
	public class EmittingDirectorParamater : DirectorParameter{

		// 座標
		public Vector3 Position{get;set;}

	}

	public class BaseZakoDirectorModel : DirectorModel {

		// いまはここにマジックナンバーがあるが、そのうち攻撃のパラメータいしたい
		private readonly float attackIntervalLimitTime = 1;
		private float attackIntervalTime = 0;

		public BaseZakoDirectorModel() : base() 
		{
		}

		public override void Initialize()
		{
			base.Initialize ();
		}
			
		public override void UpdateByFrame()
		{
			base.UpdateByFrame ();

			if (targetModel.Life.IsDead() ) {

				// 死亡時に玉を出す
				GetEnemyArea().Bettery.ReciveMessage ( "Burn" , null );

				// 消去可能にする
				targetModel.IsDelete = true;

				return;
			}

			// エリア内でコリジョンを持っている時、つまり移動中の時
			if (targetModel.View.IsCollisionEnable) {

				// 攻撃のためのインターバル時間を経過
				attackIntervalTime += Time.deltaTime;

				// 攻撃のタイミングになったら
				if (attackIntervalTime > attackIntervalLimitTime) {

					// はみ出していたら
					if (GetEnemyArea().IsNearWall (targetModel)) {

						// 攻撃のコリジョンを作成 ( この辺は別のモデルで行いたい )

						CollisionModel attackCollision = new CollisionModel (
							targetModel.View.RootTransform.GetRadius (),
							targetModel.View.RootTransform.GetPosition () + targetModel.View.Direction.normalized * targetModel.View.RootTransform.GetRadius ()
						);

						// 攻撃命令をメディエイターに送る

						BattleGlobal.Instance.CollisionMediater.RequestPlayerAttack (
							new AttackParameter (targetModel, attackCollision, 1 /* TODO : とりあえずこの値 */)
						);
					}

					attackIntervalTime = 0;
				}
			}
		}

		public override void ReciveMessage( string message , DirectorParameter param = null )
		{
			base.ReciveMessage (message, param);

			// 放出を検出
			if (message == "EmittingFromBettery") {

				// パラメータを取得
				var emittingParameter = param as EmittingDirectorParamater;

				Debug.Assert (emittingParameter != null);

				// 到着地点を決めて移動させる
				Vector3 position = emittingParameter.Position;

				// 放出開始
				targetModel.StateController.ChangeState( new ZakoEmittingState( position ) );

			}

			// 放出の終了
			if (message == "EndEmitting") {

				// 城の地点を決めて移動させる
				Vector3 position = GetTargetBasePosition();


				// 城に移動モードにする
				targetModel.StateController.ChangeState( new ZakoMoveBaseState( position ) );

			}

		}


		protected virtual BattleAreaModel GetEnemyArea(){

			Debug.Assert (false);

			return null;
		}


		protected virtual Vector3 GetTargetBasePosition(){

			Debug.Assert (false);

			return new Vector3 ();
		}

	}
}