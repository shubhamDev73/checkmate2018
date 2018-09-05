using UnityEngine;

public class MazeWin : MonoBehaviour {
    public Maze script;
    public int tries = 0;
    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.name == "Player")
        {
            script.setIsPlaying(false);
            GameManager.tries[5] = tries;
            GameManager.solved[5] = true;
        }
    }
}
