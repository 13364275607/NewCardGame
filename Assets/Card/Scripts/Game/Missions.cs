using UnityEngine;
using System.Collections;
[DisallowMultipleComponent]
public class Missions : MonoBehaviour
{ 
		void Awake ()
		{
				DataManager.instance.InitGameData ();
		}
}
