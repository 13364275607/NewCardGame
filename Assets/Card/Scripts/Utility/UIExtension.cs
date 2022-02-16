
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
public class UIExtension : MonoBehaviour
{
	public static void SetSize (RectTransform trans, Vector2 newSize)
	{
		Vector2 oldSize = trans.rect.size;
		Vector2 deltaSize = newSize - oldSize;
		trans.offsetMin = trans.offsetMin - new Vector2 (deltaSize.x * trans.pivot.x, deltaSize.y * trans.pivot.y);
		trans.offsetMax = trans.offsetMax + new Vector2 (deltaSize.x * (1f - trans.pivot.x), deltaSize.y * (1f - trans.pivot.y));
	}
	public static GameObject[] FindGameObjectsWithTag (string tag)
	{
		GameObject[] gameObjects = GameObject.FindGameObjectsWithTag (tag);
		Array.Sort (gameObjects, CompareGameObjects);
		return gameObjects;
	}
	private static int CompareGameObjects (GameObject gameObject1, GameObject gameObject2)
	{
		return gameObject1.name.CompareTo (gameObject2.name);
	}
	public static string IntToString(int value){
		if (value < 10) {
			return "0"+value;
		}
		return value.ToString ();
	}
	public static void PlayOneShotClipAt (AudioClip audioClip, Vector3 postion, float volume)
	{
		if (audioClip == null || volume == 0) {
			return;
		}
		
		GameObject oneShotAudio = new GameObject ("one shot audio");
		oneShotAudio.transform.position = postion;
		
		AudioSource tempAudioSource = oneShotAudio.AddComponent<AudioSource> ();
		tempAudioSource.clip = audioClip;
		tempAudioSource.volume = volume;
		tempAudioSource.loop = false;
		tempAudioSource.rolloffMode = AudioRolloffMode.Linear;
		tempAudioSource.Play ();
		GameObject.Destroy (oneShotAudio, audioClip.length);
	}
}
