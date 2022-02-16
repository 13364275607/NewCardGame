using System.Collections;
using System.Collections.Generic;
#if !(UNITY_WP8 || UNITY_WP8_1)
using System.Runtime.Serialization.Formatters.Binary;
#endif
using System.Xml.Serialization;
using System.IO;
using System;
using UnityEngine;

[DisallowMultipleComponent]
public class DataManager : MonoBehaviour
{
	public string fileName = "findthepairs";	
	public SerilizationMethod serilizationMethod;
	private List<MissionData> sceneMissionsData;
	private List<MissionData> fileMissionsData;
	public List<MissionData> filterdMissionsData;
	private string filePath;
	private bool isNullOrEmpty;
	private bool needsToSaveNewData;
	public static DataManager instance;
	
	void Awake ()
	{
		if (instance == null) {
			instance = this;
			DontDestroyOnLoad (gameObject);
		} else {
			Destroy(gameObject);
			return;
		}
		
	}
	public void InitGameData ()
	{
		isNullOrEmpty = false;
		needsToSaveNewData = false;
		sceneMissionsData = LoadMissionsDataFromScene ();
		if (sceneMissionsData == null) {
			return;
		}
		
		if (sceneMissionsData.Count == 0) {
			return;
		}
		fileMissionsData = LoadMissionsFromFile ();
		
		if (fileMissionsData == null) {
			isNullOrEmpty = true;
		} else {
			if (fileMissionsData.Count == 0) {
				isNullOrEmpty = true;
			}
		}

		if (isNullOrEmpty) {
			SaveMissionsToFile (sceneMissionsData);
			filterdMissionsData = sceneMissionsData;
		} else {
			filterdMissionsData = GetFilterdMissionsData ();
			if (needsToSaveNewData) {
				SaveMissionsToFile (filterdMissionsData);
			}
		}
	}
	[System.Serializable]
	public class MissionData
	{
		public int ID;
		public bool isLocked = true;
		public List<LevelData> levelsData = new List<LevelData> ();
		public LevelData FindLevelDataById (int ID)
		{
			foreach (LevelData levelData in levelsData) {
				if (levelData.ID == ID) {
					return levelData;
				}
			}
			return null;
		}
	}
	[System.Serializable]
	public class LevelData
	{
		public int ID;
		public bool isLocked = true;
		public TableLevel.StarsNumber starsNumber = TableLevel.StarsNumber.ZERO;
	}
	public void ResetGameData ()
	{
		try {
			fileMissionsData = LoadMissionsFromFile ();
			
			if (fileMissionsData == null) {
				return;
			}
			foreach (MissionData missionData in fileMissionsData) {
				if (missionData == null) {
					continue;
				}

				if(missionData.ID == 1){
					missionData.isLocked = false;
				}else{
					missionData.isLocked = true;
				}

				foreach (LevelData levelData in missionData.levelsData) {
					if (levelData == null) {
						continue;
					}
					if (levelData.ID == 1) {
						levelData.isLocked = false;
					} else {
						levelData.isLocked = true;
					}
					levelData.starsNumber = TableLevel.StarsNumber.ZERO;
				}
			}
			
			SaveMissionsToFile (fileMissionsData);
		} catch (Exception ex) {
			Debug.Log (ex.Message);
			return;
		}
		Debug.Log ("Game Data has been reset successfully");
	}
	private List<MissionData> LoadMissionsDataFromScene ()
	{
		Debug.Log ("Loading Missions Data from Scene");
		
		GameObject [] missions = UIExtension.FindGameObjectsWithTag ("Mission");;
		
		if (missions == null) {
			Debug.Log ("No Mission with 'Mission' tag found");
			return null;
		}
		
		Mission tempMission = null;
		LevelsManager tempLevelManager = null;
		
		List<MissionData> tempMissionsData = new List<MissionData> ();
		MissionData tempMissionData = null;
		for (int i = 0 ; i < missions.Length ;i++) {

			tempMission = missions[i].GetComponent<Mission> ();
			tempLevelManager =  missions[i].GetComponent<LevelsManager> ();
			tempMissionData = new MissionData ();
			if(i == 0){
				tempMissionData.isLocked = false;
			}
			tempMissionData.ID = tempMission.ID;
			tempMissionData.levelsData = GetLevelData (tempLevelManager.levels);
			
			tempMissionsData.Add (tempMissionData);
		}
		
		return tempMissionsData;
	}
	
