using System;

public interface ISaver
{
	void Save(string key, string value);
	void Save(string key, int value);
	void Save(string key, float value);
	void Save(string key, Object obj);

	int GetInt(string key);
	float GetFloat(string key);
	string GetString(string key);
	T GetFloat<T>(string key);
	bool HasKey(string key);
}

