
using UnityEngine;
using System.Collections;


using Chocolate.Battle.Character.Model;
using Chocolate.Battle.Object.Model;
using Chocolate.Battle.Zako.Model;
using Chocolate.Battle.Champion.Model;
using Chocolate.Battle.Area.Model;
using Chocolate.Battle.Bettery.Model;
using Chocolate.Battle.Map.Model;
using Chocolate.Battle.System.Phase;

using Chocolate.Data.Character.Model;
using Chocolate.Data.Character.Repository;

namespace Chocolate.Battle.System.Factory
{
	public class BattleFactory {

		private BattlePreLoadParameter parameter = null;

		public BattleFactory(){
		}

		// public --------------------

		// パラメータのセット
		public void SetParameter( BattlePreLoadParameter parameter ){
			this.parameter = parameter;
		}

		public BattleCharacterModel CreatePlayerZako(){

			Debug.Assert ( parameter != null );

			var zako = CreateZakoCharacter (parameter.zakoPrefab, false, CharacterRepository.ZakoID);
			BattleGlobal.Instance.PlayerZakoGroup.AddZako (zako);

			return zako;
		}

		public BattleCharacterModel CreatePlayerZakoBig(){

			Debug.Assert ( parameter != null );

			var zako = CreateZakoCharacter (parameter.zakoBigPrefab, false, CharacterRepository.ZakoBigID);
			BattleGlobal.Instance.PlayerZakoGroup.AddZako (zako);

			return zako;
		}

		public BattleCharacterModel CreateEnemyZako(){

			Debug.Assert ( parameter != null );

			var zako = CreateZakoCharacter (parameter.zakoEnemyPrefab, true, CharacterRepository.ZakoID);
			BattleGlobal.Instance.EnemyZakoGroup.AddZako (zako);

			return zako;
		}
			
		public BattleCharacterModel CreateEnemyZakoBig(){

			Debug.Assert ( parameter != null );

			var zako = CreateZakoCharacter (parameter.zakoBigEnemyPrefab, true, CharacterRepository.ZakoBigID);
			BattleGlobal.Instance.EnemyZakoGroup.AddZako (zako);

			return zako;
		}

		public BattleCharacterModel CreatePlayerChampion(){

			Debug.Assert ( parameter != null );

			var champion = CreateChampionCharacter (parameter.playerChampionPrefab, false, CharacterRepository.PlayerID);
			BattleGlobal.Instance.Champions.SetPlayerChampion ( champion );

			return champion;
		}

		public BattleCharacterModel CreateEnemyChampion(){

			Debug.Assert ( parameter != null );

			var champion = CreateChampionCharacter (parameter.enemyChampionPrefab, true, CharacterRepository.EnemyID);
			BattleGlobal.Instance.Champions.SetEnemyChampion ( champion );

			return champion;
		}

		public BattleMovableTargetModel CreateMovableTarget(){

			Debug.Assert ( parameter != null );

			var target = CreateMovableTarget (parameter.targetPrefab );
			return target;
		}

		public BattleAreaModel CreatePlayerArea( ){

			Debug.Assert ( parameter != null );

			return CreateArea(parameter.mapPrefab, parameter.betteryPrefab, parameter.castlePrefab );
		}


		public BattleAreaModel CreateEnemyArea( ){

			Debug.Assert ( parameter != null );

			return CreateArea(parameter.mapPrefab, parameter.betteryPrefab, parameter.castlePrefab ,true);
		}

		// private --------------------

		// チャンピオン関連
		private BattleCharacterModel CreateChampionCharacter( GameObject prefab , bool isEnemy, uint characterId ){

			if (prefab == null) {
				return null;
			}

			// オブジェクトの生成
			GameObject championObject = CreateObject( prefab );

			if (championObject == null) {
				return null;
			}

			// キャラクタのモデルの生成
			var view = new BattleViewModel (new Chocolate.Battle.Object.Model.Transform (championObject.transform));
			DirectorModel director = null;
			if(isEnemy){
				director = new EnemyChampionDirectorModel ();
			}else{
				director = new PlayerChampionDirectorModel() ;
			}

			var characterModel = new CharacterRepository ().Get (characterId);

			return BattleCharacterModel.CreateCharacter ( view,director, characterModel);
		}

