using UnityEngine;
using System.Collections;
using Chocolate.Battle.System;

namespace Chocolate.Battle.Scene
{
	public class BattleScene : MonoBehaviour {

		private BattlePhaseMover phaseMover = null;

		// Use this for initialization
		void Start () {

			phaseMover = new BattlePhaseMover();
		}
		
		// Update is called once per frame
		void Update () {

			if (phaseMover != null) {				
				phaseMover.UpdateByFrame ();
			}	
		}
	}	
}

