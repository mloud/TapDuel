using UnityEngine;
using System;
using UnityEngine.Networking;



public class InputManager : NetworkBehaviour 
{
	public Defs.Side Side = Defs.Side.Right;

	public bool UsedInMultiplayer = true;

	IGameManager gameManager;

	void Awake()
	{
		gameManager = GameObject.FindObjectOfType<GameManager>();
	}


	void Update () 
	{
		if (UsedInMultiplayer && !isLocalPlayer)
			return;

		#if UNITY_ANDROID
		for (int i = 0; i < Input.touchCount; ++i) {
			if (Input.touches[i].phase == TouchPhase.Began) {
				var side = GetSide(Input.mousePosition.x);
				if (side == Side) {			
					var wPos = ScreenToWorldPoint(Input.touches[i].position);
					Touch(wPos, side);
				}
			}
		}
		#else
		if (Input.GetMouseButtonDown(0)) {
			var side = GetSide(Input.mousePosition.x);
			if (side == Side) {
				var wPos = ScreenToWorldPoint(Input.mousePosition);
				if (UsedInMultiplayer) {
					//CmdTouch(wPos, side);
					Touch(wPos, side);
				}
				else
					Touch(wPos, side);
			}
		}

		#endif
	}



	[Command]
	void CmdTouch(Vector3 pos, Defs.Side side)
	{
		RpcClientTouch(pos, side);
	}

	[ClientRpc]
	void RpcClientTouch(Vector3 pos, Defs.Side side)
	{
		if (isLocalPlayer) {
			gameManager.OnTouch(pos, side);
		}
	}

	[Command]
	public void CmdRightClick()
	{
		RpcRightClick();
	}

	[Command]
	public void CmdWrongClick()
	{
		RpcWrongClick();
	}

	[Command]
	public void CmdNumberBonusFired(float number)
	{
		RpcNumberBonusFired(number);
	}

	[Command]
	public void CmdBonusCollected(string bonusType)
	{
		RpcBonusCollected(bonusType);
	}

	[ClientRpc]
	void RpcRightClick()
	{
		if (!isLocalPlayer) {
			gameManager.PerformRightTouch(Side);
		}
	}

	[ClientRpc]
	void RpcBonusCollected(string name)
	{
		if (!isLocalPlayer) {
			gameManager.PerformCollectBonus(Side, name);
		}
	}

	[ClientRpc]
	void RpcNumberBonusFired(float move)
	{
		if (!isLocalPlayer) {
			gameManager.PerformNumberBonusFired(Side, move);
		}
	}


	[ClientRpc]
	void RpcWrongClick()
	{
		if (!isLocalPlayer) {
			gameManager.PerformWrongTouch(Side);
		}
	}


	void Touch(Vector3 pos, Defs.Side side)
	{
		gameManager.OnTouch(pos, side);
	}

	Defs.Side GetSide(float screenXPos)
	{
		return Camera.main.ScreenToWorldPoint(new Vector3(screenXPos, 0, 0)).x < 0 ? Defs.Side.Left : Defs.Side.Right;
	}

	Vector3 ScreenToWorldPoint(Vector2 screenPos)
	{
		return Camera.main.ScreenToWorldPoint(screenPos);
	}

	public override void OnStartLocalPlayer()
	{
		gameObject.name = "LocalInputManager";
	}
}