		// 雑魚関連
		private BattleCharacterModel CreateZakoCharacter( GameObject prefab , bool isEnemy, uint characterId ){

			if (prefab == null) {
				return null;
			}

			// オブジェクトの生成
			GameObject zakoObject = CreateObject( prefab );

			if (zakoObject == null) {
				return null;
			}

			// キャラクタのモデルの生成
			var view = new BattleViewModel (new Chocolate.Battle.Object.Model.Transform (zakoObject.transform));
			DirectorModel director = null;
			if(isEnemy){
				director = new EnemyZakoDirectorModel ();
			}else{
				director = new PlayerZakoDirectorModel() ;
			}

			var characterModel = new CharacterRepository ().Get (characterId);

			return BattleCharacterModel.CreateCharacter ( view,director, characterModel);
		}			

		// 回転
		private GameObject Rotate180( GameObject targetGameObject ){
			targetGameObject.transform.Rotate ( new Vector3( 0,180,0 ) );
			return targetGameObject;
		}

		// 砲台の生成
		private BattleBetteryModel CreateBettery( GameObject prefab , bool isEnemy ){

			if (prefab == null) {
				return null;
			}

			// オブジェクトの生成
			GameObject betteryObject = CreateObject (prefab);

			if (betteryObject == null) {
				return null;
			}

			// キャラクタのモデルの生成
			var view = new BattleViewModel (new Chocolate.Battle.Object.Model.Transform (betteryObject.transform));
			DirectorModel director = null;
			if (isEnemy == true) {
				director = new EnemyBetteryDirectorModel ();
			} else {
				director = new PlayerBetteryDirectorModel ();
			}

			return BattleBetteryModel.CreateBettery ( view,director );
		}

		// マップの生成
		private BattleMapModel CreateMap( GameObject prefab, bool isEnemy ){

			if (prefab == null) {
				return null;
			}

			// オブジェクトの生成
			GameObject betteryObject = CreateObject( prefab );

			if (betteryObject == null) {
				return null;
			}

			// キャラクタのモデルの生成
			var view = new BattleViewModel (new Chocolate.Battle.Object.Model.Transform (betteryObject.transform));

			DirectorModel director = null;
			if (isEnemy) {
				director = new EnemyMapDirectorModel ();
			} else {
				director = new PlayerMapDirectorModel ();
			}

			return BattleMapModel.CreateMap ( view,director );
		}

		// ターゲットの作成
		private BattleMovableTargetModel CreateMovableTarget( GameObject prefab ){

			if (prefab == null) {
				return null;
			}

			// オブジェクトの生成
			GameObject zakoObject = CreateObject( prefab );

			if (zakoObject == null) {
				return null;
			}

			// キャラクタのモデルの生成
			var view = new BattleViewModel (new Chocolate.Battle.Object.Model.Transform (zakoObject.transform));
			DirectorModel director = new DirectorModel ();

			return BattleMovableTargetModel.CreateMovableModel ( view,director);
		}

		// オブジェクト関連
		private GameObject CreateObject( GameObject prefab , string parentGameObject = "MainGameObjects" ){

			if (prefab == null) {
				return null;
			}

			// オブジェクトの生成
			GameObject outObject = GameObject.Instantiate ( prefab );

			if (outObject == null) {
				return null;
			}

			outObject.transform.SetParent ( GameObject.Find( parentGameObject ).transform );

			return outObject;
		}

		// エリアを作成
		private BattleAreaModel CreateArea( GameObject stadiumPrefab, GameObject betteryPrefab, GameObject castlePrefab , bool isEnemy = false ){

			// マップのもとになるものを作成
			GameObject mainMap =  new GameObject();
			mainMap.transform.SetParent (  GameObject.Find("MainGameObjects").transform );
			mainMap.name = "mainMap";

			// 砲台をその傘下に追加
			var bettery = CreateBettery (betteryPrefab, isEnemy);
			bettery.View.RootTransform.SetParenet( mainMap.transform );

			// マップをその傘下に追加
			var map = CreateMap (stadiumPrefab, isEnemy);
			map.View.RootTransform.SetParenet ( mainMap.transform );

			// 城をその傘下に追加
			var castle = CreateObject (castlePrefab);
			castle.transform.SetParent ( mainMap.transform );

			// 敵の場合回転
			if (isEnemy) {
				mainMap = Rotate180 ( mainMap);
			}

			return BattleAreaModel.CreateArea ( bettery , map);
		}
	}
}