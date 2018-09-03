using UnityEngine;

public class WinningByCollision : MonoBehaviour {
    public WinCondition script;
    void OnTriggerStay(Collider col)
    {
        Debug.Log("Woah");
        if(col.CompareTag("Player")){
            script.won = true;
        }
    }
}
