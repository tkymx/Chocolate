using UnityEngine;
using System.Collections;

using Chocolate.Battle.System;

namespace Chocolate.Battle.System.Phase
{
	public interface IBattlePhase {
		void OnInit ();
		void OnUpdate();
		void OnFinish ();
		bool IsFinishPhase();
		BattlePhaseName NextPhase();
	}
}