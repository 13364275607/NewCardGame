using UnityEngine;
using System.Collections;
[DisallowMultipleComponent]
//��Ϸֹͣʱ�����ú�ɫ������
public class BlackArea : MonoBehaviour
{
		private static Animator blackAreaAnimator;
		void Awake ()
		{
				if (blackAreaAnimator == null) {
						blackAreaAnimator = GetComponent<Animator> ();
				}
		}
		void OnEnable ()
		{
				Hide ();
		}
		public static void Show ()
		{
				if (blackAreaAnimator == null) {
						return;
				}
				blackAreaAnimator.SetTrigger ("Running");
		}
		public static void Hide ()
		{
			if(blackAreaAnimator!=null)
				blackAreaAnimator.SetBool ("Running", false);
		}
}
