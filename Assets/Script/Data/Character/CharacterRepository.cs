using System;
using System.Collections.Generic;
using Chocolate.Data.Character.Model;

namespace Chocolate.Data.Character.Repository
{
	public class CharacterRepository
	{
		public const uint PlayerID =0;
		public const uint EnemyID =1;
		public const uint ZakoID =2;
		public const uint ZakoBigID =3;

		private List<CharacterModel> characterModels = new List<CharacterModel>();

		public CharacterRepository ()
		{
			characterModels.Add (
				new CharacterModel(
					PlayerID,
					"Player",
					new uint[3]{0,1,2}
				));

			characterModels.Add (
				new CharacterModel(
					EnemyID,
					"Enemy",
					new uint[3]{0,1,2}
				));
			
			characterModels.Add (
				new CharacterModel(
					ZakoID,
					"Zako",
					new uint[3]{0,1,2}
				));			

			characterModels.Add (
				new CharacterModel(
					ZakoBigID,
					"ZakoBig",
					new uint[3]{0,1,2}
				));			
		}

		public CharacterModel Get(uint id) {
			return characterModels.Find (chara => chara.ID == id);
		}
	}
}

