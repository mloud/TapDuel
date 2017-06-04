using UnityEngine;

using UnityEngine.UI;

public class BattleMenu : MonoBehaviour
{
	[SerializeField]
	Image aiImage;

	GameManager gameManager;

	public void Init()
	{
		gameManager = FindObjectOfType<GameManager>();
	}

	GameManager GetGameManager()
	{
		return FindObjectOfType<GameManager>();
	}
}

