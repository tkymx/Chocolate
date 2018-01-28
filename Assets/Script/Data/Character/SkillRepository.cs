using System;
using System.Collections.Generic;
using Chocolate.Data.Character.Model;

namespace Chocolate.Data.Character.Repository
{
	public class SkillRepository
	{
		private Dictionary<uint,SkillModel> skillModels = new Dictionary<uint, SkillModel>();

		public SkillRepository ()
		{
			skillModels [0] =
				new SkillModel (
				0,
				"LineSkill",
				"Line",
				new string[5]{ "10", "9", "8", "7", "6" }
			);

			skillModels [1] =
				new SkillModel (
				1,
				"RangeSkill",
				"Range",
				new string[5]{ "10", "9", "8", "7", "6" }
			);

			skillModels [2] =
				new SkillModel (
				2,
				"SummonSkill",
				"Summon",
				new string[5]{ "10", "9", "8", "7", "6" }
			);

		}

		public SkillModel Get(uint id) {
			return skillModels [id];
		}
	}
}

