using UnityEngine;
using System.Collections;
using System;

namespace Chocolate.Battle.Object.Model
{
	public class Transform : IDisposable {

		private UnityEngine.Transform transform;

		public Transform( UnityEngine.Transform transform ){
			this.transform = transform;
		}

		public Vector3 GetPosition(){
			return transform.position;
		}

		public void SetPosition( Vector3 pos ){
			transform.position = pos;;
		}

		public void SetParenet( UnityEngine.Transform parentTransform ){
			transform.SetParent (parentTransform);
		}

		public float GetRadius(){

			var collider = transform.GetComponent<SphereCollider> ();

			Debug.Assert (collider != null);

			return collider.radius;
		}

		public Vector3 GetColliderPosition(){

			var collider = transform.GetComponent<SphereCollider> ();

			Debug.Assert (collider != null);

			// オフセットの位置をワールド座標へ
			Vector3 worldOffSet = transform.TransformDirection (collider.center);

			Vector3 outputWorldPositoin = worldOffSet + transform.position;

			// 当たり判定は平面で行う
			outputWorldPositoin.y = 0;

			return outputWorldPositoin;
		}
	

		public Vector3 GetDirection(){
			return transform.TransformDirection (new Vector3 (0, 0, 1));
		}

		public void SetDirection( Vector3 direction ){
			transform.LookAt ( GetPosition() + direction );
		}

		public void SetVisible( bool isVisible ){

			transform.gameObject.SetActive ( isVisible );
		}

		public bool IsVisible(  ){

			return transform.gameObject.activeSelf;
		}

		public void Dispose()
		{
			GameObject.Destroy ( transform.gameObject );
		}

	}
}