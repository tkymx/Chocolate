using UnityEngine;
using System.Collections;
using Chocolate.Battle.Object.Model;

namespace Chocolate.Battle.Object.State
{
	//状態の管理を行う
	// 状態は内部から変わることはなく、ディレクタいよって変更される

	public class StateController {

		// ターゲットのキャラクタキャラ生成時に PreLoaderで呼ばれる
		protected IBattleObjectModel targetModel = null;
		public void SetTargetModel( IBattleObjectModel targetModel ){
			this.targetModel = targetModel;
		}

		// 現在の除隊
		private BaseState currentState = null;
					
		public void ChangeState( BaseState nextState ){

			Debug.Assert (targetModel != null);

			if (nextState == null) {
				return;
			}

			// 今の状態の終了処理を行う
			if (currentState != null) {
				currentState.OnExit ();
			}

			// 状態の更新
			currentState = nextState;

			// ターゲットの設定
			currentState.SetTargetModel( targetModel );

			// 次の状態に遷移
			currentState.OnEnter ();
		}

		public void UpdateByFrame(){

			Debug.Assert (targetModel != null);

			if (currentState != null) {
				currentState.OnUpdate ();
			}
		}
	}
}