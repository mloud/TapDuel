using UnityEngine;
using UnityEngine.Networking;
using System.Collections;



namespace Prototype.NetworkLobby
{
	// Subclass this and redefine the function you want
	// then add it to the lobby prefab
	public class MyLobbyHook : LobbyHook
	{
		public override void OnLobbyServerSceneLoadedForPlayer(
			NetworkManager manager, GameObject lobbyPlayer, GameObject gamePlayer) 
		{
			
		}
	}

}
