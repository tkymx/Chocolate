
using UnityEngine;
using Chocolate.Battle.Collision.Model;

namespace Chocolate.Battle.Object.Model
{
	public class BattleViewModel {

		// トランス情報
		private Transform rootTransform = null;
		public Transform RootTransform{ 
			get{ return rootTransform; } 
		}

		// 衝突可能かどうか？
		public bool IsCollisionEnable {
			get;
			set;
		}

		// 方向
		public Vector3 Direction {
			get{ return rootTransform.GetDirection (); }
			set{ value.y = 0; rootTransform.SetDirection (value); }
		}


		public BattleViewModel( Transform transform ){
			rootTransform = transform;
			IsCollisionEnable = true;
		}

		public CollisionModel GetCollision(){

			return new CollisionModel (
				rootTransform.GetRadius(),
				rootTransform.GetColliderPosition()
			);
		}
	}
}