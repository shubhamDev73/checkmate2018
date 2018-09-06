using UnityEngine;

public class GameManager : MonoBehaviour {

    public static Solved solved;
    public static int []tries;
    private static int _score;
	public static int score
	{
		get{
			return _score;
		}
		set{
			_score = value;
//			URL.Request("score.php", "score="+score);
		}
	}

    void Start () {
        tries = new int[6];     // 6 is the no. of minigames
        solved = new Solved(6, this);
		_score = 0;
		Time.timeScale = 0;
	}

    int Fib(int j)
    {
        int a = 0;
        int b = 1;
        int temp = b;
        for (int i = 0; i < j; ++i) {
            temp = b;
            b = b + a;
            a = temp;
        }
        return a;

    }
    void CalculateScore(int i)
    {
        switch(i){
            case 1:
                score += (int) (600*(Mathf.Pow(0.98f,tries[1])));
                break;
            case 2:

                break;
            case 3:
                score += (int) 400-(3*Fib(tries[3]));
                break;
            case 4:
                score += 100 - tries[4];
                break;
            case 5:
                score += 1000 - 4*(tries[5]);
                break;
            case 6:

                break;
        }
    }
    public class Solved{
        private bool []_solved;
        private GameManager parent;
        public Solved(int i, GameManager parent){
            _solved = new bool[i];
            this.parent = parent;
        }
        public bool this[int i]
        {
            get{return _solved[i];}
            set{_solved[i] = value;
                if(value)
                    parent.CalculateScore(i);
            }
        }
    }
}
