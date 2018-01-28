using System;
using UnityEngine;
using Chocolate.Battle.Object.Model;
using Chocolate.Battle.System;

namespace Chocolate.Battle.Wave.Model
{
	public class BattleWaveModel
	{
		float waveElapsedTime = 0;
		public float WaveElapsedTime{
			get { return waveElapsedTime; }
		}

		float waveIntervalTime = 20;
		public float WaveIntervalTime{
			get { return waveIntervalTime; }
		}

		public BattleWaveModel ()
		{
			waveElapsedTime = waveIntervalTime;
		}

		public void UpdateByFrame()
		{
			waveElapsedTime += Time.deltaTime;
			if (waveElapsedTime > waveIntervalTime) {

				waveElapsedTime = 0;

				// ウェーブ開始エフェクト


				// ウェーブする。
				BattleGlobal.Instance.PlayerAreaModel.GoToNextWave ();
				BattleGlobal.Instance.EnemyAreaModel.GoToNextWave ();
			}
		}
	}
}

