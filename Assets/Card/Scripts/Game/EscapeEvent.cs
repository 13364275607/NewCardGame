using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class EscapeEvent : MonoBehaviour//退出游戏时
{
		public string sceneName;//被加载的场景名称
	public bool leaveTheApplication;//是否在退出时保留应用程序。

	void Update ()
		{
				if (Input.GetKeyDown (KeyCode.Escape)) {
						OnEscapeClick ();
				}
		}
		public void OnEscapeClick ()
		{
				if (leaveTheApplication) {
					GameObject exitConfirmDialog = GameObject.Find ("ExitConfirmDialog");
					if(exitConfirmDialog!=null){
						exitConfirmDialog.GetComponent<ConfirmDialog> ().Show ();
					}
				} else {
						StartCoroutine ("LoadSceneAsync");
				}
		}
		IEnumerator LoadSceneAsync ()
		{
			if (!string.IsNullOrEmpty (sceneName)) {
				#if UNITY_PRO_LICENSE
					AsyncOperation async = SceneManager.LoadSceneAsync (sceneName);
					while (!async.isDone) {
						yield return 0;
					}
				#else
					SceneManager.LoadScene (sceneName);
					yield return 0;
				#endif
			}
		}
}
