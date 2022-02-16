using UnityEngine;
using System.Collections;
using UnityEngine.UI;
[DisallowMultipleComponent]
public class WinDialog : MonoBehaviour
{
		private TableLevel.StarsNumber starsNumber;
		public Animator winDialogAnimator;
		public Image winDialogSpriteImage;
		public Animator firstStarFading;
		public Animator secondStarFading;
		public Animator thirdStarFading;
	    public GameObject FireWork;




		void Awake ()
		{
		        
				if (winDialogAnimator == null) {
						winDialogAnimator = GetComponent<Animator> ();
				}

				if (winDialogSpriteImage == null) {
					winDialogSpriteImage = GetComponent<Image> ();
				}

				if (firstStarFading == null) {
						firstStarFading = GameObject.Find ("FirstStarFading").GetComponent<Animator> ();
				}

				if (secondStarFading == null) {
						secondStarFading = GameObject.Find ("SecondStarFading").GetComponent<Animator> ();
				}

				if (thirdStarFading == null) {
						thirdStarFading = GameObject.Find ("ThirdStarFading").GetComponent<Animator> ();
				}
		}
		void OnEnable ()
		{
				Hide ();
		}

		public void Show (TableLevel.StarsNumber starsNumber)
		{
				this.starsNumber = starsNumber;
				if (winDialogAnimator == null) {
						return;
				}
				winDialogSpriteImage.enabled = true;
				winDialogAnimator.SetTrigger ("Running");
		}

		public void Hide ()
		{
				StopAllCoroutines ();
				winDialogAnimator.SetBool ("Running", false);
				firstStarFading.SetBool ("Running", false);
				secondStarFading.SetBool ("Running", false);
				thirdStarFading.SetBool ("Running", false);
		}

		public IEnumerator FadeStars ()
		{
				float delayBetweenStars = 0.5f;
				if (starsNumber == TableLevel.StarsNumber.ONE)
				{
						FireWork.SetActive(true);
						firstStarFading.SetTrigger ("Running");
				} else if (starsNumber == TableLevel.StarsNumber.TWO) 
				{
						FireWork.SetActive(true);
						firstStarFading.SetTrigger ("Running");
						yield return new WaitForSeconds (delayBetweenStars);
						secondStarFading.SetTrigger ("Running");
				} else if (starsNumber == TableLevel.StarsNumber.THREE) 
				{
						FireWork.SetActive(true);
						firstStarFading.SetTrigger ("Running");
						yield return new WaitForSeconds (delayBetweenStars);
						secondStarFading.SetTrigger ("Running");
						yield return new WaitForSeconds (delayBetweenStars);
						thirdStarFading.SetTrigger ("Running");
				}
				yield return 0;
		}

}
