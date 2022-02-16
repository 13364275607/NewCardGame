using UnityEngine;
using System.Collections;
public class GameObjectUtil
{
		public static Transform FindChildByTag (Transform theParent, string childTag)
		{
				if (string.IsNullOrEmpty (childTag) || theParent == null) {
						return null;
				}

				foreach (Transform child in theParent) {
						if (child.tag == childTag) {
							return child;
						}
				}

				return null;
		}
}
