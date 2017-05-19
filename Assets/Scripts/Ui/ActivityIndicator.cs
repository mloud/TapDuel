using UnityEngine;

public static class ActivityIndicator
{
	public static void Show()
	{
		#if UNITY_IPHONE
		Handheld.SetActivityIndicatorStyle(iOS.ActivityIndicatorStyle.Gray);
		#elif UNITY_ANDROID
		Handheld.SetActivityIndicatorStyle(AndroidActivityIndicatorStyle.Small);
		#elif UNITY_TIZEN
		Handheld.SetActivityIndicatorStyle(TizenActivityIndicatorStyle.Small);
		#endif
	}
	public static void Hide()
	{
		Handheld.StopActivityIndicator();
	}
}

