using UnityEngine;
using UnityEngine.UI;
using System.Collections;

using Chocolate.Battle.System;

namespace Chocolate.Battle.Map.UI{

	public class BattleLifeUIView : MonoBehaviour {

		// Use this for initialization
		void Start () {
		
		}
		
		// Update is called once per frame
		void Update () {

		}
			
		public void SetCurrentHP( int currentHP, int maxHP ){

			var slider = GetComponent<Slider> ();

			Debug.Assert ( slider != null );

			// 最大HPを設定
			slider.maxValue = maxHP;

			// 今のHPを設定
			slider.value = currentHP;

		}

	}
}
