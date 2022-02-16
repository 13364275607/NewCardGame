using UnityEngine;
using System.Collections;
[DisallowMultipleComponent]
public class Destroy : MonoBehaviour
{
		public float time;
		void Start ()
		{
				Destroy (gameObject, time);
		}
}
