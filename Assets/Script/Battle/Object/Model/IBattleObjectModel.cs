using Chocolate.Battle.Object.State;
using Chocolate.Battle.Collision.Model;

namespace Chocolate.Battle.Object.Model
{
	public interface IBattleObjectModel {

		BattleViewModel View{ get; }
		BattleLifeModel Life{ get; }
		DirectorModel Director{ get; }
		StateController StateController{ get; }
		bool IsDelete{ get; set;}

		//初期化
		void Initialize();

		// 更新
		void UpdateByFrame ();	

		// メッセージの受信
		void ReciveMessage( string message, DirectorParameter param = null );

	}

}