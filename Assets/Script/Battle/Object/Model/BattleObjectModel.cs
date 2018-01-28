
using UnityEngine;

using Chocolate.Battle.Object.Model;
using Chocolate.Battle.Object.State;
using Chocolate.Battle.System;

namespace Chocolate.Battle.Object.Model
{
	public class BattleObjectModel : IBattleObjectModel  {

		private BattleLifeModel life = null;
		public BattleLifeModel Life{ get{ Debug.Assert(life != null); return life; } }

		private BattleViewModel view = null;
		public BattleViewModel View{ get{ Debug.Assert(view != null); return view; } }

		private DirectorModel director = null;
		public DirectorModel Director{ get{ Debug.Assert(director != null); return director; }}

		private StateController stateController = null;
		public StateController StateController{ get{ Debug.Assert(stateController != null); return stateController;} }

		public bool IsDelete{ get; set;}

		protected BattleObjectModel(
			BattleViewModel view,
			DirectorModel directer
		){
			this.view = view;
			this.director = directer;
			this.stateController = new StateController ();
			this.life = new BattleLifeModel ();

			// HPの設定 ( とりあえずここで行っているが実際はパラメータを作成 )
			life.SetMaxHP( 100 );

			IsDelete = false;
		}

		public virtual void Initialize (){
		}

		public virtual void UpdateByFrame (){

			// 状態の設定
			director.UpdateByFrame ();

			// 状態の更新
			stateController.UpdateByFrame();
		}

		public virtual void ReciveMessage( string message, DirectorParameter param = null ){

			director.ReciveMessage ( message, param );
		}

		// 内部的な参照を設定する( 生成時に呼ばれなくてはいけない )
		protected virtual void SetInternalRefference(){

			// ディレクタにキャラを登録
			Director.SetTargetModel( this );
			StateController.SetTargetModel (this);

			// 初期化
			Director.Initialize();

		}

		// オブジェクトの生成を行う
		public static BattleObjectModel CreateObject( BattleViewModel view , DirectorModel director /*あとで汎用的な体力を作成*/ ){

			BattleObjectModel objectModel = new BattleObjectModel (view, director);
			objectModel.SetInternalRefference ();

			// オブジェクトを追加
			BattleGlobal.Instance.ObjectsUpdater.AddObject ( objectModel );

			return objectModel;
		}
	}
}