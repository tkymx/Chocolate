using System;

namespace Chocolate.Data.Character.Model
{
	public class SkillModel
	{
		public uint ID{get; private set; }
		public string Name{ get; private set; }
		public string Type{ get; private set; }
		public string[] GrowingValue{ get; private set; }

		public SkillModel (uint id, string name, string type, string[] growingValue)
		{
			this.ID = id;
			this.Name = name;
			this.Type = type;
			this.GrowingValue = growingValue;
		}
	}
}

