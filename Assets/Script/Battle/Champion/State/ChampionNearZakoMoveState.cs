using UnityEngine;
using System.Collections;
using Chocolate.Battle.Object.State;
using Chocolate.Battle.System;
using Chocolate.Battle.Object.Model;
using Chocolate.Battle.Character.Model;

namespace Chocolate.Battle.Champion.State
{
	public class ChampionNearZakoMoveState : BaseState {

		public ChampionNearZakoMoveState (){
		}

		float speed = 0.02f;
		float lengthFromZako = 0.4f;

		IBattleObjectModel movableTarget = null;

		float waitTime = 0;
		float waitTimeLimit = 1.0f;

		float skillTime = 0;
		float skillTimeLimit = 10.0f;

		bool isAvairable = true;

		public override void OnEnter(){

			isAvairable = true;
		}
		public override void OnUpdate(){

			BattleCharacterModel model = targetModel as BattleCharacterModel;

			// 数秒おきにスキルを発動

			skillTime += Time.deltaTime;
			if (skillTime > skillTimeLimit) {

				model.Skills.Invoke (BattleSkillsModel.SlotNumber.Slot1);

				skillTime = 0;
			}

			// スキルポイントが溜まっていたらランクアップする
			if (model.Skills.CanRankUp(BattleSkillsModel.SlotNumber.Slot1)) {
				model.Skills.RankUp (BattleSkillsModel.SlotNumber.Slot1);
			}

			// 数秒ごとに一番近い雑魚に焦点をあわせる

			waitTime += Time.deltaTime;
			if (waitTime > waitTimeLimit) {

				if (movableTarget == null || isAvairable ) {

					// ターゲットが存在しない場合は一番近い雑魚に移動する。

					float minLength = float.MaxValue;

					for (int zakoIndex = 0; zakoIndex < BattleGlobal.Instance.PlayerZakoGroup.Zakos.Count; zakoIndex++) {

						var zako = BattleGlobal.Instance.PlayerZakoGroup.Zakos [zakoIndex];

						if (!zako.View.IsCollisionEnable) {
							continue;
						}

						// 雑魚とキャラの虚栄を取得
						var length = (zako.View.RootTransform.GetPosition () - targetModel.View.RootTransform.GetPosition ()).magnitude;

						// 一番距離の近い雑魚をターゲットにする
						if (length < minLength) {
							minLength = length;
							movableTarget = zako;
							lengthFromZako = zako.View.GetCollision ().Radius + model.View.GetCollision().Radius;
						}
					}

					if (movableTarget != null) {

						isAvairable = false;
					}
				}

				waitTime = 0;
			}
					
			// 存在しない場合は移動しない

			if (movableTarget == null || movableTarget.Life.IsDead ()) {

				movableTarget = null;
				return;
			}

			// 存在しているが到着している場合は向きのみかえる

			if (isAvairable) {

				// 向きを変更
				targetModel.View.Direction = movableTarget.View.RootTransform.GetPosition () - targetModel.View.RootTransform.GetPosition ();

				return;
			}
								
			// 移動する（ 本当は共通化したい ) ----------------

			// 移動先を決定
			Vector3 destinationPosition = movableTarget.View.RootTransform.GetPosition();

			// 移動方向を決める
			Vector3 vec = (destinationPosition - targetModel.View.RootTransform.GetPosition ()).normalized;

			// 移動
			targetModel.View.RootTransform.SetPosition ( targetModel.View.RootTransform.GetPosition () + vec * speed );

			// もしターゲットが雑魚の前方で止まる
			destinationPosition -= vec * lengthFromZako ;

			//到着したら到着メッセージを送る
			if ((destinationPosition - targetModel.View.RootTransform.GetPosition ()).magnitude < 0.5f) {

				isAvairable = true;
			}

			// 移動方向を設定
			targetModel.View.Direction = vec;
		}
		public override void OnExit(){
		}
	}
}