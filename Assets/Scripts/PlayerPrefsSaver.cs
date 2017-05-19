using System;
using UnityEngine;

public class PlayerPrefsSaver : ISaver
{
	public void Save (string key, string value)
	{
		PlayerPrefs.SetString(key, value);
		PlayerPrefs.Save();
	}

	public void Save (string key, int value)
	{
		PlayerPrefs.SetInt(key, value);
		PlayerPrefs.Save();
	}

	public void Save (string key, float value)
	{
		PlayerPrefs.SetFloat(key, value);
		PlayerPrefs.Save();
	}

	public void Save (string key, object obj)
	{
		PlayerPrefs.SetString(key, JsonUtility.ToJson(obj));
		PlayerPrefs.Save();
	}

	public string GetString (string key)
	{
		return PlayerPrefs.GetString(key);
	}

	public int GetInt(string key)
	{
		return PlayerPrefs.GetInt(key);
	}

	public float GetFloat(string key)
	{
		return PlayerPrefs.GetFloat(key);
	}

	public T GetFloat<T>(string key)
	{
		var str =  PlayerPrefs.GetString(key);
		return JsonUtility.FromJson<T>(str);
	}

	public bool HasKey(string key)
	{
		return PlayerPrefs.HasKey(key);
	}

}

