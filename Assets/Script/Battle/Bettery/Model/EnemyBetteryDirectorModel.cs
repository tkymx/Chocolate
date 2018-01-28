
using UnityEngine;
using System.Collections.Generic;
using Chocolate.Battle.Object.Model;
using Chocolate.Battle.System;
using Chocolate.Battle.Character.Model;
using Chocolate.Battle.Zako.Model;

namespace Chocolate.Battle.Bettery.Model
{
	public class EnemyBetteryDirectorModel : BaseBetteryDirectorModel 
	{
		public override void OnBurn()
		{
			if (currentSettdBollType == "Zako") { 
				
				BurnInternal (
					targetModel.View.RootTransform.GetPosition (), 
					BattleGlobal.Instance.PlayerAreaModel.CalcRandomPointInArea (),
					BattleGlobal.Instance.Factory.CreateEnemyZako ()
				);		
			} else if (currentSettdBollType == "ZakoBig") {
				
				BurnInternal ( 
					targetModel.View.RootTransform.GetPosition(), 
					BattleGlobal.Instance.PlayerAreaModel.CalcRandomPointInArea(),
					BattleGlobal.Instance.Factory.CreateEnemyZakoBig()
				);		
			}
		}
	}
}