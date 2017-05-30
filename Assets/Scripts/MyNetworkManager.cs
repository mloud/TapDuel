using UnityEngine.Networking;


public class MyNetworkManager : NetworkManager
{
	public override void OnClientDisconnect(NetworkConnection conn)
	{
		Menu.Instance.OpenInfoPopup("Client disconnected from server", "", null);
	}

	public override void OnServerConnect(NetworkConnection conn)
	{
		
	}

//	public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId, NetworkReader extraMessageReader)
//	{
//		if (extraMessageReader != null)
//		{
//			var s = extraMessageReader.ReadMessage<StringMessage>();
//			UnityEngine.Debug.Log("my name is " + s.value);
//		}
//		OnServerAddPlayer(conn, playerControllerId, extraMessageReader);
//	}
//
//	// called when a match is created
//	public override void OnMatchCreate(bool success, string extendedInfo, MatchInfo matchInfo)
//	{
//		if (!success) {
//			Menu.Instance.OpenInfoPopup("Error", "OnMatchCreate: " + extendedInfo, null);
//		}
//		//base.OnMatchCreate(success,extendedInfo, matchInfo);
//	}
//
//	// called when a list of matches is received
//	public override void OnMatchList(bool success, string extendedInfo, List<MatchInfoSnapshot> matchList)
//	{
//		
//	}
//
//	// called when a match is joined
//	public override void OnMatchJoined(bool success, string extendedInfo, MatchInfo matchInfo)
//	{
//		
//	}
}

