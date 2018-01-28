using UnityEngine;
using UnityEngine.UI;
using System.Collections;

using Chocolate.Battle.System;

namespace Chocolate.Battle.Map.UI{
	
	public class BattleUIPresenter : MonoBehaviour {

		[SerializeField]
		private BattleLifeUIView playerLifeUIView;

		[SerializeField]
		private BattleLifeUIView enemyLifeUIView;

		[SerializeField]
		private BattleWaveTimeView waveTimeView;

		// Use this for initialization
		void Start () {

		}

		// Update is called once per frame
		void Update () {
			
			// 敵と味方のエリア
			var enemyAreaLife = BattleGlobal.Instance.EnemyAreaModel.Map.Life;
			var playerAreaLife = BattleGlobal.Instance.PlayerAreaModel.Map.Life;

			Debug.Assert ( enemyAreaLife != null && enemyLifeUIView != null );
			Debug.Assert ( playerAreaLife != null && playerLifeUIView != null );

			enemyLifeUIView.SetCurrentHP ( (int)enemyAreaLife.CurrentHP , (int)enemyAreaLife.MaxHP );
			playerLifeUIView.SetCurrentHP ( (int)playerAreaLife.CurrentHP , (int)playerAreaLife.MaxHP );


			var waveTime = BattleGlobal.Instance.WaveModel;
			waveTimeView.SetCurrentTime ((int)waveTime.WaveElapsedTime, (int)waveTime.WaveIntervalTime);
		}
	}

}
