using UnityEngine;
using System.Collections;
using Chocolate.Battle.Object.State;
using Chocolate.Battle.System;

namespace Chocolate.Battle.Champion.State
{
	public class ChampionKeyMoveState : BaseState {

		public ChampionKeyMoveState (){
		}

		float speed = 0.02f;
		float lengthFromZako = 0.4f;

		public override void OnEnter(){
		}
		public override void OnUpdate(){

			// ターゲットが存在していなければ終了する
			if (!BattleGlobal.Instance.MovableTarget.IsExistTarget ()) {

				// 敵がターゲッティングされていたらそっちを見る
				if (BattleGlobal.Instance.MovableTarget.IsZakoTarget() ) {

					// 方向を決定
					targetModel.View.Direction = BattleGlobal.Instance.MovableTarget.View.RootTransform.GetPosition () - targetModel.View.RootTransform.GetPosition();
				}			

				return;
			}
				
			// 移動する（本当はキャラクタの移動状態を変化させるような何かしらのメッセージを送信するようにしたい）----------------

			// 移動先を決定
			Vector3 destinationPosition = BattleGlobal.Instance.MovableTarget.View.RootTransform.GetPosition();

			// 移動方向を決める
			Vector3 vec = (destinationPosition - targetModel.View.RootTransform.GetPosition ()).normalized;

			// 移動
			targetModel.View.RootTransform.SetPosition ( targetModel.View.RootTransform.GetPosition () + vec * speed );

			// もしターゲットが雑魚なら前方で止まる
			if (BattleGlobal.Instance.MovableTarget.IsZakoTarget ()) {

				destinationPosition -= vec * lengthFromZako ;
			}

			//到着したら到着メッセージを送る
			if ((destinationPosition - targetModel.View.RootTransform.GetPosition ()).magnitude < 0.5f) {

				BattleGlobal.Instance.MovableTarget.Arrive ();
			}

			// 移動方向を設定
			targetModel.View.Direction = vec;
		}
		public override void OnExit(){
		}
	}
}