using UnityEngine;
using Steamworks;

public class SteamCheck : MonoBehaviour
{
	void Start()
	{
		Debug.Log($"SteamManager.Initialized: {SteamManager.Initialized}");

		if (SteamManager.Initialized)
		{
			Debug.Log($"Steam User: {SteamFriends.GetPersonaName()}");
			Debug.Log($"App ID: {SteamUtils.GetAppID()}");
		}
	}
}