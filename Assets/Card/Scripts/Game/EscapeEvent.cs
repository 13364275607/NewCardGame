using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class EscapeEvent : MonoBehaviour//�˳���Ϸʱ
{
		public string sceneName;//�����صĳ�������
	public bool leaveTheApplication;//�Ƿ����˳�ʱ����Ӧ�ó���

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
