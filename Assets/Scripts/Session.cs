using UnityEngine;
using Data;
using System.Collections;
using Txt;
using Usr;

public class Session
{
	public static Session Instance {
		get { 
			if (instance == null) {
				instance = new Session();
			}
			return instance;
		}
	}

	public bool DataLoaded { get; private set; }
	public GameData Data { get; private set;}
	public IUser User { get; private set; }
	public INetActions Actions { get; private set; }

	readonly IGameDataLoader dataLoader;
	static Session instance;

	Session()
	{
		dataLoader = new FireBaseDataLoader();
		User = new FirebaseUser();
		Data = new GameData();
		Actions = new NetActions();
	}


	public IEnumerator AuthorizeUser()
	{
		ActivityIndicator.Show();
		User.SetErrorOutput(
			(str)=>Menu.Instance.OpenInfoPopup(TextManager.Instance.Get(TextConts.STR_ERROR), str, null)
		);

		bool authorizeFinished = false;
		User.Authorize((succ)=> authorizeFinished = true);
		yield return new WaitUntil(()=> authorizeFinished);

		ActivityIndicator.Hide();
	}

	public IEnumerator LoadGameData()
	{
		ActivityIndicator.Show();

		dataLoader.Init();
		dataLoader.SetErrorOutput(
			(str)=>Menu.Instance.OpenInfoPopup(TextManager.Instance.Get(TextConts.STR_ERROR), str, null)
		);
	
		bool worldDownloaded = false;
		dataLoader.GetWorld((res) => {
			Data.World = res;
			worldDownloaded = true;
		});
		yield return new WaitUntil(()=> worldDownloaded);


		bool players = false;
		dataLoader.GetPlayers((res) => {
			Data.Players = res;
			players = true;
		});
		yield return new WaitUntil(()=> players);


		Data.Utils = new DataUtils(Data.World);

		ActivityIndicator.Hide();
	}
}

