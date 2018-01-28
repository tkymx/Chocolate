using UnityEngine;
using System.Collections;

namespace Chocolate.Battle.Object.Model
{
	public class BattleLifeModel {

		private float maxHp = 0;
		public float MaxHP{
			get{ return maxHp; }
		}

		private float currentHP = 0;
		public float CurrentHP{
			get{ return currentHP; }
		}

		public BattleLifeModel( ){

			this.maxHp = 0;
			this.currentHP = 0;
		}

		// 最大HPの設定
		public void SetMaxHP( float maxHp ){

			this.maxHp = maxHp;

			Reset ();
		}

		// HPのリセットを行う
		public void Reset(){

			// HPをリセット
			currentHP = maxHp;
		}

		// ダメージの適応を行う
		public void ApplyDamage( float damage ){

			// HPを減らす
			this.currentHP -= damage;

			if (this.currentHP < 0) {

				this.currentHP = 0;
			}
		}

		// 死んでいるかどうか
		public bool IsDead(){

			return currentHP <= 0;
		}
	}
}