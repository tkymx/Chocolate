using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Chocolate.Battle.System;
using Chocolate.Battle.Character.Model;

public class BattleSkillSlotUIPresenter : MonoBehaviour {

	[SerializeField]
	private BattleSkillsModel.SlotNumber slotNumber;

	[SerializeField]
	private Button skillButton;

	[SerializeField]
	private Button skillUpButton;

	[SerializeField]
	private Text skillName;

	private float skillTIme = 0;
	public float skillRefreshTimeLimit = 10;

	// Use this for initialization
	void Start () {
	

		// スキルの実行
		skillButton.onClick.AddListener ( () => {

			// スキルの実行
			BattleGlobal.Instance.Champions.PlayerChampion.Skills.Invoke(slotNumber);

			skillTIme = 0;
		});

		// スキルの強化
		skillUpButton.onClick.AddListener ( () => {

			// スキルのランクアップ
			BattleGlobal.Instance.Champions.PlayerChampion.Skills.RankUp(slotNumber);
		}); 

		skillTIme = skillRefreshTimeLimit;
	}
	
	// Update is called once per frame
	void Update () {
	

		// 回復時間 
		// TODO : あとでクラス化する
		if (skillTIme < skillRefreshTimeLimit) {
			skillTIme += Time.deltaTime;

			skillButton.interactable = false;
		} else {

			skillButton.interactable = true;
		}

		// ランク表記
		skillButton.GetComponentInChildren<Text>().text = 
			BattleGlobal.Instance.Champions.PlayerChampion.Skills.GetSlotSkill(slotNumber).CurrentSkillRank.ToString() + " Rank";

		// 押したらスキルをレベルアップする
		// TODO : あとでクラス化する
		skillUpButton.interactable = 
			BattleGlobal.Instance.Champions.PlayerChampion.Skills.CanRankUp(slotNumber);

		skillUpButton.GetComponentInChildren<Text>().text = 
			BattleGlobal.Instance.Champions.PlayerChampion.Skills.GetSlotSkill(slotNumber).RankUpPoint.ToString() + " pt で UP";

		skillName.text =
			BattleGlobal.Instance.Champions.PlayerChampion.Skills.GetSlotSkill (slotNumber).Skill.Name;
	}
}