	private List<LevelData> GetLevelData (List<Level> levels)
	{
		if (levels == null) {
			return null;
		}
		
		LevelData tempLevelData = null;
		List<LevelData> tempLevelsData = new List<LevelData> ();
		int ID = 1;
		for (int i = 0; i <levels.Count; i++) {
			tempLevelData = new LevelData ();
			tempLevelData.ID = ID;
			ID++;
			if (i == 0) {
				tempLevelData.isLocked = false;
			}
			tempLevelsData.Add (tempLevelData);
		}
		
		return tempLevelsData;
	}
	private List<MissionData> GetFilterdMissionsData ()
	{
		if (fileMissionsData == null || sceneMissionsData == null) {
			return null;
		}
		
		MissionData tempMissionData = null;
		List<MissionData> tempFilteredMissionsData = new List<MissionData> ();
		
		foreach (MissionData missionData in sceneMissionsData) {
		
			tempMissionData = FindMissionDataById (missionData.ID, fileMissionsData);
			if (tempMissionData != null) {
				if (missionData.levelsData.Count == tempMissionData.levelsData.Count) {
					tempFilteredMissionsData.Add (tempMissionData);
				} else {
					needsToSaveNewData = true;
					tempFilteredMissionsData.Add (missionData);
				}
			} else { 
				needsToSaveNewData = true;
				tempFilteredMissionsData.Add (missionData);
			}
		}
		return tempFilteredMissionsData;
	}
	public void SaveMissionsToFile (List<MissionData> missionsData)
	{
		#if UNITY_ANDROID || UNITY_IPHONE
		if (serilizationMethod == SerilizationMethod.BINARY) {
			SaveDataToBinaryFile (missionsData);
		} else if (serilizationMethod == SerilizationMethod.XML) {
			SaveDataToXMLFile (missionsData);
		}
		#elif UNITY_WP8
		if (serilizationMethod == SerilizationMethod.XML) {
			SaveDataToXMLFile (missionsData);
		}
		#else
		if (serilizationMethod == SerilizationMethod.BINARY) {
			SaveDataToBinaryFile (missionsData);
		} else if (serilizationMethod == SerilizationMethod.XML) {
			SaveDataToXMLFile (missionsData);
		}
		#endif
	}
	public List<MissionData> LoadMissionsFromFile ()
	{
		#if UNITY_ANDROID || UNITY_IPHONE
		if (serilizationMethod == SerilizationMethod.BINARY) {
			return	LoadDataFromBinaryFile<List<MissionData>> ();
		} else if (serilizationMethod == SerilizationMethod.XML) {
			return	LoadDataFromXMLFile<List<MissionData>> ();
		}
		#elif UNITY_WP8 || UNITY_WP8_1
		if (serilizationMethod == SerilizationMethod.XML) {
			return	LoadDataFromXMLFile<List<MissionData>> ();
		}
		#else
		if (serilizationMethod == SerilizationMethod.BINARY) {
			return	LoadDataFromBinaryFile<List<MissionData>> ();
		} else if (serilizationMethod == SerilizationMethod.XML) {
			return	LoadDataFromXMLFile<List<MissionData>> ();
		}
		#endif
		
		return null;
	}

