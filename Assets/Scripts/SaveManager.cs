
public class SaveManager
{
	public static SaveManager Instance {
		get { 
			if (instance == null)  {
				instance = new SaveManager();
			}
			return instance;
		}
	}

	static SaveManager instance;

	public ISaver Impl { get; private set; }

	SaveManager()
	{
		Impl = new PlayerPrefsSaver();
	}
}

