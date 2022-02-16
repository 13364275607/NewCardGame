using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
[DisallowMultipleComponent]
public class GridCell : MonoBehaviour
{       
		public int index;
		[HideInInspector]
		public int pairID;
		public bool alreadyUsed;

		void Start ()
		{
			GetComponent<Button>().onClick.AddListener(() => GameObject.FindObjectOfType<UIEvents>().GridCellButtonEvent(this));
		}
}
