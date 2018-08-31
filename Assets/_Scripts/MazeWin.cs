using UnityEngine;

public class MazeWin : MonoBehaviour {
    public Maze script;
    void OnTriggerEnter(Collider col)
    {
        Debug.Log(col.gameObject.name);
        if(col.gameObject.name == "Player")
        {
            script.setIsPlaying(false);
        }
    }
}
