using System;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;
using System.Diagnostics;

public class MyNetworkManager : NetworkManager
{
	public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId, NetworkReader extraMessageReader)
	{
		if (extraMessageReader != null)
		{
			var s = extraMessageReader.ReadMessage<StringMessage>();
			UnityEngine.Debug.Log("my name is " + s.value);
		}
		OnServerAddPlayer(conn, playerControllerId, extraMessageReader);


	}
}

