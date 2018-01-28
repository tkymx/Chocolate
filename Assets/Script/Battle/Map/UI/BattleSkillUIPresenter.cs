using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Chocolate.Battle.System;


public class BattleSkillUIPresenter : MonoBehaviour {

	[SerializeField]
	private Text playerSkillPoint;

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
		
		// 今のスキルポイントを表示
		playerSkillPoint.text = 
			BattleGlobal.Instance.Champions.PlayerChampion.Skills.CurrentSkillPoint.ToString() + " pt";

	}
}
