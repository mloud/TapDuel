using System;
using UnityEngine;
using System.Collections.Generic;

namespace Txt
{
	public class TextManager : MonoBehaviour
	{
		Dictionary<string, string> texts;

		public static TextManager Instance {
			get { 
				if (instance == null) {
					instance = new GameObject("TextManager").AddComponent<TextManager>();
				}
				return instance;
			}
		}

		static TextManager instance;

		void Awake()
		{
			Init();
		}


		public string Get(string key)
		{
			string text = null;
			texts.TryGetValue(key, out text);
			if (string.IsNullOrEmpty(text)) {
				text = key;
			}

			return text;
		}

		void Init()
		{
			texts = new Dictionary<string, string>();
			texts.Add(TextConts.STR_LEFT_PLAYER_WON, "Player on the left won");
			texts.Add(TextConts.STR_RIGHT_PLAYER_WON, "Player on the right won");
			texts.Add(TextConts.STR_EXIT_THE_GAME, "Do you want to exit the game?");
			texts.Add(TextConts.STR_PRESS_AGAIN_EXIT, "Press back key again to exit the game");
			texts.Add(TextConts.STR_PRESS_AGAIN_LEAVE, "Press back key again to leave the battle");
			texts.Add(TextConts.STR_ERROR, "Error");
			texts.Add(TextConts.STR_RESET_THE_GAME, "Do you want to reset your progress?");
			texts.Add(TextConts.STR_TYPE_NAME, "Type your name");
		}
	}
}

