using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI : MonoBehaviour {

	public Transform cam, startPoint;
	public Text ipText, usernameText, passwordText, idText, scoreText, timeText, instructionsText, gameElementsText;
	public GameObject skeleton, okButton;
	private bool move = false;
	private string[] allTexts = new string[6];
	private float timer;

	void Start () {
			timer = 60 * 60 + 45 * 60;
			allTexts[0] = "On exactly this day, 200 years ago, I was brutally murdered here, right where you stand. But my murder still remains a mystery today. I won't let you leave this place until you solve my murder. Oh, and you have only 2 hours.\nHA HA HA HA...";
			allTexts[1] = "Behind these doors roams the skeleton of my murderer. You'll have to catch him. Guess which room can contain him, and if you are wrong, he moves to its adjacent room. Corner him and catch him!!";
			allTexts[2] = "MiniGame2";
			allTexts[3] = "Some lamps outside this room can be controlled by these levers. Match all the levers to their correct lamps to shed some light on my murder.";
			allTexts[4] = "Let's play a game!! There are 42 coins on the left side of this scale. Taking turns, we'll remove maximum 4 coins. Let's see who can balance the scale. As a newcomer, I'll give you a head start.";
			allTexts[5] = "Move circle, evade from square and escape.";
	}

	public void Play () {
		// URL.ip = ipText.text;
		// URL.username = usernameText.text;
		// URL.password = passwordText.text;
		// string result = URL.Request("register.php", "bitsid="+idText.text);
		// if(result == "register"){
			transform.GetChild(0).gameObject.SetActive(false);
			move = true;
		// }else if(result.Contains("login")){
		// 	GameManager.score = Int32.Parse(result.Substring(5));
		// 	transform.GetChild(0).gameObject.SetActive(false);
		// 	move = true;
		// }else if(result == "error logging"){
		// 	Debug.Log("wrong password");
		// }else{
		// 	Debug.Log("user exists");
		// }
	}

	public void Display (int game) {
		transform.GetChild(2).gameObject.SetActive(true);
		instructionsText.text = allTexts[game];
		Cursor.visible = true;
		Time.timeScale = 0;
		FindObjectOfType<EventSystem>().SetSelectedGameObject(okButton);
	}

	public void OK () {
		transform.GetChild(2).gameObject.SetActive(false);
		Cursor.visible = false;
		Time.timeScale = 1;
		if(skeleton) skeleton.SetActive(false);
	}

	public void DisplayText (int n) {
		if(n == 0)
			gameElementsText.text = "";
		else
			gameElementsText.text = n.ToString();
	}

	void Update () {
		if(!move)
			return;
		cam.Translate(((startPoint.position - cam.position).normalized) * 0.25f, Space.World);
		if((cam.position - startPoint.position).sqrMagnitude <= 0.01f){
			cam.position = startPoint.position;
			Cursor.visible = false;
			Time.timeScale = 1;
			DisplayText(0);
			transform.GetChild(1).gameObject.SetActive(true);
			move = false;
		}
	}

	void FixedUpdate () {
		scoreText.text = "Score: " + GameManager.score.ToString();
		timeText.text = "Time: " + Mathf.Floor(timer/3600).ToString() + "h " + (Mathf.Floor(timer/60)%60).ToString() + "m " + Mathf.Floor(timer%60).ToString() + "s";
		timer -= Time.fixedDeltaTime;
	}

}
