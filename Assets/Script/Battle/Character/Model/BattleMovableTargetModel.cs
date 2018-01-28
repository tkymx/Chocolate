using UnityEngine;
using System.Collections;

using Chocolate.Battle.Object.Model;
using Chocolate.Battle.Object.State;
using Chocolate.Battle.System;
using Chocolate.Data.Character.Model;

namespace Chocolate.Battle.Character.Model
{
	// タッチしたときの位置情報を取得

	public class BattleMovableTargetModel : BattleObjectModel {

		protected BattleMovableTargetModel(
			BattleViewModel view,
			DirectorModel directer
		) : base(view,directer) {

			View.RootTransform.SetVisible ( false );
		}

		private IBattleObjectModel targetZako = null;

		public bool IsExistTarget(){

			return View.RootTransform.IsVisible();
		}

		public void Arrive(){
						
			View.RootTransform.SetVisible(false);
		}

		public bool IsZakoTarget(){

			return targetZako != null;
		}

		public override void UpdateByFrame (){

			base.UpdateByFrame ();

			// 雑魚が設定されていたら
			if (targetZako != null && !targetZako.Life.IsDead() ) {

				// 雑魚の座標をセットする
				View.RootTransform.SetPosition ( targetZako.View.RootTransform.GetPosition() );
			}

			// マウス入力で左クリックをした瞬間
			if (Input.GetMouseButton (0)) {
				
				// ターゲットが見つかったら座標を変える
				var isGetTargetPosition = FindTargetPosition();

				if (!isGetTargetPosition) {
					return;
				}

				// ターゲットの雑魚
				this.targetZako = FindTargetZakoFromPosition ();

				View.RootTransform.SetVisible ( 
					isGetTargetPosition || ( targetZako != null )
				);
			}

		}

		// ターゲットの位置を探索
		private bool FindTargetPosition(){

			Camera  c = Camera.main;
			Vector2 mousePos = new Vector2();

			// Get the mouse position from Event.
			// Note that the y position from Event is inverted.
			mousePos.x = Input.mousePosition.x;
			mousePos.y = Input.mousePosition.y;

			var x1 = c.transform.position;
			var x2 = c.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, c.nearClipPlane));

			Vector3 v = x1 - x2;

			if (v.magnitude < 0.001) {
				return false;
			}

			float a = -x1.y / v.y;
			Vector3 groundPos = new Vector3 ( a * v.x + x1.x , 0.1f , a * v.z + x1.z );

			// 移動がエリア範囲内かどうか判定
			var areaCollisioin = BattleGlobal.Instance.PlayerAreaModel.Map.View.GetCollision();
			var length = (groundPos - areaCollisioin.Position).magnitude;

			if ( length > areaCollisioin.Radius ) {
				return false;
			}

			// 今のターゲット位置を推定
			View.RootTransform.SetPosition( groundPos );

			return true;

		}

		private IBattleObjectModel FindTargetZakoFromPosition(){

			IBattleObjectModel kouhoZako = null;
			float minLength = float.MaxValue;

			foreach (var zako in BattleGlobal.Instance.EnemyZakoGroup.Zakos) {

				if (zako.View.GetCollision ().IsIn (View.GetCollision() )) {

					var length = (zako.View.GetCollision().Position -  View.GetCollision().Position).magnitude;
					if (minLength > length) {
						kouhoZako = zako;
						minLength = length;
					}
				}
			}

			return kouhoZako;
		}

		public static BattleMovableTargetModel CreateMovableModel( BattleViewModel view , DirectorModel director){

			BattleMovableTargetModel movableModel = new BattleMovableTargetModel (view, director);
			movableModel.SetInternalRefference ();

			// オブジェクトを追加
			BattleGlobal.Instance.ObjectsUpdater.AddObject ( movableModel );

			return movableModel;
		}

	}
}