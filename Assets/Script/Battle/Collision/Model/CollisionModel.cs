using UnityEngine;
using System.Collections;

namespace Chocolate.Battle.Collision.Model
{
	public class CollisionModel {

		public float Radius{ get; private set; }

		public Vector3 Position{ get; private set; }

		public CollisionModel(
			float radius,
			Vector3 position){

			this.Radius = radius;
			this.Position = position;
		}

		public void Scale( float scale ){

			Radius *= scale;
		}

		// this コリジョンに対して　collision がはみ出しているかどうか
		public bool IsHamidashi( CollisionModel collision ){

			// コリジョン
			float lengthFromCenterToCenter = (Position - collision.Position).magnitude;

			// はみ出し判定
			if (lengthFromCenterToCenter > (Radius - collision.Radius)) {
				return true;
			}

			return false;
		}

		// this コリジョンに対して　collision がはみ出しているかどうか
		public bool IsIn( CollisionModel collision ){

			// コリジョン
			float lengthFromCenterToCenter = (Position - collision.Position).magnitude;

			// はみ出し判定
			if (lengthFromCenterToCenter < (Radius + collision.Radius)) {
				return true;
			}

			return false;
		}

	}
}