
using UnityEngine;
using System.Collections.Generic;
using Chocolate.Battle.System;
using Chocolate.Battle.Object.Model;
using Chocolate.Battle.Character.Model;
using Chocolate.Battle.Collision.Model;
using Chocolate.Battle.Area.Model;

namespace Chocolate.Battle.Collision
{
	public class AttackParameter{

		// 攻撃者情報
		public IBattleObjectModel fromModel;

		// 攻撃のあたり情報
		public CollisionModel TargetCollision{get;set;}

		// 攻撃時のダメージ情報
		public float DamageValue{ get; set;}

		public AttackParameter( 
			IBattleObjectModel fromModel,
			CollisionModel targetCollision, 
			float damageValue ){

			this.fromModel = fromModel;
			this.TargetCollision = targetCollision;
			this.DamageValue = damageValue;
		}
	}

	public class BattleCollisionMediater{

		// 当たり判定による押し出し情報------------------------------------

		private readonly static float EnemyCollisionSmooth = 0.5f;  
		private readonly static float MapCollisionSmooth = 1.0f;  
		private readonly static float ChampionCollisionSmooth = 1.0f;  
		 
		// 定数関連 ------------------------------------

		private List< AttackParameter > requestPlayerAttackParameters = new List<AttackParameter>();

		private List< AttackParameter > requestPlayerChampionAttackParameters = new List<AttackParameter>();

		// public ------------------------------------

		// プレイヤーの雑魚の攻撃
		public void RequestPlayerAttack( AttackParameter attackParameter ){

			// 攻撃情報の追加
			requestPlayerAttackParameters.Add ( attackParameter );

		}

		// プレイヤーのチャンピオンの攻撃
		public void RequestPlayerChampionAttack( AttackParameter attackParameter ){

			// 攻撃情報の追加
			requestPlayerChampionAttackParameters.Add ( attackParameter );

		}

		public void Mediate(){

			// TODO : とりあえず書いているが、そのうちルーチン化は絶対する

			// 敵エリアとの当たり判定
			{
				var enemyAreaCollision = BattleGlobal.Instance.EnemyAreaModel.Map.View.GetCollision ();
				var playerAreaCollision = BattleGlobal.Instance.PlayerAreaModel.Map.View.GetCollision ();

				// 攻撃時のダメージ判定情報------------------------------------

				// 敵のエリアと味方のダメージ判定
				foreach (var attackParameter in requestPlayerAttackParameters) {

					// 壁との当たり判定（現状はマップとの当たり判定で代用、本当は壁にコリジョンをもたせたい ）
					if (!attackParameter.TargetCollision.IsIn (enemyAreaCollision)) {
						continue;
					}

					// 壁にダメージを与える
					ApplyDamage(BattleGlobal.Instance.EnemyAreaModel.Map.Life, attackParameter);

					// ダメージエフェクトをつけたい ( まあ、なんとか、攻撃しているが。。。って感じ ) 
					GameObject prefab = Resources.Load<GameObject>( "Particle/AttackWall" );
					GameObject instance = GameObject.Instantiate ( prefab );
					instance.transform.position = attackParameter.TargetCollision.Position;

				}

				// 味方のエリアと敵のダメージ判定
				foreach (var attackParameter in requestPlayerAttackParameters) {

					// 壁との当たり判定（現状はマップとの当たり判定で代用、本当は壁にコリジョンをもたせたい ）
					if (!attackParameter.TargetCollision.IsIn (playerAreaCollision)) {
						continue;
					}

					// 壁にダメージを与える
					ApplyDamage(BattleGlobal.Instance.PlayerAreaModel.Map.Life, attackParameter);

					// ダメージエフェクトをつけたい ( まあ、なんとか、攻撃しているが。。。って感じ ) 
					GameObject prefab = Resources.Load<GameObject>( "Particle/AttackWall" );
					GameObject instance = GameObject.Instantiate ( prefab );
					instance.transform.position = attackParameter.TargetCollision.Position;
				}
			}

			// 敵の雑魚と味方のチャンピオン
			{
				foreach (var attackParameter in requestPlayerChampionAttackParameters) {

					foreach (var zako in BattleGlobal.Instance.EnemyZakoGroup.Zakos) {

						// 敵との当たり判定
						if (!attackParameter.TargetCollision.IsIn (zako.View.GetCollision ())) {
							continue;
						}

						// 敵にダメージを与える
						ApplyDamage(zako.Life, attackParameter);

						// ダメージエフェクトをつけたい ( まあ、なんとか、攻撃しているが。。。って感じ ) 
						GameObject prefab = Resources.Load<GameObject>( "Particle/Attack" );
						GameObject instance = GameObject.Instantiate ( prefab );
						instance.transform.position = attackParameter.TargetCollision.Position;

					}

					foreach (var zako in BattleGlobal.Instance.PlayerZakoGroup.Zakos) {

						// 敵との当たり判定
						if (!attackParameter.TargetCollision.IsIn (zako.View.GetCollision ())) {
							continue;
						}

						// 敵にダメージを与える
						ApplyDamage(zako.Life, attackParameter);

						// ダメージエフェクトをつけたい ( まあ、なんとか、攻撃しているが。。。って感じ ) 
						GameObject prefab = Resources.Load<GameObject>( "Particle/Attack" );
						GameObject instance = GameObject.Instantiate ( prefab );
						instance.transform.position = attackParameter.TargetCollision.Position;

					}
				}
			}

			// 初期化
			requestPlayerAttackParameters.Clear();
			requestPlayerChampionAttackParameters.Clear ();

			// 雑魚どおしの当たり判定------------------------------------

			// 雑魚とチャンピオンの当たり判定
			ExtrusionZakoFromChampion (
				BattleGlobal.Instance.Champions.PlayerChampion,
				BattleGlobal.Instance.EnemyZakoGroup.Zakos
			);

			ExtrusionZakoFromChampion (
				BattleGlobal.Instance.Champions.EnemyChampion,
				BattleGlobal.Instance.PlayerZakoGroup.Zakos
			);

			ExtrusionZakoFromZako ( BattleGlobal.Instance.PlayerZakoGroup.Zakos );
			ExtrusionZakoFromZako ( BattleGlobal.Instance.EnemyZakoGroup.Zakos );

			// 敵のエリアの当たり判定------------------------------------

			ExtrusionZakoFromArea( 
				BattleGlobal.Instance.EnemyAreaModel, 
				BattleGlobal.Instance.PlayerZakoGroup.Zakos );

			ExtrusionZakoFromArea( 
				BattleGlobal.Instance.PlayerAreaModel, 
				BattleGlobal.Instance.EnemyZakoGroup.Zakos );

		}

