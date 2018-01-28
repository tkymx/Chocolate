using UnityEngine;
using System.Collections;

using Chocolate.Battle.Object.Model;
using Chocolate.Battle.Collision.Model;
using Chocolate.Battle.System;
using Chocolate.Battle.Collision;
using Chocolate.Data.Character.Model;

namespace Chocolate.Battle.Character.Model
{
	public class BattleSkillModel {

		private SkillModel skillModel;
		public SkillModel Skill {
			get{return skillModel;}
		}

		private int rankUpPoint = 5; 
		public int RankUpPoint{
			get { return rankUpPoint; }
		}

		private int currentSkillRank = 0;
		public int CurrentSkillRank{
			get { return currentSkillRank; }
		}

		private BattleCharacterModel targetModel = null;

		public BattleSkillModel( BattleCharacterModel targetModel, BattleSkillsModel.SlotNumber slotNumber ){

			this.targetModel = targetModel;
			this.currentSkillRank = 1;
			this.skillModel = targetModel.Character.GetSkillModel ((uint)slotNumber);
		}

		public bool CanRankUp(int currentSkillPoint)
		{
			return currentSkillPoint >= rankUpPoint;
		}

		/// <summary>
		/// スキルのランクをアップする
		/// </summary>
		public void RankUp()
		{
			currentSkillRank++;
			rankUpPoint = rankUpPoint * 2;
		}

		/// <summary>
		/// スキルを発動する
		/// </summary>
		public void Invoke()
		{
			// 今はスキル情報がないので特定のスキルをつける
			// 今後はスキル定義を読み込むようにしたい

			switch (skillModel.Type) {
			case "Line":
				InvokeLineAttack ();
				break;
			case "Range":
				InvokeRangeAttack ();
				break;
			case "Summon":
				InvokeSummonAttack ();
				break;
			default:
				Debug.AssertFormat (false, "そのスキルのタイプは存在しません");
				break;
			}
		}

		private void InvokeLineAttack() {

			// 攻撃のコリジョンを作成 ( この辺は別のモデルで行いたい )
			for (int i = 0; i < currentSkillRank; i++) {

				var attackCollision = new CollisionModel (
					targetModel.View.RootTransform.GetRadius () * 2,
					targetModel.View.RootTransform.GetPosition () + targetModel.View.Direction.normalized * targetModel.View.RootTransform.GetRadius () * (i*2)
				);

				// 攻撃命令をメディエイターに送る

				BattleGlobal.Instance.CollisionMediater.RequestPlayerChampionAttack (
					new AttackParameter (targetModel, attackCollision, 50)
				);

				// ダメージエフェクトをつけたい ( まあ、なんとか、攻撃しているが。。。って感じ ) 
				GameObject prefab = Resources.Load<GameObject>( "Particle/Skill" );
				GameObject instance = GameObject.Instantiate ( prefab );
				instance.transform.position = attackCollision.Position;

			}			
		}

		private void InvokeRangeAttack() {

			var attackCollision = new CollisionModel (
				targetModel.View.RootTransform.GetRadius () * ( 2 + currentSkillRank * 0.5f ),
	            targetModel.View.RootTransform.GetPosition () + targetModel.View.Direction.normalized * targetModel.View.RootTransform.GetRadius () * (2)
	        );

			// 攻撃命令をメディエイターに送る

			BattleGlobal.Instance.CollisionMediater.RequestPlayerChampionAttack (
				new AttackParameter (targetModel, attackCollision, 50 * currentSkillRank)
			);

			// ダメージエフェクトをつけたい ( まあ、なんとか、攻撃しているが。。。って感じ ) 
			GameObject prefab = Resources.Load<GameObject>( "Particle/Skill" );
			GameObject instance = GameObject.Instantiate ( prefab );
			instance.transform.position = attackCollision.Position;
			instance.transform.localScale = new Vector3 (1, 1, 1) * (0.5f + currentSkillRank * 0.5f);
		}

		private void InvokeSummonAttack() {

			// そのうちオプションなどをつけて放ちたい

			BattleGlobal.Instance.PlayerAreaModel.Bettery.ReciveMessage ("BurningBig");		
		}

	}
}
