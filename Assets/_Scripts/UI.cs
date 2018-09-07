using System;
using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class UI : MonoBehaviour {

	public Transform cam, startPoint;
	public TextMeshProUGUI ipText, usernameText, passwordText, idText, errorText, scoreText, timeText, instructionsText, gameElementsText;
	public GameObject skeleton, okButton, gameElements, instructionsScreen, instructionsKeyPrefab;
    public Transform instructionKeys;
    public PathMove game2;
    public bool showing = false;
	private bool move = false;
	private string[] allTexts = new string[8];
	private float timer;
    private int maxN;
    private TextOnUI script;

	void Start () {
			timer = 60 * 60 + 45 * 60;
			allTexts[0] = "On exactly this day, 200 years ago, I was brutally murdered here, right where you stand. But my murder still remains a mystery today. I won't let you leave this place until you solve my murder. Oh, and you have only 2 hours.\nHA HA HA HA...";
			allTexts[1] = "Behind these doors roams the skeleton of my murderer. You'll have to catch him. Guess which room can contain him, and if you are wrong, he moves to its adjacent room. Corner him and catch him!!";
			allTexts[2] = "MiniGame2";
			allTexts[3] = "Some lamps outside this room can be controlled by these levers. Match all the levers to their correct lamps to shed some light on my murder.";
			allTexts[4] = "Let's play a game!! There are 42 coins on the left side of this scale. Taking turns, we'll remove maximum 4 coins. Let's see who can balance the scale. As a newcomer, I'll give you a head start.";
			allTexts[5] = "Move circle, evade from square and escape.";
            allTexts[6] = "MiniGame6";
            allTexts[7] = "No interactable objects nearby";
	}

	public void Play () {
        URL.ip = ipText.text;
		URL.username = usernameText.text;
		URL.password = passwordText.text;
        try{
            string result = URL.Request("register.php", "bitsid="+idText.text);
            if(result == "register"){
                transform.GetChild(0).gameObject.SetActive(false);
                move = true;
            }else if(result.Contains("login")){
                GameManager.score = Int32.Parse(result.Substring(5));
                transform.GetChild(0).gameObject.SetActive(false);
                move = true;
            }else{
                errorText.text = result;
                StartCoroutine(HideError());
            }
        }catch{
            errorText.text = "Wrong IP entered.";
            StartCoroutine(HideError());
        }

        // DELETE THIS!!!
        transform.GetChild(0).gameObject.SetActive(false);
        move = true;
	}

    IEnumerator HideError () {
        yield return new WaitForSeconds(1);
        errorText.text = "";
    }

	public void Display (int game, string[] buttons, string[] keys) {
		instructionsScreen.SetActive(true);
		instructionsText.text = allTexts[game];
        for(int i = 0; i < buttons.Length; i++){
            Transform key = Instantiate(instructionsKeyPrefab).transform;
            key.SetParent(instructionKeys);
            key.localScale = new Vector3(1, 1, 1);
            RectTransform rt = key.GetComponent<RectTransform>();
            rt.anchorMin = rt.anchorMax = new Vector2((float)(i+1)/(buttons.Length + 1), 0.15f);
            rt.anchoredPosition = Vector2.zero;
            key.GetChild(1).GetComponent<TextMeshProUGUI>().text = buttons[i];
            key.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = keys[i];
        }
        Cursor.visible = true;
        Time.timeScale = 0;
		FindObjectOfType<EventSystem>().SetSelectedGameObject(okButton);
        StartCoroutine(HideInstructions());
	}

	public void OK () {
        foreach(Transform key in instructionKeys)
            Destroy(key.gameObject);
        showing = false;
		instructionsScreen.SetActive(false);
		Cursor.visible = false;
        Time.timeScale = 1;
		if(skeleton) skeleton.SetActive(false);
	}

    IEnumerator HideInstructions () {
        yield return new WaitForSeconds(2f);
        OK();
    }

	public void ShowGameElements (int max, TextOnUI source) {
        maxN = max;
        script = source;
        gameElements.SetActive(true);
        gameElementsText.text = "1";
        Time.timeScale = 0;
	}

    public void HideGameElements () {
        script.Set();
        gameElements.SetActive(false);
        Time.timeScale = 1;
    }

    public int GetText () {
        return Int32.Parse(gameElementsText.text);
    }

    public void ChangeText (int n) {
        int toSet = Int32.Parse(gameElementsText.text) + n;
        if(toSet <= 0)
            gameElementsText.text = maxN.ToString();
        else if(toSet > maxN)
            gameElementsText.text = "1";
        else
            gameElementsText.text = toSet.ToString();
    }

	void Update () {
        // updating UI elements
        if(game2.isPlaying)
            scoreText.text = "Cost: " + game2.cost.ToString();
        else
            scoreText.text = "Score: " + GameManager.score.ToString();
		timeText.text = Mathf.Floor(timer/3600).ToString() + "h " + (Mathf.Floor(timer/60)%60).ToString() + "m " + Mathf.Floor(timer%60).ToString() + "s";
		timer -= Time.deltaTime;

        if(Input.GetButtonDown("Instructions") && !showing && !instructionsScreen.activeSelf)
            Display(7, new string[] {"Instructions", "Submit"}, new string[] {"I", "E"});

        // moving player in start
        if(move){
            cam.Translate(((startPoint.position - cam.position).normalized) * 0.25f, Space.World);
            if((cam.position - startPoint.position).sqrMagnitude <= 0.01f){
                cam.position = startPoint.position;
                Cursor.visible = false;
                Time.timeScale = 1;
                transform.GetChild(1).gameObject.SetActive(true);
                move = false;
            }
		}
	}

}
