using UnityEngine;

public class GameManager : MonoBehaviour {

	private static int _score;
	public static int score
	{
		get{
			return _score;
		}
		set{
			_score = value;
			URL.Request("score.php", "score="+score);
		}
	}
	public static bool[] solved = {false, false, false, false, false, false}; // 0 is placeholder

	void Start () {
		_score = 0;
		Time.timeScale = 0;
	}

}
