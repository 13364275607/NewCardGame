using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
[DisallowMultipleComponent]
[RequireComponent(typeof(LevelsManager))]
[RequireComponent(typeof(Image))]
[RequireComponent(typeof(Button))]
[ExecuteInEditMode]
public class Mission : MonoBehaviour
{

		public int ID = -1;
		public static Mission selectedMission;
		public string missionTitle = "New Mission";
		[HideInInspector]
		public LevelsManager levelsManagerComponent;
			
		void Awake ()
		{
				InitMission ();
		}

		void Start ()
		{
				if (Application.isPlaying) {
						SetStarsScore ();
				}
		}

		void Update ()
		{
				#if UNITY_EDITOR
				if (!Application.isPlaying) {
						InitMission ();
				}
				#endif
		}
		private void InitMission ()
		{
				if (Application.isPlaying) {
						GetComponent<Button> ().onClick.AddListener (() => GameObject.FindObjectOfType<UIEvents> ().MissionButtonEvent (this));

						bool validName = int.TryParse (name.Split ('-') [1], out ID);
						if (!validName) {
								Debug.LogError ("Invalid Mission Name");
						}
				}
				levelsManagerComponent = GetComponent<LevelsManager> ();
		
				if (levelsManagerComponent != null) {
						if (string.IsNullOrEmpty (missionTitle) || missionTitle == "New Mission") {
								missionTitle = "Pack " + ID;
						}
				}
		
				if (Application.isPlaying) {
						Debug.Log ("Setting up Mission <b>" + missionTitle + "</b> of ID " + ID);
				}

				Transform missionTitleTransform = transform.Find ("Title");
				if (missionTitleTransform != null) {
						Text uiText = missionTitleTransform.GetComponent<Text> ();
						if (uiText != null)
								uiText.text = missionTitle;
				}
		}
		public void SetStarsScore ()
		{
				Transform score = transform.Find ("Score");	
				if (score != null) {
						Transform starsCount = score.Find ("StarsCount");
						if (starsCount != null) {
								starsCount.GetComponent<Text> ().text = GetStarsCount ();
						}
				}
		}
		public string GetStarsCount ()
		{
				string result = "";
				try {
						int totalStarsCount = 3;
						int currentStarsCount = 0;

						DataManager.MissionData missionData = DataManager.FindMissionDataById (ID, DataManager.instance.filterdMissionsData);

						if (!levelsManagerComponent.singleLevel) {
								totalStarsCount = levelsManagerComponent.levels.Count * 3;
								foreach (DataManager.LevelData lvl in missionData.levelsData) {
										currentStarsCount += StarsEnumToInt (lvl.starsNumber);
								}
						} else {
								if (missionData.levelsData.Count != 0)
										currentStarsCount += StarsEnumToInt (missionData.levelsData [0].starsNumber);
						}
						
						result = currentStarsCount + "/" + totalStarsCount;
				} catch (Exception ex) {
				}
				return result;
		}
		private int StarsEnumToInt (TableLevel.StarsNumber starsNumber)
		{
				if (starsNumber == TableLevel.StarsNumber.ONE) {
						return 1;
				} else if (starsNumber == TableLevel.StarsNumber.TWO) {
						return 2;
				} else if (starsNumber == TableLevel.StarsNumber.THREE) {
						return 3;
				}
				return 0;
		}
}
