using System;
using Txt;
using UnityEngine.Networking.Match;
using UnityEngine;

public partial class GameManager
{
	MatchInfo currentMatchInfo;

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

	public void OnListBattles()
	{
		networkManager.StartMatchMaker();
		Menu.Instance.ShowLoadingCircle(true);
		networkManager.matchMaker.ListMatches(0, 10, "", true, 0, 0,
			(succ, info, matches) => {
				if (succ) {
					Menu.Instance.OpenMatchesPopup(matches);
				} else {
					Menu.Instance.OpenInfoPopup("Error", "ListMatches " + info, null);
				}
				Menu.Instance.ShowLoadingCircle(false);
			}
		);
	}

	public void OnShare()
	{
		App.InviteToGame();
	}

	public void OnCreateMatch()
	{
		Menu.Instance.ShowLoadingCircle(true);
		Menu.Instance.OpenCreateMatchPopup((matchName)=> {
			networkManager.StartMatchMaker();
			networkManager.matchMaker.CreateMatch(
				matchName,2, true, "", "", "", 0, 0,
				(succ, info, matchInfo) => {
					if (succ) {
						networkManager.StartHost(matchInfo);
						currentMatchInfo = matchInfo;
						Menu.Instance.OpenInfoPopup("","Waiting for opponent", networkManager.StopHost);
					} else {
						Menu.Instance.OpenInfoPopup("Error", "CreateMatch " + info, null);
					}
					Menu.Instance.ShowLoadingCircle(false);
				}
			);
		}, 	
			()=>Menu.Instance.ShowLoadingCircle(false));
	}


}

