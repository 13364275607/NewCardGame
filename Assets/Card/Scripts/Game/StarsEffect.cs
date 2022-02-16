using UnityEngine;
using System.Collections;
[DisallowMultipleComponent]
public class StarsEffect : MonoBehaviour
{
		private Vector3 tempPosition;
		public GameObject starsEffectPrefab;
		[Range(-50,50)]
		public float starEffectZPosition = -5;
		private Vector3 angle = new Vector3 (0, 180, 0);
		public void CreateStarsEffect ()
		{
				tempPosition = transform.position;
				tempPosition.z = starEffectZPosition;
				GameObject starsEffect = Instantiate (starsEffectPrefab, tempPosition, Quaternion.Euler(angle)) as GameObject;
				starsEffect.transform.parent = transform;
		}
}
