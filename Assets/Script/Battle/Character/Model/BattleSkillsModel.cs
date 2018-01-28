using UnityEngine;
using System.Collections;

using Chocolate.Battle.Object.Model;
using Chocolate.Battle.Collision.Model;
using Chocolate.Battle.System;
using Chocolate.Battle.Collision;

namespace Chocolate.Battle.Character.Model
{
	public class BattleSkillsModel {

		public enum SlotNumber
		{
			Slot1 = 0,
			Slot2 = 1,
			Slot3 = 2,
		};
			
		private BattleSkillModel[] slots = new BattleSkillModel[3];
		public BattleSkillModel GetSlotSkill(SlotNumber slotNumber) {
			return slots [(int)slotNumber];
		}

		private int currentSkillPoint = 0;
		public int CurrentSkillPoint{
			get { return currentSkillPoint; }
		}

		public BattleSkillsModel( BattleCharacterModel targetModel ){

			this.currentSkillPoint = 0;

			for (int slotIndex = 0; slotIndex < slots.Length; slotIndex++) {
				slots[slotIndex] = new BattleSkillModel (targetModel, (SlotNumber)slotIndex);
			}
		}

		public void AddSkillPoint(int point)
		{
			currentSkillPoint += point;
		}

		public bool CanRankUp(SlotNumber slotNumber)
		{
			return slots [(int)slotNumber].CanRankUp (currentSkillPoint);
		}

		/// <summary>
		/// スキルのランクをアップする
		/// </summary>
		public void RankUp(SlotNumber slotNumber)
		{
			currentSkillPoint -= slots [(int)slotNumber].RankUpPoint;
			slots [(int)slotNumber].RankUp ();
		}

		/// <summary>
		/// スキルを発動する
		/// </summary>
		public void Invoke(SlotNumber slotNumber)
		{
			if (slots [(int)slotNumber] != null) {
				slots [(int)slotNumber].Invoke ();
			}
		}
	}
}
