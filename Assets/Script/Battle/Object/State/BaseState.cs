using UnityEngine;
using System.Collections;
using Chocolate.Battle.Object.Model;

namespace Chocolate.Battle.Object.State
{
	// 状態
	// この状態の中で表現したいアニメーションを書いてもいいかも

	public abstract class BaseState{		

		protected IBattleObjectModel targetModel;

		// 状態を持つモデルを決定（StateControllerから呼ばれる）
		public void SetTargetModel( IBattleObjectModel targetModel ){
			this.targetModel = targetModel;
		}

		public abstract void OnEnter();
		public abstract void  OnUpdate();
		public abstract void OnExit();
	}
}