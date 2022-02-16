/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

using UnityEngine;
using System.Collections;

///Developed by Indie Studio
///https://www.assetstore.unity3d.com/en/#!/publisher/9268
///www.indiestd.com
///info@indiestd.com
///copyright © 2016 IGS. All rights reserved.
public class ConfirmDialog : MonoBehaviour
{
		public Animator animator;
		public Animator blackAreaAnimator;

		void Start ()
		{
				if (animator == null) {
						animator = GetComponent<Animator> ();
				}

				if (blackAreaAnimator == null) {
						blackAreaAnimator = GameObject.Find ("BlackArea").GetComponent<Animator> ();
				}
		}

		public void Show ()
		{
				blackAreaAnimator.SetTrigger ("Running");
				animator.SetBool ("Off", false);
				animator.SetTrigger ("On");
		}

		public void Hide ()
		{
				blackAreaAnimator.SetBool ("Running", false);
				animator.SetBool ("On", false);
				animator.SetTrigger ("Off");
		}

		private void ResetAnimationParameters ()
		{
				if (animator == null) {
						return;
				}
				animator.SetBool ("On", false);
				animator.SetBool ("Off", false);
		}
}
