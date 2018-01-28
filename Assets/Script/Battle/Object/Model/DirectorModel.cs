using UnityEngine;
using System.Collections;
using Chocolate.Battle.Object.State;

namespace Chocolate.Battle.Object.Model
{
	public interface DirectorParameter{
	}

	public class DirectorModel {


		// ターゲットのキャラクタキャラ生成時に PreLoaderで呼ばれる
		protected IBattleObjectModel targetModel;
		public void SetTargetModel( IBattleObjectModel targetModel ){
			this.targetModel = targetModel;
		}

		// 生成時に呼ばれる初期化
		public virtual void Initialize(){

			targetModel.StateController.ChangeState ( new IdleState() );
		}

		public virtual void UpdateByFrame(){

			// 特に何もしない

		}

		public virtual void ReciveMessage( string message , DirectorParameter param = null ){

			// 特に何もしない ( 上から受け渡される )
		
		}

	}
}