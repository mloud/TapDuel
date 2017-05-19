using UnityEngine;
using Data;
using System.Collections;
using Txt;


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

	readonly IGameDataLoader dataLoader;
	static Session instance;

	Session()
	{
		dataLoader = new FireBaseDataLoader();
		Data = new GameData();
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

		Data.Utils = new DataUtils(Data.World);

		ActivityIndicator.Hide();
	}
}

