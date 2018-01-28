
using UnityEngine;
using Chocolate.Battle.Object.Model;
using Chocolate.Battle.System;
using Chocolate.Battle.Character.Model;
using Chocolate.Battle.Zako.Model;

namespace Chocolate.Battle.Bettery.Model
{
	public class BattleBetteryModel : BattleObjectModel {

		protected BattleBetteryModel(
			BattleViewModel view,
			DirectorModel directer
		) : base(view,directer) {
		}

		public static BattleBetteryModel CreateBettery( BattleViewModel view , DirectorModel director ){

			BattleBetteryModel betteryModel = new BattleBetteryModel (view, director);
			betteryModel.SetInternalRefference ();

			// オブジェクトを追加
			BattleGlobal.Instance.ObjectsUpdater.AddObject ( betteryModel );

			return betteryModel;
		}


	}
}