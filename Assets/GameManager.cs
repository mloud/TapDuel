using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using Txt;
using System;
using Data;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour, IGameManager
{
	[SerializeField]
	AudioSource audio;
	
	[SerializeField]
	Ui ui;

	public Action RightTouchAction { get; set; }
	public Action WrongTouchAction { get; set; }
	public Action<string> BonusCollectedAction { get; set; }
	public Action<float> NumberBonusFiredAction { get; set; }


	public List<InputManager> InputManagers { get; set; }

	[SerializeField]
	InputManager inputManagerPrefab;

	[SerializeField]
	BonusGenerator leftBonusGen;

	[SerializeField]
	BonusGenerator rightBonusGen;

	[SerializeField]
	Transform leftBonusContainer;

	[SerializeField]
	Transform rightBonusContainer;

	[SerializeField]
	GameObject objectsContainer;

	[SerializeField]
	GameVisual visual;

	[SerializeField]
	List<Mover> leftMovers;

	[SerializeField]
	List<Mover> rightMovers;

	[SerializeField]
	Transform leftContainer;
	[SerializeField]
	Transform rightContainer;

	[SerializeField]
	GameObject center;

	Mover leftMoverPrefab;
	Mover rightMoverPrefab;

	public bool IsRunning { get { return running; } }
	public float BattleTime { get { return battleTime; } }

	World World { get { return Session.Instance.Data.World; }}

	Ai ai;

	Action<Defs.Side> ShapeChanged;

	bool running;
	bool starting;

	float linePosition;
	float timeLeft;
	float battleTime;

	BattleMode battleMode;
	BattleRecorder leftBattleRecorder;
	BattleRecorder rightBattleRecorder;

	public BattleRecord BattleRec;

	int missionId = -1;

	IEnumerator InitCoroutine()
	{
		timeLeft = World.BattleConfig.BattleDuration;
		running = false;
		starting = true;

		rightBonusGen.Init();
		leftBonusGen.Init();

		yield break;
	}

	BattleRecorder GetBattleRecorder(Defs.Side side)
	{
		return side == Defs.Side.Left ? leftBattleRecorder : rightBattleRecorder;
	}

	BonusGenerator GetBonusGenerator(Defs.Side side)
	{
		return side == Defs.Side.Left ? leftBonusGen : rightBonusGen;
	}

	public List<Mover> GetMovers(Defs.Side side) 
	{
		return side == Defs.Side.Left ? leftMovers : rightMovers;
	}

	Transform GetBonusContainer(Defs.Side side) 
	{
		return side == Defs.Side.Left ? leftBonusContainer : rightBonusContainer;
	}

	Transform GetShapeContainer(Defs.Side side) 
	{
		return side == Defs.Side.Left ? leftContainer : rightContainer;
	}

	public Mover GetActualMoverShape(Defs.Side side)
	{
		return side == Defs.Side.Left ? leftMoverPrefab : rightMoverPrefab;
	}

	void SetActualMoverShape(Mover mover, Defs.Side side)
	{
		if (side == Defs.Side.Left) {
			leftMoverPrefab = mover;
		} else {
			rightMoverPrefab = mover;
		}
	}

	void Awake()
	{ 
		Application.targetFrameRate = 30;

		leftBonusGen.Action = OnBonusPicked;
		leftBonusGen.BonusOutOfScreenAction = BonusOutOfScreen;

		rightBonusGen.Action = OnBonusPicked;
		rightBonusGen.BonusOutOfScreenAction = BonusOutOfScreen;

		ui.SetTimerActive(false, false);
		ui.bar.Hide();

		gameObject.AddComponent<MultiplayerComponent>();

		StartCoroutine(InitCoroutine());
	}


	void Update () 
	{
		if (running) {
			timeLeft -= Time.deltaTime;
			battleTime += Time.deltaTime;
		
			ui.SetTimer(timeLeft);
			if (timeLeft < 0) {
				ResolveBattleEnd();
				running = false;
			}

			if (Input.GetKeyDown(KeyCode.W)) {
				ResolveBattleEnd(1);
			} else if (Input.GetKeyDown(KeyCode.Q)) {
				ResolveBattleEnd(-1);
			}
		} 

		leftBonusGen.Running = running;
		rightBonusGen.Running = running;

		if (!starting && running) {
			ui.bar.Set(GetHPCoef());
		}
	}

	public void SwapMovers(Defs.Side side)
	{
		var pos = new List<Vector3>();
		var movers = GetMovers(side);

		movers.ForEach(x=>pos.Add(x.transform.position));
		for (int i = 0; i < movers.Count; ++i) {
			StartCoroutine(movers[i].MoveTo(pos[(i + 1) % pos.Count]));
		}
	}

	void OnTouch(Transform tr, Defs.Side side)
	{
		OnTouch(tr.position, side);
	}

	public void PerformRightTouch (Defs.Side side)
	{
		var movers = GetMovers(side);
		var prefab = GetActualMoverShape(side);
		var okMover = movers.Find(x=>x.type == prefab.type);
		ApplyRightTouch(side, okMover);
	}


	public void PerformWrongTouch (Defs.Side side)
	{
		var movers = GetMovers(side);
		var prefab = GetActualMoverShape(side);

		var wrongMovers = movers.FindAll(x=>x.type != prefab.type);
		var wrongMover = wrongMovers[UnityEngine.Random.Range(0, wrongMovers.Count)];
		ApplyWrongTouch(side, wrongMover);
	}

	public void PerformCollectBonus(Defs.Side side, string name) 
	{
		if (name.Equals("Shuffle")) {
			SwapMovers(GameUtils.GetOppositeSide(side));
		}
	}

	public void PerformNumberBonusFired(Defs.Side side, float move)
	{
		MoveLineByStep(-(int)side * move);
	}


	public void CollectBonus(BonusBase BonusBase, Defs.Side side) 
	{
		var numbers = GameObject.FindObjectsOfType<BonusNumber>().ToList();
		numbers.RemoveAll(x=>x.Side != side);
		if (numbers.Count > 0) {
			numbers[0].Collect();
		} else {
			Debug.LogError("CollectBonus but no bonus found");
		}
	}


	void ApplyRightTouch(Defs.Side side, Mover mover)
	{
		mover.PlayCorrectTap();
		float step = (int)side * -1 * World.BattleConfig.PositiveMove;
		GenerateMoverShape(side);

		if (battleMode != BattleMode.BeatMeRecording)
			MoveLineByStep(step);
	}

	void ApplyWrongTouch(Defs.Side side, Mover mover)
	{
		mover.PlayWrongTap();
		float step = (int)side * World.BattleConfig.NegativeMove;
		GenerateMoverShape(side);

		if (battleMode != BattleMode.BeatMeRecording)
			MoveLineByStep(step);
	}

	public void OnTouch(Vector3 pos, Defs.Side side)
	{
		if (!running)
			return;

		var mover = GetMovers(side).Find(x=>x.IsInside(pos));
		if (mover != null) {
			if (mover.type == GetActualMoverShape(side).type) {
				GetBattleRecorder(side).AddEvent(BattleEventType.OkTap, battleTime);
				ApplyRightTouch(side, mover);
				if (RightTouchAction != null)
					RightTouchAction();
			} else {
				mover.PlayWrongTap();
				GetBattleRecorder(side).AddEvent(BattleEventType.WrongTap, battleTime);
				ApplyWrongTouch(side, mover);
				if (WrongTouchAction != null)
					WrongTouchAction();
			}
		}

		var bonuses = GameObject.FindObjectsOfType<BonusBase>().ToList();
		bonuses.ForEach(bonus=> {
			if (bonus.Side == side) {
				if (bonus.IsInside(pos)) {
					bonus.Collect();
					if (BonusCollectedAction != null)
						BonusCollectedAction(bonus.Name);
				}
			}
		});

	}


	void MoveLineByStep(float step)
	{
		step = TrimStep(step);
		visual.MoveLine(step);
		linePosition += step;
		OnMoveLineUpdate();	
	}


	float TrimStep(float step)
	{
		if ( (linePosition + step) > GetRightSidePosition()) {
			step -= (linePosition + step) - GetRightSidePosition();
		} else if ((linePosition + step) < GetLeftSidePosition()) {
			step += GetLeftSidePosition() - (linePosition + step);
		}

		return step;
	}

	void OnMoveLineUpdate()
	{
		if (linePosition <= visual.GetLeftWinLinePosition()) {
			ResolveBattleEnd();
		}
		else if (linePosition >= visual.GetRightWinLinePosition()) {
			ResolveBattleEnd();
		}
	}

	float GetHPCoef()
	{
		float rightPos = visual.GetRightWinLinePosition();
		float leftPos = visual.GetLeftWinLinePosition();

		float max = Mathf.Abs(rightPos - leftPos);
		float curr = visual.GetLinePosition() - leftPos;
		return curr / max;
	}


	void GenerateMoverShape(Defs.Side side)
	{
		int rndIndex = UnityEngine.Random.Range(0, GetMovers(side).Count);

		var actualShape = GetActualMoverShape(side);
		if (actualShape != null) {
			Destroy(actualShape.gameObject);
		}

		var moverShape = (GameObject.Instantiate(GetMovers(side)[rndIndex].gameObject)).GetComponent<Mover>();
		moverShape.transform.SetParent(GetShapeContainer(side));
		moverShape.transform.localPosition = Vector3.zero;
		moverShape.PlayOut();
		//moverShape.SetColor(Color.black);

		SetActualMoverShape(moverShape, side);

		if (ShapeChanged != null)
			ShapeChanged(side);
	}

	void ResolveBattleEnd(int forcedWinSide = 0)
	{
		if (running) {
			audio.Stop();


			if (battleMode == BattleMode.BeatMeRecording) {
				Menu.Instance.OpenInputTextPopup("Type name of your beat me battle",(str)=>{
					var rec = new BattleRecord()	;
					rec.Events = GetBattleRecorder(Defs.Side.Right).Events;
					rec.Name = str;
					rec.Id = GetBattleRecorder(Defs.Side.Right).ToString();
					rec.UserId = Session.Instance.User.Id();
					rec.UserName = Session.Instance.User.Name();
					Session.Instance.Actions.CreateBeatMe(rec);
				});
			} else {
				Defs.Side winningSide = visual.GetLinePosition() < 0 ?
					Defs.Side.Right :Defs.Side.Left;

				if (forcedWinSide != 0)
					winningSide = forcedWinSide == -1 ? Defs.Side.Left : Defs.Side.Right;

				// Right player won
				if (winningSide == Defs.Side.Right)
					ui.ShowMessage(TextManager.Instance.Get(TextConts.STR_RIGHT_PLAYER_WON), true);
				// Left Player won
				else
					ui.ShowMessage(TextManager.Instance.Get(TextConts.STR_LEFT_PLAYER_WON), true);

				if (battleMode == BattleMode.Solo && winningSide == GetPlayerSide()) {
					ProgressManager.Instance.SaveLastFinishedMission(missionId);
					Session.Instance.User.Profile.FinishedMission = missionId;
					Session.Instance.Actions.UpdateUser();
				}
			}

			running = false;
			Invoke("Wait", 2.0f);
		}
		ui.SetTimerActive(false);
	}

	Defs.Side GetPlayerSide()
	{
		return ai != null ? GameUtils.GetOppositeSide (ai.Side) : Defs.Side.Right;
	}

	void Wait()
	{
		App.Restart();
	}

	public void NewGame()
	{
		InputManagers =  new List<InputManager> { 
			CreateInputManager(Defs.Side.Right),
			CreateInputManager(Defs.Side.Left)
		};


		battleMode = BattleMode.Duel;
		ui.SetToBattleGameMode();
		StartCoroutine(CountDownCoroutine());
	}

	public void NewBeatMeGameRecording()
	{
		InputManagers =  new List<InputManager> { CreateInputManager(Defs.Side.Right) };
		battleMode = BattleMode.BeatMeRecording;
		ui.SetToBattleGameMode();
		StartCoroutine(CountDownCoroutine());
	}

	public void NewBeatMePlay()
	{
		InputManagers =  new List<InputManager> { CreateInputManager(Defs.Side.Right) };
		battleMode = BattleMode.BeatMe;
		ui.SetToBattleGameMode();
		gameObject.AddComponent<BeatMeComponent>().Set(BattleRec);
		StartCoroutine(CountDownCoroutine());
	}

	public void NewMultiGame()
	{
		battleMode = BattleMode.Multiplayer;
		ui.SetToBattleGameMode();
		StartCoroutine(CountDownCoroutine());
	}


	public void NewAiGame()
	{
		InputManagers =  new List<InputManager> { CreateInputManager(Defs.Side.Right) };
		battleMode = BattleMode.Solo;
		ui.SetToBattleGameMode();

		int lastFinishedMission = ProgressManager.Instance.GetLastFinishedMission();
		int nextMission = Mathf.Min(lastFinishedMission + 1, Session.Instance.Data.Utils.GetMissionsCount() - 1);
		AiSettings aiSettings = Session.Instance.Data.Utils.GetAiSettings(nextMission);
	
		missionId = nextMission;

		ai = new GameObject("Ai").AddComponent<Ai>();
		ai.Set(new AiContext(GetMovers, GetActualMoverShape, Defs.Side.Left, OnTouch, aiSettings));
		ShapeChanged += ai.OnShapeChanged;
		StartCoroutine(CountDownCoroutine());
	}

	IEnumerator CountDownCoroutine()
	{
		objectsContainer.SetActive(true);
		leftMovers.ForEach(x=>x.PlayOut());
		rightMovers.ForEach(x=>x.PlayOut());


		starting = true;
		ui.ShowMessage(3.ToString());
		yield return new WaitForSeconds(0.5f);
		ui.ShowMessage(2.ToString());
		yield return new WaitForSeconds(0.5f);
		ui.ShowMessage(1.ToString());
		yield return new WaitForSeconds(0.5f);
		ui.ShowMessage(0.5f.ToString());

		ui.HideMessage();
	
		visual.PlayLineAppear();
		yield return new WaitForSeconds(0.3f);

	
		ui.bar.PlayChargeAnim();
		yield return new WaitForSeconds(0.5f);
		ui.bar.DisableAnimator();

		GenerateMoverShape(Defs.Side.Left);
		GenerateMoverShape(Defs.Side.Right);


		ui.SetTimerActive(true);
		audio.Play();
		running = true;
		starting = false;
		battleTime = 0;
		leftBattleRecorder = new BattleRecorder();
		rightBattleRecorder = new BattleRecorder();
	}

	float GetLeftSidePosition()
	{
		return visual.GetLeftWinLinePosition();
	}

	float GetRightSidePosition()
	{
		return visual.GetRightWinLinePosition();
	}

	void BonusOutOfScreen(BonusBase bonus)
	{
		if (bonus.Name.Equals("Number")) {
			LooseBonusRow(bonus.Side);
		}
	}

	public void LooseBonusRow(Defs.Side side)
	{
		var container = GetBonusContainer(side);
		for(int i = 0; i < container.childCount; ++i) {
			Destroy(container.GetChild(i).gameObject);
		}
		GetBattleRecorder(side).AddEvent(BattleEventType.NumbersLost, battleTime);
	}

	void OnBonusPicked(BonusBase bonusBase)
	{
		if (bonusBase is BonusShuffle) {
			SwapMovers(GameUtils.GetOppositeSide(bonusBase.Side));
			GetBattleRecorder(bonusBase.Side).AddEvent(BattleEventType.Shuffle, battleTime);
		} else if (bonusBase is BonusNumber) {
			Transform bonusContainer = GetBonusContainer(bonusBase.Side);
			if (bonusBase.Falling) {
				var pos = bonusContainer.position - (int)bonusBase.Side * bonusContainer.childCount * new Vector3(1.05f, 0,0);
				bonusBase.transform.SetParent(bonusContainer);
				bonusBase.transform.position = pos;
				GetBattleRecorder(bonusBase.Side).AddEvent(BattleEventType.NumberTapped, battleTime);
			} else {
				StartCoroutine(PickNumberBonus(bonusContainer, GetBonusGenerator(bonusBase.Side), bonusBase.Side));
			}
		}
	}
		
	IEnumerator PickNumberBonus(Transform container, BonusGenerator generator, Defs.Side side)
	{
		GetBattleRecorder(side).AddEvent(BattleEventType.NumbersFired, battleTime);
		float distance = World.BattleConfig.PositiveMove *  World.BattleConfig.NumberCoef[container.childCount - 1];
		MoveLineByStep(-(int)side * distance);

		if (NumberBonusFiredAction != null) {
			NumberBonusFiredAction(distance);
		}

		generator.ResetNumber();
		while (container.childCount > 0) {
			var bonus = container.GetChild(0).GetComponent<BonusNumber>();
			bonus.DestroyOnHit = true;
			bonus.Explode();
			yield return new WaitForSeconds(0.1f);
		}
	}

	InputManager CreateInputManager(Defs.Side side)
	{
		var inputManager = Instantiate(inputManagerPrefab.gameObject).GetComponent<InputManager>();
		inputManager.Side = side;
		inputManager.UsedInMultiplayer = false;
		return inputManager;
	}



	public void OnReset()
	{
		Menu.Instance.OpenQuestionPopup ("", TextManager.Instance.Get (TextConts.STR_RESET_THE_GAME), 
			// yes
			()=> {
				ProgressManager.Instance.ResetProgress();
				App.Restart();
			},
			//no
			() => {}
		);
	}

	public void OnConnect()
	{
		Menu.Instance.OpenInputTextPopup (
			TextManager.Instance.Get (TextConts.STR_TYPE_NAME), (str)=> {
				Session.Instance.User.SetName(str, (result)=> {
					ui.Refresh();
				});
			});
	}

	public void OnLbs()
	{
		ActivityIndicator.Show();
		Session.Instance.Actions.GetLeaderboards((lb)=>{
			Menu.Instance.OpenLBPopup(lb);
			ActivityIndicator.Hide();
		});
	}

	public void OnBeatMe()
	{
		ActivityIndicator.Show();
		Session.Instance.Actions.GetBeatMe((recs)=>{
			Menu.Instance.OpenBeatMePopup(recs);
			ActivityIndicator.Hide();
		});
	}

	public void OnLobby()
	{
		SceneManager.LoadScene("Lobby");
	}
}
