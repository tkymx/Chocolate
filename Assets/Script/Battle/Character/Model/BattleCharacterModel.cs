using Chocolate.Battle.Object.Model;
using Chocolate.Battle.Object.State;
using Chocolate.Battle.System;
using Chocolate.Data.Character.Model;

namespace Chocolate.Battle.Character.Model
{
	// 今後消したいので後に回す

	public class BattleCharacterModel : BattleObjectModel {

		private CharacterModel character = null;
		public CharacterModel Character {
			get {return character;}
		}

		private BattleSkillsModel skills = null;
		public BattleSkillsModel Skills{
			get{ return skills; }
		}

		protected BattleCharacterModel(
			BattleViewModel view,
			DirectorModel directer,
			CharacterModel characterModel
		) : base(view,directer) 
		{			
			this.character = characterModel;
		}

		public override void Initialize ()
		{
			base.Initialize ();

			// スキルの生成
			skills = new BattleSkillsModel ( this );
		}

		public override void ReciveMessage( string message, DirectorParameter param = null )
		{
			base.ReciveMessage (message, param);

			if (message == "receive_damage") {
				skills.AddSkillPoint (1);
			}
		}
			
		public static BattleCharacterModel CreateCharacter( BattleViewModel view , DirectorModel director, CharacterModel characterModel )
		{
			BattleCharacterModel battleCharacterModel = new BattleCharacterModel (view, director, characterModel);
			battleCharacterModel.SetInternalRefference ();

			// オブジェクトを追加
			BattleGlobal.Instance.ObjectsUpdater.AddObject ( battleCharacterModel );

			return battleCharacterModel;
		}

	}
}