		private void ApplyDamage (BattleLifeModel life, AttackParameter attackParameter)
		{
			life.ApplyDamage (attackParameter.DamageValue);
			attackParameter.fromModel.ReciveMessage ("receive_damage");
		}

		private void ExtrusionZakoFromZako( List<BattleCharacterModel> models ){

			for( int iTarget = 0; iTarget < models.Count ; iTarget++ )
			{
				var iModel = models [iTarget];
				var iCollision = iModel.View.GetCollision ();

				// 衝突判定を行わない場合はしない
				if (!iModel.View.IsCollisionEnable) {
					continue;
				}

				for( int jTarget = iTarget+1 /*重なり削減*/ ; jTarget < models.Count ; jTarget++ )
				{

					var jModel = models [jTarget];
					var jCollision = jModel.View.GetCollision ();

					// 衝突判定を行わない場合はしない
					if (!jModel.View.IsCollisionEnable) {
						continue;
					}

					if (iCollision.IsIn (jCollision)) {

						// 雑魚中心からエリア中心までのベクトル
						Vector3 vecFromZakoToZako = iCollision.Position - jCollision.Position;

						// 雑魚中心からエリア中心までの距離
						float lengthFromZakoToZako = (vecFromZakoToZako).magnitude;

						// 制限距離
						float limitedLength = iCollision.Radius + jCollision.Radius;

						// めり込み距離
						float hamidashi = limitedLength - lengthFromZakoToZako;

						// はみ出しの解除
						iModel.View.RootTransform.SetPosition ( iModel.View.RootTransform.GetPosition() + vecFromZakoToZako.normalized * hamidashi * EnemyCollisionSmooth );
						jModel.View.RootTransform.SetPosition ( jModel.View.RootTransform.GetPosition() - vecFromZakoToZako.normalized * hamidashi * EnemyCollisionSmooth );

					}

				}
			}

		}

		private void ExtrusionZakoFromArea( BattleAreaModel area, List<BattleCharacterModel> models ){

			if (area.Map.Life.IsDead ()) {
				return;
			}

			var areaCollision = area.Map.View.GetCollision ();

			foreach (var zako in models ) {

				// 衝突判定を行わない場合はしない
				if (!zako.View.IsCollisionEnable) {
					continue;
				}

				var zakoCollision = zako.View.GetCollision ();

				if (areaCollision.IsHamidashi (zakoCollision)) {

					// 雑魚中心からエリア中心までのベクトル
					Vector3 vecFromAreaToZako = areaCollision.Position - zakoCollision.Position;

					// 雑魚中心からエリア中心までの距離
					float lengthFromAreaToZako = (vecFromAreaToZako).magnitude;

					// 制限距離
					float limitedLength = areaCollision.Radius - zakoCollision.Radius;

					// はみ出し距離
					float hamidashi = lengthFromAreaToZako - limitedLength;

					// はみ出しの解除
					zako.View.RootTransform.SetPosition ( zako.View.RootTransform.GetPosition() + vecFromAreaToZako.normalized * hamidashi * MapCollisionSmooth );

				}
			}

		}

		private void ExtrusionZakoFromChampion( IBattleObjectModel champion, List<BattleCharacterModel> models ){

			var iModel = champion;
			var iCollision = iModel.View.GetCollision ();

			// 衝突判定を行わない場合はしない
			if (!iModel.View.IsCollisionEnable) {
				return;
			}

			for( int jTarget = 0 ; jTarget < models.Count ; jTarget++ )
			{

				var jModel = models [jTarget];
				var jCollision = jModel.View.GetCollision ();

				// 衝突判定を行わない場合はしない
				if (!jModel.View.IsCollisionEnable) {
					continue;
				}

				if (iCollision.IsIn (jCollision)) {

					// 雑魚中心からエリア中心までのベクトル
					Vector3 vecFromZakoToZako = iCollision.Position - jCollision.Position;

					// 雑魚中心からエリア中心までの距離
					float lengthFromZakoToZako = (vecFromZakoToZako).magnitude;

					// 制限距離
					float limitedLength = iCollision.Radius + jCollision.Radius;

					// めり込み距離
					float hamidashi = limitedLength - lengthFromZakoToZako;

					// はみ出しの解除
					iModel.View.RootTransform.SetPosition ( iModel.View.RootTransform.GetPosition() + vecFromZakoToZako.normalized * hamidashi * ( 1.0f - ChampionCollisionSmooth ) );
					jModel.View.RootTransform.SetPosition ( jModel.View.RootTransform.GetPosition() - vecFromZakoToZako.normalized * hamidashi * ChampionCollisionSmooth );

				}

			}
		}
	}
}