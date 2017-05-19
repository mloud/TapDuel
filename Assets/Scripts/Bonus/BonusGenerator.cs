using System;
using UnityEngine;
using System.Linq;
using Data;

public class BonusGenerator : MonoBehaviour
{
	public bool Running { get; set; }
	public Action<BonusBase> Action;
	public Action<BonusBase> BonusOutOfScreenAction;

	[SerializeField]
	Defs.Side side;

	[SerializeField]
	GameObject bonusPrefab;

	[SerializeField]
	GameObject numberBonusPrefab;

	[SerializeField]
	Transform spawnTr;

	BattleConfig Config { get { return Session.Instance.Data.World.BattleConfig; } }

	float shuffleTimer;
	float numberTimer;

	int number;

	public void Init()
	{
		shuffleTimer = Config.ShuffleInterval;
		numberTimer = Config.NumberInterval;
		number = 1;
	}

	public void ResetNumber()
	{
		var bonusNumbers = GameObject.FindObjectsOfType<BonusNumber>().ToList();
		bonusNumbers.RemoveAll(x=>!x.Falling || x.Side != side);

		number = 1;

		for (int i = 0; i <  bonusNumbers.Count; ++i) {
			bonusNumbers[i].SetNumber(number);
			number++;
		}
	}

	void Update()
	{
		if (Running) {
			if (shuffleTimer < 0) {
				GenerateShuffle();

				float interval = Config.ShuffleInterval;
				shuffleTimer = interval;
				interval *= 0.95f;
			} else {
				shuffleTimer -= Time.deltaTime;
			}

			if (numberTimer < 0) {

				if (GameObject.FindObjectsOfType<BonusNumber>().ToList().Where(x=>x.Side == side).ToList().Count < 4)
					GenerateNumber();

				numberTimer = Config.NumberInterval;
			} else {
				numberTimer -= Time.deltaTime;
			} 
		}
	}

	void GenerateShuffle()
	{
		var go = GameObject.Instantiate(bonusPrefab);
		var bonus = go.GetComponent<BonusShuffle>();
		bonus.Action = Action;
		bonus.OutOfScreenAction = OnBonusOutOfScreen;
		bonus.Side = side;
		bonus.Speed = Config.BonusFallingSpeed;
		go.transform.position = spawnTr.position;
	}

	void GenerateNumber()
	{
		var go = GameObject.Instantiate(numberBonusPrefab);
		var bonus = go.GetComponent<BonusNumber>();
		bonus.SetNumber(number);
		bonus.Action = Action;
		bonus.OutOfScreenAction = OnBonusOutOfScreen;
		bonus.Side = side;
		bonus.Speed = Config.BonusFallingSpeed;
		go.transform.position = spawnTr.position;
		number++;
	}

	void OnBonusOutOfScreen(BonusBase bonus)
	{
		if (bonus is BonusNumber) {
			ResetNumber();
		}
		BonusOutOfScreenAction(bonus);
	}
}

