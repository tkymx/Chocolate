using UnityEngine;
using System.Collections;
using Chocolate.Battle.Object.Model;
using Chocolate.Battle.Zako.State;
using Chocolate.Battle.System;
using Chocolate.Battle.Collision.Model;
using Chocolate.Battle.Collision;
using Chocolate.Battle.Area.Model;

namespace Chocolate.Battle.Zako.Model
{
	public class PlayerZakoDirectorModel : BaseZakoDirectorModel {

		public PlayerZakoDirectorModel() : base() {
		}

		protected override Vector3 GetTargetBasePosition(){
			return new Vector3(0,0.5f ,8 );
		}

		public override void UpdateByFrame(){

			base.UpdateByFrame ();


		}

		protected override BattleAreaModel GetEnemyArea(){
			return BattleGlobal.Instance.EnemyAreaModel;
		}

	}
}