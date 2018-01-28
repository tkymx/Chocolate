
using UnityEngine;
using Chocolate.Battle.Object.Model;
using Chocolate.Battle.System;
using Chocolate.Battle.Character.Model;
using Chocolate.Battle.Zako.Model;

namespace Chocolate.Battle.Bettery.Model
{
	public class PlayerBetteryDirectorModel : BaseBetteryDirectorModel 
	{
		public override void OnBurn()
		{
			if (currentSettdBollType == "Zako") { 

				BurnInternal (
					targetModel.View.RootTransform.GetPosition (), 
					BattleGlobal.Instance.EnemyAreaModel.CalcRandomPointInArea (),
					BattleGlobal.Instance.Factory.CreatePlayerZako ()
				);
				
			} else if (currentSettdBollType == "ZakoBig") {			
				
				BurnInternal (
					targetModel.View.RootTransform.GetPosition (), 
					BattleGlobal.Instance.EnemyAreaModel.CalcRandomPointInArea (),
					BattleGlobal.Instance.Factory.CreatePlayerZakoBig ()
				);	
			}
		}
	}
}