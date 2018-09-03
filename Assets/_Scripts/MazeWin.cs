using UnityEngine;

public class MazeWin : MonoBehaviour {
    public Maze script;
    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.name == "Player")
        {
            script.setIsPlaying(false);
        }
    }
}
