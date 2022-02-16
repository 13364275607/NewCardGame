using UnityEngine;
using System.Collections;
using UnityEngine.UI;
[DisallowMultipleComponent]
public class TimeOutDialog : MonoBehaviour
{
		public  Animator timeOutDialogAnimator;
		public Image timeOutDialogSpriteImage;
		void Awake ()
		{
				if (timeOutDialogAnimator == null) {
						timeOutDialogAnimator = GetComponent<Animator> ();
				}
		
				if (timeOutDialogSpriteImage == null) {
						timeOutDialogSpriteImage = GetComponent<Image> ();
				}
		}
		void OnDisable ()
		{
				Hide ();
		}
		public void Show ()
		{
				if (timeOutDialogAnimator == null) {
						return;
				}
				timeOutDialogSpriteImage.enabled = true;
				timeOutDialogAnimator.SetTrigger ("Running");
		}
		public void Hide ()
		{
				if (timeOutDialogAnimator != null)
						timeOutDialogAnimator.SetBool ("Running", false);
		}
}
