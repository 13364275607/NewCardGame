using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
[DisallowMultipleComponent]
public class TableLevel : MonoBehaviour
{
		public static TableLevel selectedLevel;
		public int ID = -1;
		public StarsNumber starsNumber = StarsNumber.ZERO;
		void Start ()
		{
				GetComponent<Button>().onClick.AddListener(() => GameObject.FindObjectOfType<UIEvents>().LevelButtonEvent(this));
				if (ID == -1) {
						string [] tokens = gameObject.name.Split ('-');
						if (tokens != null) {
								ID = int.Parse (tokens [1]);
						}
				}
				GameObject leveTitleGameObject = transform.Find ("LevelTitle").gameObject;
				if (leveTitleGameObject != null) {
						TextMesh textMeshComponent = leveTitleGameObject.GetComponent<TextMesh> ();
						if (textMeshComponent != null) {
								if (string.IsNullOrEmpty (textMeshComponent.text)) {
										textMeshComponent.text = ID.ToString ();
								}
						}
				}
		}

		public enum StarsNumber
		{
				ZERO,
				ONE,
				TWO,
				THREE
		}
}
