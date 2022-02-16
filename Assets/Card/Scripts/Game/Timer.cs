using UnityEngine;
using System.Collections;
using UnityEngine.UI;
[DisallowMultipleComponent]
public class Timer : MonoBehaviour
{

		public Text uiText;
		public int timeInSeconds = 60;
		private int tempTime;
		private bool isRunning;
		public bool autoStart;
		public GameObject timeOutGameObject;
		public string timeOutCallBack;
		public string prefix = "Time : ";

		void Awake ()
		{
				if (uiText == null) {
						uiText = GetComponent<Text> ();
				}
				
				if (autoStart) {
						///Start the Timer
						StartTimer ();
				}
		}
		public void StartTimer ()
		{
				Reset ();
				if (!isRunning) {
						isRunning = true;
						StartCoroutine ("Wait");
				}
		}
		public void Stop ()
		{
				if (isRunning) {
						isRunning = false;
						StopCoroutine ("Wait");
				}
		}

		public void Reset ()
		{
				tempTime = timeInSeconds;
		}
		private IEnumerator Wait ()
		{
				while (isRunning) {
						tempTime--;
						ApplyTime ();
						if (tempTime == 0) {
								Stop ();
								if (timeOutGameObject != null && !string.IsNullOrEmpty (timeOutCallBack)) {
										timeOutGameObject.SendMessage (timeOutCallBack);//Fire the timeout callback
								}
						}
						yield return new WaitForSeconds (1);
				}
		}
		private void ApplyTime ()
		{
				if (uiText == null) {
						return;
				}
				uiText.text = prefix + tempTime;
		}
		public void SetTime(int time){
			timeInSeconds = tempTime = time;
			ApplyTime ();
		}
		public int GetTimeInSeconds(){
			return tempTime;
		}
}
