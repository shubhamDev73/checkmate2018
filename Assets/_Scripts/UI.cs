using System;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour {

	public Transform cam, startPoint;
	public Text score, time, gameText;
	public GameObject skeleton;

	private bool move = false;
	private String[] allTexts = new String[6];
	private float timer;

	void Start () {
			timer = 3600 * 2;
			allTexts[0] = "On exactly this day, 200 years ago, I was brutally murdered here, right where you stand. But my murder still remains a mystery today. I won't let you leave this place until you solve my murder. Oh, and you have only 2 hours.\nHA HA HA HA...";
			allTexts[1] = "Behind these doors roams the skeleton of my murderer. You'll have to catch him. Guess which room can contain him, and if you are wrong, he moves to its adjacent room. Corner him and catch him!!";
			allTexts[2] = "";
			allTexts[3] = "Some lamps outside this room can be controlled by these levers. Match all the levers to their correct lamps to shed some light on my murder.";
			allTexts[4] = "";
			allTexts[5] = "";
	}

	public void Play () {
		transform.GetChild(0).gameObject.SetActive(false);
		move = true;
	}

	public void Display (int game) {
		transform.GetChild(2).gameObject.SetActive(true);
		gameText.text = allTexts[game];
		Cursor.visible = true;
		Time.timeScale = 0;
	}

	public void OK() {
		transform.GetChild(2).gameObject.SetActive(false);
		Cursor.visible = false;
		Time.timeScale = 1;
		if(skeleton) skeleton.SetActive(false);
	}

	void Update () {
		if(!move)
			return;
		cam.Translate(((startPoint.position - cam.position).normalized) * 0.25f, Space.World);
		if((cam.position - startPoint.position).sqrMagnitude <= 0.01f){
			cam.position = startPoint.position;
			Cursor.visible = false;
			Time.timeScale = 1;
			transform.GetChild(1).gameObject.SetActive(true);
			move = false;
		}
	}

	void FixedUpdate () {
		score.text = "Score: " + GameManager.score.ToString();
		time.text = "Time: " + Mathf.Floor(timer/3600).ToString() + "h " + (Mathf.Floor(timer/60)%60).ToString() + "m " + Mathf.Floor(timer%60).ToString() + "s";
		timer -= Time.fixedDeltaTime;
	}

}
