using UnityEngine;
using UnityEngine.UI;
using System.Collections;

using Chocolate.Battle.System;

namespace Chocolate.Battle.Map.UI{
	
	public class BattleWaveTimeView : MonoBehaviour
	{

		public void SetCurrentTime( int currentTime, int maxTime ){

			var slider = GetComponent<Slider> ();

			Debug.Assert ( slider != null );

			// 最大HPを設定
			slider.maxValue = maxTime;

			// 今のHPを設定
			slider.value = currentTime;

		}
	}
}

