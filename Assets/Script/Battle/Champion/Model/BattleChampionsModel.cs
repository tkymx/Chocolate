using UnityEngine;
using System.Collections;

using Chocolate.Battle.Character.Model;

namespace Chocolate.Battle.Champion.Model{
	public class BattleChampionsModel {

		private BattleCharacterModel playerChampion = null;
		public BattleCharacterModel PlayerChampion{
			get{ Debug.Assert ( playerChampion != null ); return playerChampion; }
		}

		private BattleCharacterModel enemyChampion = null;
		public BattleCharacterModel EnemyChampion{
			get{ Debug.Assert ( enemyChampion != null ); return enemyChampion; }
		}
			
		public BattleChampionsModel(){
		}

		// public -------------------------

		// プレイヤーのチャンピオンをセットする
		public void SetPlayerChampion( BattleCharacterModel character ){
			playerChampion = character;
		}

		// 敵ののチャンピオンをセットする
		public void SetEnemyChampion( BattleCharacterModel character ){
			enemyChampion = character;
		}

	}
}