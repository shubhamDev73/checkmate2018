using UnityEngine;

public class GameManager : MonoBehaviour {

	public static int score;
	public static bool[] solved = {false, false}; // 0 is placeholder

	void Start () {
		score = 0;
		Time.timeScale = 0;
	}
	
}
