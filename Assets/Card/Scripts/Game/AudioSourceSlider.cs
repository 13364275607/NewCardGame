using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class AudioSourceSlider : MonoBehaviour
{
		public string audioSourceHolderName;
		[Range(0,1)]
		public int audioSourceIndex;//音频源的索引。
		void Start ()
		{
				if (string.IsNullOrEmpty (audioSourceHolderName)) {
						return;
				}
				GameObject auidoSourceHolder = GameObject.Find (audioSourceHolderName);
				if (auidoSourceHolder != null) {
						Slider slider = GetComponent<Slider> ();
						if (slider != null) {
								AudioSource [] audioSources = auidoSourceHolder.GetComponents<AudioSource> ();//在滑块上应用音频源的音量
								if (audioSourceIndex >= 0 && audioSourceIndex < audioSources.Length) {
										slider.value = audioSources [audioSourceIndex].volume;
								}
						}
				} else {
					Debug.Log("AudioSources holder is not found");
				}
		}
}
