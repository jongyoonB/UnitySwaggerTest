using UnityEngine;

public static class JsonHelper
{
	//public static T[] FromJson<T>(string json)
	//{
	//	Wrapper<T> wrapper = UnityEngine.JsonUtility.FromJson<Wrapper<T>>(json);
	//	return wrapper.user;
	//}

	//[Serializable]
	//private class Wrapper<T>
	//{
	//	public T[] user;
	//}

	public static T[] jsonToInstance<T>(string json)
	{
		string newJson = "{ \"array\": " + json + "}";
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(newJson);
       
        return wrapper.array;
	}

	//Usage:
	//string jsonString = JsonHelper.arrayToJson<YouObject>(objects);
	public static string arrayToJson<T>(T[] array)
	{
		Wrapper<T> wrapper = new Wrapper<T>();
		wrapper.array = array;

		return JsonUtility.ToJson(wrapper);
	}

	[System.Serializable]
	private class Wrapper<T>
	{
		public T[] array;
	}
}