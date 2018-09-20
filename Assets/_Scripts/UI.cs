using System;
using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour {

	public Transform cam, startPoint;
	public TextMeshProUGUI ipText, usernameText, passwordText, idText, errorText, scoreText, timeText, instructionsText, gameElementsText, puzzleElementsText, triesText;
	public GameObject skeleton, okButton, gameElements, instructionsScreen, instructionsKeyPrefab, puzzleElements, leaderboard;
    public Transform instructionKeys;
    public MonoBehaviour movementX, movementY;
    public AudioSource bgm;
    public PathMove game2;
    public bool showing = false;
	private bool move = false;
	private string[] allTexts = new string[15];
	private float timer;
    private int maxN;
    private TextOnUI script;

	void Start () {
			timer = 60 * 60 + 45 * 60;
			allTexts[0] = "On exactly this day, 200 years ago, I was brutally murdered here, right where you stand. But my murder still remains a mystery today. I won't let you leave this place until you solve my murder. Oh, and you have only 2 hours.\nHA HA HA HA...";
			allTexts[1] = "Behind these doors roams the skeleton of my murderer. You'll have to catch him. Guess which room can contain him, and if you are wrong, he moves to its adjacent room. Corner him and catch him!!";
			allTexts[2] = "This is the maze of sinners. Each step forward doubles your sin, backwards halves your sin, towards left increases your sin by 2 and towards right decreases it by 2. Get to the top-left corner with the least sin.";
			allTexts[3] = "Lamps in the park outside this room can be controlled by these levers. Match all the levers to their correct lamps to shed some light on my murder. Try not to enter this room too many times.";
			allTexts[4] = "Let's play a game!! There are 42 coins on the right side of this scale. Taking turns, we'll remove maximum 4 coins. Let's see who can balance the scale. As a newcomer, I'll give you a head start.";
			allTexts[5] = "Haha, trapped. The square demon blocks your way to exit and tries to catch up to you. It prefers moving horizontally, than vertically. His demonic powers allow him to move twice in one turn. Exit the maze, evading him.";
            allTexts[6] = "It is said that only the righteous can perfectly balance The Balance of Truth. Are you the righteous one?";
            allTexts[7] = "No interactable objects nearby";
            allTexts[8] = allTexts[9] = allTexts[10] = allTexts[11] = allTexts[12] = allTexts[13] = allTexts[14] = "Answer the question on the board wisely to survive!!";
	}

	public void Play () {
        URL.ip = ipText.text;
		URL.username = usernameText.text;
		URL.password = passwordText.text;
        try{
            string result = URL.Request("register.php", "bitsid="+idText.text);
            if(result == "success"){
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
        //Time.timeScale = 0;
        movementX.enabled = false;
        movementY.enabled = false;
		FindObjectOfType<EventSystem>().SetSelectedGameObject(okButton);
        StartCoroutine(HideInstructions());
	}

	public void OK () {
        foreach(Transform key in instructionKeys)
            Destroy(key.gameObject);
        showing = false;
		instructionsScreen.SetActive(false);
		Cursor.visible = false;
        //Time.timeScale = 1;
        movementX.enabled = true;
        movementY.enabled = true;
		if(skeleton) skeleton.SetActive(false);
	}

    IEnumerator HideInstructions () {
        yield return new WaitForSeconds(10f);
        OK();
    }

	public void ShowGameElements (int max, TextOnUI source) {
        maxN = max;
        script = source;
        Cursor.visible = true;
        gameElements.SetActive(true);
        gameElementsText.text = "1";
        // Time.timeScale = 0;
        movementX.enabled = false;
        movementY.enabled = false;
	}

    public void HideGameElements () {
        script.Set();
		Cursor.visible = false;
        gameElements.SetActive(false);
        movementX.enabled = true;
        movementY.enabled = true;
        // Time.timeScale = 1;
    }

    public int GetText () {
        return Int32.Parse(gameElementsText.text);
    }

	public void ShowPuzzleElements (TextOnUI source) {
        script = source;
        Cursor.visible = true;
        puzzleElements.SetActive(true);
        //Time.timeScale = 0;
        movementX.enabled = false;
        movementY.enabled = false;
	}

    public void HidePuzzleElements () {
        script.Set();
		Cursor.visible = false;
        puzzleElements.SetActive(false);
        //Time.timeScale = 1;
        movementX.enabled = true;
        movementY.enabled = true;
    }

    public string GetPuzzleText () {
        return puzzleElementsText.text;
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

    public void ShowTries (int n){
        triesText.text = "Tries: "+n.ToString();
    }

    public void ShowLeaderboard(){
        leaderboard.GetComponent<TextMeshProUGUI>().text = URL.Request("leaderboard.php", "");
        leaderboard.SetActive(true);
    }

	void Update () {
        //bypassing server
        if(!move && Input.GetKey(KeyCode.LeftAlt) && Input.GetKey(KeyCode.B) && Input.GetKey(KeyCode.Y))
            URL.server = false;

        if(instructionsScreen.activeSelf && Input.GetButtonDown("Exit"))
            OK();

        if(Input.GetButtonDown("Leaderboard") && URL.server)
            ShowLeaderboard();
        if(Input.GetButtonDown("Exit") && leaderboard.activeSelf)
            leaderboard.SetActive(false);

        // updating UI elements
        if(game2.isPlaying)
            scoreText.text = "Sin: " + game2.cost.ToString();
        else
            scoreText.text = "Score: " + GameManager.score.ToString();
		timeText.text = Mathf.Floor(timer/3600).ToString() + "h " + (Mathf.Floor(timer/60)%60).ToString() + "m " + Mathf.Floor(timer%60).ToString() + "s";
		timer -= Time.deltaTime;
        if(Input.GetKey(KeyCode.LeftAlt) && Input.GetKey(KeyCode.E) && Input.GetKey(KeyCode.N))
            timer = 3;
        if(timer <= 0)
            SceneManager.LoadScene("Credits");

        if(timer <= 90 * 60)
            bgm.volume = Mathf.Clamp(bgm.volume - 0.01f, 0.2f, 1);

        if(Input.GetButtonDown("Instructions") && !showing && !instructionsScreen.activeSelf)
            Display(7, new string[] {"Instructions", "Leaderboard"}, new string[] {"I", "L"});

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
