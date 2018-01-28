
using System.Linq;
using System.Collections.Generic;
using Chocolate.Battle.Object.Model;

namespace Chocolate.Battle.Object
{
	public class BalleObjectsUpdater {

		private List<IBattleObjectModel> targetModels = new List<IBattleObjectModel>();

		public BalleObjectsUpdater(){
		}

		// オブジェクトの生成時に追加される。
		// TODO : 追加してくれるようにプログラムを書かなくては行けないは至難の技
		public void AddObject( IBattleObjectModel targetModel ){

			// 初期化
			targetModel.Initialize ();

			// 追加
			targetModels.Add ( targetModel );
		}			

		// 更新を行う
		public void UpdateByFrame(){

			var updateModels = targetModels.ToArray();

			targetModels.Clear ();

			// 更新
			foreach( var model in updateModels ){
			
				if ( model.IsDelete ) {

					continue;
				}

				model.UpdateByFrame ();

				targetModels.Add ( model );
			}

		}

	}


}
