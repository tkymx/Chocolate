using UnityEngine;
using Chocolate.Battle.Object.State;
using Chocolate.Battle.Object.Model;

namespace Chocolate.Battle.Zako.State
{
	public class ZakoEmittingState : BaseState {

		private Vector3 destinationPosition;

		public ZakoEmittingState( 
			Vector3 destinationPosition ){
			this.destinationPosition = destinationPosition;
		}

		float speed = 0.1f;

		public override void OnEnter(){

			targetModel.View.IsCollisionEnable = false;
			
		}

		public override void OnUpdate(){

			if (targetModel.Life.IsDead ()) {
				return;
			}

			// TODO : もっと Y軸のことも考えてふんわり投げられるようにする

			// 移動を少しづつ遅くする（空気抵抗みたいなもの）
			speed -= 0.0001f;

			// 移動する（本当はキャラクタの移動状態を変化させるような何かしらのメッセージを送信するようにしたい）

			// 移動方向を決める
			Vector3 vec = (destinationPosition - targetModel.View.RootTransform.GetPosition ()).normalized;

			// 移動
			targetModel.View.RootTransform.SetPosition ( targetModel.View.RootTransform.GetPosition () + vec * speed );

			//到着したら到着メッセージを送る
			if ((destinationPosition - targetModel.View.RootTransform.GetPosition ()).magnitude < 0.5f) {
				targetModel.Director.ReciveMessage ( "EndEmitting" );
			}

		}
		public override void OnExit(){

			if (targetModel.Life.IsDead ()) {
				return;
			}

			targetModel.View.IsCollisionEnable = true;

		}
	}
}