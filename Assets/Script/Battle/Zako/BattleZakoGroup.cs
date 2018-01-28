
using System.Linq;
using System.Collections.Generic;
using Chocolate.Battle.Zako.Model;
using Chocolate.Battle.Character.Model;

namespace Chocolate.Battle.Zako{

	public class BattleZakoGroup {

		private List<BattleCharacterModel> zakos = new List<BattleCharacterModel>();
		public List<BattleCharacterModel> Zakos{
			get { return zakos; }
		}

		public void AddZako( BattleCharacterModel zako ){
			
			zakos.Add (zako);
		}

		public void UpdateByFrame(){

			var zakoArray = zakos.ToArray ();

			var nextZakos = new List<BattleCharacterModel>();

			foreach (var zako in zakoArray) {

				// 死亡処理を行う
				if ( zako.IsDelete ) {

					// GameObject との結びつきを切る
					zako.View.RootTransform.Dispose ();

					continue;
				}

				nextZakos.Add ( zako );
			}

			zakos = nextZakos;
		}
	}

}
