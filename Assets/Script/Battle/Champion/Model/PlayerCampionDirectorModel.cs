using UnityEngine;
using System.Collections;
using Chocolate.Battle.Object.Model;
using Chocolate.Battle.Zako.State;
using Chocolate.Battle.Champion.State;
using Chocolate.Battle.System;
using Chocolate.Battle.Collision.Model;
using Chocolate.Battle.Collision;
using Chocolate.Battle.Zako;
using Chocolate.Battle.Object.State;

namespace Chocolate.Battle.Champion.Model
{
	public class PlayerChampionDirectorModel : ChampionDirectorModel {

		public PlayerChampionDirectorModel() : base() {

		}

		// 生成時に呼ばれる初期化
		public override void Initialize(){

			base.Initialize ();

			targetModel.StateController.ChangeState ( new ChampionKeyMoveState()  );
		}

		protected override BattleZakoGroup GetOppositeZakoGroup(){

			return BattleGlobal.Instance.EnemyZakoGroup;
		}

	}

}