	public void SaveDataToXMLFile<Type> (Type data)
	{
		SettingUpFilePath ();
		if (string.IsNullOrEmpty (filePath)) {
			Debug.Log ("Null or Empty path");
			return;
		}
		
		if (data == null) {
			Debug.Log ("Data is Null");
			return;
		}
		
		Debug.Log ("Saving Data to XML File");
		
		XmlSerializer serializer = new XmlSerializer (typeof(Type));
		TextWriter textWriter = new StreamWriter (filePath);
		serializer.Serialize (textWriter, data);
		textWriter.Close ();
	}
	public Type LoadDataFromXMLFile<Type> ()
	{
		Type data = default(Type);
		SettingUpFilePath ();
		
		if (string.IsNullOrEmpty (filePath)) {
			Debug.Log ("Null or Empty file path");
			return data;
		}
		
		if (!File.Exists (filePath)) {
			Debug.Log (filePath + " is not exists");
			return data;
		}
		
		Debug.Log ("Loading Data from XML File");
		
		XmlSerializer deserializer = new XmlSerializer (typeof(Type));
		TextReader textReader = new StreamReader (filePath);
		data = (Type)deserializer.Deserialize (textReader);
		textReader.Close ();
		
		return data;
	}
	public void SaveDataToBinaryFile<Type> (Type data)
	{
		#if !(UNITY_WP8 || UNITY_WP8_1)
		SettingUpFilePath ();
		if (string.IsNullOrEmpty (filePath)) {
			Debug.Log ("Null or Empty file path");
			return;
		}
		
		if (data == null) {
			Debug.Log ("Data is Null");
			return;
		}
		
		Debug.Log ("Saving Data to Binary File");
		
		FileStream file = null;
		try {
			BinaryFormatter bf = new BinaryFormatter ();
			file = File.Open (filePath, FileMode.Create);
			bf.Serialize (file, data);
			file.Close ();
		} catch (Exception ex) {
			file.Close ();
			Debug.LogError ("Exception : " + ex.Message);
		}
		#endif
	}
	

	public Type LoadDataFromBinaryFile<Type> ()
	{
		Type data = default(Type);
		
		#if !(UNITY_WP8 || UNITY_WP8_1)
		SettingUpFilePath ();
		if (string.IsNullOrEmpty (filePath)) {
			Debug.Log ("Null or Empty file path");
			return data;
		}
		
		if (!File.Exists (filePath)) {
			Debug.Log (filePath + " is not exists");
			return data;
		}
		
		Debug.Log ("Loading Data from Binary File");
		
		
		FileStream file = null;
		try {
			BinaryFormatter bf = new BinaryFormatter ();
			file = File.Open (filePath, FileMode.Open);
			data = (Type)bf.Deserialize (file);
			file.Close ();
		} catch (Exception ex) {
			file.Close ();
			Debug.LogError ("Exception : " + ex.Message);
		}
		#endif
		return data;
	}
	public static MissionData FindMissionDataById (int ID, List<MissionData> missionsData)
	{
		if (missionsData == null) {
			return null;
		}
		
		foreach (MissionData missionData in missionsData) {
			if (missionData.ID == ID) {
				return missionData;
			}
			
		}
		
		return null;
	}
	private void SettingUpFilePath ()
	{
		string fileExtension = "";
		
		#if UNITY_ANDROID
		filePath = GetAndroidFileFolder();
		if (serilizationMethod == SerilizationMethod.BINARY) {
			fileExtension = ".bin";
		} else if (serilizationMethod == SerilizationMethod.XML) {
			fileExtension = ".xml";
		}
		#elif UNITY_IPHONE
		//Get iPhone Documents Path
		filePath = GetIPhoneFileFolder();
		if (serilizationMethod == SerilizationMethod.BINARY) {
			fileExtension = ".bin";
		} else if (serilizationMethod == SerilizationMethod.XML) {
			fileExtension = ".xml";
		}
		#elif UNITY_WP8 || UNITY_WP8_1
		//Get Windows Phone 8 Path
		filePath = GetWP8FileFolder();
		if (serilizationMethod == SerilizationMethod.XML) {
			fileExtension = ".xml";
		}
		#else
		//Others
		filePath = GetOthersFileFolder ();
		if (serilizationMethod == SerilizationMethod.BINARY) {
			fileExtension = ".bin";
		} else if (serilizationMethod == SerilizationMethod.XML) {
			fileExtension = ".xml";
		}
		#endif
		filePath += "/" + fileName + fileExtension;
	}
	
	public static string GetAndroidFileFolder ()
	{
		return Application.persistentDataPath;
	}
	
	public static string GetIPhoneFileFolder ()
	{
		return Application.persistentDataPath;
	}
	
	public static string GetWP8FileFolder ()
	{
		return Application.dataPath;
	}
	
	public static string GetOthersFileFolder ()
	{
		return Application.dataPath;
	}
	
	public enum SerilizationMethod
	{
		#if UNITY_WP8 || UNITY_WP8_1
		XML
		#else
		BINARY,
		XML
		#endif
	}
	;	
}
