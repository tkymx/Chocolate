
using UnityEngine;
using Chocolate.Battle.System;
using Chocolate.Battle.Object.Model;
using Chocolate.Battle.Collision.Model;
using Chocolate.Battle.Collision;
using Chocolate.Battle.Zako;

namespace Chocolate.Battle.Champion.Model
{
	public class ChampionDirectorModel : DirectorModel {

		public ChampionDirectorModel() : base() {


		}

		private readonly static float AttackIntervalTIme = 1;
		private float attackTime = 0;


		// 生成時に呼ばれる初期化
		public override void Initialize(){

			base.Initialize ();
		}

		public override void UpdateByFrame(){

			base.UpdateByFrame ();

			AttackZako ();
		}

		private void AttackZako(){

			// 一定時間は攻撃できない

			attackTime += Time.deltaTime;
			if (attackTime < AttackIntervalTIme) {
				return;
			}
			attackTime = 0;

			// ここもやっぱり共通化しないといけないな

			if (targetModel.Life.IsDead ()) {
				return;
			}

			var targetCollision = targetModel.View.GetCollision ();

			// 攻撃をする判定を行う範囲は広くする
			targetCollision.Scale( 2.0f );

			foreach (var zako in GetOppositeZakoGroup().Zakos ) {

				var enemyCollision = zako.View.GetCollision ();

				// 近くにいたら
				if (targetCollision.IsIn (enemyCollision)) {

					// 攻撃のコリジョンを作成 ( この辺は別のモデルで行いたい )

					var attackCollision = new CollisionModel (
						targetModel.View.RootTransform.GetRadius (),
						targetModel.View.RootTransform.GetPosition () + targetModel.View.Direction.normalized * targetModel.View.RootTransform.GetRadius ()
					);

					// 攻撃方向をむく

//					targetModel.View.Direction = enemyCollision.Position - targetModel.View.RootTransform.GetColliderPosition ();

					// 攻撃命令をメディエイターに送る

					BattleGlobal.Instance.CollisionMediater.RequestPlayerChampionAttack (
						new AttackParameter (targetModel, attackCollision, 30 /* TODO : とりあえずこの値 */)
					);						

					return;
				}
			}
		}

		protected virtual BattleZakoGroup GetOppositeZakoGroup(){

			Debug.Assert ( false );

			return null;
		}
	}
}