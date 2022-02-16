using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

[DisallowMultipleComponent]
public class UIEvents : MonoBehaviour
{
		public void ChangeMusicLevel (Slider slider)
		{
				if (slider == null) {
						return;
				}
				GameObject.Find ("AudioSources").GetComponents<AudioSource> () [0].volume = slider.value;
		}

		public void ChangeEffectsLevel (Slider slider)
		{
				if (slider == null) {
						return;
				}
				GameObject.Find ("AudioSources").GetComponents<AudioSource> () [1].volume = slider.value;
		}

		public void ShowExitConfirmDialog ()
		{
				GameObject.Find ("ExitConfirmDialog").GetComponent<ConfirmDialog> ().Show ();
				AudioSources.instance.PlayWaterBubbleSound ();
		}
		public void MissionButtonEvent (Mission value)
		{
				if (value == null) {
						Debug.Log ("Event parameter value is undefined");
						return;
				}

				Mission.selectedMission = value;
				LoadLevelsScene ();
		}

		public void LevelButtonEvent (TableLevel value)
		{
				if (value == null) {
						Debug.Log ("Event parameter value is undefined");
						return;
				}

				TableLevel.selectedLevel = value;
				LevelsTable.selectedLevelID = TableLevel.selectedLevel.ID;
				LoadGameScene ();
		}

		public void GridCellButtonEvent (GridCell gridCell)
		{
				if (gridCell == null || !GameManager.enableClick) {
						return;
				}

				GameManager gameManager = GameObject.FindObjectOfType (typeof(GameManager)) as GameManager;

		
				if (gridCell.alreadyUsed) {
						return;
				}

				if (GameManager.previousGridCell != null ? GameManager.previousGridCell.index == gridCell.index : false) {
						gameManager.ResetGridCells (0, 0, new GridCell[] {
							gridCell
						});
						GameManager.previousGridCell = null;
						return;
				}
				gridCell.transform.Find ("Blured").GetComponent<Animator> ().SetTrigger("isRunning");
				gridCell.transform.Find ("Content").GetComponent<Image> ().sprite = GameManager.currentLevel.pairs [gridCell.pairID].onClickSprite;
			
				if (GameManager.previousGridCell == null) {
						GameManager.previousGridCell = gridCell;
				} else {
						if (gridCell.pairID == GameManager.previousGridCell.pairID) {
								gridCell.alreadyUsed = GameManager.previousGridCell.alreadyUsed = true;
								bool completed = gameManager.CheckLevelComplete ();
								if (!completed) {
										gameManager.PlayCorrectSFX ();
								} else {
										gameManager.PlayCompletedSFX ();
								}
								GameManager.previousGridCell.transform.Find ("Blured").GetComponent<Animator> ().SetBool("isRunning",false);
								gridCell.transform.Find ("Blured").GetComponent<Animator> ().SetBool("isRunning",false);
								Debug.Log ("New pair found");
						} else {
								gameManager.ResetGridCells (0.7f, 0, new GridCell[] {
										GameManager.previousGridCell,
										gridCell
								});
								gameManager.PlayWrongSFX ();
								Debug.Log ("Not a pair");
						}
						GameManager.previousGridCell = null;
				}
		}

		public void GameNextButtonEvent ()
		{
				GameObject.Find ("GameScene").GetComponent<GameManager> ().NextLevel ();
		}

		public void GameBackButtonEvent ()
		{
				GameObject.Find ("GameScene").GetComponent<GameManager> ().PreviousLevel ();
		}

		public void GameMenuButtonEvent ()
		{
				GameObject.Find("FireWorks").SetActive(false);
				try {
						if (Mission.selectedMission.levelsManagerComponent.singleLevel) {
								LoadMissionsScene ();
						} else {
								LoadLevelsScene ();
						}
				} catch (Exception ex) {
				}
		}

		public void GameRefreshButtonEvent ()
		{
				GameObject.Find ("GameScene").GetComponent<GameManager> ().RefreshGrid ();
		}

		public void WinDialogNextButtonEvent ()
		{
				GameObject.Find("FireWorks").SetActive(false);
				if (Mission.selectedMission.levelsManagerComponent.singleLevel) {
						LoadMissionsScene ();
						return;
				}

				if (LevelsTable.selectedLevelID == LevelsTable.tableLevels.Count) {
						LoadLevelsScene ();
						return;
				}
				BlackArea.Hide ();
				GameObject.FindObjectOfType<WinDialog> ().Hide ();
				GameObject.Find ("GameScene").GetComponent<GameManager> ().NextLevel ();
		}

		public void LoadMainScene ()
		{
				AudioSources.instance.PlayWaterBubbleSound ();
				StartCoroutine (LoadSceneAsync ("Main"));
		}

		public void LoadHowToPlayScene ()
		{
				AudioSources.instance.PlayWaterBubbleSound ();
				StartCoroutine (LoadSceneAsync ("HowToPlay"));
		}

		public void LoadMissionsScene ()
		{
				AudioSources.instance.PlayWaterBubbleSound ();
				StartCoroutine (LoadSceneAsync ("Missions"));
		}

		public void LoadOptionsScene ()
		{
				AudioSources.instance.PlayWaterBubbleSound ();
				StartCoroutine (LoadSceneAsync ("Options"));
		}

		public void LoadLevelsScene ()
		{
				AudioSources.instance.PlayWaterBubbleSound ();
				StartCoroutine (LoadSceneAsync ("Levels"));
		}

		public void LoadGameScene ()
		{
				AudioSources.instance.PlayWaterBubbleSound ();
				StartCoroutine (LoadSceneAsync ("Game"));
		}
		IEnumerator LoadSceneAsync (string sceneName)//异步加载场景
		{
				if (!string.IsNullOrEmpty (sceneName)) {
						SceneManager.LoadScene (sceneName);
						yield return 0;
				}
		}
		public void RestGameConfirmDialog(GameObject value)//2022.1.10更新，重置游戏数据
		{
		if (value == null)
		{
			return;
		}
		else
		{
			DataManager.instance.ResetGameData();
		}
		AudioSources.instance.PlayWaterBubbleSound();
		}

}
