
using UnityEngine;
using Chocolate.Battle.Object.Model;
using Chocolate.Battle.System;
using Chocolate.Battle.Character.Model;
using Chocolate.Battle.Zako.Model;

namespace Chocolate.Battle.Bettery.Model
{
	public class BaseBetteryDirectorModel : DirectorModel 
	{	
		enum BurnType
		{
			Stop,
			Burning			
		};

		// 現在のタイプ
		BurnType currentBurnType;

		// 現在セットされている玉の種類( Zako or ZakoBig )
		protected string currentSettdBollType = "Zako";

		float 	time = 0;
		int 	restBurnCount = 0;
		int 	currentRestBurnCount = 0;

		// 生成時に呼ばれる初期化
		public override void Initialize()
		{
			base.Initialize ();

			currentBurnType = BurnType.Stop;

			AddBurnCount(3);
		}

		public void ForceBurn() {
			OnBurn ();
		}

		// 
		public override void UpdateByFrame() 
		{
			base.UpdateByFrame ();

			if (currentBurnType == BurnType.Burning) {
				
				// 一定間隔で玉を発射している
				time += Time.deltaTime;

				if (time > 0.1f) {
					if (restBurnCount > 0) {

						// 雑魚を発射する
						currentSettdBollType = "Zako";

						// 発射
						OnBurn ();

						// 消費
						restBurnCount--;
					}
					time = 0;
				}

				// カウントが０になったら止まる
				if (restBurnCount <= 0) {
					currentBurnType = BurnType.Stop;
				}
			} 
			if (currentBurnType == BurnType.Stop) {

				// 貯める
			}
		}

		// 玉を追加する
		public void AddBurnCount(int n)
		{
			currentRestBurnCount += n;
		}

		// 発射させる
		public void Burning(int n)
		{
			// 前準備
			currentBurnType = BurnType.Burning;
			AddBurnCount(n);

			// 初期化
			restBurnCount = currentRestBurnCount;
			currentRestBurnCount = 0;
		}

		public virtual void OnBurn()
		{
			Debug.Assert (false, "子クラスで使用されないといけません。");
		}

		protected void BurnInternal( Vector3 startPosition , Vector3 endPosition , BattleCharacterModel zako )
		{
			// 生成と初期座標のセット
			zako.View.RootTransform.SetPosition ( startPosition );

			// 雑魚を放出のメッセージを作成
			EmittingDirectorParamater parameter = new EmittingDirectorParamater();
			parameter.Position = endPosition;

			// メッセージを送信
			zako.ReciveMessage ( "EmittingFromBettery", parameter );
		}

		public override void ReciveMessage( string message , DirectorParameter param = null )
		{
			base.ReciveMessage (message, param);

			if (message == "Burn") {			
				AddBurnCount(2);
			}

			if (message == "Burning") {			
				Burning (2);
			}

			if (message == "BurningBig") {			
				currentSettdBollType = "ZakoBig";
				ForceBurn ();
			}

		}
	}
}