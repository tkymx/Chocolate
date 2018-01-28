using UnityEngine;
using System.Collections;
using Chocolate.Battle.Object.State;

namespace Chocolate.Battle.Zako.State
{
	public class ZakoMoveBaseState : BaseState {

		private Vector3 destinationPosition;

		public ZakoMoveBaseState( 
			Vector3 destinationPosition ){
			this.destinationPosition = destinationPosition;
		}

		float speed = 0.005f;

		public override void OnEnter(){
		}
		public override void OnUpdate(){

			if (targetModel.Life.IsDead ()) {
				return;
			}

			// 移動する（本当はキャラクタの移動状態を変化させるような何かしらのメッセージを送信するようにしたい）----------------

			// 移動方向を決める
			Vector3 vec = (destinationPosition - targetModel.View.RootTransform.GetPosition ()).normalized;

			// 移動
			targetModel.View.RootTransform.SetPosition ( targetModel.View.RootTransform.GetPosition () + vec * speed );

			//到着したら到着メッセージを送る
			if ((destinationPosition - targetModel.View.RootTransform.GetPosition ()).magnitude < 0.5f) {
				targetModel.Director.ReciveMessage ( "EndEmitting" );
			}

			// 移動方向を設定
			targetModel.View.Direction = vec;

		}
		public override void OnExit(){
		}
	}
}