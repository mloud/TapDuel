using UnityEngine;
using TMPro;
using Txt;
using UnityEngine.UI;

public class InGameMenu : MonoBehaviour
{
    [SerializeField]
    UnityEngine.UI.Button duelButton;

    [SerializeField]
    UnityEngine.UI.Button soloButton;

    [SerializeField]
    UnityEngine.UI.Button pvpButton;

    [SerializeField]
    UnityEngine.UI.Button lbButton;

    [SerializeField]
    UnityEngine.UI.Button shareButton;

    [SerializeField]
    UnityEngine.UI.Button resetButton;

    [SerializeField]
    UnityEngine.UI.Button fbButton;

	[SerializeField]
	UnityEngine.UI.Button multiplayerButton;


	[SerializeField]
    Image profileImage;

    [SerializeField]
    TextMeshProUGUI profileName;

    GameManager gameManager;

    public void Init()
    {
        gameManager = FindObjectOfType<GameManager>();

        duelButton.onClick.RemoveAllListeners();
        soloButton.onClick.RemoveAllListeners();

        multiplayerButton.onClick.RemoveAllListeners();
        lbButton.onClick.RemoveAllListeners();
        resetButton.onClick.RemoveAllListeners();
        shareButton.onClick.RemoveAllListeners();

		duelButton.onClick.AddListener(() => StartCoroutine(GetGameManager().NewGame()));
        soloButton.onClick.AddListener(() => GetGameManager().NewAiGame());
		multiplayerButton.onClick.AddListener (OnMultiplayer);
        resetButton.onClick.AddListener(() => GetGameManager().OnReset());
        lbButton.onClick.AddListener(() => GetGameManager().OnLbs());

        shareButton.onClick.AddListener(OnShare);

        App.Instance.FB.InitAction = OnFacebookLogginChanged;
        App.Instance.FB.AuthAction = OnFacebookAuth;
		App.Instance.FB.ProfilePictureChangedAction = OneProfilePictureChanged;
        App.Instance.FB.ProfileNameChangedAction = OnProfileNameChanged;
	}

    GameManager GetGameManager()
    {
        return FindObjectOfType<GameManager>();
    }

    void OnFacebookAuth(bool loggedIn)
    {
		if (loggedIn) {
    	    App.Instance.Menu.OpenInfoPopup("Facebook", App.Instance.Text.Get(TextConts.STR_LOGGED_TO_FACEBOOK), null);
		} 
        OnFacebookLogginChanged(loggedIn);
    }

    void OnFacebookLogginChanged(bool loggedIn)
    {
        var text = fbButton.GetComponentInChildren<TextMeshProUGUI>();
        text.text = App.Instance.Text.Get(loggedIn ? TextConts.STR_CONNECTED : TextConts.STR_LOG_IN);

        fbButton.onClick.RemoveAllListeners();
        fbButton.onClick.AddListener(() => {
            if (!loggedIn) {
                App.Instance.FB.Login();
            };
        });
    }

    void OnProfileNameChanged(string userName)
    {
        profileName.text = userName;
    }

    void OneProfilePictureChanged(Texture2D texture)
    {
        profileImage.gameObject.SetActive(true);
        profileImage.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
    }

    void OnShare()
    {
        App.Instance.FB.InviteToApp();
    }

    void OnMultiplayer()
    {
        App.Instance.Menu.OpenCreateJoinPopup();
    }

}

