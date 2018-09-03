using UnityEngine;

public class WinCondition : MonoBehaviour {

    public Transform player, winPosition, losePosition;
    public int gameNo;
    private bool _won;
    private int []tries = new int[5]{0,0,0,0,0};
    public bool won
    {
        get {return _won;}
        set {
            _won = value;
            if(value){
                player.position = new Vector3 (winPosition.position.x,player.position.y,winPosition.position.z);
                wonGame();
                Debug.Log("Won");
            }
            else
            {
                if(GameManager.solved[gameNo]){
                    _won = true;
                    return;
                }
                player.position = new Vector3 (losePosition.position.x,player.position.y, losePosition.position.z);
                resetGame();
            }

        }
    }
	void Start () {
	}

	void Update () {
        if(Input.GetButtonDown("Exit"))
        {
            won = false;
        }
        if(Input.GetButtonDown("Reset"))
        {
            won = false;
        }
	}
    void wonGame()
    {
        switch(gameNo)
        {
            case 2:

                GameManager.score += Mathf.Pow(0.98f, tries[gameNo])*400 - 8*PathPlate.cost;

                if(PathPlate.cost <= 0){
                    GameManager.score += 200;
                }else if (PathPlate.cost <=10){
                    GameManager.score += 100;
                }else if (PathPlate.cost <=20)
                    GameManager.score += 50;
                GameManager.solved[2] = true;
                break;
        }
    }
    void resetGame()
    {
        switch(gameNo){
            case 2:
                foreach(GameObject wall in GameObject.FindGameObjectsWithTag("Wall2")){
                    Destroy(wall);
                }
                foreach(PathPlate script in GameObject.FindObjectsOfType<PathPlate>()){
                    script.Start();
                }
                tries[gameNo] +=1;
                break;
        }
    }

}
