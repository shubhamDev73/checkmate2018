using UnityEngine;

public class GameManager : MonoBehaviour {

	public static int score;
	public static bool[] solved = {false, false, false, false, false};

	void Start () {
		score = 0;
		Time.timeScale = 0;
	}
	
}
