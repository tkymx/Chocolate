using UnityEngine;
using System.Collections;
using Chocolate.Battle.Object.Model;
using Chocolate.Battle.Zako.State;
using Chocolate.Battle.Champion.State;
using Chocolate.Battle.System;
using Chocolate.Battle.Collision.Model;
using Chocolate.Battle.Collision;
using Chocolate.Battle.Zako;


namespace Chocolate.Battle.Champion.Model
{
	public class EnemyChampionDirectorModel : ChampionDirectorModel {

		public EnemyChampionDirectorModel() : base() {

		}

		// 生成時に呼ばれる初期化
		public override void Initialize(){

			base.Initialize ();

			targetModel.StateController.ChangeState ( new ChampionNearZakoMoveState() );
		}

		protected override BattleZakoGroup GetOppositeZakoGroup(){

			return BattleGlobal.Instance.PlayerZakoGroup;
		}

	}

}