using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class GameManager : MonoBehaviour
{

		public GameObject gridCellPrefab;
		[Range(0,1)]
		public GameObject cellContentPrefab;//卡片内容预制
		[Range(-20,20)]
		public float cellContentZPosition = -5;
		public RectTransform gridRectTransform;
		public Text levelText;
		public Text missionText;
		public static GridCell[] gridCells;
		public AudioClip correctSFX;
		public AudioClip worngSFX;
		public AudioClip completedSFX;
		public AudioClip levelLockedSFX;
		public Image nextButtonImage;
		public Image backButtonImage;
		public static GridCell previousGridCell;
		public static Level currentLevel;
		private bool isRunning;
		public static bool enableClick = true;
		private Timer timer;
		public AudioSource effectsAudioSource;
	
		void Start ()
		{
				if (timer == null) {
						timer = GameObject.Find ("Time").GetComponent<Timer> ();
				}
		
				if (nextButtonImage == null) {
						nextButtonImage = GameObject.Find ("NextButton").GetComponent<Image> ();
				}
		
				if (backButtonImage == null) {
						backButtonImage = GameObject.Find ("BackButton").GetComponent<Image> ();
				}
		
				if (effectsAudioSource == null) {
						effectsAudioSource = GameObject.Find ("AudioSources").GetComponents<AudioSource> () [1];
				}
		
				if (levelText == null) {
						levelText = GameObject.Find ("Level").GetComponent<Text> ();
				}
		
				if (missionText == null) {
						missionText = GameObject.Find ("MissionTitle").GetComponent<Text> ();
				}
		
				if (gridRectTransform == null) {
						gridRectTransform = GameObject.Find ("Grid").GetComponent<RectTransform> ();
				}
		
				enableClick = true;
				CreateNewLevel ();
		}

		void OnDisable ()//当卡牌不可见时,时间停止
	{

				if (timer != null)
						timer.Stop ();
		}
		void Update ()
		{
				if (!isRunning) {
						return;
				}
		}
		public void RefreshGrid ()//刷新网格
		{	
				try {
						timer.Stop ();//时间停止
						ResetGridCells (0, 0, gridCells);//重置网格
						previousGridCell = null;  //上一张网格清空。
						timer.timeInSeconds = currentLevel.timeLimit;////重新恢复时间
			            timer.StartTimer ();
				} catch (Exception ex) {

				}
		}
		private void CreateNewLevel ()//创建新的关卡
		{
				try {
						missionText.text = Mission.selectedMission.missionTitle;
						levelText.text = "Level " + TableLevel.selectedLevel.ID;//标题显示的第几关

						if (Mission.selectedMission.levelsManagerComponent.singleLevel) {
								GameObject.FindObjectOfType<EscapeEvent> ().sceneName = "Missions";
								backButtonImage.enabled = nextButtonImage.enabled = false;//隐藏按钮
			} else {
								GameObject.FindObjectOfType<EscapeEvent> ().sceneName = "Levels";
						}

						currentLevel = Mission.selectedMission.levelsManagerComponent.levels [TableLevel.selectedLevel.ID - 1];
						ResetGameContents ();
						BuildTheGrid ();
						SettingUpPairs ();
						SettingUpNextBackAlpha ();
						timer.Stop ();
						timer.SetTime (currentLevel.timeLimit);
						StartCoroutine (RunTimeAfter (Mission.selectedMission.levelsManagerComponent.initialDelay));
						ResetGridCells (Mission.selectedMission.levelsManagerComponent.initialDelay, Mission.selectedMission.levelsManagerComponent.dalayBetweenCells, gridCells);
						isRunning = true;
				} catch (Exception ex) {
						Debug.LogWarning ("Make sure you selected a level");
				}
		}
		private void BuildTheGrid ()
		{
				LevelsManager levelsManagerComponent = Mission.selectedMission.levelsManagerComponent;
		
				Debug.Log ("Building the Grid " + levelsManagerComponent.levels[TableLevel.selectedLevel.ID-1].numberOfRows + "x" + levelsManagerComponent.levels[TableLevel.selectedLevel.ID-1].numberOfColumns);
		
				Vector2 cellSize = levelsManagerComponent.cellSize, spacing = levelsManagerComponent.spacing;
				if (levelsManagerComponent.matchGrid) {
					cellSize = new Vector2 (gridRectTransform.rect.width / levelsManagerComponent.levels[TableLevel.selectedLevel.ID-1].numberOfColumns, gridRectTransform.rect.height / levelsManagerComponent.levels[TableLevel.selectedLevel.ID-1].numberOfRows);
				}
		
				RectTransform rectTransform;
				int gridCellIndex;
				float x, y;
			
				gridCells = new GridCell[levelsManagerComponent.levels[TableLevel.selectedLevel.ID-1].numberOfRows * levelsManagerComponent.levels[TableLevel.selectedLevel.ID-1].numberOfColumns];
		
				gridRectTransform.name = "Grid " + levelsManagerComponent.levels[TableLevel.selectedLevel.ID-1].numberOfRows + "x" + levelsManagerComponent.levels[TableLevel.selectedLevel.ID-1].numberOfColumns;
		
				for (int i = 0; i < levelsManagerComponent.levels[TableLevel.selectedLevel.ID-1].numberOfRows; i++) {
					for (int j = 0; j < levelsManagerComponent.levels[TableLevel.selectedLevel.ID-1].numberOfColumns; j++) {

								gridCellIndex = i * levelsManagerComponent.levels[TableLevel.selectedLevel.ID-1].numberOfColumns + j;

								GameObject gridCell = Instantiate (gridCellPrefab) as GameObject;

								gridCell.GetComponent<Button>().interactable = false;
								gridCell.transform.Find("Blured").GetComponent<Image> ().enabled = false;
								gridCell.transform.Find ("Content").GetComponent<Image> ().enabled = false;

								gridCell.name = "GridCell-" + gridCellIndex;

								gridCell.transform.SetParent (gridRectTransform);

								gridCell.transform.localScale = Vector3.one;

								gridCell.transform.localPosition = Vector3.zero;

								rectTransform = gridCell.GetComponent<RectTransform> ();
								x = -gridRectTransform.rect.width / 2 + cellSize.x * j;
								y = gridRectTransform.rect.height / 2 - cellSize.y * (i + 1);
								rectTransform.offsetMin = new Vector2 (x, y) + spacing / 2;
				
								x = rectTransform.offsetMin.x + cellSize.x;
								y = rectTransform.offsetMin.y + cellSize.y;
								rectTransform.offsetMax = new Vector2 (x, y) - spacing;

								GridCell gridCellComponent = gridCell.GetComponent<GridCell> ();

								gridCellComponent.index = gridCellIndex;
				
								gridCells [gridCellIndex] = gridCellComponent;
						}
				}	
		}

		private void SettingUpPairs ()
		{
				Debug.Log ("Setting up the Cells Pairs");
		

				if (currentLevel == null) {
						Debug.Log ("level is undefined");
						return;
				}
		

				if (Mission.selectedMission.levelsManagerComponent.randomShuffleOnBuild) {
						//Random Shuffle on Build
						FPShuffle.RandomPairsShuffle (currentLevel.pairs, Mission.selectedMission.levelsManagerComponent.levels[TableLevel.selectedLevel.ID-1].numberOfRows * Mission.selectedMission.levelsManagerComponent.levels[TableLevel.selectedLevel.ID-1].numberOfColumns);
				}

				Level.Pair elementsPair = null;
				int pairID;
				for (int i = 0; i <currentLevel.pairs.Count; i++) {
			
						pairID = i;
						elementsPair = currentLevel.pairs [i];

						gridCells [elementsPair.firstElement.index].GetComponent<Button>().interactable = true;
						gridCells [elementsPair.firstElement.index].transform.Find("Blured").GetComponent<Image> ().enabled = true;
						gridCells [elementsPair.firstElement.index].transform.Find ("Content").GetComponent<Image> ().enabled = true;
						gridCells [elementsPair.firstElement.index].GetComponent<Image> ().sprite = elementsPair.backgroundSprite;
						gridCells [elementsPair.firstElement.index].transform.Find ("Content").GetComponent<Image> ().sprite = elementsPair.onClickSprite;
						gridCells [elementsPair.firstElement.index].pairID = pairID;

						gridCells [elementsPair.secondElement.index].GetComponent<Button>().interactable = true;
						gridCells [elementsPair.secondElement.index].transform.Find("Blured").GetComponent<Image> ().enabled = true;
						gridCells [elementsPair.secondElement.index].transform.Find ("Content").GetComponent<Image> ().enabled = true;
						gridCells [elementsPair.secondElement.index].GetComponent<Image> ().sprite = elementsPair.backgroundSprite;
						gridCells [elementsPair.secondElement.index].transform.Find ("Content").GetComponent<Image> ().sprite = elementsPair.onClickSprite;
						gridCells [elementsPair.secondElement.index].pairID = pairID;
				}
		}
	
		public void NextLevel ()
		{
				if (LevelsTable.selectedLevelID >= 1 && LevelsTable.selectedLevelID < LevelsTable.tableLevels.Count) {
						DataManager.MissionData currentMissionData = DataManager.FindMissionDataById (Mission.selectedMission.ID, DataManager.instance.filterdMissionsData);
						if (LevelsTable.selectedLevelID + 1 <= currentMissionData.levelsData.Count) {
								DataManager.LevelData nextLevelData = currentMissionData.FindLevelDataById (LevelsTable.selectedLevelID + 1);
								if (nextLevelData.isLocked) {
										if (levelLockedSFX != null && effectsAudioSource != null) {
												UIExtension.PlayOneShotClipAt (levelLockedSFX, Vector3.zero, effectsAudioSource.volume);
										}
										return;
								}
						}
						TableLevel.selectedLevel = LevelsTable.tableLevels [LevelsTable.selectedLevelID];
						CreateNewLevel ();
						LevelsTable.selectedLevelID++;
			
				} else {
						if (levelLockedSFX != null) {
								UIExtension.PlayOneShotClipAt (levelLockedSFX, Vector3.zero, effectsAudioSource.volume);
						}
				}
		}
		public void PreviousLevel ()//回到同一模式下的上一个关卡
	{
				if (LevelsTable.selectedLevelID > 1 && LevelsTable.selectedLevelID <= LevelsTable.tableLevels.Count) {
						LevelsTable.selectedLevelID--;
						TableLevel.selectedLevel = LevelsTable.tableLevels [LevelsTable.selectedLevelID - 1];
						CreateNewLevel ();
				} else {
						PlayLevelLockedSFX ();
				}
		}
		private void SettingUpNextBackAlpha ()
		{
				Color tempColor;
				if (TableLevel.selectedLevel.ID == 1) {
						tempColor = backButtonImage.color;
						tempColor.a = 0.5f;
						backButtonImage.color = tempColor;
						backButtonImage.GetComponent<Button> ().interactable = false;
			
						tempColor = nextButtonImage.color;
						tempColor.a = 1;
						nextButtonImage.color = tempColor;
						nextButtonImage.GetComponent<Button> ().interactable = true;
				} else if (TableLevel.selectedLevel.ID == LevelsTable.tableLevels.Count) {
						tempColor = backButtonImage.color;
						tempColor.a = 1;
						backButtonImage.color = tempColor;
						backButtonImage.GetComponent<Button> ().interactable = true;
			
						tempColor = nextButtonImage.color;
						tempColor.a = 0.5f;
						nextButtonImage.color = tempColor;
						nextButtonImage.GetComponent<Button> ().interactable = false;
				} else {
						tempColor = backButtonImage.color;
						tempColor.a = 1;
						backButtonImage.color = tempColor;
						backButtonImage.GetComponent<Button> ().interactable = true;
			
						tempColor = nextButtonImage.color;
						tempColor.a = 1;
						nextButtonImage.color = tempColor;
						nextButtonImage.GetComponent<Button> ().interactable = true;
				}
		}
		private void ResetGameContents ()
		{
				GameObject [] gridCells = GameObject.FindGameObjectsWithTag ("GridCell");
				foreach (GameObject gridCell in gridCells) {
						Destroy (gridCell);
				}
		}
		public bool CheckLevelComplete ()
		{
				bool isLevelComplete = true;
		
				foreach (GridCell gridCell in gridCells) {
						if(!gridCell.GetComponent<Button>().interactable){
								continue;
						}
						if (!gridCell.alreadyUsed) {
								isLevelComplete = false;
								break;
						}
				}
		
				if (isLevelComplete) {
						timer.Stop ();
						isRunning = false;
			
						try {
								DataManager.MissionData currentMissionData = DataManager.FindMissionDataById (Mission.selectedMission.ID, DataManager.instance.filterdMissionsData);
								DataManager.LevelData currentLevelData = currentMissionData.FindLevelDataById (TableLevel.selectedLevel.ID);
								if (timer.GetTimeInSeconds () >= currentLevel.threeStarsTimePeriod && timer.GetTimeInSeconds () <= currentLevel.timeLimit) {
										currentLevelData.starsNumber = TableLevel.StarsNumber.THREE;
								} else if (timer.GetTimeInSeconds () >= currentLevel.twoStarsTimePeriod && timer.GetTimeInSeconds () < currentLevel.threeStarsTimePeriod) {
										currentLevelData.starsNumber = TableLevel.StarsNumber.TWO;
								} else {
										currentLevelData.starsNumber = TableLevel.StarsNumber.ONE;
								}

								if (currentLevelData .ID + 1 <= currentMissionData.levelsData.Count) {
										DataManager.LevelData nextLevelData = currentMissionData.FindLevelDataById (TableLevel.selectedLevel.ID + 1);
										nextLevelData.isLocked = false;
								}
								DataManager.instance.SaveMissionsToFile (DataManager.instance.filterdMissionsData);
								BlackArea.Show ();
								GameObject.FindObjectOfType<WinDialog> ().Show (currentLevelData.starsNumber);
								Debug.Log ("You completed level " + TableLevel.selectedLevel.ID);
						} catch (Exception ex) {
								Debug.Log (ex.Message);
						}
			

				}
				return isLevelComplete;
		}
	
		public void ResetGridCells (float initialDelay, float delayBetweenCells, GridCell[] gcs)
		{
				if (gcs != null) {
						StartCoroutine (ResetCellsCoroutine (initialDelay, delayBetweenCells, gcs));
				}
		}
	
		private IEnumerator ResetCellsCoroutine (float initialDelay, float delayBetweenCells, GridCell[] gcs)
		{
				enableClick = false;
				yield return new WaitForSeconds (initialDelay);
				if (gcs != null) {
						foreach (GridCell gc in gcs) {
								if (gc != null) {
										gc.transform.Find ("Content").GetComponent<Image> ().sprite = currentLevel.pairs [gc.pairID].normalSprite;
										gc.transform.Find ("Blured").GetComponent<Animator> ().SetBool ("isRunning", false);
										gc.alreadyUsed = false;
								}
								yield return new WaitForSeconds (delayBetweenCells);
						}
				}
				enableClick = true;
		}

		private IEnumerator RunTimeAfter (float delayTime)
		{
				yield return new WaitForSeconds (delayTime);
				if (timer != null) {
						timer.StartTimer ();
				}
		}

		public void OnTimeOut ()
		{
				Debug.Log ("Time is out");
				BlackArea.Show ();
				GameObject.FindObjectOfType<TimeOutDialog> ().Show ();
		}

		public void PlayWrongSFX ()
		{
				if (worngSFX != null && effectsAudioSource != null) {
						UIExtension.PlayOneShotClipAt (worngSFX, Vector3.zero, effectsAudioSource.volume);
				}
		}
	
		public void PlayCorrectSFX ()
		{
				if (correctSFX != null && effectsAudioSource != null) {
						UIExtension.PlayOneShotClipAt (correctSFX, Vector3.zero, effectsAudioSource.volume);
				}
		}
	
		public void PlayCompletedSFX ()
		{
				if (completedSFX != null && effectsAudioSource != null) {
						UIExtension.PlayOneShotClipAt (completedSFX, Vector3.zero, effectsAudioSource.volume);
				}
		}
	
		public void PlayLevelLockedSFX ()
		{
				if (levelLockedSFX != null && effectsAudioSource != null) {
						UIExtension.PlayOneShotClipAt (levelLockedSFX, Vector3.zero, effectsAudioSource.volume);
				}
		}
}
