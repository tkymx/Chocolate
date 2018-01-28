using System;
using Chocolate.Data.Character.Repository;

namespace Chocolate.Data.Character.Model
{
	public class CharacterModel
	{
		public uint ID{ get; private set; }
		public string Name{ get; private set;}
		public uint[] SkillIds{ get; private set;}

		public CharacterModel (uint id, string name, uint[] skillIds)
		{
			this.ID = id;
			this.Name = name;
			this.SkillIds = skillIds;
		}

		public SkillModel GetSkillModel(uint slotNumber){
			return new SkillRepository ().Get (SkillIds [slotNumber]);
		}
	}
}

