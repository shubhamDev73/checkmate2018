using UnityEngine;

public class GameManager : MonoBehaviour {

    public UI ui;
    public static Solved solved;
    public static Tries tries; //NOTE TRIES[2] SIGNIFIES COST IN THAT GAME
    public static int temp2, temp6; //temp values for game 2 and 6
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

    void Start () {
        tries = new Tries(6, this);
        // 6 is the no. of minigames
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
                score -= temp2;
                score += 400 - 6*tries[2];
                temp2 = 400 - 6*tries[2];
                if(tries[2] <= 0){        //!!!NOTE TRIES[2] SIGNIFIES COST IN THAT GAME!!!
                    score+= 200;
                    temp2 += 200;
                }
                else if(tries[2] <=10)
                {
                    score +=100;
                    temp2 += 100;
                }
                else if(tries[2] <= 50)
                {
                    score += 50;
                    temp2 += 50;
                }

                break;
            case 3:
                score += Mathf.Clamp((int) 400-(3*Fib(tries[3])), 3, 400);
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
    public class Tries{
        private int [] _tries;
        private GameManager parent;
        public Tries(int i, GameManager parent){
            _tries = new int[i];
            this.parent = parent;
        }
        public int this[int i]
        {
            get{return _tries[i];}
            set{_tries[i] = value;
                // UI SHOW TRIES {3, 5}
                switch(i){
                    case 3:
                        parent.ui.ShowTries(tries[i]);
                        break;
                    case 5:
                        parent.ui.ShowTries(tries[i]);
                        break;
                }
            }
        }
    }
}
