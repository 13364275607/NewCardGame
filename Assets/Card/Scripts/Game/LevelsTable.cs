using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class LevelsTable : MonoBehaviour
{
		public static List<TableLevel> tableLevels;
	    public static int selectedLevelID;
		public Transform levelsParent;
		public Sprite starOn;
		public Sprite starOff;
		public GameObject levelPrefab;
		private Transform tempTransform;
		private DataManager.MissionData tempMissionData;
		private DataManager.LevelData tempLevelData;

		void Awake ()
		{
				tableLevels = new List<TableLevel> ();

				try {
						CreateLevels ();

						if (Mission.selectedMission.levelsManagerComponent.singleLevel) {
								if (tableLevels.Count != 0) {
										TableLevel.selectedLevel = tableLevels [0];		
										GameObject.FindObjectOfType<UIEvents>().LoadGameScene();
										return;
								}
						}
						if (string.IsNullOrEmpty (Mission.selectedMission.missionTitle)) {
								GameObject.Find ("TopTitle").GetComponent<Text> ().text = "Mission Title";
						} else {
								GameObject.Find ("TopTitle").GetComponent<Text> ().text = Mission.selectedMission.missionTitle;
						}
				} catch (Exception ex) {
						Debug.Log ("Make sure you selected a mission");
				}
		}
		private void CreateLevels ()
		{
				tableLevels.Clear ();
				LevelsManager levelsManagerComponent = Mission.selectedMission.levelsManagerComponent;

				TableLevel tableLevelComponent = null;
				GameObject tableLevelGameObject = null;
				int ID = 0;
				int levelsCount = 1;
				
				if (!Mission.selectedMission.levelsManagerComponent.singleLevel) {
					levelsCount = levelsManagerComponent.levels.Count;
				}
				for (int i = 0; i < levelsCount ; i++) {

						ID = (i + 1);

						tableLevelGameObject = Instantiate (levelPrefab, Vector3.zero, Quaternion.identity) as GameObject;
						tableLevelGameObject.transform.SetParent (levelsParent);
						tableLevelComponent = tableLevelGameObject.GetComponent<TableLevel> ();
						tableLevelComponent.ID = ID;
						tableLevelGameObject.name = "Level-" + ID;
						tableLevelGameObject.transform.localScale = Vector3.one;

						SettingUpLevel (tableLevelComponent, ID);
						tableLevels.Add (tableLevelComponent);
				}

				if (levelsManagerComponent.levels.Count == 0) {
						Debug.Log ("There are no Levels in this Mission");
				} else {
						Debug.Log ("New levels have beeen created");
				}
		}
		private void SettingUpLevel (TableLevel tableLevel, int ID)
		{
				if (tableLevel == null) {
						return;
				}
				tempMissionData = DataManager.FindMissionDataById (Mission.selectedMission.ID, DataManager.instance.filterdMissionsData);
				if (tempMissionData == null) {
						Debug.Log ("Null MissionData");
						return;
				}
				tempLevelData = tempMissionData.FindLevelDataById (tableLevel.ID);
				if (tempLevelData == null) {
						Debug.Log ("Null LevelData");
						return;
				}
				if (tempLevelData.isLocked) {
						return;
				}
				tableLevel.GetComponent<Button> ().interactable = true;
				tableLevel.transform.Find ("Stars").gameObject.SetActive (true);
				tableLevel.transform.Find ("Lock").gameObject.SetActive (false);
				tableLevel.transform.Find ("LevelTitle").gameObject.SetActive (true);
				tableLevel.transform.Find ("LevelTitle").GetComponent<Text> ().text = ID.ToString ();
				tableLevel.starsNumber = tempLevelData.starsNumber;
				tempTransform = tableLevel.transform.Find ("Stars");
				if (tempLevelData.starsNumber == TableLevel.StarsNumber.ONE) {
						tempTransform.Find ("FirstStar").GetComponent<Image> ().sprite = starOn;
						tempTransform.Find ("SecondStar").GetComponent<Image> ().sprite = starOff;
						tempTransform.Find ("ThirdStar").GetComponent<Image> ().sprite = starOff;
				} else if (tempLevelData.starsNumber == TableLevel.StarsNumber.TWO) {
						tempTransform.Find ("FirstStar").GetComponent<Image> ().sprite = starOn;
						tempTransform.Find ("SecondStar").GetComponent<Image> ().sprite = starOn;
						tempTransform.Find ("ThirdStar").GetComponent<Image> ().sprite = starOff;
				} else if (tempLevelData.starsNumber == TableLevel.StarsNumber.THREE) {
						tempTransform.Find ("FirstStar").GetComponent<Image> ().sprite = starOn;
						tempTransform.Find ("SecondStar").GetComponent<Image> ().sprite = starOn;
						tempTransform.Find ("ThirdStar").GetComponent<Image> ().sprite = starOn;
				} else {//Zero Stars
						tempTransform.Find ("FirstStar").GetComponent<Image> ().sprite = starOff;
						tempTransform.Find ("SecondStar").GetComponent<Image> ().sprite = starOff;
						tempTransform.Find ("ThirdStar").GetComponent<Image> ().sprite = starOff;
				}
		}
}
