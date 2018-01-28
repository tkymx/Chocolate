using UnityEngine;
using Chocolate.Battle.System.Phase;

namespace Chocolate.Battle.System
{
	public enum BattlePhaseName{
		UnKnown,
		PreLoad,
		InGame,
		Finish
	};

	public class BattlePhaseMover {

		private BattlePhaseName currentPhaseName = BattlePhaseName.UnKnown;
		private IBattlePhase currentPhase = null;

		private BattlePreLoadParameter CreatePreLoadParameterTest(){

			BattlePreLoadParameter parameter = new BattlePreLoadParameter ();
			parameter.zakoPrefab = Resources.Load ( "Battle/Zako/Zako0" ) as GameObject;
			parameter.zakoBigPrefab = Resources.Load ( "Battle/Zako/Zako0Big" ) as GameObject;
			parameter.zakoEnemyPrefab = Resources.Load ( "Battle/Zako/Zako1" ) as GameObject;
			parameter.zakoBigEnemyPrefab = Resources.Load ( "Battle/Zako/Zako1Big" ) as GameObject;
			parameter.playerChampionPrefab = Resources.Load ( "Battle/Champion/Champion0" ) as GameObject;
			parameter.enemyChampionPrefab = Resources.Load ( "Battle/Champion/Champion0" ) as GameObject;
			parameter.betteryPrefab = Resources.Load ( "Battle/Bettery/Bettery0" ) as GameObject;
			parameter.mapPrefab = Resources.Load ( "Battle/Stadium/Stadium0" ) as GameObject;
			parameter.castlePrefab = Resources.Load ( "Battle/Castle/Castle0" ) as GameObject;
			parameter.targetPrefab = Resources.Load ( "Battle/System/Target" ) as GameObject;

			Debug.Assert (parameter.zakoPrefab != null, "zakoPrefab がありません"  );
			Debug.Assert (parameter.betteryPrefab != null, "betteryPrefab がありません"  );
			Debug.Assert (parameter.mapPrefab != null, "mapPrefab がありません"  );
			Debug.Assert (parameter.castlePrefab != null, "castlePrefab がありません"  );

			return parameter;
		}

		private void ChangePhase( BattlePhaseName phaseName ){

			if (currentPhaseName == phaseName) {
				return;
			}
			currentPhaseName = phaseName;

			if (currentPhase != null) {
				currentPhase.OnFinish ();
			}

			if (phaseName == BattlePhaseName.PreLoad) {
				currentPhase = new BattlePreLoadPhase ( CreatePreLoadParameterTest() );
			}
			else if (phaseName == BattlePhaseName.InGame) {
				currentPhase = new BattleInGamePhase ();
			}

			currentPhase.OnInit ();

			Debug.Log ("Success Change Scene To " + currentPhaseName.ToString() );
		} 

		public BattlePhaseMover(){
			ChangePhase ( BattlePhaseName.PreLoad );
		}			

		public void UpdateByFrame(){

			if (currentPhase != null) {

				if (currentPhase.IsFinishPhase ()) {
					ChangePhase ( currentPhase.NextPhase() );
				}

				currentPhase.OnUpdate ();
			}

		}
	}
}