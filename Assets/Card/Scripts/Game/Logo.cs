using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class Logo : MonoBehaviour {

	public float sleepTime = 5;
	// Use this for initialization
	void Start () {
		Invoke ("LoadMainScene", sleepTime);
	}

	private void LoadMainScene(){
		SceneManager.LoadScene ("Main");
	}
	
}
