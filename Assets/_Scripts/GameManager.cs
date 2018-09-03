using UnityEngine;

public class GameManager : MonoBehaviour {

	public static float score;
	public static bool[] solved = {false, false, false, false, false, false}; // 0 is placeholder

	void Start () {
		score = 0;
		Time.timeScale = 0;
	}

}
